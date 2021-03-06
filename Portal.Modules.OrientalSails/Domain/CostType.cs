namespace Portal.Modules.OrientalSails.Domain
{
    public class CostType
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual bool IsDailyInput { get; set; }
        public virtual bool IsCustomType { get; set; }
        public virtual bool IsSupplier { get; set; }
        public virtual Agency DefaultAgency { get; set; }
        public virtual ExtraOption Service { get; set; }
        public virtual bool IsMonthly { get; set; }
        public virtual bool IsYearly { get; set; }
        public virtual bool IsDaily { get; set; }
        public virtual string GroupName { get; set; }
    }

    public enum CostBase
    {
        Both,
        Customer,
        Booking
    }
}
