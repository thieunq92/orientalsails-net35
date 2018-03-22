using System.Collections;
using CMS.Core.DataAccess;
using CMS.Core.Domain;

namespace CMS.Core.Service.SiteStructure
{
    /// <summary>
    /// Default implementation of ITemplateService.
    /// </summary>
    public class TemplateService : ITemplateService
    {
        private readonly ICommonDao _commonDao;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="commonDao"></param>
        public TemplateService(ICommonDao commonDao)
        {
            _commonDao = commonDao;
        }

        #region ITemplateService Members

        public IList GetAllTemplates()
        {
            return _commonDao.GetAll(typeof (Template), "Name");
        }

        public Template GetTemplateById(int templateId)
        {
            return (Template) _commonDao.GetObjectById(typeof (Template), templateId);
        }

        #endregion
    }
}