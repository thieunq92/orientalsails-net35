using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Domain
{
    public class CheckingTransaction
    {
        public virtual int CheckingTransactionId { get; set; }
        public virtual string MerchTxnRef { get; set; }
        public virtual bool Processed { get; set; }
    }
}