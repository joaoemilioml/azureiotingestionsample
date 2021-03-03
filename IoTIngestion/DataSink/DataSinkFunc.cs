using System;
using Domain;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DataSink
{
    public static class DataSinkFunc
    {
        [FunctionName("datasink-func")]
        public static void Run([ServiceBusTrigger("datasink-q", Connection = "ServiceBusConnectionString")]string message, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {message}");
            var receivedMessage = JsonConvert.DeserializeObject<OutboundMessage>(message);
            var pruuLogger = new PruuLog();
            //Change this to something unique to you
            var pruuLogKey = "minicursoiot";
            foreach (var metric in receivedMessage.Metrics)
            {
                pruuLogger.LogInfo(pruuLogKey, $"{metric.DeviceId},{metric.Timestamp}, {metric.MetricName}, {metric.MetricValue}");
            }
        }
    }
}
