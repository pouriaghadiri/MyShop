using System.Security.Cryptography;
using System.Text;

namespace AngularMyApp.Core
{
    public static class SecurityHelper
    {
        private static readonly RandomNumberGenerator _randomNumberGenerator = RandomNumberGenerator.Create();

        public static string GetHashSha256(string value)
        {
            var algoritm = new SHA256CryptoServiceProvider();
            var byteValue = Encoding.UTF8.GetBytes(value);
            var shaValue = algoritm.ComputeHash(byteValue);
            return Convert.ToBase64String(shaValue);
        }
    }
}
