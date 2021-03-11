using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SimulateDevices
{
    public class DeviceType1RandomGenerator
    {
        private readonly int numberOfMessages = 10;
        private readonly int numberOfNodes = 2;
        
        public List<string> metrics = new List<string>
        {
            "Temperature", "Umidity", "Pressure",  "Wind Speed", "Air Density"
        };
        public List<DeviceType1InboundMessage> Generate()
        {
            var messages = new List<DeviceType1InboundMessage>();
            for (int i = 0; i < numberOfMessages; i++)
            {
                var nodes = new List<DeviceType1ChildDevice>();

                for (int j = 0; j < numberOfNodes; j++)
                {
                    nodes.Add(new DeviceType1ChildDevice
                    {
                        DeviceId = $"DT1_Device{j}",
                        Timestamp = DateTime.UtcNow,
                        Metrics = metrics.Select(x => new DeviceType1Metric
                        {
                            Name = x,
                            Value = (decimal)new Random().NextDouble() * 100
                        }).ToList()
                    });

                    Thread.Sleep(1000);

                }

                messages.Add(new DeviceType1InboundMessage
                {
                    Nodes = nodes
                });

            }

            return messages;
        }
    }
}
