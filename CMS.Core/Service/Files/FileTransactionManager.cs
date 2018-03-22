using Castle.Core;
using Castle.Services.Transaction;

namespace CMS.Core.Service.Files
{
    /// <summary>
    /// ITransactionManager implementation.
    /// </summary>
    [PerThread]
    public class FileTransactionManager : DefaultTransactionManager
    {
    }
}