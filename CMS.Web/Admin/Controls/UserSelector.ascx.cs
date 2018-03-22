using System;
using System.ComponentModel;
using System.Web.UI;

namespace CMS.Web.Admin.Controls
{
    public partial class UserSelector : UserControl
    {
        protected string _clear;
        protected string _processModalData;
        protected string _showModal;

        private string _unassigned;

        [DefaultValue("Unassigned")]
        public string UnassignedText
        {
            get
            {
                if (_unassigned != null)
                {
                    return _unassigned;
                }
                return "Unassigned";
            }
            set { _unassigned = value; }
        }

        public int SelectedUserId
        {
            get
            {
                if (string.IsNullOrEmpty(hiddenId.Value))
                {
                    return 0;
                }
                return Convert.ToInt32(hiddenId.Value);
            }
            set
            {
                hiddenId.Value = value.ToString();
            }
        }        

        /// <summary>
        /// Please set after selected user id
        /// </summary>
        public string SelectedUser
        {
            set { labelUserName.Text = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            _clear = this.ClientID + "_clear";
            _processModalData = this.ClientID + "_process";
            _showModal = this.ClientID + "_showModal";

            #region -- Popup center --

            const string centerPopup =
                @"function PopupCenter(pageURL, title,h,w) {
var left = (screen.width/2)-(w/2);
var top = (screen.height/2)-(h/2);
var targetWin = window.open (pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width='+w+', height='+h+', top='+top+', left='+left);
targetWin.focus();
return targetWin;
}";
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "centerPopup", centerPopup, true);
            //Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "centerPopup", centerPopup, true);

            #endregion

            #region -- Get Query String --

            const string getQuery =
                @"function getQueryString( name )
{
  name = name.replace(/[\[]/,""\\\["").replace(/[\]]/,""\\\]"");
  var regexS = ""[\\?&]""+name+""=([^&#]*)"";
  var regex = new RegExp( regexS );
  var results = regex.exec( window.location.href );
  if( results == null )
    return """";
  else
    return results[1];
}";
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "getQuery", getQuery, true);

            #endregion

            #region -- Show Modal Popup --

            string showModal = @"function " + _showModal +
                               @"()
{
PopupCenter('/Modules/User/UserSelector.aspx?command=" + _processModalData +
                               @"',640,480);
}";
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), this.ClientID + "_showModal", showModal,
                                                    true);

            #endregion

            #region -- Clear --

            string clear =
                @"function " + _clear + @"()
{
self.document.getElementById('" + labelUserName.ClientID +
                @"').innerHTML  = '" + UnassignedText + @"';
self.document.getElementById('" + hiddenId.ClientID +
                @"').value  = '';
self.document.getElementById('" + btnRemove.ClientID + @"').disabled = true;
}";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), this.ClientID + "_clear", clear, true);

            #endregion

            #region -- ProcessModalData --

            string processModal =
                @"
function " + _processModalData +
                @"( userId, username )
{
self.focus();
self.document.getElementById('" + labelUserName.ClientID +
                @"').innerHTML  = username;
self.document.getElementById('" + hiddenId.ClientID +
                @"').value  = userId;
self.document.getElementById('" + btnRemove.ClientID +
                @"').disabled = false;
}";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), this.ClientID + "_process", processModal,
                                                        true);

            #endregion

            btnSelect.Attributes.Add("onClick", _showModal + "()");
            btnRemove.Attributes.Add("onClick", _clear + "()");
            if (!IsPostBack)
            {
                if (SelectedUserId > 0)
                {
                    hiddenId.Value = SelectedUserId.ToString();
                }
                else
                {
                    btnRemove.Attributes.Add("disabled", "true");
                }
            }
        }
    }
}