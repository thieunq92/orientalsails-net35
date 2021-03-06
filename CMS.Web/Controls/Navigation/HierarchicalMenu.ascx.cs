using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CMS.Core.Domain;
using CMS.Web.UI;
using CMS.Web.Util;

namespace CMS.Web.Controls.Navigation
{
    /// <summary>
    ///		Summary description for HierarchicalMenu.
    /// </summary>
    public class HierarchicalMenu : UserControl
    {
        private PageEngine _page;
        protected PlaceHolder plhNodesHierarchical;

        private void Page_Load(object sender, EventArgs e)
        {
            if (Page is PageEngine)
            {
                _page = (PageEngine) Page;
                BuildNavigationTree();
            }
        }

        private void BuildNavigationTree()
        {
            HtmlGenericControl mainList = new HtmlGenericControl("ul");
            mainList.Attributes.Add("id", "hierarchicalmenu");
            if (_page.RootNode.ShowInNavigation && _page.RootNode.ViewAllowed(_page.CuyahogaUser))
            {
                mainList.Controls.Add(BuildListItemFromNode(_page.RootNode));
            }
            foreach (Node node in _page.RootNode.ChildNodes)
            {
                if (node.ShowInNavigation && node.ViewAllowed(_page.CuyahogaUser))
                {
                    HtmlControl listItem = BuildListItemFromNode(node);
                    if (node.ChildNodes.Count > 0)
                    {
                        HtmlControl list = BuildListFromNodes(node.ChildNodes);
                        // Double check: nếu có thành viên không ẩn (list có phần tử) mới hiển thị
                        if (list.Controls.Count > 0)
                        {
                            listItem.Controls.Add(list);
                        }
                    }
                    mainList.Controls.Add(listItem);
                }
            }
            if (_page.CuyahogaUser != null
                && _page.CuyahogaUser.HasPermission(AccessLevel.Administrator))
            {
                HtmlGenericControl listItem = new HtmlGenericControl("li");
                HyperLink hpl = new HyperLink();
                hpl.NavigateUrl = _page.ResolveUrl("~/Admin");
                hpl.Text = "Admin";
                listItem.Controls.Add(hpl);
                mainList.Controls.Add(listItem);
            }
            plhNodesHierarchical.Controls.Add(mainList);
        }

        private HtmlControl BuildListItemFromNode(Node node)
        {
            HtmlGenericControl listItem = new HtmlGenericControl("li");
            HyperLink hpl = new HyperLink();
            if (node.ChildNodes.Count == 0 || node.IsRootNode)
            {
                hpl.NavigateUrl = UrlHelper.GetUrlFromNode(node);
            }
            else
            {
                hpl.NavigateUrl = String.Empty;
            }
            UrlHelper.SetHyperLinkTarget(hpl, node);
            hpl.Text = node.Title;
            // Little dirty trick to highlight the active item :)
            if (node.Id == _page.ActiveNode.Id)
            {
                listItem.Attributes.Add("class", "selected");
            }
            listItem.Controls.Add(hpl);
            return listItem;
        }

        private HtmlControl BuildListFromNodes(IList nodes)
        {
            HtmlGenericControl list = new HtmlGenericControl("ul");
            foreach (Node node in nodes)
            {
                if (node.ViewAllowed(_page.CuyahogaUser) && node.ShowInNavigation)
                {
                    HtmlControl listItem = BuildListItemFromNode(node);
                    if (node.ChildNodes.Count > 0)
                    {
                        HtmlControl childlist = BuildListFromNodes(node.ChildNodes);
                        // Double check: nếu có thành viên không ẩn (list có phần tử) mới hiển thị
                        if (childlist.Controls.Count > 0)
                        {
                            listItem.Controls.Add(childlist);
                        }
                    }
                    list.Controls.Add(listItem);
                }
            }
            return list;
        }

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }

        #endregion
    }
}