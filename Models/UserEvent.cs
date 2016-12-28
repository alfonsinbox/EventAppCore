
using System;

namespace EventAppCore.Models
{
    public class UserEvent : DbEntityBase
    {
        public User User { get; set; }

        public Event Event { get; set; }

        public int Role { get; set; }

        public string Note { get; set; }
    }
}