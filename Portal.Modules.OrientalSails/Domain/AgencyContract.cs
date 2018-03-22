using System;

namespace Portal.Modules.OrientalSails.Domain
{
    public class AgencyContract
    {
        public virtual int Id { get; set; }
        public virtual string ContractName { get; set; }
        public virtual byte[] ContractFile { get; set; }
        public virtual DateTime ExpiredDate { get; set; }
        public virtual Agency Agency { get; set; }
        public virtual string FileName { get; set; }
        public virtual string FilePath { get; set; }
        public virtual DateTime? CreateDate { set; get; }
        public virtual Boolean Received { set; get; }
    }
}
