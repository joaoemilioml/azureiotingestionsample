using Microsoft.Azure.Devices.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Providers
{
    public sealed class IoTHubClient : IDisposable
    {
        public string HostName { get; }
        public string SharedAccessKeyName { get; }
        public string SharedAccessKey { get; }
        private readonly DeviceClient deviceClient;

        public IoTHubClient(string connectionString)
        {
            deviceClient = DeviceClient.CreateFromConnectionString(connectionString, TransportType.Mqtt);
        }



        public async Task SendString(string data)
        {

            var b = Encoding.UTF8.GetBytes(data);
            using Message eventMessage = new Message(b);
            await deviceClient.SendEventAsync(eventMessage).ConfigureAwait(false);


        }

        public async Task SendRaw(string t)
        {

            var stream = Encoding.UTF8.GetBytes(t);
            using var message = new Message(stream)
            {
                MessageId = Guid.NewGuid().ToString()
            };
            try
            {
                await deviceClient.SendEventAsync(message).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SendRaw(string fromTopic,byte[] stream)
        {

            using var message = new Message(stream)
            {
                MessageId = Guid.NewGuid().ToString()
            };
            message.Properties["topic"] = fromTopic;
            try
            {
                await deviceClient.SendEventAsync(message).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
            deviceClient.Dispose();
        }
    }
}
