using System;
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
    public partial class QuestionView : SailsAdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rptGroups.DataSource = Module.QuestionGroupGetAll();
                rptGroups.DataBind();
                rptDayboatGroup.DataSource = Module.QuestionGroupGetAllDayboat();
                rptDayboatGroup.DataBind();
            }
        }

        protected void rptGroups_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is QuestionGroup)
            {
                QuestionGroup group = (QuestionGroup) e.Item.DataItem;
                Repeater rptQuestions = (Repeater) e.Item.FindControl("rptQuestions");
                HyperLink hplEdit = (HyperLink) e.Item.FindControl("hplEdit");

                rptQuestions.DataSource = group.Questions;
                rptQuestions.DataBind();
                hplEdit.NavigateUrl = string.Format("QuestionGroupEdit.aspx?NodeId={0}&SectionId={1}&groupid={2}",
                                                    Node.Id, Section.Id, group.Id);
            }
        }

        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            Control ctrl = (Control) sender;
            HiddenField hiddenId = (HiddenField) ctrl.Parent.FindControl("hiddenId");
            QuestionGroup group = Module.QuestionGroupGetById(Convert.ToInt32(hiddenId.Value));
            group.Deleted = true;
            Module.SaveOrUpdate(group, UserIdentity);

            rptGroups.DataSource = Module.QuestionGroupGetAll();
            rptGroups.DataBind();
        }
    }
}
