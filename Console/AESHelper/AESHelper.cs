using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AESHelper
{
    public class AESHelper
    {

        /// <summary>
        /// 生成新AESKey
        /// </summary>
        /// <returns></returns>
        public static string GenerateNewKey()
        {
            string AESKey = "";

            using (Aes aesAlg = Aes.Create())
            {
                AESKey = Convert.ToBase64String(aesAlg.Key);

            }

            return AESKey;
        }
 
        /// <summary>
        /// 获取时间戳并加密
        /// </summary>
        /// <returns></returns>
        public static string GetTimestamp(string AESKey)
        {
            return AESEncrypt(GetTimestamp(), AESKey);
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(2010, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }


        /// <summary>
        /// 签名算法
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetSha1(string str)
        {
            //建立SHA1对象
            SHA1 sha = new SHA1CryptoServiceProvider();
            //将mystr转换成byte[] 
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] dataToHash = enc.GetBytes(str);
            //Hash运算
            byte[] dataHashed = sha.ComputeHash(dataToHash);
            //将运算结果转换成string
            string hash = BitConverter.ToString(dataHashed).Replace("-", "");
            return hash;
        }

        /// <summary>
        /// AES加密算法
        /// </summary>
        /// <param name="plainText">明文字符串</param>
        /// <returns>将加密后的密文转换为Base64编码，以便显示</returns>
        public static string AESEncrypt(string plainText, string AESKey)
        {

            //分组加密算法
            RijndaelManaged rijndaelCipher = new RijndaelManaged();

            byte[] inputByteArray = Encoding.UTF8.GetBytes(plainText);

            //设置密钥及密钥向量
            rijndaelCipher.Key = Convert.FromBase64String(AESKey);

            rijndaelCipher.GenerateIV();

            byte[] keyIv = rijndaelCipher.IV;

            byte[] cipherBytes = null;

            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, rijndaelCipher.CreateEncryptor(), CryptoStreamMode.Write))
            {

                cs.Write(inputByteArray, 0, inputByteArray.Length);

                cs.FlushFinalBlock();

                cipherBytes = ms.ToArray();

            }

            return Convert.ToBase64String(keyIv.Concat(cipherBytes).ToArray());

        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="cipherText">密文字符串</param>
        /// <returns>返回解密后的明文字符串</returns>
        public static string AESDecrypt(string encryptText, string AESKey)
        {

            byte[] cipherText = Convert.FromBase64String(encryptText);

            int length = cipherText.Length;

            SymmetricAlgorithm des = Rijndael.Create();

            des.Key = Convert.FromBase64String(AESKey);//加解密双方约定好的密钥

            byte[] iv = new byte[16];

            Buffer.BlockCopy(cipherText, 0, iv, 0, 16);

            des.IV = iv;

            byte[] decryptBytes = new byte[length - 16];

            byte[] passwdText = new byte[length - 16];

            Buffer.BlockCopy(cipherText, 16, passwdText, 0, length - 16);

            using (MemoryStream ms = new MemoryStream(passwdText))
            using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read))
            {

                cs.Read(decryptBytes, 0, decryptBytes.Length);

            }

            return Encoding.UTF8.GetString(decryptBytes).Replace("\0", "");   ///将字符串后尾的'\0'去掉

        }

    }
}
