using System.Collections;
using Castle.Services.Transaction;
using CMS.Core.DataAccess;
using CMS.Core.Domain;

namespace CMS.Core.Service.SiteStructure
{
    /// <summary>
    /// Provides functionality for section management.
    /// </summary>
    [Transactional]
    public class SectionService : ISectionService
    {
        private readonly ICommonDao _commonDao;
        private readonly ISiteStructureDao _siteStructureDao;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="siteStructureDao"></param>
        /// <param name="commonDao"></param>
        public SectionService(ISiteStructureDao siteStructureDao, ICommonDao commonDao)
        {
            _siteStructureDao = siteStructureDao;
            _commonDao = commonDao;
        }

        #region ISectionService Members

        public Section GetSectionById(int sectionId)
        {
            return (Section) _commonDao.GetObjectById(typeof (Section), sectionId);
        }

        public IList GetSortedSectionsByNode(Node node)
        {
            return _siteStructureDao.GetSortedSectionsByNode(node);
        }

        public IList GetUnconnectedSections()
        {
            return _siteStructureDao.GetUnconnectedSections();
        }

        public IList GetTemplatesBySection(Section section)
        {
            return _siteStructureDao.GetTemplatesBySection(section);
        }

        public IList GetSectionsByModuleTypes(IList moduleTypes)
        {
            return _siteStructureDao.GetSectionsByModuleTypes(moduleTypes);
        }

        [Transaction(TransactionMode.RequiresNew)]
        public void SaveSection(Section section)
        {
            _commonDao.SaveOrUpdateObject(section);
        }

        [Transaction(TransactionMode.RequiresNew)]
        public void UpdateSection(Section section)
        {
            _commonDao.SaveOrUpdateObject(section);
        }

        [Transaction(TransactionMode.RequiresNew)]
        public void DeleteSection(Section section)
        {
            _commonDao.SaveOrUpdateObject(section);
        }

        #endregion
    }
}