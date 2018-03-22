using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI.WebControls;
using CMS.Web.Util;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;
using Aspose.Words;
using System.IO;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class SurveyInput : SailsAdminBasePage
    {
        private Question _currentQuestion;
        private IList<string> _currentOptions;
        private bool _hasQuestion = true;
        private AnswerSheet _currentSheet;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["sheetId"] != null)
            {
                _currentSheet = Module.AnswerSheetGetById(Convert.ToInt32(Request.QueryString["sheetId"]));
            }
            else
            {
                _currentSheet = new AnswerSheet();
            }


            if (!IsPostBack)
            {
                rptGroups.DataSource = Module.QuestionGroupGetAll();
                rptGroups.DataBind();
                rptDayboat.DataSource = Module.QuestionGroupGetAllDayboat();
                rptDayboat.DataBind();
                ddlCruises.DataSource = Module.CruiseGetAll();
                ddlCruises.DataTextField = "Name";
                ddlCruises.DataValueField = "Id";
                ddlCruises.DataBind();

                ddlNationalities.DataSource = Module.NationalityGetAll();
                ddlNationalities.DataTextField = "Name";
                ddlNationalities.DataValueField = "Id";
                ddlNationalities.DataBind();
                ddlNationalities.Items.Insert(0, "-- Not available --");

                if (Request.QueryString["startdate"] != null)
                {
                    DateTime date = DateTime.FromOADate(Convert.ToDouble(Request.QueryString["startdate"]));
                    txtStartDate.Text = date.ToString("dd/MM/yyyy");

                    ddlBookings.DataSource = Module.BookingGetByStartDate(date, null, false);
                    ddlBookings.DataTextField = "BookingIdOS";
                    ddlBookings.DataValueField = "Id";
                    ddlBookings.DataBind();
                }
                else if (Request.QueryString["bookingid"]!=null)
                {
                    Booking booking = Module.BookingGetById(Convert.ToInt32(Request.QueryString["bookingid"]));
                    ddlBookings.DataSource = Module.BookingGetByStartDate(booking.StartDate, null, false);
                    ddlBookings.DataTextField = "BookingIdOS";
                    ddlBookings.DataValueField = "Id";
                    ddlBookings.DataBind();
                    ddlBookings.SelectedValue = booking.Id.ToString();

                    ddlCruises.SelectedValue = booking.Cruise.Id.ToString();
                    _currentSheet.Booking = booking;

                    if (booking.Trip.NumberOfDay != 3)
                    {
                        pnDayboat.Visible = false;
                    }
                }
                

                if (_currentSheet.Booking != null)
                {
                    Booking booking = _currentSheet.Booking;
                    SailExpense expense = Module.ExpenseGetByDate(booking.Cruise, booking.StartDate);
                    txtGuide.Visible = false;
                    txtDriver.Visible = false;

                    ddlGuide.Items.Clear();
                    ddlDrivers.Items.Clear();
                    foreach (ExpenseService sv in expense.Services)
                    {
                        if (sv.Type.Id == SailsModule.GUIDE_COST)
                        {
                            ddlGuide.Items.Add(sv.Supplier.Name);
                        }
                        if (sv.Type.Id == 2)
                        {
                            ddlDrivers.Items.Add(sv.Name);
                        }
                    }
                    ddlGuide.Items.Insert(0, "-- Not specified --");
                    ddlDrivers.Items.Insert(0, "-- Not specified --");

                    if (booking.Trip.NumberOfDay != 3)
                    {
                        pnDayboat.Visible = false;
                    }
                }
                else
                {
                    ddlGuide.Visible = false;
                    ddlDrivers.Visible = false;
                }

                ddlBookings.Items.Insert(0, "-- Not available --");                

                if (_currentSheet.Id > 0)
                {
                    if (_currentSheet.Booking != null)
                    {
                        ddlBookings.SelectedValue = _currentSheet.Booking.Id.ToString();
                        if (!string.IsNullOrEmpty(_currentSheet.Guide))
                        {
                            ddlGuide.SelectedValue = _currentSheet.Guide;
                        }
                        if (!string.IsNullOrEmpty(_currentSheet.Driver))
                        {
                            ddlDrivers.SelectedValue = _currentSheet.Driver;
                        }
                    }
                    else
                    {
                        txtGuide.Text = _currentSheet.Guide;
                        txtDriver.Text = _currentSheet.Driver;
                    }

                    if (_currentSheet.Nationality!=null)
                    {
                        ddlNationalities.SelectedValue = _currentSheet.Nationality.Id.ToString();
                    }
                    ddlCruises.SelectedValue = _currentSheet.Cruise.Id.ToString();
                    if (_currentSheet.Date.HasValue)
                        txtStartDate.Text = _currentSheet.Date.Value.ToString("dd/MM/yyyy");
                    txtName.Text = _currentSheet.Name;
                    txtEmail.Text = _currentSheet.Email;
                    txtAddress.Text = _currentSheet.Address;
                    txtRoomNumber.Text = _currentSheet.RoomNumber;
                }
            }
        }

        protected void rptGroups_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is QuestionGroup)
            {
                QuestionGroup group = (QuestionGroup)e.Item.DataItem;
                ValueBinder.BindLiteral(e.Item, "litGroupName", group.Name);
                TextBox txtComment = (TextBox)e.Item.FindControl("txtComment");

                Repeater rptQuestions = (Repeater)e.Item.FindControl("rptQuestions");
                _currentOptions = group.Selections;
                rptQuestions.DataSource = group.Questions;
                rptQuestions.DataBind();

                foreach (AnswerGroup grp in _currentSheet.Groups)
                {
                    if (grp.Group.Id == group.Id)
                    {
                        txtComment.Text = grp.Comment;
                        break;
                    }
                }

                if (group.Questions.Count > 0)
                {
                    Repeater rptOptions = (Repeater)e.Item.FindControl("rptOptions");
                    rptOptions.DataSource = _currentOptions;
                    rptOptions.DataBind();
                    _hasQuestion = true;
                }
                else
                {
                    //_hasQuestion = false;
                }
            }
        }

        protected void rptQuestion_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is Question)
            {
                Question question = (Question)e.Item.DataItem;
                ValueBinder.BindLiteral(e.Item, "litQuestion", question.Content);

                _currentQuestion = question;

                Repeater rptOptions = (Repeater)e.Item.FindControl("rptOptions");
                rptOptions.DataSource = _currentOptions;
                rptOptions.DataBind();
            }
        }

        protected void rptOptions_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RadioButton radOption = (RadioButton)e.Item.FindControl("radOption");
            radOption.GroupName = _currentQuestion.Id.ToString();

            if (!_hasQuestion)
            {
                radOption.Text = e.Item.DataItem.ToString();
            }

            foreach (AnswerOption option in _currentSheet.Options)
            {
                if (option.Question.Id == _currentQuestion.Id)
                {
                    if (option.Option == e.Item.ItemIndex + 1) radOption.Checked = true;
                    break;
                }
            }

            string script = "SetUniqueRadioButton('$" + _currentQuestion.Id + "',this)";
            radOption.Attributes.Add("onclick", script);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            #region -- Load booking or load cruises --
            if (ddlBookings.SelectedIndex > 0)
            {
                Booking booking = Module.BookingGetById(Convert.ToInt32(ddlBookings.SelectedValue));
                _currentSheet.Booking = booking;
                _currentSheet.Date = booking.StartDate;
                _currentSheet.Cruise = booking.Cruise;
            }
            else
            {
                _currentSheet.Date = DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                Cruise cruise = Module.CruiseGetById(Convert.ToInt32(ddlCruises.SelectedValue));
                _currentSheet.Cruise = cruise;
            }
            #endregion

            #region -- Save sheet --
            if (ddlGuide.Visible)
            {
                if (ddlGuide.SelectedIndex > 0)
                _currentSheet.Guide = ddlGuide.SelectedValue;
                else
                {
                    _currentSheet.Guide = string.Empty;
                }
            }
            else
            {
                _currentSheet.Guide = txtGuide.Text;
            }
            if (ddlDrivers.Visible)
            {
                if (ddlDrivers.SelectedIndex > 0)
                    _currentSheet.Driver = ddlDrivers.SelectedValue;
                else
                {
                    _currentSheet.Driver = string.Empty;
                }
            }
            else
            {
                _currentSheet.Driver = txtDriver.Text;
            }

            _currentSheet.Name = txtName.Text;
            _currentSheet.Email = txtEmail.Text;
            _currentSheet.Address = txtAddress.Text;
            _currentSheet.RoomNumber = txtRoomNumber.Text;
            if (ddlNationalities.SelectedIndex > 0)
            {
                _currentSheet.Nationality = Module.NationalityGetById(Convert.ToInt32(ddlNationalities.SelectedValue));
            }
            Module.SaveOrUpdate(_currentSheet, UserIdentity);
            #endregion

            foreach (RepeaterItem groupItem in rptGroups.Items)
            {
                HiddenField hiddenId = (HiddenField)groupItem.FindControl("hiddenId");
                TextBox txtComment = (TextBox)groupItem.FindControl("txtComment");
                Repeater rptQuestions = (Repeater)groupItem.FindControl("rptQuestions");

                QuestionGroup grp = Module.QuestionGroupGetById(Convert.ToInt32(hiddenId.Value));
                AnswerGroup group = _currentSheet.GetGroup(grp);
                group.AnswerSheet = _currentSheet;
                group.Group = grp;
                group.Comment = txtComment.Text;
                Module.SaveOrUpdate(group, UserIdentity);

                foreach (RepeaterItem questionItem in rptQuestions.Items)
                {
                    HiddenField hiddenQId = (HiddenField)questionItem.FindControl("hiddenId");
                    Question question = Module.QuestionGetById(Convert.ToInt32(hiddenQId.Value));
                    Repeater rptOptions = (Repeater)questionItem.FindControl("rptOptions");

                    AnswerOption option = _currentSheet.GetOption(question);
                    option.Question = question;
                    option.AnswerSheet = _currentSheet;
                    for (int ii = 0; ii < rptOptions.Items.Count; ii++)
                    {
                        RadioButton radOption = (RadioButton)rptOptions.Items[ii].FindControl("radOption");
                        if (radOption.Checked)
                        {
                            option.Option = ii + 1;
                        }
                    }
                    Module.SaveOrUpdate(option);
                }
            }

            foreach (RepeaterItem groupItem in rptDayboat.Items)
            {
                HiddenField hiddenId = (HiddenField)groupItem.FindControl("hiddenId");
                TextBox txtComment = (TextBox)groupItem.FindControl("txtComment");
                Repeater rptQuestions = (Repeater)groupItem.FindControl("rptQuestions");

                QuestionGroup grp = Module.QuestionGroupGetById(Convert.ToInt32(hiddenId.Value));
                AnswerGroup group = _currentSheet.GetGroup(grp);
                group.AnswerSheet = _currentSheet;
                group.Group = grp;
                group.Comment = txtComment.Text;
                Module.SaveOrUpdate(group, UserIdentity);

                foreach (RepeaterItem questionItem in rptQuestions.Items)
                {
                    HiddenField hiddenQId = (HiddenField)questionItem.FindControl("hiddenId");
                    Question question = Module.QuestionGetById(Convert.ToInt32(hiddenQId.Value));
                    Repeater rptOptions = (Repeater)questionItem.FindControl("rptOptions");

                    AnswerOption option = _currentSheet.GetOption(question);
                    option.Question = question;
                    option.AnswerSheet = _currentSheet;
                    for (int ii = 0; ii < rptOptions.Items.Count; ii++)
                    {
                        RadioButton radOption = (RadioButton)rptOptions.Items[ii].FindControl("radOption");
                        if (radOption.Checked)
                        {
                            option.Option = ii + 1;
                        }
                    }
                    Module.SaveOrUpdate(option);
                }
            }

            ClientScript.RegisterClientScriptBlock(typeof(SurveyInput), "closure", "window.close();", true);
        }

        protected void btnExportFeedBack_Click(object sender, EventArgs e) {
            var doc = new Document();
            var documentBuilder = new DocumentBuilder(doc);
            foreach (QuestionGroup questionGroup in Module.QuestionGroupGetAll()) {
                documentBuilder.InsertParagraph();
                documentBuilder.Write(questionGroup.Name);
                var questions = questionGroup.Questions;
                foreach (Question question in questions)
                {
                    documentBuilder.InsertParagraph();
                    documentBuilder.Write(question.Name);
                }
            }
            
            Response.Clear();
            Response.Buffer = true;
            //Response.ContentType = "application/pdf";
            Response.ContentType = "application/msword";
            Response.AppendHeader("content-disposition",
                //"attachment; filename=" + string.Format("{0}.pdf", "voucher" + batch.Id));
                                  "attachment; filename=" + string.Format("test.doc"));

            var m = new MemoryStream();
            doc.Save(m, SaveFormat.Doc);

            Response.OutputStream.Write(m.GetBuffer(), 0, m.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();

            m.Close();
            Response.End();
        }
    }
}