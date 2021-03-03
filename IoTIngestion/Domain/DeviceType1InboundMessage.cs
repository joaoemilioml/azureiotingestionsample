using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DeviceType1InboundMessage
    {
        public List<DeviceType1ChildDevice> Nodes { get; set; }
    }

    public class DeviceType1ChildDevice
    {
        public string DeviceId { get; set; }
        public DateTime Timestamp { get; set; }
        public List<DeviceType1Metric> Metrics { get; set; }
    }

    public class DeviceType1Metric
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
    }
}
