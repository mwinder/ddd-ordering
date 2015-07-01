using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ordering.Api.Helpers;
using Ordering.Api.Models;

namespace Ordering.Api.Controllers
{
    [RoutePrefix("api/PurchaseOrder")]
    public class PurchaseOrderController : ApiController
    {
        public PurchaseOrder Get(int id)
        {
            return CreatePurchaseOrder(id);
        }

        public IEnumerable<PurchaseOrder> Get()
        {
            for (int id = 1; id <= 20; id++)
            {
                yield return CreatePurchaseOrder(id);
            }
        }

        private PurchaseOrder CreatePurchaseOrder(int id)
        {
            var purchaseOrder = new PurchaseOrder(id, string.Format("Product-{0:00}", id));

            purchaseOrder.Links.Add("approve", new Link(Url.LinkUri(new {id}, routeName: "Approve")));
            purchaseOrder.Links.Add("decline", new Link(Url.LinkUri(new {id}, routeName: "Decline")));

            return purchaseOrder;
        }

        public HttpResponseMessage Post(PurchaseOrder purchaseOrder)
        {
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        [Route("{id}/Approve", Name = "Approve")]
        public HttpResponseMessage Approve(int id)
        {
            return Request.CreateResponse(HttpStatusCode.Accepted);
        }

        [Route("{id}/Decline", Name = "Decline")]
        public HttpResponseMessage Decline(int id)
        {
            return Request.CreateResponse(HttpStatusCode.Accepted);
        }
    }
}