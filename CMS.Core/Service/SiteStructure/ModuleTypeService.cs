using System.Collections;
using Castle.Services.Transaction;
using CMS.Core.DataAccess;
using CMS.Core.Domain;

namespace CMS.Core.Service.SiteStructure
{
    [Transactional]
    public class ModuleTypeService : IModuleTypeService
    {
        private readonly ICommonDao _commonDao;
        private readonly ISiteStructureDao _siteStructureDao;

        public ModuleTypeService(ISiteStructureDao siteStructureDao, ICommonDao commonDao)
        {
            _siteStructureDao = siteStructureDao;
            _commonDao = commonDao;
        }

        #region IModuleTypeService Members

        public IList GetAllModuleTypesInUse()
        {
            return _siteStructureDao.GetAllModuleTypesInUse();
        }

        public IList GetAllModuleTypes()
        {
            return _commonDao.GetAll(typeof (ModuleType));
        }

        public ModuleType GetModuleById(int moduleTypeId)
        {
            return _commonDao.GetObjectById(typeof (ModuleType), moduleTypeId) as ModuleType;
        }

        [Transaction(TransactionMode.RequiresNew)]
        public void SaveModuleType(ModuleType moduleType)
        {
            _commonDao.SaveObject(moduleType);
        }

        [Transaction(TransactionMode.RequiresNew)]
        public void SaveOrUpdateModuleType(ModuleType moduleType)
        {
            _commonDao.SaveOrUpdateObject(moduleType);
        }

        [Transaction(TransactionMode.RequiresNew)]
        public void DeleteModuleType(ModuleType moduleType)
        {
            _commonDao.DeleteObject(moduleType);
        }

        #endregion
    }
}