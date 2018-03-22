using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.Core.Domain;
using CMS.Web.UI;

namespace CMS.Web.Controls.Navigation
{
    public partial class NavigationPath : UserControl
    {
        private PageEngine _page;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (!(Page is PageEngine))
            {
                return;
            }
            _page = (PageEngine)Page;
            BuildNavigationTree();
            base.Render(writer);
        }

        private void BuildNavigationTree()
        {
            IList dictionary = _page.NavigationPath;

            foreach(string[] entry in dictionary)
            {
                HyperLink hpl = new HyperLink();
                hpl.Text = entry[0];
                hpl.NavigateUrl = entry[1];
                if (plhPath.Controls.Count == 0)
                {
                    plhPath.Controls.Add(hpl);
                }
                else
                {
                    Label label = new Label();
                    label.Text = " >> ";
                    plhPath.Controls.Add(label);
                    plhPath.Controls.Add(hpl);
                }
            }
        }
    }
}