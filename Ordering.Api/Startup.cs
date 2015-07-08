using System;
using System.Web.Http;
using Ordering.Api.Domain;
using Owin;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using Swashbuckle.Application;

namespace Ordering.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var loggingConfiguration = new LoggerConfiguration()
                .Enrich.WithMachineName()
                .Enrich.WithThreadId();

            loggingConfiguration.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200/"))
            {
                IndexFormat = "ordering-{0:yyyy-MM-dd}"
            });

            Log.Logger = loggingConfiguration.CreateLogger();

            SimpleServiceBus.Instance.Subscribe(new EventLog());

            var configuration = new HttpConfiguration();
            configuration
                .RegisterApi();
            configuration
                .EnableSwagger(c => c.SingleApiVersion("v1", "Ordering API"))
                .EnableSwaggerUi();

            app.UseWebApi(configuration);
        }

        private class EventLog : IObserver<Event>
        {
            private static readonly ILogger Logger = Log.ForContext<EventLog>();

            public void OnNext(Event value)
            {
                Logger.Information("{event:l}", value);
            }

            public void OnError(Exception error)
            {
            }

            public void OnCompleted()
            {
            }
        }
    }
}