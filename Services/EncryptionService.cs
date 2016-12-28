using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace EventAppCore.Services
{
    public class EncryptionService : IEncryptionService
    {
        private const int SaltSize = 32;
        private const int NumIterations = 10000;

        public string CreatePasswordSaltHash(string password)
        {
            var buf = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(buf);
            }

            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password.Trim(),
                salt: buf,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: NumIterations,
                numBytesRequested: 256 / 8));

            var salt = Convert.ToBase64String(buf);

            return salt + ':' + hashed;
        }

        public bool IsPasswordValid(string password, string saltHash)
        {
            var parts = saltHash.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)
            {
                return false;
            }

            var buf = Convert.FromBase64String(parts[0]);

            var computedHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password.Trim(),
                salt: buf,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: NumIterations,
                numBytesRequested: 256 / 8));

            return parts[1].Equals(computedHash);
        }
    }
}