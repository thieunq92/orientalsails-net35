using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class DocumentView : SailsAdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var all = new AjaxControlToolkit.TabPanel();
            all.HeaderText = "All";
            all.ID = "tabAll";

            var listall = Module.DocumentGetAll();
            var litAll = new Literal();
            litAll.Text = GenerateHtmlList(listall);

            all.Controls.Add(litAll);
            
            tabDocuments.Tabs.Add(all);

            var list = Module.DocumentGetCategory();
            foreach (DocumentCategory category in list)
            {
                var tab = new AjaxControlToolkit.TabPanel();
                tab.HeaderText = category.Name;
                tab.ID = "tab" + category.Id;

                var filterlist = new ArrayList();

                foreach (DocumentCategory document in listall)
                {
                    if (document.Parent.Id == category.Id)
                    {
                        filterlist.Add(document);
                    }
                }

                var litText = new Literal();
                litText.Text = GenerateHtmlList(filterlist);

                tab.Controls.Add(litText);

                tabDocuments.Tabs.Add(tab);
            }

            tabDocuments.ActiveTabIndex = 0;
        }

        private string GenerateHtmlList(IList list)
        {
            StringBuilder strB = new StringBuilder();
            strB.Append("<div class='data_grid'>");
            strB.Append("<table style='width: 600px;'>");
            strB.Append("<tr><th>Name</th><th style='width: 200px;'>Category</th></tr>");
            foreach (DocumentCategory document in list)
            {
                if (!string.IsNullOrEmpty(document.Url))
                {
                    strB.Append("<tr>");
                    strB.AppendFormat("<td><a target='_blank' href='{1}'>{0}</a></td>", document.Name, document.Url);
                    string parent = document.Parent!=null?document.Parent.Name:"";
                    strB.AppendFormat("<td>{0}</td>", parent);
                    strB.Append("</tr>");
                }
            }
            strB.Append("</table>");
            strB.Append("</div>");
            return strB.ToString();
        }
    }
}