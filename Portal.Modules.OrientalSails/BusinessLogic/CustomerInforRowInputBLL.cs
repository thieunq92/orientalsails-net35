using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.BusinessLogic
{
    public class CustomerInforRowInputBLL
    {
        public NationalityRepository NationalityRepository { get; set; }

        public CustomerInforRowInputBLL()
        {
            NationalityRepository = new NationalityRepository();
        }

        public void Dispose()
        {
            if (NationalityRepository != null)
            {
                NationalityRepository.Dispose();
                NationalityRepository = null;
            }
        }

        public IList<Nationality> NationalityGetAll()
        {
            return NationalityRepository.NationalityGetAll();
        }
    }
}