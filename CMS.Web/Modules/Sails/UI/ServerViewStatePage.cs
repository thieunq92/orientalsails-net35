using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Portal.Modules.OrientalSails.Web.UI
{
    public class viewStateSvrMgr
    {
        // This uses an array and mod to cycle repeatedly through an array (so limited size)

        //WARNING:  When the user uses the "back" button on the browser, IE will not rerequest the page, so 
        // if the user posts again they need the viewstate to still be present on the server.  Need to set the VIEW_STATE_NUM_PAGES
        // to a tradeoff of number of back pages allowed and 
        // the amount of memory consumed by the viewstate kept per page.  

        //Name of storage location for veiwstate information
        private const string SESSION_VIEW_STATE_MGR = "VIEW_STATE_MGR";
        private const short VIEW_STATE_NUM_PAGES = 10; //Number of pages to keep viewstate for

        private readonly string[] ViewStates = new string[VIEW_STATE_NUM_PAGES]; //Store for viewstates
        private long lPageCount; //Number of pages seen by this customer 

        //Determine if server side is enabled or not from web.config file
        public bool ServerSideEnabled
        {
            get
            {
                //Not a problem if someone changes the value in web.config, because new AppDomain will
                // be started and all in process session is lost anyway
                return true;
            }
        }

        public long SaveViewState(string szViewState)
        {
            //Increment the total page seen counter
            lPageCount++;

            //Now use the modulas operator (%) to find remainder of that and size of viewstate storage, this creates a
            // circular array where it continually cycles through the array index range (effectively keeps
            // the last requests to match size of storage)
            short siIndex = (short) (lPageCount%VIEW_STATE_NUM_PAGES);

            //Now save the viewstate for this page to the current position.  
            ViewStates[siIndex] = szViewState;

            return lPageCount;
        }


        public string GetViewState(long lRequestNumber)
        {
            //Could cycle though the array and make sure that the given request number is actually
            // present (in case the array is not big enough).  Much faster to just take the
            // given request number and recalculate where it should be stored
            short siIndex = (short) (lRequestNumber%VIEW_STATE_NUM_PAGES);

            return ViewStates[siIndex];
        }


        public static viewStateSvrMgr GetViewStateSvrMgr()
        {
            viewStateSvrMgr oViewStateMgr;

            //Check if already created the order object in session
            if (null == HttpContext.Current.Session[SESSION_VIEW_STATE_MGR])
            {
                //Not already in session, create a new one and put in session
                oViewStateMgr = new viewStateSvrMgr();
                HttpContext.Current.Session[SESSION_VIEW_STATE_MGR] = oViewStateMgr;
            }
            else
            {
                //Return the session order
                oViewStateMgr = (viewStateSvrMgr) HttpContext.Current.Session[SESSION_VIEW_STATE_MGR];
            }

            return oViewStateMgr;
        }
    }

    public class ServerViewStatePage : SailsAdminBasePage
    {
        //Setup the name of the hidden field on the client for storing the viewstate key
        public const string VIEW_STATE_FIELD_NAME = "__VIEWSTATE_SERVER";

        //Setup a formatter for the viewstate information
        private LosFormatter _formatter;

        //overriding method of Page class
        protected override object LoadPageStateFromPersistenceMedium()
        {
            //If server side enabled use it, otherwise use original base class implementation
            if (viewStateSvrMgr.GetViewStateSvrMgr().ServerSideEnabled)
            {
                return LoadViewState();
            }
            else
            {
                return base.LoadPageStateFromPersistenceMedium();
            }
        }

        //overriding method of Page class
        protected override void SavePageStateToPersistenceMedium(object viewState)
        {
            if (viewStateSvrMgr.GetViewStateSvrMgr().ServerSideEnabled)
            {
                SaveViewState(viewState);
            }
            else
            {
                base.SavePageStateToPersistenceMedium(viewState);
            }
        }

        //implementation of method
        private object LoadViewState()
        {
            if (_formatter == null)
            {
                _formatter = new LosFormatter();
            }

            //Check if the client has form field that stores request key
            if (null == Request.Form[VIEW_STATE_FIELD_NAME])
            {
                //Did not see form field for viewstate, return null to try to continue (could log event...)
                return null;
            }

            //Make sure it can be converted to request number (in case of corruption)
            long lRequestNumber = 0;
            try
            {
                lRequestNumber = Convert.ToInt64(Request.Form[VIEW_STATE_FIELD_NAME]);
            }
            catch
            {
                //Could not covert to request number, return null (could log event...)
                return null;
            }

            //Get the viewstate for this page
            string _viewState = viewStateSvrMgr.GetViewStateSvrMgr().GetViewState(lRequestNumber);

            //If find the viewstate on the server, convert it so ASP.Net can use it
            if (_viewState == null)
                return null;
            else
                return _formatter.Deserialize(_viewState);
        }


        //implementation of method
        private void SaveViewState(object viewState)
        {
            if (_formatter == null)
            {
                _formatter = new LosFormatter();
            }

            //Save the viewstate information
            StringBuilder _viewState = new StringBuilder();
            StringWriter _writer = new StringWriter(_viewState);
            _formatter.Serialize(_writer, viewState);
            long lRequestNumber = viewStateSvrMgr.GetViewStateSvrMgr().SaveViewState(_viewState.ToString());

            //Need to register the viewstate hidden field (must be present or postback things don't 
            // work, we use in our case to store the request number)
            ClientScript.RegisterHiddenField(VIEW_STATE_FIELD_NAME, lRequestNumber.ToString());
        }
    }
}