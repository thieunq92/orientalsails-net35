using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository
{
    public class RepositoryBase<TEntity> : IRepository<TEntity>, IDisposable
    {
        protected ISession _session = null;
        protected ITransaction _transaction = null;

        public RepositoryBase()
        {
            _session = Database.OpenSession();
        }

        public RepositoryBase(ISession session)
        {
            _session = session;
        }

        public void BeginTransaction()
        {
            _transaction = _session.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _transaction.Commit();
            CloseTransaction();
        }

        public void CloseTransaction()
        {
            _transaction.Dispose();
            _transaction = null;
        }

        public void RollbackTransaction()
        {
            _transaction.Rollback();
            CloseTransaction();
            CloseSession();
        }

        public void CloseSession()
        {
            _session.Close();
            _session.Dispose();
            _session = null;
        }

        public virtual void SaveOrUpdate(TEntity obj)
        {
            _session.SaveOrUpdate(obj);
        }

        public virtual void Delete(TEntity obj)
        {
            _session.Delete(obj);
        }

        public virtual TEntity GetById(object objId)
        {
            return _session.Load<TEntity>(objId);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return (from entity in _session.Query<TEntity>() select entity);
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                CommitTransaction();
            }

            if (_session != null)
            {
                _session.Flush();
                CloseSession();
            }
        }
    }
}