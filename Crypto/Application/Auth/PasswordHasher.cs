using Crypto.Entities;
using Microsoft.AspNetCore.Identity;

namespace Crypto.Application.Auth
{
    public class PasswordHasher
    {
        private readonly PasswordHasher<User> _hasher = new();
        public string Hash(User user, string password)
        {
            return _hasher.HashPassword(user, password);
        }


        public bool Verify(User user, string hash, string password)
        {
            var result = _hasher.VerifyHashedPassword(user, hash, password);

            return result ==  PasswordVerificationResult.Success;
        }
    }
}
