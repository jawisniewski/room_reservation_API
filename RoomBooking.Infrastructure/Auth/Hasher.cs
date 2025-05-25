using RoomBooking.Application.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace RoomBooking.Infrastructure.Auth
{
    public class Hasher : IHasher
    {
        public string Hash(string textToHash)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(textToHash);
                var hash = sha.ComputeHash(bytes);

                return Convert.ToBase64String(hash);
            }
        }

        public bool Verify(string textToVerify, string hash)
        {
            var hashedText = Hash(textToVerify);

            return hashedText == hash;
        }
    }
}
