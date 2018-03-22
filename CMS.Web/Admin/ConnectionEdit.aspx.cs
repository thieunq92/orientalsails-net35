using System;
using System.Collections;
using System.Web.UI.WebControls;
using CMS.Core.Communication;
using CMS.Core.Domain;
using CMS.Web.Admin.UI;
using CMS.Web.Util;

namespace CMS.Web.Admin
{
    /// <summary>
    /// Summary description for ConnectionEdit.
    /// </summary>
    public class ConnectionEdit : AdminBasePage
    {
        private IActionProvider _activeActionProvider;
        private Section _activeSection;
        protected Button btnBack;
        protected Button btnSave;

        protected DropDownList ddlAction;
        protected DropDownList ddlSectionTo;
        protected Label lblModuleType;
        protected Label lblSectionFrom;
        protected Panel pnlTo;

        private void Page_Load(object sender, EventArgs e)
        {
            if (Context.Request.QueryString["SectionId"] != null)
            {
                Title = "Add connection";

                // Get section data
                _activeSection = (Section) base.CoreRepository.GetObjectById(typeof (Section),
                                                                             Int32.Parse(
                                                                                 Context.Request.QueryString["SectionId"
                                                                                     ]));

                ModuleBase moduleInstance = base.ModuleLoader.GetModuleFromSection(_activeSection);
                if (moduleInstance is IActionProvider)
                {
                    _activeActionProvider = moduleInstance as IActionProvider;

                    if (! IsPostBack)
                    {
                        BindSection();
                        BindCompatibleSections();
                    }
                }
                else
                {
                    ShowError("The module that is connected to the section doesn't support outgoing connections.");
                }
            }
        }

        private void BindSection()
        {
            lblSectionFrom.Text = _activeSection.FullName;
            lblModuleType.Text = _activeSection.ModuleType.Name;
            ActionCollection outboundActions = _activeActionProvider.GetOutboundActions();
            foreach (CMS.Core.Communication.Action action in outboundActions)
            {
                // Only add actions that are not assigned yet.
                if (_activeSection.Connections[action.Name] == null)
                {
                    ddlAction.Items.Add(action.Name);
                }
            }
        }

        private void BindCompatibleSections()
        {
            string selectedAction = String.Empty;

            if (ddlAction.SelectedIndex == -1 && ddlAction.Items.Count > 0)
            {
                selectedAction = ddlAction.Items[0].Value;
            }
            else
            {
                selectedAction = ddlAction.SelectedValue;
            }

            ArrayList compatibleModuleTypes = new ArrayList();
            // Get all ModuleTypes.
            IList moduleTypes = CoreRepository.GetAll(typeof (ModuleType));
            foreach (ModuleType mt in moduleTypes)
            {
                string assemblyQualifiedName = mt.ClassName + ", " + mt.AssemblyName;
                Type moduleTypeType = Type.GetType(assemblyQualifiedName);

                if (moduleTypeType != null) // throw exception when moduleTypeType == null?
                {
                    ModuleBase moduleInstance = base.ModuleLoader.GetModuleFromType(mt);
                    if (moduleInstance is IActionConsumer)
                    {
                        IActionConsumer actionConsumer = moduleInstance as IActionConsumer;
                        CMS.Core.Communication.Action currentAction = _activeActionProvider.GetOutboundActions().FindByName(selectedAction);
                        if (actionConsumer.GetInboundActions().Contains(currentAction))
                        {
                            compatibleModuleTypes.Add(mt);
                        }
                    }
                }
            }

            if (compatibleModuleTypes.Count > 0)
            {
                // Retrieve all sections that have the compatible ModuleTypes
                IList compatibleSections = CoreRepository.GetSectionsByModuleTypes(compatibleModuleTypes);
                if (compatibleSections.Count > 0)
                {
                    pnlTo.Visible = true;
                    btnSave.Enabled = true;
                    ddlSectionTo.DataSource = compatibleSections;
                    ddlSectionTo.DataValueField = "Id";
                    ddlSectionTo.DataTextField = "FullName";
                    ddlSectionTo.DataBind();
                }
                else
                {
                    pnlTo.Visible = false;
                    btnSave.Enabled = false;
                }
            }
        }

        private void RedirectToSectionEdit()
        {
            if (ActiveNode != null)
            {
                Context.Response.Redirect(
                    String.Format("SectionEdit.aspx?NodeId={0}&SectionId={1}", ActiveNode.Id, _activeSection.Id));
            }
            else
            {
                Context.Response.Redirect(String.Format("SectionEdit.aspx?SectionId={0}", _activeSection.Id));
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            RedirectToSectionEdit();
        }

        private void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCompatibleSections();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _activeSection.Connections[ddlAction.SelectedValue]
                = base.CoreRepository.GetObjectById(typeof (Section)
                                                    , Int32.Parse(ddlSectionTo.SelectedValue));
            try
            {
                base.CoreRepository.UpdateObject(_activeSection);

                RedirectToSectionEdit();
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
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
            this.ddlAction.SelectedIndexChanged += new System.EventHandler(this.ddlAction_SelectedIndexChanged);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            this.Load += new System.EventHandler(this.Page_Load);
        }

        #endregion
    }
}