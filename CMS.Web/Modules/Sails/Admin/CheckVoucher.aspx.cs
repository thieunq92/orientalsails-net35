using System;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using NHibernate.Criterion;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Utils;
using Portal.Modules.OrientalSails.Web.Controls;
using Portal.Modules.OrientalSails.Web.UI;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class CheckVoucher : SailsAdminBasePage
    {
        private string[] codes;
        /// <summary>
        /// sự kiện khi tải trang gắn mã voucher vào voucher repeater để tìm kiếm gói voucher theo voucher
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string clientId = Request.QueryString["clientid"];

            string script = string.Format(
    @"function Done(name, id)
{{
    idcontrol = window.opener.document.getElementById('{0}');
	idcontrol.value = id;
    namecontrol = window.opener.document.getElementById('{1}');
    namecontrol.value = name;
    window.close();
}}", clientId, clientId + AgencySelector.PNAMEID);

            Page.ClientScript.RegisterClientScriptBlock(typeof(AgencySelectorPage), "done", script, true);

            if (!String.IsNullOrEmpty(Request.QueryString["code"]))
            {
                var trimmedCode = Request.QueryString["code"].Trim();
                if (trimmedCode.EndsWith(";"))
                    codes = trimmedCode.Remove(trimmedCode.Length - 1).Split(new char[] { ';' });
                else
                    codes = trimmedCode.Split(new char[] { ';' });

                txtVoucherCode.Text = trimmedCode;

                rptVoucher.DataSource = codes;
                rptVoucher.DataBind();
            }
        }

        /// <summary>
        /// kiểm tra voucher code đã được dùng hay chưa
        /// </summary>
        /// <param name="code">voucher code</param>
        /// <param name="count">dùng để dưa kết quả của biến count ra ngoài</param>
        /// <param name="batch">gói voucher</param>
        public void CheckCode(String code, out int count, out VoucherBatch batch)
        {
            /*Điều kiện tìm booking : tìm theo bookingid và booking chưa bị deleted và đã có voucher code và status không phải là cancelled */
            int batchid;

            AbstractCriterion crit = Expression.Eq("Deleted", false);
            crit = Expression.And(crit, Expression.Or(Expression.Not(Expression.Eq("VoucherCode", null)), Expression.Not(Expression.Eq("VoucherCode", ""))));

            if (!string.IsNullOrEmpty(Request.QueryString["bookingid"]))
            {
                crit = Expression.And(crit,
                                      Expression.Not(Expression.Eq("Id",
                                                                   Convert.ToInt32(Request.QueryString["bookingid"]))));
                crit = Expression.And(crit,
                                     Expression.Not(Expression.Eq("Status",
                                                                  StatusType.Cancelled)));
            }

            count = 0;
            var bookingList = Module.GetObject<Booking>(crit, 0, 0);

            /* kiểm tra voucher nhập vào đã dùng trong các booking vừa tìm hay chưa, nếu trùng tăng biến đếm count
                và đưa kết quả ra ngoài để xử lý
             */
            foreach (Booking booking in bookingList)
            {
                string[] codeArray = booking.VoucherCode.Split(new char[] { ';' });
                for (int i = 0; i < codeArray.Length; i++)
                {
                    if (code == codeArray[i])
                    {
                        count++;
                    }
                }
            }
            try
            {
                /* giải mã voucher xem nó thuộc gói voucher nào*/
                VoucherCodeEncryption.Decrypt(Convert.ToUInt32(code), out batchid);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            /*tìm gói voucher và đưa kết quả ra ngoài để xử lý*/
            batch = Module.GetObject<VoucherBatch>(batchid);
        }

        /// <summary>
        /// sự kiên nút check click vào sẽ bắt đầu kiểm tra voucher
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCheck_ItemDataBound(object sender, EventArgs e)
        {
            PageRedirect(string.Format("CheckVoucher.aspx?NodeId={0}&SectionId={1}&code={2}&bookingid={3}", Node.Id,
                                       Section.Id, txtVoucherCode.Text, Request.QueryString["bookingid"]));
        }

        /// <summary>
        /// sự kiện repeater voucher gắn thông tin voucher tìm được vào bảng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptVoucher_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            int count;
            VoucherBatch batch;
            DateTime date;
            if (Request.QueryString["bookingid"] != null)
            {
                var booking = Module.BookingGetById(Convert.ToInt32(Request.QueryString["bookingid"]));
                date = booking.StartDate;
            }
            else
            {
                date = DateTime.Today;
            }


            CheckCode((String)e.Item.DataItem, out count, out batch);

            var litVoucherCode = e.Item.FindControl("litVoucherCode") as Literal;
            var litStatus = e.Item.FindControl("litStatus") as Literal;
            var litProgramName = e.Item.FindControl("litProgramName") as Literal;
            var litAgency = e.Item.FindControl("litAgency") as Literal;
            var litQuantity = e.Item.FindControl("litQuantity") as Literal;
            var litApplyFor = e.Item.FindControl("litApplyFor") as Literal;
            var litCruise = e.Item.FindControl("litCruise") as Literal;
            var litTrip = e.Item.FindControl("litTrip") as Literal;
            var litValue = e.Item.FindControl("litValue") as Literal;
            var litValidUntil = e.Item.FindControl("litValidUntil") as Literal;
            var litIssueDate = e.Item.FindControl("litIssueDate") as Literal;
            var litNote = e.Item.FindControl("litNote") as Literal;
            var hplContract = e.Item.FindControl("hplContract") as HyperLink;
            var plhValid = e.Item.FindControl("plhValid") as PlaceHolder;
            var plhInvalid = e.Item.FindControl("plhInvalid") as PlaceHolder;

            litVoucherCode.Text = (String)e.Item.DataItem;

            if (batch == null)
            {
                plhValid.Visible = false;
                plhInvalid.Visible = true;
                return;
            }

            if (litStatus != null)
            {
                if (count > 0)
                {
                    litStatus.Text = "Used!";
                }
                else if (batch.ValidUntil >= date)
                {
                    litStatus.Text = "Valid";
                }
                else
                {
                    litStatus.Text = "Outdated!";
                }
            }

            if (litProgramName != null) litProgramName.Text = batch.Name;
            if (batch.Agency != null)
                if (litAgency != null) litAgency.Text = batch.Agency.Name;
            litQuantity.Text = batch.Quantity.ToString();
            litApplyFor.Text = batch.NumberOfPerson.ToString();
            litCruise.Text = batch.Cruise.Name;
            litTrip.Text = batch.Trip.Name;
            litValue.Text = batch.Value.ToString();
            litValidUntil.Text = batch.ValidUntil.ToString("dd/MM/yyyy");
            if (batch.IssueDate.HasValue)
            {
                litIssueDate.Text = batch.IssueDate.Value.ToString("dd/MM/yyyy");
            }
            if (!string.IsNullOrEmpty(batch.ContractFile))
            {
                hplContract.NavigateUrl = batch.ContractFile;
            }
            else
            {
                hplContract.Visible = false;
            }
            litNote.Text = batch.Note;
        }
    }
}
