using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimulateDevices
{
    public class DeviceType2RandomGenerator
    {
        private readonly int numberOfMessages = 100;
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
                    DeviceId = $"DT2_Device{i}",
                    Timestamp = DateTime.UtcNow,
                    Data = metrics.ToDictionary(x => x, x => (decimal) new Random().NextDouble()*100)
                });

            }

            return messages;
        }
    }
}
