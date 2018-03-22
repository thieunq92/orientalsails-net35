using System.Collections.Specialized;
using System.Configuration;

namespace CMS.Core.Util
{
    /// <summary>
    /// Summary description for Config.
    /// </summary>
    public class Config
    {
        private Config()
        {
        }

        public static NameValueCollection GetConfiguration()
        {
            return (NameValueCollection) ConfigurationSettings.GetConfig("BitPortalSettings");
        }
    }

    public class CuyahogaSectionHandler : NameValueSectionHandler
    {
        protected override string KeyAttributeName
        {
            get { return "setting"; }
        }

        protected override string ValueAttributeName
        {
            get { return base.ValueAttributeName; }
        }
    }
}