using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventHubs;
using System.Text;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Domain;
using System.Collections.Generic;

namespace IoTDeviceType2
{
    public static class IotDeviceType2Func
    {

        [FunctionName("iot-device-type-2-func")]
        [return: ServiceBus("datasink-q", Connection = "ServiceBusConnectionString")]
        public static OutboundMessage Run([IoTHubTrigger("messages/events", Connection = "IoTHubConnection")]EventData message, ILogger log)
        {
            var messageStr = Encoding.UTF8.GetString(message.Body.Array);
            log.LogInformation($"C# IoT Hub trigger function processed a message: {messageStr}");

            return new DeviceType2Translator().Translate(messageStr);
        }
    }
}