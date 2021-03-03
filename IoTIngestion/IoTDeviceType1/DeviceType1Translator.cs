using Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IoTDeviceType1
{
    public class DeviceType1Translator
    {
        public OutboundMessage Translate(string message)
        {
            var inboundMessage = JsonConvert.DeserializeObject<DeviceType1InboundMessage>(message);
            List<OutboundMessageMetric> outboundMetrics = inboundMessage.Nodes
                                    .SelectMany(node => node.Metrics
                                                            .Select(metric => 
                                                            new OutboundMessageMetric 
                                                            {
                                                                DeviceId = node.DeviceId,
                                                                Timestamp = node.Timestamp,
                                                                MetricName = metric.Name,
                                                                MetricValue = metric.Value
                                                            })
                                                ).ToList();
            return new OutboundMessage { Metrics = outboundMetrics};
        }
    }
}
