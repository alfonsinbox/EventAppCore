using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventAppCore.Models.View
{
    public class ViewUser
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string Username { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string MockProperty { get; set; }
/*        [InverseProperty("UsersWithInterest")]
        public ICollection<Category> Interests { get; set; }*/
    }
}