using CMS.Core.Domain;

namespace CMS.Web.UI
{
    public interface IModuleControl
    {
        ModuleBase Module { set;}
    }

    public interface ISelectorControl
    {
        object Selected { get; set;}
    }

    public interface IDataBindable
    {
        object Datasource { set;}
    }
}
