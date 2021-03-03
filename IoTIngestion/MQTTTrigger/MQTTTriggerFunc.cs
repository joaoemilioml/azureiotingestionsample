using CaseOnline.Azure.WebJobs.Extensions.Mqtt;
using CaseOnline.Azure.WebJobs.Extensions.Mqtt.Messaging;
using Domain.Providers;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;


namespace MQTTTrigger
{
    public static class MQTTTriggerFunc
    {
        private static IoTHubClient IoTHub;
        //MqttTrigger(typeof(CustomMosquittoConfigProvider)
        [FunctionName("MQTTTriggerFunction")] 
        public static async Task Run([MqttTrigger("minicursoiot/+/#", ConnectionString = "MqttConnectionString")] IMqttMessage data, ILogger log, ExecutionContext context)
        {
            log.LogInformation($"Payload {Convert.ToBase64String(data.GetMessage())}");
            var config = new ConfigurationBuilder()
                        .SetBasePath(context.FunctionAppDirectory)
                        .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables()
                        .Build();
            if (IoTHub == null)
            {
                IoTHub = new IoTHubClient(config["IoTHubConnectionString"]);
            }
            await IoTHub.SendRaw(data.Topic, data.GetMessage());
        }
    }
}
