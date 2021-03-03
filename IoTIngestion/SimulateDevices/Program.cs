using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimulateDevices
{
    class Program
    {
        private static IConfigurationRoot _config;

        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
            .AddJsonFile($"appsettings.json", true, true)
            .AddEnvironmentVariables();

            _config = builder.Build();

            Console.WriteLine("Starting execution");
            var tasks = new List<Task>
            {
                SendDeviceToCloudMessagesAsync(),
                new MqttClient().Connect(_config["MqttBroker"], _config["MqttUser"], _config["MqttPassword"])
            };

            await Task.WhenAll(tasks);
            Console.WriteLine("Finished execution. Press Any Key to exit");
            Console.ReadLine();
        }

        private async static Task SendDeviceToCloudMessagesAsync()
        {
            string ConnectionString = _config["IoTHubConnectionString"];
            var client = DeviceClient.CreateFromConnectionString(ConnectionString);
            var messages = new DeviceType1RandomGenerator().Generate();
            foreach (var message in messages)
            {
                Console.WriteLine($"Sending Message through SDK");
                var iotMessage = new Message(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(message)));
                await client.SendEventAsync(iotMessage);
                Thread.Sleep(1000);

            }
        }

    }
}
