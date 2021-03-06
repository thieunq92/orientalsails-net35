using System;
using CMS.Core.Domain;

namespace Portal.Modules.OrientalSails.Domain
{
    public class VoucherBatch
    {
        public virtual int Id { get; set; }
        public virtual bool Deleted { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
        public virtual User ModifiedBy { get; set; }
        public virtual DateTime? IssueDate { get; set; }
        public virtual int Quantity { get; set; }
        public virtual int NumberOfPerson { get; set; }
        public virtual SailsTrip Trip { get; set; }
        public virtual Cruise Cruise { get; set; }
        public virtual Agency Agency { get; set; }
        public virtual double Value { get; set; }
        public virtual string ContractFile { get; set; }
        public virtual DateTime ValidUntil { get; set; }
        public virtual bool Issued { get; set; }
        public virtual string Template { get; set; }
        public virtual string Name { get; set; }
        public virtual string Note { get; set; }
    }
}
