using System;

namespace EventAppCore.Models
{
    public class DbEntityBase
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}