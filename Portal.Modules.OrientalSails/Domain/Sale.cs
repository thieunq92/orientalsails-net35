using System;
using System.Collections;
using CMS.Core.Domain;

namespace Portal.Modules.OrientalSails.Domain
{
    public class Sale
    {
        private IList users;
        protected int _id;

        public virtual int Id { get; set; }
        public virtual User User { get; set; }

        public virtual IList Users
        {
            get
            {
                if (users == null)
                {
                    users = new ArrayList();
                }
                return users;
            }
            set { users = value; }
        }
    }
}