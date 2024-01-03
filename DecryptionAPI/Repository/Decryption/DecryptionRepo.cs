using DecryptionAPI.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;

namespace DecryptionAPI.Repository.Decryption
{
    public class DecryptionRepo
    {
        public readonly IConfiguration _config;
        private readonly ResponseDto _dto;

        public DecryptionRepo(IConfiguration config, ResponseDto dto)
        {
            _config = config;
            _dto = dto;
        }


            public async Task<ResponseDto> FromHexToBase64(string hexString)
            {
                // hexString = "7771762f4e4733392b5a3670417a71477770736a6c773d3d";
                if (hexString == null || (hexString.Length & 1) == 1)
                {
                    throw new ArgumentException();
                }
                var sb = new StringBuilder();
                for (var i = 0; i < hexString.Length; i += 2)
                {
                    var hexChar = hexString.Substring(i, 2);
                    sb.Append((char)Convert.ToByte(hexChar, 16));
            }

            _dto.response = Convert.ToString(sb);


           

                return _dto;
            }

      /*  public async Task<dynamic> DecryptText(string text)
        {
            // For AES Encryption/Decryption
            string DecryptKey = _config.GetValue<string>("Decrypt:DKey");

            string keyString = DecryptKey;
            string ivString = DecryptKey;
            byte[] key = Encoding.UTF8.GetBytes(keyString);
            byte[] iv = Encoding.UTF8.GetBytes(ivString);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;
                aesAlg.Mode = CipherMode.CBC;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                byte[] encryptedBytes = Convert.FromBase64String(text);
                byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

                string decryptedText = Encoding.UTF8.GetString(decryptedBytes);
                Console.WriteLine(decryptedText);
                Console.WriteLine("working");
                return decryptedText;
            }

       }   */


    }
}
