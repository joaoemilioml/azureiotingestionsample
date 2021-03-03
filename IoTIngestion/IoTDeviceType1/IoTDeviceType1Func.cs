using System;
using System.Collections.Generic;
using Domain;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace IoTDeviceType1
{
    public static class IoTDeviceType1Func
    {
        [FunctionName("device-type-1-func")]
        [return: ServiceBus("datasink-q", Connection = "ServiceBusConnectionString")]
        public static OutboundMessage Run([ServiceBusTrigger("device-type-1-q", Connection = "ServiceBusConnectionString")]string message, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {message}");
            return new DeviceType1Translator().Translate(message);
        }
    }
}
