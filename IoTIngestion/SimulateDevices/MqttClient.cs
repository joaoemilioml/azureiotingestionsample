using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace SimulateDevices
{
    class MqttClient
    {
        private const string TopicName = "minicursoiot/topic";

        public async Task Connect(string server, string user, string password)
        {

            var factory = new MqttFactory();
            //List<X509Certificate> certs = new List<X509Certificate>
            //    {
            //        new X509Certificate2("mosq-ca.crt")
            //    };
            var options = new MqttClientOptionsBuilder()
            .WithClientId("IOT_TEST")
            .WithTcpServer(server, 1883)            
            .WithCredentials(user, password)
            //.WithTls((s)=> {
            //    s.Certificates = certs;
            //    s.UseTls = true;                
            //    s.AllowUntrustedCertificates = true;
            //})
            .WithCleanSession()
            .Build();


            var mqttClient = factory.CreateMqttClient();
            
            
            mqttClient.UseDisconnectedHandler(async e =>
            {
                Console.WriteLine("### DISCONNECTED FROM SERVER ###");

                try
                {
                    await mqttClient.ConnectAsync(options, CancellationToken.None); // Since 3.0.5 with CancellationToken
                }
                catch
                {
                    Console.WriteLine("### RECONNECTING FAILED ###");
                }
            });

            var r1 = await mqttClient.ConnectAsync(options, CancellationToken.None);

            var telemetries = new DeviceType2RandomGenerator().Generate();
            var topicNumber = 0;
            var count = 0;
            foreach (var telemetry in telemetries)
            {
                if (count % 10 == 0)
                {
                    topicNumber++;
                }
                count++;

                string p = JsonConvert.SerializeObject(telemetry);
                MqttApplicationMessage message = BuildMqttMessage(p, TopicName + "/" + topicNumber);
                Console.WriteLine("Sending Message");
                Console.WriteLine(r1.ReasonString);
                var r = await mqttClient.PublishAsync(message, CancellationToken.None);
                Console.WriteLine(r.ReasonCode);
                Console.WriteLine("Sent");

                await Task.Delay(TimeSpan.FromSeconds(1));
            }
            Console.WriteLine("Messages Sent");

        }

        private static MqttApplicationMessage BuildMqttMessage(string p, string topicName)
        {
            return new MqttApplicationMessageBuilder()
            .WithTopic(topicName)
            .WithPayload(p)
            .Build();
        }

       
    }
}
