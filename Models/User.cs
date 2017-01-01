using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventAppCore.Models
{
    public class User : DbEntityBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public DateTimeOffset BirthDate { get; set; }

        public string SaltAndHash { get; set; }

        [MaxLength(200)]
        public string Email { get; set; }

        [MaxLength(200)]
        public string Username { get; set; }

        public string Ssn { get; set; }

        public int AccessLevel { get; set; }

        public bool Confirmed { get; set; }

        public string ConfirmationToken { get; set; }

        public string ForgottenPasswordToken { get; set; }

        public string AccessCode { get; set; }

        public string ProfilePictureUrl { get; set; }

        public bool Suspended { get; set; }

        public string SystemNotes { get; set; }

        public ICollection<UserEvent> UserEvents { get; set; }

        [InverseProperty("BelongsTo")]
        public ICollection<RefreshToken> RefreshTokens { get; set; }
        
        public string MockProperty { get; set; } = "Just:a:value!";

        /*[InverseProperty("CreatedBy")]
        public ICollection<Category> CreatedCategories { get; set; }*/

        /*[InverseProperty("UsersWithInterest")]
        public ICollection<Category> Interests { get; set; }*/
    }
}