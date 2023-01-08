using System.Security.Cryptography;
using System.Text;

namespace MKFotografiaBackend.Helpers
{
    public class Algorithms
    {
        public static string HashString(string input)
        {
            StringBuilder builder = new StringBuilder();
            using (HashAlgorithm algorithm = SHA256.Create())
            {
                byte[] bytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("X2"));
                }
            }
            return builder.ToString();
        }
    }
}
