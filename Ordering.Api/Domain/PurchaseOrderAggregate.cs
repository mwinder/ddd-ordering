using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.Api.Domain
{
    public class PurchaseOrderAggregate : IEnumerable<Event>
    {
        private readonly PurchaseOrderState state;
        private readonly List<Event> uncommittedEvents = new List<Event>();

        public PurchaseOrderAggregate(PurchaseOrderState state)
        {
            this.state = state;
        }

        public void Submit(string productCode, int quantity)
        {
            Apply(new PurchaseOrderSubmitted { Id = state.Id, ProductCode = productCode, Quantity = quantity });
        }

        public void Approve()
        {
            Apply(new PurchaseOrderApproved { Id = state.Id });
        }

        public void Decline(string reason)
        {
            if (string.IsNullOrEmpty(reason))
                throw new InvalidOperationException("Reason is required");

            Apply(new PurchaseOrderDeclined { Id = state.Id, Reason = reason });
        }

        private void Apply(PurchaseOrderSubmitted purchaseOrderSubmitted)
        {
            state.Status = PurchaseOrderStatus.Submitted;
            state.ProductCode = purchaseOrderSubmitted.ProductCode;
            state.Quantity = purchaseOrderSubmitted.Quantity;

            Publish(purchaseOrderSubmitted);
        }

        private void Apply(PurchaseOrderApproved purchaseOrderApproved)
        {
            state.Status = PurchaseOrderStatus.Approved;

            Publish(purchaseOrderApproved);
        }

        private void Apply(PurchaseOrderDeclined purchaseOrderDeclined)
        {
            state.Status = PurchaseOrderStatus.Declined;

            Publish(purchaseOrderDeclined);
        }

        private void Publish(Event e)
        {
            uncommittedEvents.Add(e);
        }

        public IEnumerator<Event> GetEnumerator()
        {
            return uncommittedEvents.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public PurchaseOrderState GetState()
        {
            return state;
        }
    }

    public class PurchaseOrderId : Identity<int>
    {
        private PurchaseOrderId(int id) : base(id) { }

        public static implicit operator PurchaseOrderId(int identity)
        {
            return new PurchaseOrderId(identity);
        }
    }

    public class PurchaseOrderState
    {
        public PurchaseOrderId Id { get; set; }
        public PurchaseOrderStatus Status { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
    }

    public enum PurchaseOrderStatus
    {
        Submitted,
        Approved,
        Declined
    }

    public interface IPurchaseOrderRepository
    {
        PurchaseOrderAggregate GetById(int id);
        IEnumerable<PurchaseOrderAggregate> GetAll();
        PurchaseOrderAggregate Create(int id);
        void Save(PurchaseOrderAggregate purchaseOrder);
    }

    public class SimplePurchaseOrderRepository : IPurchaseOrderRepository
    {
        public static readonly IPurchaseOrderRepository Instance = new SimplePurchaseOrderRepository(SimpleServiceBus.Instance);

        static SimplePurchaseOrderRepository()
        {
            var quantity = new Random();
            for (int id = 1; id <= 20; id++)
            {
                var purchaseOrder = new PurchaseOrderAggregate(new PurchaseOrderState { Id = id });
                purchaseOrder.Submit(string.Format("Product-{0:00}", id), quantity.Next(10, 100));

                Instance.Save(purchaseOrder);
            }
        }

        private static readonly List<PurchaseOrderState> Store = new List<PurchaseOrderState>();
        private readonly ServiceBus _bus;

        private SimplePurchaseOrderRepository(ServiceBus bus)
        {
            _bus = bus;
        }

        public PurchaseOrderAggregate GetById(int id)
        {
            var purchaseOrderState = Store.Find(a => a.Id == id);
            return new PurchaseOrderAggregate(purchaseOrderState);
        }

        public IEnumerable<PurchaseOrderAggregate> GetAll()
        {
            return Store.Select(a => new PurchaseOrderAggregate(a));
        }

        public PurchaseOrderAggregate Create(int id)
        {
            return new PurchaseOrderAggregate(new PurchaseOrderState { Id = id });
        }

        public void Save(PurchaseOrderAggregate purchaseOrder)
        {
            var purchaseOrderState = purchaseOrder.GetState();
            if (!Store.Exists(a => a.Id == purchaseOrderState.Id))
            {
                Store.Add(purchaseOrderState);
            }

            foreach (var @event in purchaseOrder)
            {
                _bus.Publish(@event);
            }
        }
    }
}