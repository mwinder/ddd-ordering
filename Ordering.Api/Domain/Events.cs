namespace Ordering.Api.Domain
{
    public class Event { }

    public class PurchaseOrderSubmitted : Event
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
    }

    public class PurchaseOrderApproved : Event
    {
        public int Id { get; set; }
    }

    public class PurchaseOrderDeclined : Event
    {
        public int Id { get; set; }
        public string Reason { get; set; }
    }
}