using System.Security.Cryptography;
using System.Text;

namespace DecryptionAPI.Repository.Encryption
{
    public class EncryptionRepo
    {
        public string EncryptText(string text)
        {
            string keyString = "8080808080808080";
            string ivString = "8080808080808080";

            byte[] key = Encoding.UTF8.GetBytes(keyString);
            byte[] iv = Encoding.UTF8.GetBytes(ivString);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;
                aesAlg.Mode = CipherMode.CBC;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                byte[] encryptedBytes;
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(text);
                        }
                        encryptedBytes = msEncrypt.ToArray();
                    }
                }

                string base64String = Convert.ToBase64String(encryptedBytes);
                Console.WriteLine(base64String);
                return base64String;
            }
        }
    }
}
