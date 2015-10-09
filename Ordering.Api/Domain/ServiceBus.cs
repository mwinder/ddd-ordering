using System;
using System.Collections.Generic;
using System.Linq;
using NServiceBus;

namespace Ordering.Api.Domain
{
    public interface ServiceBus
    {
        void Publish<T>(T message);
        void Subscribe<T>(IObserver<T> observer);
    }

    public class SimpleServiceBus : ServiceBus
    {
        public readonly static ServiceBus Instance = new SimpleServiceBus();

        private readonly Queue<object> store = new Queue<object>();
        private readonly List<object> subscribers = new List<object>();

        public void Subscribe<T>(IObserver<T> observer)
        {
            subscribers.Add(observer);
        }

        public void Publish<T>(T message)
        {
            store.Enqueue(message);

            foreach (var subscriber in subscribers.OfType<IObserver<T>>())
            {
                subscriber.OnNext(message);
            }
        }
    }

    public class NServiceBusServiceBus : ServiceBus
    {
        public static ServiceBus Instance;

        private readonly IBus _bus;

        public NServiceBusServiceBus(IBus bus)
        {
            _bus = bus;
        }

        public void Publish<T>(T message)
        {
            _bus.Publish(message);
        }

        public void Subscribe<T>(IObserver<T> observer)
        {
        }
    }
}