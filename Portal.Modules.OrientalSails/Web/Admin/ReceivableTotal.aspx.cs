using System;
using System.Web.UI.WebControls;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class ReceivableTotal : SailsAdminBase
    {
        private int index;
        private USDRate _currentRate;
        private double _total;
        private double _payable;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                index = 0;
                _currentRate = Module.ExchangeGetByDate(DateTime.Today);
                rptAgencies.DataSource = Module.AgencyGetReceivable();
                rptAgencies.DataBind();
                index = 0;
                rptPayables.DataSource = Module.AgencyGetPayable();
                rptPayables.DataBind();
            }
        }

        protected void rptAgencies_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is vAgency)
            {
                vAgency agency = (vAgency) e.Item.DataItem;
                index++;
                Literal litNo = e.Item.FindControl("litNo") as Literal;
                if (litNo!=null)
                {
                    litNo.Text = index.ToString();
                }

                HyperLink hplName = e.Item.FindControl("hplName") as HyperLink;
                if (hplName!=null)
                {
                    hplName.Text = agency.Name;
                    hplName.NavigateUrl = string.Format("PaymentReport.aspx?NodeId={0}&SectionId={1}&agencyid={2}&mode=all",
                                                        Node.Id, Section.Id, agency.Id);
                }

                Literal litTotal = e.Item.FindControl("litTotal") as Literal;
                if (litTotal!=null)
                {
                    double receivable = (agency.Total - agency.Paid)*_currentRate.Rate - agency.PaidBase;
                    litTotal.Text = receivable.ToString("#,0.#");
                    _total += receivable;
                }
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                Literal litTotal = e.Item.FindControl("litTotal") as Literal;
                if (litTotal != null)
                {
                    litTotal.Text = _total.ToString("#,0.#");
                }
            }
        }

        protected void rptPayables_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is vAgency)
            {
                vAgency agency = (vAgency)e.Item.DataItem;
                index++;
                Literal litNo = e.Item.FindControl("litNo") as Literal;
                if (litNo != null)
                {
                    litNo.Text = index.ToString();
                }

                HyperLink hplName = e.Item.FindControl("hplName") as HyperLink;
                if (hplName != null)
                {
                    hplName.Text = agency.Name;
                    hplName.NavigateUrl = string.Format("PayableList.aspx?NodeId={0}&SectionId={1}&supplierid={2}&mode=all",
                                                        Node.Id, Section.Id, agency.Id);
                }

                Literal litTotal = e.Item.FindControl("litTotal") as Literal;
                if (litTotal != null)
                {
                    double payable = agency.Payable;
                    litTotal.Text = payable.ToString("#,0.#");
                    _payable += payable;
                }
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                Literal litTotal = e.Item.FindControl("litTotal") as Literal;
                if (litTotal != null)
                {
                    litTotal.Text = _payable.ToString("#,0.#");
                }
            }
        }
    }
}
