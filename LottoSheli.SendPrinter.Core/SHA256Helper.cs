using System.Security.Cryptography;
using System.Text;

namespace LottoSheli.SendPrinter.Core
{
    public class SHA256Helper
    {
        public static string ComputeHash(string randomString)
        {
            using var crypt = SHA256.Create();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
