﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class OutboundMessage
    {
        public List<OutboundMessageMetric> Metrics { get; set; }
    }
    public class OutboundMessageMetric
    {
        [JsonIgnore]
        public Guid Id => Guid.NewGuid();
        public string DeviceId { get; set; }
        public DateTime Timestamp { get; set; }
        public string MetricName { get; set; }
        public decimal MetricValue { get; set; }
    }
}
