namespace Ordering.Api.Models
{
    public class PurchaseOrder : Linkable
    {
        public PurchaseOrder()
        {
        }

        public PurchaseOrder(int id, string productCode, int quantity)
        {
            Id = id;
            ProductCode = productCode;
            Quantity = quantity;
        }

        public int Id { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
    }
}