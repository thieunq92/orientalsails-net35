using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI.WebControls;
using Aspose.Words;
using Aspose.Words.Rendering;
using CMS.Core.Domain;
using CMS.Web.Util;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Utils;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class VoucherEdit : SailsAdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = @"Voucher program edit";
            if (!IsPostBack)
            {
                ddlCruises.DataSource = Module.CruiseGetAll();
                ddlCruises.DataValueField = "Id";
                ddlCruises.DataTextField = "Name";
                ddlCruises.DataBind();

                ddlTrips.DataSource = Module.TripGetAll(true);
                ddlTrips.DataValueField = "Id";
                ddlTrips.DataTextField = "Name";
                ddlTrips.DataBind();

                string[] files = Directory.GetFiles(Server.MapPath("/UserFiles/VoucherTemplates/"),
                                                    string.Format("*.doc"), SearchOption.AllDirectories);
                ddlTemplates.Items.Clear();
                foreach (string file in files)
                {
                    string filename = Path.GetFileName(file);
                    ddlTemplates.Items.Add(new ListItem(filename, filename));
                }

                if (Request.QueryString["batchid"] != null)
                {
                    var batch = Module.GetObject<VoucherBatch>(Convert.ToInt32(Request.QueryString["batchid"]));
                    agencySelector.SelectedAgency = batch.Agency;
                    ddlCruises.SelectedValue = batch.Cruise.Id.ToString();
                    ddlTrips.SelectedValue = batch.Trip.Id.ToString();

                    txtVoucher.Text = batch.Quantity.ToString();
                    ddlPersons.SelectedValue = batch.NumberOfPerson.ToString();
                    txtValue.Text = batch.Value.ToString();

                    ddlTemplates.SelectedValue = batch.Template;
                    if (batch.IssueDate.HasValue)
                    {
                        txtIssueDate.Text = batch.IssueDate.Value.ToString("dd/MM/yyyy");
                    }

                    txtValidUntil.Text = batch.ValidUntil.ToString("dd/MM/yyyy");

                    if (!string.IsNullOrEmpty(batch.ContractFile))
                    {
                        hplContract.Visible = true;
                        hplContract.NavigateUrl = batch.ContractFile;
                        hplContract.Text = @"View old contract";
                    }

                    txtName.Text = batch.Name;

                    var values = new int[batch.Quantity];
                    for (int ii = 1; ii <= batch.Quantity; ii++)
                    {
                        values[ii - 1] = batch.Id*1000 + ii;
                    }

                    rptVouchers.DataSource = values;
                    rptVouchers.DataBind();

                    if (batch.Issued)
                    {
                        buttonSave.Visible = false;
                    }

                    txtNote.Text = batch.Note;
                }
                else
                {
                    buttonIssue.Visible = false;
                }
            }
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            VoucherBatch batch;
            if (Request.QueryString["batchid"] != null)
            {
                batch = Module.GetObject<VoucherBatch>(Convert.ToInt32(Request.QueryString["batchid"]));

                if (batch.Issued && UserIdentity.HasPermission(AccessLevel.Administrator))
                {
                    ShowError("You can not change voucher program after issue date unless you're administrators!");
                    return;
                }
            }
            else
            {
                batch = new VoucherBatch();
            }

            batch.Name = txtName.Text;
            batch.Agency = agencySelector.SelectedAgency;
            batch.Cruise = Module.CruiseGetById(Convert.ToInt32(ddlCruises.SelectedValue));
            batch.Trip = Module.TripGetById(Convert.ToInt32(ddlTrips.SelectedValue));
            DateTime date;
            if (DateTime.TryParseExact(txtIssueDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                                       DateTimeStyles.None, out date))
            {
                batch.IssueDate = date;
            }
            else
            {
                batch.IssueDate = null;
            }
            batch.ValidUntil = DateTime.ParseExact(txtValidUntil.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            batch.NumberOfPerson = Convert.ToInt32(ddlPersons.SelectedValue);

            batch.Quantity = Convert.ToInt32(txtVoucher.Text);
            batch.NumberOfPerson = Convert.ToInt32(ddlPersons.SelectedValue);
            batch.Value = Convert.ToInt32(txtValue.Text);
            batch.Template = ddlTemplates.SelectedValue;
            batch.Note = txtNote.Text;

            if (fileuploadContract.HasFile)
                batch.ContractFile = FileHelper.Upload(fileuploadContract);

            Module.SaveOrUpdate(batch, UserIdentity);

            PageRedirect(string.Format("VoucherEdit.aspx?NodeId={0}&SectionId={1}&batchid={2}", Node.Id, Section.Id,
                                       batch.Id));
        }

        protected void rptVouchers_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var value = (int) e.Item.DataItem;
            ValueBinder.BindLiteral(e.Item, "litCode", VoucherCodeEncryption.Encrypt(Convert.ToUInt32(value)));
        }

        protected void buttonIssue_Click(object sender, EventArgs e)
        {
            VoucherBatch batch;
            if (Request.QueryString["batchid"] != null)
            {
                batch = Module.GetObject<VoucherBatch>(Convert.ToInt32(Request.QueryString["batchid"]));
            }
            else
            {
                return;
            }

            var doc = new Document(Server.MapPath("/Userfiles/VoucherTemplates/" + ddlTemplates.SelectedValue));
            //var awPrintDoc = new AsposeWordsPrintDocument(doc);

            #region -- Generate docs --

            var table = new DataTable("Voucher");
            table.Columns.Add("VoucherName");
            table.Columns.Add("Agency");
            table.Columns.Add("ApplyFor");
            table.Columns.Add("Cruise");
            table.Columns.Add("Trip");
            table.Columns.Add("Value");
            table.Columns.Add("ValidUntil");
            table.Columns.Add("IssueDate");
            table.Columns.Add("Code");

            //double total = 0;
            for (int ii = 1; ii <= batch.Quantity; ii++)
            {
                DataRow row = table.NewRow();
                row["VoucherName"] = batch.Name;
                if (batch.Agency != null)
                {
                    row["Agency"] = batch.Agency.Name;
                }

                if (batch.NumberOfPerson == 1)
                {
                    row["ApplyFor"] = "Single person";
                }
                else
                {
                    row["ApplyFor"] = "02 PERSONS, 01 sharing cabin";
                }
                row["Cruise"] = batch.Cruise.Name;
                row["Trip"] = batch.Trip.Name;
                row["Value"] = batch.Value;
                row["ValidUntil"] = batch.ValidUntil.ToString("dd/MM/yyyy");
                if (batch.IssueDate.HasValue)
                {
                    row["IssueDate"] = batch.IssueDate.Value.ToString("dd/MM/yyyy");
                }
                row["Code"] = VoucherCodeEncryption.Encrypt(Convert.ToUInt32(batch.Id*1000 + ii));
                table.Rows.Add(row);
            }

            doc.MailMerge.Execute(table);

            Response.Clear();
            Response.Buffer = true;
            //Response.ContentType = "application/pdf";
            Response.ContentType = "application/msword";
            Response.AppendHeader("content-disposition",
                                  //"attachment; filename=" + string.Format("{0}.pdf", "voucher" + batch.Id));
                                  "attachment; filename=" + string.Format("{0}.doc", "voucher" + batch.Id));

            var m = new MemoryStream();

            doc.Save(m, SaveFormat.Doc);

            Response.OutputStream.Write(m.GetBuffer(), 0, m.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();

            m.Close();
            Response.End();

            #endregion

            if (!batch.IssueDate.HasValue)
            {
                batch.IssueDate = DateTime.Today;
                batch.Issued = true;
                Module.SaveOrUpdate(batch, UserIdentity);
            }
        }

        protected void buttonIssuePDF_Click(object sender, EventArgs e)
        {
            VoucherBatch batch;
            if (Request.QueryString["batchid"] != null)
            {
                batch = Module.GetObject<VoucherBatch>(Convert.ToInt32(Request.QueryString["batchid"]));
            }
            else
            {
                return;
            }

            var doc = new Document(Server.MapPath("/Userfiles/VoucherTemplates/" + ddlTemplates.SelectedValue));
            //var awPrintDoc = new AsposeWordsPrintDocument(doc);

            #region -- Generate docs --

            var table = new DataTable("Voucher");
            table.Columns.Add("VoucherName");
            table.Columns.Add("Agency");
            table.Columns.Add("ApplyFor");
            table.Columns.Add("Cruise");
            table.Columns.Add("Trip");
            table.Columns.Add("Value");
            table.Columns.Add("ValidUntil");
            table.Columns.Add("IssueDate");
            table.Columns.Add("Code");

            //double total = 0;
            for (int ii = 1; ii <= batch.Quantity; ii++)
            {
                DataRow row = table.NewRow();
                row["VoucherName"] = batch.Name;
                if (batch.Agency != null)
                {
                    row["Agency"] = batch.Agency.Name;
                }

                if (batch.NumberOfPerson == 1)
                {
                    row["ApplyFor"] = "Single person";
                }
                else
                {
                    row["ApplyFor"] = "02 PERSONS, 01 sharing cabin";
                }
                row["Cruise"] = batch.Cruise.Name;
                row["Trip"] = batch.Trip.Name;
                row["Value"] = batch.Value;
                row["ValidUntil"] = batch.ValidUntil.ToString("dd/MM/yyyy");
                if (batch.IssueDate.HasValue)
                {
                    row["IssueDate"] = batch.IssueDate.Value.ToString("dd/MM/yyyy");
                }
                row["Code"] = VoucherCodeEncryption.Encrypt(Convert.ToUInt32(batch.Id*1000 + ii));
                table.Rows.Add(row);
            }

            doc.MailMerge.Execute(table);

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            //Response.ContentType = "application/msword";
            Response.AppendHeader("content-disposition",
                                  "attachment; filename=" + string.Format("{0}.pdf", "voucher" + batch.Id));
            //                  "attachment; filename=" + string.Format("{0}.doc", "voucher" + batch.Id));

            var m = new MemoryStream();

            doc.Save(m, SaveFormat.Doc);

            Response.OutputStream.Write(m.GetBuffer(), 0, m.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();

            m.Close();
            Response.End();

            #endregion

            if (!batch.IssueDate.HasValue)
            {
                batch.IssueDate = DateTime.Today;
                batch.Issued = true;
                Module.SaveOrUpdate(batch, UserIdentity);
            }
        }
    }
}