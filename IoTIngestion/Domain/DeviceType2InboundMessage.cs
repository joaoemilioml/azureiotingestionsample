using System;
using System.Collections.Generic;

namespace Domain
{
    public class DeviceType2InboundMessage
    {
        public string DeviceId { get; set; }
        public DateTime Timestamp { get; set; }
        public Dictionary<string, decimal> Data { get; set; }
    }
}
