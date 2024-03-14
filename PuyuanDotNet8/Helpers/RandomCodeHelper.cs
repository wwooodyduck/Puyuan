using System.Security.Cryptography;

namespace PuyuanDotNet8.Helpers
{
    public class RandomCodeHelper
    {
        public static string Create(int bits_size)
        {
            var _crypto = new RNGCryptoServiceProvider();
            var bytes = bits_size;
            var bytesarray = new byte[bytes];
            _crypto.GetBytes(bytesarray);
            return Convert.ToBase64String(bytesarray);
        }
    }
}
