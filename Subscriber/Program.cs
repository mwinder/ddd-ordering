using System;
using System.Reflection;
using NServiceBus;
using Ordering.Api.Domain;

namespace Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            var busConfiguration = new BusConfiguration();
            busConfiguration.DiscardFailedMessagesInsteadOfSendingToErrorQueue();
            busConfiguration.UseTransport<AzureServiceBusTransport>();
            busConfiguration.UsePersistence<AzureStoragePersistence>();
            busConfiguration.EndpointName("ordering");
            busConfiguration.Conventions()
                .DefiningEventsAs(a => a.IsSubclassOf(typeof(Event)));

            busConfiguration.AssembliesToScan(
                Assembly.GetExecutingAssembly(),
                Assembly.LoadFrom("Ordering.Api.dll"),
                Assembly.LoadFrom("NServiceBus.Azure.Transports.WindowsAzureServiceBus.dll"));

            var bus = Bus.Create(busConfiguration).Start();

            bus.Subscribe<PurchaseOrderSubmitted>();

            Console.ReadLine();
        }
    }

    public class PurchaseOrderEvents : IHandleMessages<PurchaseOrderSubmitted>, IHandleMessages<PurchaseOrderApproved>
    {
        public void Handle(PurchaseOrderSubmitted message)
        {
            Console.WriteLine(message);
        }

        public void Handle(PurchaseOrderApproved message)
        {
            Console.WriteLine(message);
        }
    }
}
