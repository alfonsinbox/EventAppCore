using System;
using System.ComponentModel.DataAnnotations;

namespace EventAppCore.Models.View
{
    public class SignUpUser
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public DateTimeOffset BirthDate { get; set; }

        [MaxLength(200)]
        public string Email { get; set; }

        [MaxLength(200)]
        public string Username { get; set; }

        public string Password { get; set; }

        public string SSN { get; set; }

        public string ProfilePictureUrl { get; set; }

    }
}