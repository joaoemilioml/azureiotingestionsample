using Domain;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace IoTDeviceType2
{
    public class DeviceType2Translator
    {
        public OutboundMessage Translate(string message)
        {
            var inboundMessage = JsonConvert.DeserializeObject<DeviceType2InboundMessage>(message);
            List<OutboundMessageMetric> outboundMetrics = inboundMessage.Data.Select(x => new OutboundMessageMetric
            {
                DeviceId = inboundMessage.DeviceId,
                Timestamp = inboundMessage.Timestamp,
                MetricName = x.Key,
                MetricValue = x.Value
            }).ToList();

            return new OutboundMessage { Metrics = outboundMetrics };
        }
    }
}
