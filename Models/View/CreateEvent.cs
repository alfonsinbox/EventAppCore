using System;

namespace EventAppCore.Models.View
{
    public class CreateEvent
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPrivate { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset EndTime { get; set; }

        public string LocationId { get; set; }

        public string MainImageUrl { get; set; }
    }
}