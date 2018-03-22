using System;
using System.Web.UI.WebControls;
using CMS.Web.Util;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{

    public partial class CruisesEdit : SailsAdminBase
    {
        private Cruise _cruise;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["cruiseid"]!=null)
            {
                int cruiseid = Convert.ToInt32(Request.QueryString["cruiseid"]);
                _cruise = Module.CruiseGetById(cruiseid);
                if (!IsPostBack)
                {
                    BindCruise();                    
                }
            }
            else
            {
                _cruise = new Cruise();
            }

            if (!IsPostBack)
            {
                BindTrips();
            }
        }

        private void BindCruise()
        {
            txtCode.Text = _cruise.Code;
            txtCruiseCode.Text = _cruise.CruiseCode;
            textBoxName.Text = _cruise.Name;
            fckDescription.Value = _cruise.Description;
            if (!string.IsNullOrEmpty(_cruise.RoomPlan))
            {
                hplRoomPlan.Visible = true;
                litRoomPlan.Visible = false;
                hplRoomPlan.NavigateUrl = _cruise.RoomPlan;
            }
            else
            {
                hplRoomPlan.Visible = false;
                litRoomPlan.Visible = true;
            }
        }

        private void BindTrips()
        {
            rptTrips.DataSource = Module.TripGetAll(true);
            rptTrips.DataBind();
        }

        private void GetCruise()
        {
            _cruise.Code = txtCode.Text;
            _cruise.CruiseCode = txtCruiseCode.Text;
            _cruise.Name = textBoxName.Text;
            _cruise.Description = fckDescription.Value;
            if (fileRoomPlan.HasFile)
            {
                _cruise.RoomPlan = FileHelper.Upload(fileRoomPlan);
            }
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            GetCruise();
            Module.SaveOrUpdate(_cruise, UserIdentity);

            foreach (RepeaterItem item in rptTrips.Items)
            {
                CheckBox chkTrip = (CheckBox) item.FindControl("chkTrip");
                HiddenField hiddenId = (HiddenField) item.FindControl("hiddenId");
                SailsTrip trip = Module.TripGetById(Convert.ToInt32(hiddenId.Value));
                CruiseTrip cr = Module.CruiseTripGet(_cruise, trip);
                if (chkTrip.Checked && cr.Id < 0)
                {
                    Module.SaveOrUpdate(cr);
                }
                if (!chkTrip.Checked && cr.Id > 0)
                {
                    Module.Delete(cr);
                }
            }
            PageRedirect(string.Format("CruisesList.aspx?NodeId={0}&SectionId={1}",Node.Id, Section.Id));
        }

        protected void rptTrips_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is SailsTrip)
            {
                SailsTrip trip = (SailsTrip) e.Item.DataItem;
                CheckBox chkTrip = e.Item.FindControl("chkTrip") as CheckBox;
                if (chkTrip!=null)
                {
                    chkTrip.Text = trip.Name;
                    if (_cruise.Trips.Contains(trip))
                    {
                        chkTrip.Checked = true;
                    }
                }
            }
        }
    }
}
