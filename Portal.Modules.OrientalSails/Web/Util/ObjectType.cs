namespace Portal.Modules.OrientalSails.Web.Util
{
    public enum CustomerType
    {
        Baby,
        Children,
        Adult,
        PlaceHolder
    }

    public enum StatusType
    {
        Pending = 0,
        Approved = 1,
        //Rejected = 2,
        Cancelled = 3,
        CutOff = 4,
    }

    public enum TripOption
    {
        Option1,
        Option2,
        Option3
    }

    public enum BookingType
    {
        Single,
        Shared,
        Double
    }
}