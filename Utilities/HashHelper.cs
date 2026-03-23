using System.Security.Cryptography;
using System.Text;

namespace Skygate_AspNet_MVC.Utilities
{
    public class HashHelper
    {
        public static string ToSHA256(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2")); // 轉為 16 進位小寫，與您資料庫格式一致
                }
                return builder.ToString();
            }
        }
    }
}
