using System;
using System.Collections.Generic;
using System.Web;
using CMS.Core.Domain;

namespace Portal.Modules.OrientalSails.Domain
{
    public class Transaction
    {
        public const int BOOKING = 0;
        public const int COMMISSION = 1;
        public const int GUIDECOLLECT = 2;
        public const int CALCULATED_EXPENSE = 3;
        public const int MANUALDAILY_EXPENSE = 4;
        private string agencyName;

        public virtual int Id { get; set; }
        public virtual Booking Booking { get; set; }
        public virtual int TransactionType { get; set; }
        public virtual double USDAmount { get; set; }
        public virtual double VNDAmount { get; set; }
        public virtual Agency Agency { get; set; }
        public virtual string AgencyName
        {
            get
            {
                if (Agency != null)
                {
                    return Agency.Name;
                }
                return agencyName;
            }
            set { agencyName = value; }
        }

        public virtual string Note { get; set; }

        public virtual bool IsExpense { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual User CreatedBy { get; set; }

    }
}