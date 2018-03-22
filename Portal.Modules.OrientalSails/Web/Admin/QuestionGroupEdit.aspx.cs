using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class QuestionGroupEdit : SailsAdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["groupid"] != null)
                {
                    QuestionGroup group = Module.QuestionGroupGetById(Convert.ToInt32(Request.QueryString["groupid"]));
                    txtSubject.Text = group.Name;
                    txtSelection1.Text = group.Selection1;
                    txtSelection2.Text = group.Selection2;
                    txtSelection3.Text = group.Selection3;
                    txtSelection4.Text = group.Selection4;
                    txtSelection5.Text = group.Selection5;
                    txtPriority.Text = group.Priority.ToString();
                    chkIsInDayboatForm.Checked = group.IsInDayboatForm;
                    ddlGoodChoice.SelectedValue = group.GoodChoice.ToString();

                    rptSubCategory.DataSource = group.Questions;
                    rptSubCategory.DataBind();
                }
            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            ((Control)sender).Parent.Visible = false;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            IList<Question> data = RepeaterToList();
            data.Add(new Question());
            rptSubCategory.DataSource = data;
            rptSubCategory.DataBind();
        }

        protected IList<Question> RepeaterToList()
        {
            IList<Question> result = new List<Question>();
            foreach (RepeaterItem item in rptSubCategory.Items)
            {
                HiddenField hiddenId = (HiddenField)item.FindControl("hiddenId");
                TextBox txtName = (TextBox)item.FindControl("txtName");
                TextBox txtContent = (TextBox)item.FindControl("txtContent");

                Question question = new Question();
                int id = Convert.ToInt32(hiddenId.Value);
                if (id > 0)
                {
                    question = Module.QuestionGetById(id);
                }
                question.Name = txtName.Text;
                question.Content = txtContent.Text;
                question.Deleted = !item.Visible;
                result.Add(question);
            }

            return result;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            QuestionGroup group;
            if (Request.QueryString["groupid"] != null)
            {
                group = Module.QuestionGroupGetById(Convert.ToInt32(Request.QueryString["groupid"]));
            }
            else
            {
                group = new QuestionGroup();
            }
            group.Name = txtSubject.Text;
            group.Selection1 = txtSelection1.Text;
            group.Selection2 = txtSelection2.Text;
            group.Selection3 = txtSelection3.Text;
            group.Selection4 = txtSelection4.Text;
            group.Selection5 = txtSelection5.Text;
            group.Priority = Convert.ToInt32(txtPriority.Text);
            group.GoodChoice = Convert.ToInt32(ddlGoodChoice.SelectedValue);
            group.IsInDayboatForm = chkIsInDayboatForm.Checked;
            Module.SaveOrUpdate(group, UserIdentity);

            IList<Question> data = RepeaterToList();
            foreach (Question q in data)
            {
                q.Group = group;
                Module.SaveOrUpdate(q, UserIdentity);
            }

            PageRedirect(string.Format("QuestionView.aspx?NodeId={0}&SectionId={1}", Node.Id, Section.Id));
        }

        protected void rptSubCategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is Question)
            {
                Question question = (Question)e.Item.DataItem;

                TextBox txtName = (TextBox)e.Item.FindControl("txtName");
                TextBox txtContent = (TextBox)e.Item.FindControl("txtContent");

                txtName.Text = question.Name;
                txtContent.Text = question.Content;
                e.Item.Visible = !question.Deleted;
            }
        }
    }
}
