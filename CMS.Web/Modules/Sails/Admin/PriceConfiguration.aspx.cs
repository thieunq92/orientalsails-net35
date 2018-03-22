using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.Core.Domain;
using CMS.Web.Util;
using NHibernate.Criterion;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class PriceConfiguration : SailsAdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["roleid"]))
                {
                    var role = Module.RoleGetById(Convert.ToInt32(Request.QueryString["roleid"]));
                    rptPriceTables.DataSource = Module.GetPriceTable(role);
                    rptPriceTables.DataBind();
                    labelTitle.Text = string.Format("Price config for {0}", role.Name);
                }
                else
                {
                    var agency = Module.GetObject<Agency>(Convert.ToInt32(Request.QueryString["agentid"]));
                    ICriterion agencyCriterion = Expression.Eq("Agency", agency);
                    rptPriceTables.DataSource = Module.GetObject<SailsPriceTable>(agencyCriterion, 0, 0);
                    rptPriceTables.DataBind();
                    labelTitle.Text = string.Format("Price config for {0}", agency.Name);
                }           
            }
        }

        protected void rptPriceTables_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var table = e.Item.DataItem as SailsPriceTable;
            if (table!=null)
            {
                if (table.StartDate != null)
                    ValueBinder.BindLiteral(e.Item, "litValidFrom", table.StartDate.Value.ToString("dd/MM/yyyy"));
                if (table.EndDate != null)
                    ValueBinder.BindLiteral(e.Item, "litValidTo", table.EndDate.Value.ToString("dd/MM/yyyy"));
                var hplEdit = e.Item.FindControl("hplEdit") as HyperLink;
                if (hplEdit!=null)
                {
                    hplEdit.NavigateUrl = string.Format("PriceTableConfig.aspx?SectionId={0}&NodeId={1}&tableid={2}",
                                                        Section.Id, Node.Id, table.Id);
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            var sailsPriceTable = new SailsPriceTable();
            if (!String.IsNullOrEmpty(Request.QueryString["roleid"]))
            {
                sailsPriceTable.Role = Module.GetObject<Role>(Convert.ToInt32(Request.QueryString["roleid"]));
            }
            else
            {
                sailsPriceTable.Agency = Module.GetObject<Agency>(Convert.ToInt32(Request.QueryString["agentid"]));
            }
            Module.SaveOrUpdate(sailsPriceTable);
            PageRedirect(string.Format("PriceTableConfig.aspx?SectionId={0}&NodeId={1}&tableid={2}", Section.Id, Node.Id, sailsPriceTable.Id));
            
        }
    }
}