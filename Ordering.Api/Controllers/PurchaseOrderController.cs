using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using Ordering.Api.Domain;
using Ordering.Api.Models;

namespace Ordering.Api.Controllers
{
    [RoutePrefix("PurchaseOrder")]
    public class PurchaseOrderController : ApiController
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;

        public PurchaseOrderController()
            : this(SimplePurchaseOrderRepository.Instance) { }

        public PurchaseOrderController(IPurchaseOrderRepository purchaseOrderRepository)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
        }

        public IEnumerable<PurchaseOrder> Get()
        {
            return _purchaseOrderRepository.GetAll().Select(a => Map(a.GetState(), Url));
        }

        public PurchaseOrder Get(int id)
        {
            var aggregate = _purchaseOrderRepository.GetById(id);
            return Map(aggregate.GetState(), Url);
        }

        private static PurchaseOrder Map(PurchaseOrderState state, UrlHelper url)
        {
            var purchaseOrder = new PurchaseOrder(state.Id, state.ProductCode, state.Quantity);
            purchaseOrder.Link("approve", url.Link("Approve", new { purchaseOrder.Id }));
            purchaseOrder.Link("decline", url.Link("Decline", new { purchaseOrder.Id }));

            return purchaseOrder;
        }

        public HttpResponseMessage Post(PurchaseOrder purchaseOrder)
        {
            var aggregate = _purchaseOrderRepository.Create(purchaseOrder.Id);
            aggregate.Submit(purchaseOrder.ProductCode, purchaseOrder.Quantity);
            _purchaseOrderRepository.Save(aggregate);

            return Request.CreateResponse(HttpStatusCode.Created);
        }

        [Route("{id}/Approve", Name = "Approve")]
        public HttpResponseMessage Approve(int id)
        {
            var aggregate = _purchaseOrderRepository.GetById(id);
            aggregate.Approve();
            _purchaseOrderRepository.Save(aggregate);

            return Request.CreateResponse(HttpStatusCode.Accepted);
        }

        [Route("{id}/Decline", Name = "Decline")]
        public HttpResponseMessage Decline(int id, string reason)
        {
            var aggregate = _purchaseOrderRepository.GetById(id);
            aggregate.Decline(reason);
            _purchaseOrderRepository.Save(aggregate);

            return Request.CreateResponse(HttpStatusCode.Accepted);
        }
    }
}