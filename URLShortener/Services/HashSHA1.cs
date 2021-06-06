using System.Security.Cryptography;
using System.Text;

namespace URLShortener.Services {
    class HashSHA1 : IHash {
        public string GetHash(string text) {
            using var sha = new SHA1Managed();
            byte[] textData = Encoding.UTF8.GetBytes(text);
            byte[] hash = sha.ComputeHash(textData);

            StringBuilder sb = new StringBuilder();
            foreach (byte b in hash) {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
