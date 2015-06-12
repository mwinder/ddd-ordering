using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ordering.Api.Models;
using Ordering.Api.Helpers;

namespace Ordering.Api.Controllers
{
    public class PurchaseOrderController : ApiController
    {
        public PurchaseOrder Get(int id)
        {
            var purchaseOrder = new PurchaseOrder(id);
            purchaseOrder.Links.Add(new Link("approve", Url.Link(new { action = "Approve" })));
            purchaseOrder.Links.Add(new Link("decline", Url.Link(new { action = "Decline" })));
            return purchaseOrder;
        }

        public IEnumerable<PurchaseOrder> Get()
        {
            for (int id = 1; id <= 20; id++)
            {
                yield return new PurchaseOrder(id);
            }
        }

        public HttpResponseMessage Post(PurchaseOrder purchaseOrder)
        {
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        public HttpResponseMessage Approve(int id)
        {
            return Request.CreateResponse(HttpStatusCode.Accepted);
        }

        public HttpResponseMessage Decline(int id)
        {
            return Request.CreateResponse(HttpStatusCode.Accepted);
        }
    }
}