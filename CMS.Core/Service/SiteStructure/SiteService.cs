using System;
using System.Collections;
using CMS.Core.DataAccess;
using CMS.Core.Domain;
using log4net;

namespace CMS.Core.Service.SiteStructure
{
    /// <summary>
    /// Provides functionality to manage site instances.
    /// </summary>
    public class SiteService : ISiteService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof (SiteService));
        private readonly ICommonDao _commonDao;
        private readonly ISiteStructureDao _siteStructureDao;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="siteStructureDao"></param>
        public SiteService(ISiteStructureDao siteStructureDao, ICommonDao commonDao)
        {
            _siteStructureDao = siteStructureDao;
            _commonDao = commonDao;
        }

        #region ISiteService Members

        public Site GetSiteById(int siteId)
        {
            return (Site) _commonDao.GetObjectById(typeof (Site), siteId);
        }

        public Site GetSiteBySiteUrl(string siteUrl)
        {
            return _siteStructureDao.GetSiteBySiteUrl(siteUrl);
        }

        public SiteAlias GetSiteAliasById(int siteAliasId)
        {
            return (SiteAlias) _commonDao.GetObjectById(typeof (SiteAlias), siteAliasId);
        }

        public SiteAlias GetSiteAliasByUrl(string url)
        {
            return _siteStructureDao.GetSiteAliasByUrl(url);
        }

        public IList GetSiteAliasesBySite(Site site)
        {
            return _siteStructureDao.GetSiteAliasesBySite(site);
        }

        public IList GetAllSites()
        {
            return _commonDao.GetAll(typeof (Site));
        }

        public void SaveSite(Site site)
        {
            try
            {
                // We need to use a specific DAO to also enable clearing the query cache.
                _siteStructureDao.SaveSite(site);
            }
            catch (Exception ex)
            {
                log.Error("Error saving site", ex);
                throw;
            }
        }

        public void DeleteSite(Site site)
        {
            if (site.RootNodes.Count > 0)
            {
                throw new Exception(
                    "Can't delete a site when there are still related nodes. Please delete all nodes before deleting an entire site.");
            }
            else
            {
                IList aliases = _siteStructureDao.GetSiteAliasesBySite(site);
                if (aliases.Count > 0)
                {
                    throw new Exception("Unable to delete a site when a site has related aliases.");
                }
                else
                {
                    try
                    {
                        // We need to use a specific DAO to also enable clearing the query cache.
                        _siteStructureDao.DeleteSite(site);
                    }
                    catch (Exception ex)
                    {
                        log.Error("Error deleting site", ex);
                        throw;
                    }
                }
            }
        }

        public void SaveSiteAlias(SiteAlias siteAlias)
        {
            try
            {
                // We need to use a specific DAO to also enable clearing the query cache.
                _siteStructureDao.SaveSiteAlias(siteAlias);
            }
            catch (Exception ex)
            {
                log.Error("Error saving site alias", ex);
                throw;
            }
        }

        public void DeleteSiteAlias(SiteAlias siteAlias)
        {
            try
            {
                // We need to use a specific DAO to also enable clearing the query cache.
                _siteStructureDao.DeleteSiteAlias(siteAlias);
            }
            catch (Exception ex)
            {
                log.Error("Error deleting site alias", ex);
                throw;
            }
        }

        #endregion
    }
}