using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.Web.Util;
using NHibernate.Criterion;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Utils;
using Portal.Modules.OrientalSails.Web.UI;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class VoucherList : SailsAdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // get data              
                var list = Module.GetObject<VoucherBatch>(null, pagerVoucher.PageSize, pagerVoucher.CurrentPageIndex, new Order("ValidUntil",false));
                pagerVoucher.AllowCustomPaging = true;
                pagerVoucher.VirtualItemCount = Module.GetObject<VoucherBatch>(null, 0, 0).Count;
                rptVoucherList.DataSource = list;          
                rptVoucherList.DataBind();
            }
        }

        protected void rptVoucherList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is VoucherBatch)
            {
                var batch = (VoucherBatch)e.Item.DataItem;

                if (batch.Agency != null)
                    ValueBinder.BindLiteral(e.Item, "litAgency", batch.Agency.Name);
                ValueBinder.BindLiteral(e.Item, "litTotal", batch.Quantity);
                var criterion = Expression.And(Expression.Eq("Deleted", false),
                    Expression.And(Expression.Eq("Batch.Id", batch.Id),
                        Expression.Not(Expression.Eq("Status", StatusType.Cancelled)))
                    );
                var bookingUsedBatchVoucher = Module.GetObject<Booking>(criterion, 0, 0);
                int countVoucherUsed = 0;
                if (bookingUsedBatchVoucher != null)
                {      
                    foreach (Booking booking in bookingUsedBatchVoucher)
                    {
                        countVoucherUsed = countVoucherUsed + booking.VoucherCode.Split(new char[]{';'}).Length;
                    }
                }
                ValueBinder.BindLiteral(e.Item, "litRemain", batch.Quantity - countVoucherUsed);

                if (countVoucherUsed > 0)
                {
                    var hplBookings = (HyperLink)e.Item.FindControl("hplBookings");
                    hplBookings.Visible = true;
                    hplBookings.NavigateUrl = string.Format("BookingList.aspx?NodeId={0}&SectionId={1}&batchid={2}",
                                                            Node.Id, Section.Id, batch.Id);
                }

                ValueBinder.BindLiteral(e.Item, "litValidUntil", batch.ValidUntil.ToString("dd/MM/yyyy"));
                if (!string.IsNullOrEmpty(batch.ContractFile))
                {
                    var hplContract = (HyperLink)e.Item.FindControl("hplContract");
                    hplContract.Text = @"Download file";
                    hplContract.NavigateUrl = batch.ContractFile;
                }

                var hplName = (HyperLink)e.Item.FindControl("hplName");
                hplName.Text = batch.Name;
                hplName.NavigateUrl = string.Format("VoucherEdit.aspx?NodeId={0}&SectionId={1}&batchid={2}", Node.Id,
                                                    Section.Id, batch.Id);

                var hplEdit = (HyperLink)e.Item.FindControl("hplEdit");
                hplEdit.NavigateUrl = string.Format("VoucherEdit.aspx?NodeId={0}&SectionId={1}&batchid={2}", Node.Id,
                                                    Section.Id, batch.Id);
            }
        }

        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            var button = (IButtonControl)sender;

            var id = Convert.ToInt32(button.CommandArgument);

            var batch = Module.GetObject<VoucherBatch>(id);
            Module.SaveOrUpdate(batch, UserIdentity);

            PageRedirect(Request.RawUrl);
        }
    }
}