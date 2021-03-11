using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SimulateDevices
{
    public class DeviceType2RandomGenerator
    {
        private readonly int numberOfMessages = 20;
        public List<string> metrics = new List<string>
        {
            "Temperature", "Umidity", "Pressure", "Flow"
        };
        public List<DeviceType2InboundMessage> Generate()
        {
            var messages = new List<DeviceType2InboundMessage>();
            for (int i = 0; i < numberOfMessages; i++)
            {
                messages.Add(new DeviceType2InboundMessage
                {
                    DeviceId = $"DT2_Device",
                    Timestamp = DateTime.UtcNow,
                    Data = metrics.ToDictionary(x => x, x => (decimal) new Random().NextDouble()*100)
                });

                Thread.Sleep(1000);

            }

            return messages;
        }
    }
}
