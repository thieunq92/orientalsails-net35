using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.Core.Domain;
using CMS.Core.Util;
using CMS.Web.Admin.UI;
using CMS.Web.UI;
using CMS.Web.Util;
using log4net;

namespace CMS.Web.Admin
{
    /// <summary>
    /// Summary description for NodeEdit.
    /// </summary>
    public class NodeEdit : AdminBasePage
    {
        private static readonly ILog log = LogManager.GetLogger(typeof (NodeEdit));

        protected Button btnCancel;
        protected Button btnDelete;
        protected ImageButton btnDown;
        protected ImageButton btnLeft;
        protected Button btnNew;
        protected ImageButton btnRight;
        protected Button btnSave;
        protected ImageButton btnUp;
        protected CheckBox chkLink;
        protected CheckBox chkPropagateToChildNodes;
        protected CheckBox chkPropagateToSections;
        protected CheckBox chkShowInNavigation;
        protected DropDownList ddlCultures;
        protected DropDownList ddlLinkTarget;
        protected DropDownList ddlTemplates;
        protected HyperLink hplNewMenu;
        protected HyperLink hplNewSection;
        protected Label lblParentNode;
        protected Panel pnlLink;
        protected Panel pnlMenus;
        protected Panel pnlSections;
        protected Panel pnlTemplate;
        protected RegularExpressionValidator revShortDescription;
        protected RequiredFieldValidator rfvShortDescription;
        protected RequiredFieldValidator rfvTitle;
        protected Repeater rptDeleteRoles;
        protected Repeater rptMenus;
        protected Repeater rptRoles;
        protected Repeater rptSections;
        protected TextBox txtLinkUrl;
        protected TextBox txtMetaDescription;
        protected TextBox txtMetaKeywords;
        protected TextBox txtShortDescription;
        protected TextBox txtTitle;
        protected Label labelExtension;

        private void Page_Load(object sender, EventArgs e)
        {
            Title = "Sửa nút";
            labelExtension.Text = UrlHelper.EXTENSION;
            // Note: ActiveNode is handled primarily by the AdminBasePage because other pages use it.
            // ActiveNode is always freshly retrieved (also after postbacks), so it will be tracked by NHibernate.
            if (Context.Request.QueryString["NodeId"] != null
                && Int32.Parse(Context.Request.QueryString["NodeId"]) == -1)
            {
                // Create an empty new node if NodeId is set to -1
                ActiveNode = new Node();
                if (Context.Request.QueryString["ParentNodeId"] != null)
                {
                    int parentNodeId = Int32.Parse(Context.Request.QueryString["ParentNodeId"]);
                    ActiveNode.ParentNode = (Node) CoreRepository.GetObjectById(typeof (Node), parentNodeId);
                    // Copy Site property from parent.
                    ActiveNode.Site = ActiveNode.ParentNode.Site;

                    if (! IsPostBack)
                    {
                        // Set defaults.
                        ActiveNode.Template = ActiveNode.ParentNode.Template;
                        ActiveNode.Culture = ActiveNode.ParentNode.Culture;
                        // Copy security from parent.
                        ActiveNode.CopyRolesFromParent();
                    }
                }
                else if (Context.Request.QueryString["SiteId"] != null)
                {
                    int siteId = Int32.Parse(Context.Request.QueryString["SiteId"]);
                    ActiveNode.Site = (Site) CoreRepository.GetObjectById(typeof (Site), siteId);

                    // Set defaults inheriting from site
                    ActiveNode.Culture = ActiveNode.Site.DefaultCulture;
                    ActiveNode.Template = ActiveNode.Site.DefaultTemplate;
                }
                // Short description is auto-generated, so we don't need the controls with new nodes.
                txtShortDescription.Visible = false;
                rfvShortDescription.Enabled = false;
                revShortDescription.Enabled = false;
            }
            if (! IsPostBack)
            {
                // There could be a section movement in the request. Check this and move sections if necessary.
                if (Context.Request.QueryString["SectionId"] != null && Context.Request.QueryString["Action"] != null)
                {
                    MoveSections();
                }
                else
                {
                    if (ActiveNode != null)
                    {
                        BindNodeControls();
                        BindSections();
                        if (ActiveNode.IsRootNode)
                        {
                            BindMenus();
                        }
                    }
                    BindCultures();
                    BindTemplates();
                    BindRoles();
                }
            }
            if (ActiveNode != null)
            {
                BindPositionButtonsVisibility();
            }
        }

        private void BindNodeControls()
        {
            txtTitle.Text = ActiveNode.Title;
            txtShortDescription.Text = ActiveNode.ShortDescription;
            if (ActiveNode.ParentNode != null)
            {
                lblParentNode.Text = ActiveNode.ParentNode.Title;
            }
            chkShowInNavigation.Checked = ActiveNode.ShowInNavigation;
            txtMetaDescription.Text = ActiveNode.MetaDescription;
            txtMetaKeywords.Text = ActiveNode.MetaKeywords;

            chkLink.Enabled = ActiveNode.Sections.Count == 0;
            if (ActiveNode.IsExternalLink)
            {
                chkLink.Checked = true;
                pnlLink.Visible = true;
                pnlMenus.Visible = false;
                pnlTemplate.Visible = false;
                pnlSections.Visible = false;
                txtLinkUrl.Text = ActiveNode.LinkUrl;
                ddlLinkTarget.Items.FindByValue(ActiveNode.LinkTarget.ToString()).Selected = true;
            }
            // main buttons visibility
            btnNew.Visible = (ActiveNode.Id > 0);
            btnDelete.Visible = (ActiveNode.Id > 0);
            btnDelete.Attributes.Add("onclick", "return confirmDeleteNode();");
        }

        private void BindCultures()
        {
            ddlCultures.DataSource = Globalization.GetOrderedCultures();
            ddlCultures.DataValueField = "Key";
            ddlCultures.DataTextField = "Value";
            ddlCultures.DataBind();
            if (ActiveNode.Culture != null)
            {
                ddlCultures.Items.FindByValue(ActiveNode.Culture).Selected = true;
            }
        }

        private void BindPositionButtonsVisibility()
        {
            // node location buttons visibility
            btnUp.Visible = (ActiveNode.Position > 0);
            btnDown.Visible = ((ActiveNode.ParentNode != null) &&
                               (ActiveNode.Position != ActiveNode.ParentNode.ChildNodes.Count - 1) &&
                               ActiveNode.Id != -1);
            btnLeft.Visible = (ActiveNode.Level > 0 && ActiveNode.Id != -1);
            btnRight.Visible = (ActiveNode.Position > 0);
        }

        private void BindTemplates()
        {
            IList templates = CoreRepository.GetAll(typeof (Template), "Name");

            // Bind
            ddlTemplates.DataSource = templates;
            ddlTemplates.DataValueField = "Id";
            ddlTemplates.DataTextField = "Name";
            ddlTemplates.DataBind();
            if (ActiveNode != null && ActiveNode.Template != null)
            {
                ListItem li = ddlTemplates.Items.FindByValue(ActiveNode.Template.Id.ToString());
                if (li != null)
                {
                    li.Selected = true;
                }
            }
        }

        private void BindMenus()
        {
            pnlMenus.Visible = true;
            rptMenus.DataSource = CoreRepository.GetMenusByRootNode(ActiveNode);
            rptMenus.DataBind();
            hplNewMenu.NavigateUrl = String.Format("~/Admin/MenuEdit.aspx?MenuId=-1&NodeId={0}", ActiveNode.Id);
        }

        private void BindSections()
        {
            IList sortedSections = CoreRepository.GetSortedSectionsByNode(ActiveNode);
            // Synchronize sections, otherwise we'll have two collections with the same Sections
            ActiveNode.Sections = sortedSections;
            rptSections.DataSource = sortedSections;
            rptSections.DataBind();
            if (ActiveNode.Id > 0 && ActiveNode.Template != null)
            {
                // Also enable add section link
                hplNewSection.NavigateUrl = String.Format("~/Admin/SectionEdit.aspx?SectionId=-1&NodeId={0}",
                                                          ActiveNode.Id);
                hplNewSection.Visible = true;
            }
        }

        private void BindRoles()
        {
            IList roles = CoreRepository.GetAll(typeof (Role), "PermissionLevel");
            rptRoles.ItemDataBound += rptRoles_ItemDataBound;
            rptRoles.DataSource = roles;
            rptRoles.DataBind();
        }

        private void SetTemplate()
        {
            if (ddlTemplates.Visible && ddlTemplates.SelectedValue != "-1")
            {
                int templateId = Int32.Parse(ddlTemplates.SelectedValue);
                ActiveNode.Template = (Template) CoreRepository.GetObjectById(typeof (Template), templateId);
            }
        }

        private void SetRoles()
        {
            ActiveNode.NodePermissions.Clear();
            foreach (RepeaterItem ri in rptRoles.Items)
            {
                // HACK: RoleId is stored in the ViewState because the repeater doesn't have a DataKeys property.
                CheckBox chkView = (CheckBox) ri.FindControl("chkViewAllowed");
                CheckBox chkEdit = (CheckBox) ri.FindControl("chkEditAllowed");
                if (chkView.Checked || chkEdit.Checked)
                {
                    NodePermission np = new NodePermission();
                    np.Node = ActiveNode;
                    np.Role = (Role) CoreRepository.GetObjectById(typeof (Role), (int) ViewState[ri.ClientID]);
                    np.ViewAllowed = chkView.Checked;
                    np.EditAllowed = chkEdit.Checked;
                    ActiveNode.NodePermissions.Add(np);
                }
            }
        }

        private void SaveNode()
        {
            CoreRepository.ClearQueryCache("Nodes");

            if (ActiveNode.Id > 0)
            {
                CoreRepository.UpdateNode(ActiveNode
                                               , chkPropagateToChildNodes.Checked
                                               , chkPropagateToSections.Checked);
            }
            else
            {
                IList rootNodes = CoreRepository.GetRootNodes(ActiveNode.Site);
                ActiveNode.CalculateNewPosition(rootNodes);
                // Add node to the parent node's ChildNodes first
                if (ActiveNode.ParentNode != null)
                {
                    ActiveNode.ParentNode.ChildNodes.Add(ActiveNode);
                }
                CoreRepository.SaveObject(ActiveNode);
                Context.Response.Redirect(String.Format("NodeEdit.aspx?NodeId={0}&message=Node created sucessfully",
                                                        ActiveNode.Id));
            }
        }

        private void MoveSections()
        {
            int sectionId = Int32.Parse(Context.Request.QueryString["SectionId"]);
            Section section = (Section) CoreRepository.GetObjectById(typeof (Section), sectionId);
            section.Node = ActiveNode;
            if (Context.Request.QueryString["Action"] == "MoveUp")
            {
                section.MoveUp();
                CoreRepository.FlushSession();
                // reset sections, so they will be refreshed from the database when required.
                ActiveNode.ResetSections();
            }
            else if (Context.Request.QueryString["Action"] == "MoveDown")
            {
                section.MoveDown();
                CoreRepository.FlushSession();
                // reset sections, so they will be refreshed from the database when required.
                ActiveNode.ResetSections();
            }
            // Redirect to the same page without the section movement parameters
            Context.Response.Redirect(Context.Request.Path + String.Format("?NodeId={0}", ActiveNode.Id));
        }

        private void SetShortDescription()
        {
            // TODO: check uniqueness. It's now handled by the database constraint but that is not
            // too descriptive.
            if (ActiveNode.Id > 0)
            {
                ActiveNode.ShortDescription = txtShortDescription.Text;
            }
            else
            {
                // Generate the short description for new nodes.
                string title = ActiveNode.Title;
                //VnFontConverter vnFontConverter = new VnFontConverter();
                //vnFontConverter.Convert(ref title, FontIndex.iUNI, FontIndex.iNOSIGN);
                ActiveNode.CreateShortDescription(title);
            }
        }

        private void MoveNode(NodePositionMovement npm)
        {
            CoreRepository.ClearQueryCache("Nodes");

            IList rootNodes = CoreRepository.GetRootNodes(ActiveNode.Site);
            ActiveNode.Move(rootNodes, npm);
            CoreRepository.FlushSession();
            Context.Response.Redirect(Context.Request.RawUrl);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SetTemplate();
                if (IsValid)
                {
                    ActiveNode.Title = txtTitle.Text;
                    ActiveNode.Culture = ddlCultures.SelectedValue;
                    ActiveNode.ShowInNavigation = chkShowInNavigation.Checked;
                    ActiveNode.MetaDescription = txtMetaDescription.Text.Trim().Length > 0
                                                     ? txtMetaDescription.Text.Trim()
                                                     : null;
                    ActiveNode.MetaKeywords = txtMetaKeywords.Text.Trim().Length > 0
                                                  ? txtMetaKeywords.Text.Trim()
                                                  : null;
                    if (chkLink.Checked)
                    {
                        ActiveNode.LinkUrl = txtLinkUrl.Text;
                        ActiveNode.LinkTarget =
                            (LinkTarget) Enum.Parse(typeof (LinkTarget), ddlLinkTarget.SelectedValue);
                    }
                    else // rabol: [#CUY-51] - Clear the link in the database
                    {
                        ActiveNode.LinkUrl = null;
                        ActiveNode.LinkTarget = LinkTarget.Self;
                    }
                    ActiveNode.Validate();
                    SetShortDescription();
                    SetRoles();
                    SaveNode();
                    ShowMessage("Nút đã được lưu.");
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (ex.InnerException != null)
                {
                    msg += ", " + ex.InnerException.Message;
                }
                ShowError(msg);
                log.Error("Lỗi trong quá trình lưu nút", ex);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            // Create an url with NodeId -1 and the Id of the current node as ParentId
            string url = String.Format("NodeEdit.aspx?NodeId=-1&ParentNodeId={0}", ActiveNode.Id);
            // Redirect to the new url
            Context.Response.Redirect(url);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (ActiveNode.Id == -1 && ActiveNode.ParentNode != null)
            {
                Context.Response.Redirect(String.Format("NodeEdit.aspx?NodeId={0}", ActiveNode.ParentNode.Id));
            }
            else
            {
                Context.Response.Redirect("Default.aspx");
            }
        }

        private void ddlTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SetTemplate();
                // Also save the current node (validate first)
                ActiveNode.Title = txtTitle.Text;
                ActiveNode.Culture = ddlCultures.SelectedValue;
                Validate();
                if (IsValid)
                {
                    SetShortDescription();
                    SetRoles();
                    SaveNode();
                    ShowMessage("Nút đã được lưu khi thay đổi mẫu giao diện.");
                    BindSections();
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
                log.Error("Có lỗi trong quá trình đổi giao diện.", ex);
            }
        }

        private void btnUp_Click(object sender, ImageClickEventArgs e)
        {
            MoveNode(NodePositionMovement.Up);
        }

        private void btnDown_Click(object sender, ImageClickEventArgs e)
        {
            MoveNode(NodePositionMovement.Down);
        }

        private void btnLeft_Click(object sender, ImageClickEventArgs e)
        {
            MoveNode(NodePositionMovement.Left);
        }

        private void btnRight_Click(object sender, ImageClickEventArgs e)
        {
            MoveNode(NodePositionMovement.Right);
        }

        private void rptSections_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Section section = e.Item.DataItem as Section;
            if (section != null)
            {
                HyperLink hplEdit = (HyperLink) e.Item.FindControl("hplEdit");

                // HACK: as long as ~/ doesn't work properly in mono we have to use a relative path from the Controls
                // directory due to the template construction.
                hplEdit.NavigateUrl = String.Format("~/Admin/SectionEdit.aspx?SectionId={0}&NodeId={1}", section.Id,
                                                    ActiveNode.Id);
                if (section.CanMoveUp())
                {
                    HyperLink hplSectionUp = (HyperLink) e.Item.FindControl("hplSectionUp");
                    hplSectionUp.NavigateUrl = Context.Request.RawUrl +
                                               String.Format("&SectionId={0}&Action=MoveUp", section.Id);
                    hplSectionUp.Visible = true;
                }
                if (section.CanMoveDown())
                {
                    HyperLink hplSectionDown = (HyperLink) e.Item.FindControl("hplSectionDown");
                    hplSectionDown.NavigateUrl = Context.Request.RawUrl +
                                                 String.Format("&SectionId={0}&Action=MoveDown", section.Id);
                    hplSectionDown.Visible = true;
                }
                LinkButton lbtDelete = (LinkButton) e.Item.FindControl("lbtDelete");
                lbtDelete.Attributes.Add("onclick", "return confirm('Bạn có chắc chắn?')");

                // Check if the placeholder exists in the currently attached template
                BaseTemplate templateControl =
                    (BaseTemplate) LoadControl(UrlHelper.GetApplicationPath() + ActiveNode.Template.Path);
                Label lblNotFound = (Label) e.Item.FindControl("lblNotFound");
                lblNotFound.Visible = (templateControl.Containers[section.PlaceholderId] == null);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (ActiveNode.Sections.Count > 0)
            {
                ShowError(
                    "Không thể xóa nút khi có các vùng phân hệ trong nó. Hãy xóa hoặc tạm gỡ toàn bộ các vùng này trước.");
            }
            else if (ActiveNode.ChildNodes.Count > 0)
            {
                ShowError("Không thể xóa nút khi còn các nút con. Hãy xóa hết các nút con trước.");
            }
            else
            {
                try
                {
                    CoreRepository.ClearQueryCache("Nodes");

                    bool hasParentNode = (ActiveNode.ParentNode != null);
                    if (hasParentNode)
                    {
                        ActiveNode.ParentNode.ChildNodes.Remove(ActiveNode);
                    }
                    else
                    {
                        IList rootNodes = CoreRepository.GetRootNodes(ActiveNode.Site);
                        rootNodes.Remove(ActiveNode);
                    }
                    CoreRepository.DeleteNode(ActiveNode);
                    // Reset the position of the 'neighbour' nodes.
                    if (ActiveNode.Level == 0)
                    {
                        ActiveNode.ReOrderNodePositions(CoreRepository.GetRootNodes(ActiveNode.Site),
                                                        ActiveNode.Position);
                    }
                    else
                    {
                        ActiveNode.ReOrderNodePositions(ActiveNode.ParentNode.ChildNodes, ActiveNode.Position);
                    }
                    CoreRepository.FlushSession();
                    if (hasParentNode)
                    {
                        Context.Response.Redirect(String.Format("NodeEdit.aspx?NodeId={0}", ActiveNode.ParentNode.Id));
                    }
                    else
                    {
                        Context.Response.Redirect("Default.aspx");
                    }
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                    log.Error(String.Format("Có lỗi khi xóa nút: {0}.", ActiveNode.Id), ex);
                }
            }
        }

        private void rptRoles_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Role role = e.Item.DataItem as Role;
            if (role != null)
            {
                CheckBox chkView = (CheckBox) e.Item.FindControl("chkViewAllowed");
                chkView.Checked = ActiveNode.ViewAllowed(role);
                CheckBox chkEdit = (CheckBox) e.Item.FindControl("chkEditAllowed");
                if (role.HasPermission(AccessLevel.Editor) || role.HasPermission(AccessLevel.Administrator))
                {
                    chkEdit.Checked = ActiveNode.EditAllowed(role);
                }
                else
                {
                    chkEdit.Visible = false;
                }
                // Add RoleId to the ViewState with the ClientID of the repeateritem as key.
                ViewState[e.Item.ClientID] = role.Id;
            }
        }

        private void rptMenus_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            CustomMenu menu = e.Item.DataItem as CustomMenu;
            if (menu != null)
            {
                HyperLink hplEdit = e.Item.FindControl("hplEditMenu") as HyperLink;
                if (hplEdit != null)
                {
                    hplEdit.NavigateUrl = String.Format("~/Admin/MenuEdit.aspx?MenuId={0}&NodeId={1}", menu.Id,
                                                        ActiveNode.Id);
                }
            }
        }

        private void rptSections_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete" || e.CommandName == "Detach")
            {
                int sectionId = Int32.Parse(e.CommandArgument.ToString());
                Section section = (Section) CoreRepository.GetObjectById(typeof (Section), sectionId);

                if (e.CommandName == "Delete")
                {
                    section.Node = ActiveNode;
                    try
                    {
                        // First tell the module to remove its content.
                        ModuleBase module = ModuleLoader.GetModuleFromSection(section);
                        module.DeleteModuleContent();
                        // Make sure there is no gap in the section indexes. 
                        // ABUSE: this method was not designed for this, but works fine.
                        section.ChangeAndUpdatePositionsAfterPlaceholderChange(section.PlaceholderId, section.Position,
                                                                               false);
                        // Now delete the Section.
                        ActiveNode.Sections.Remove(section);
                        CoreRepository.DeleteObject(section);
                    }
                    catch (Exception ex)
                    {
                        ShowError(ex.Message);
                        log.Error(String.Format("Error deleting section : {0}.", section.Id), ex);
                    }
                }
                if (e.CommandName == "Detach")
                {
                    try
                    {
                        // Make sure there is no gap in the section indexes. 
                        // ABUSE: this method was not designed for this, but works fine.
                        section.ChangeAndUpdatePositionsAfterPlaceholderChange(section.PlaceholderId, section.Position,
                                                                               false);
                        // Now detach the Section.
                        ActiveNode.Sections.Remove(section);
                        section.Node = null;
                        section.PlaceholderId = null;
                        CoreRepository.UpdateObject(section);
                        // Update search index to make sure the content of detached sections doesn't 
                        // show up in a search.
                        SearchHelper.UpdateIndexFromSection(section);
                    }
                    catch (Exception ex)
                    {
                        ShowError(ex.Message);
                        log.Error(String.Format("Error detaching section : {0}.", section.Id), ex);
                    }
                }
                BindSections();
            }
        }

        private void chkLink_CheckedChanged(object sender, EventArgs e)
        {
            pnlLink.Visible = chkLink.Checked;
            pnlMenus.Visible = ! chkLink.Checked;
            pnlTemplate.Visible = ! chkLink.Checked;
            pnlSections.Visible = ! chkLink.Checked;
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnUp.Click += new System.Web.UI.ImageClickEventHandler(this.btnUp_Click);
            this.btnDown.Click += new System.Web.UI.ImageClickEventHandler(this.btnDown_Click);
            this.btnLeft.Click += new System.Web.UI.ImageClickEventHandler(this.btnLeft_Click);
            this.btnRight.Click += new System.Web.UI.ImageClickEventHandler(this.btnRight_Click);
            this.chkLink.CheckedChanged += new System.EventHandler(this.chkLink_CheckedChanged);
            this.ddlTemplates.SelectedIndexChanged += new System.EventHandler(this.ddlTemplates_SelectedIndexChanged);
            this.rptMenus.ItemDataBound +=
                new System.Web.UI.WebControls.RepeaterItemEventHandler(this.rptMenus_ItemDataBound);
            this.rptSections.ItemDataBound +=
                new System.Web.UI.WebControls.RepeaterItemEventHandler(this.rptSections_ItemDataBound);
            this.rptSections.ItemCommand +=
                new System.Web.UI.WebControls.RepeaterCommandEventHandler(this.rptSections_ItemCommand);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.Load += new System.EventHandler(this.Page_Load);
        }

        #endregion
    }
}