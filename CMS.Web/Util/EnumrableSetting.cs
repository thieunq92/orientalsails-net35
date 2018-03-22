using System.Web.UI.WebControls;
using CMS.Core.Domain;

namespace CMS.Web.Util
{
    public abstract class EnumrableSetting
    {
        protected ModuleBase _module;

        public ModuleBase Module
        {
            get { return _module; }
            set { _module = value; }
        }
        
        public abstract DropDownList BindDataToDropDownList(string ID, string value);

        public abstract bool ValidateSetting(string setting);
    }
}
