namespace Ordering.Api.Models
{
    public class PurchaseOrder : Linkable
    {
        public PurchaseOrder()
        {
        }

        public PurchaseOrder(int id, string productCode)
        {
            Id = id;
            ProductCode = productCode;
        }

        public int Id { get; set; }
        public string ProductCode { get; set; }
    }
}