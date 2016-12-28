using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventAppCore.Models
{
    public class RefreshToken : DbEntityBase
    {
        [InverseProperty("RefreshTokens")]
        public User BelongsTo { get; set; }

        public string Token { get; set; } = Guid.NewGuid().ToString().Replace("-", "");

        public bool Revoked { get; set; }

        public string Device { get; set; }
    }
}