namespace Ordering.Api.Domain
{
    public class Event { }

    public class PurchaseOrderSubmitted : Event
    {
        public PurchaseOrderId Id { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
    }

    public class PurchaseOrderApproved : Event
    {
        public PurchaseOrderId Id { get; set; }
    }

    public class PurchaseOrderDeclined : Event
    {
        public PurchaseOrderId Id { get; set; }
        public string Reason { get; set; }
    }
}