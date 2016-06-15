using System;
using System.Collections.Generic;

namespace cerealize.common
{
    public class Metric
    {
        public string ObjectId { get; set; }
        public string SeriesId { get; set; }
        public string SerializerId { get; set; }

        public TimeSpan ElapsedTimeForSamplingSize { get; set; }
        public int SamplingSize { get; set; }
        public int SerializedBytesLength { get; set; }
        public MetricStage Stage { get; set; }

        public bool PassedAllTests { get; set; }
        public List<string> FailureMessages { get; set; }
    }
}