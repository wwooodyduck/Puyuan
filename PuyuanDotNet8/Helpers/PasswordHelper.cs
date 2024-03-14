using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace PuyuanDotNet8.Helpers
{
    public class PasswordHelper
    {
        private readonly int bits;
        private readonly byte[] pepper;

        public PasswordHelper(IConfiguration configuration)
        {
            bits = configuration.GetValue<int>("Secret:Bits");
            pepper = Convert.FromBase64String(configuration.GetValue<string>("Secret:Pepper"));
        }

        private string Hash(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 1024 / 8
                ));
        }

        private byte[] ConcatSaltAndPepper(byte[] salt)
        {
            byte[] newSalt = new byte[salt.Length + pepper.Length];
            Buffer.BlockCopy(salt, 0, newSalt, 0, salt.Length);
            Buffer.BlockCopy(pepper, 0, newSalt, salt.Length, pepper.Length);
            return newSalt;
        }

        public string HashPassword(string password)
        {
            byte[] salt = new byte[bits / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            byte[] newSalt = ConcatSaltAndPepper(salt);
            return Convert.ToBase64String(salt) + ":" + Hash(password, newSalt);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            string[] passwordInfo = hashedPassword.Split(':');
            if (passwordInfo.Length != 2)
            {
                return false;
            }
            byte[] salt = Convert.FromBase64String(passwordInfo[0]);
            byte[] newSalt = ConcatSaltAndPepper(salt);
            if (Hash(password, newSalt).Equals(passwordInfo[1]))
            {
                return true;
            }
            return false;
        }
    }
}
