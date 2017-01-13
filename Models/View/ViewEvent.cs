using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventAppCore.Models.View
{
    public class ViewEvent
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPrivate { get; set; }

        public bool IsCanceled { get; set; }

        public string CanceledReason { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset EndTime { get; set; }

        public ViewUser CreatedBy { get; set; }

        public ViewLocation Location { get; set; }

        //public ICollection<Category> Categories { get; set; }

        public string MainImageUrl { get; set; }

        [NotMapped]
        public bool IsAttending { get; set; }

        [NotMapped]
        public bool IsCreator { get; set; }
    }
}