using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventAppCore.Models
{
    public class Category : DbEntityBase
    {
        public string Name { get; set; }

        /*[InverseProperty("CreatedCategories")]
        public User CreatedBy { get; set; }*/

        /*[InverseProperty("Interests")]
        public ICollection<User> UsersWithInterest { get; set; }*/

        public ICollection<Event> EventsWithCategory { get; set; }
    }
}