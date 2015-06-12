namespace Ordering.Api.Models
{
    public class PurchaseOrder : Linkable
    {
        public PurchaseOrder()
        {
            
        }

        public PurchaseOrder(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}