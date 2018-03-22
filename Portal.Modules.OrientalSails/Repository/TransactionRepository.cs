using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository
{
    public class TransactionRepository : RepositoryBase<Transaction>
    {
        public TransactionRepository() : base() { }

        public TransactionRepository(ISession session) : base() { }
    }
}