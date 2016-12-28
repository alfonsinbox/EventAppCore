using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventAppCore.Models
{
    public class Event : DbEntityBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPrivate { get; set; }

        public bool IsCanceled { get; set; }

        public string CanceledReason { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset EndTime { get; set; }

        public User CreatedBy { get; set; }

        public Location Location { get; set; }

        public ICollection<UserEvent> UserEvents { get; set; }

        public ICollection<Category> Categories { get; set; }

        public string MainImageUrl { get; set; }

        [NotMapped]
        public bool IsAttending { get; set; }

        [NotMapped]
        public bool IsCreator { get; set; }
    }
}