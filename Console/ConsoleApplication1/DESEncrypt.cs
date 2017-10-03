using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public static class DESEncrypt
    {
        #region DES Class
        /// <summary>  
        /// ClassName: DES 加密类   
        /// DES加密、解密类库，字符串加密结果使用BASE64编码返回，支持文件的加密和解密  
        /// Time:2010-04-30  
        /// Auther:Hyey.wl  
        /// DES 的摘要说明。  
        /// </summary>  
        /// <summary>  
        /// myiv is  iv  
        /// </summary>  
        static string myiv = "0";
        /// <summary>  
        /// mykey is key  
        /// </summary>  
        static string mykey = "asia123?";

        /// <summary>  
        /// DES加密偏移量  
        /// 必须是>=8位长的字符串  
        /// </summary>  
        public static string IV
        {
            get { return myiv; }
            set { myiv = value; }
        }

        /// <summary>  
        /// DES加密的私钥  
        /// 必须是8位长的字符串  
        /// </summary>  
        public static string Key
        {
            get { return mykey; }
            set { mykey = value; }
        }


        /// <summary>  
        /// 对字符串进行DES加密  
        /// Encrypts the specified sourcestring.  
        /// </summary>  
        /// <param name="sourcestring">The sourcestring.待加密的字符串</param>  
        /// <returns>加密后的BASE64编码的字符串</returns>  
        public static string Encrypt(string sourceString)
        {
            byte[] btKey = Encoding.Default.GetBytes(mykey);
            byte[] btIV = Encoding.Default.GetBytes(myiv);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = Encoding.Default.GetBytes(sourceString);
                try
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);
                        cs.FlushFinalBlock();
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>  
        /// Decrypts the specified encrypted string.  
        /// 对DES加密后的字符串进行解密  
        /// </summary>  
        /// <param name="encryptedString">The encrypted string.待解密的字符串</param>  
        /// <returns>解密后的字符串</returns>  
        public static string Decrypt(string encryptedString)
        {
            byte[] btKey = Encoding.Default.GetBytes(mykey);
            byte[] btIV = Encoding.Default.GetBytes(myiv);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = Convert.FromBase64String(encryptedString);
                try
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);
                        cs.FlushFinalBlock();
                    }
                    return Encoding.Default.GetString(ms.ToArray());
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>  
        /// Encrypts the file.  
        /// 对文件内容进行DES加密  
        /// </summary>  
        /// <param name="sourceFile">The source file.待加密的文件绝对路径</param>  
        /// <param name="destFile">The dest file.加密后的文件保存的绝对路径</param>  
        public static void EncryptFile(string sourceFile, string destFile)
        {
            if (!File.Exists(sourceFile)) throw new FileNotFoundException("指定的文件路径不存在！", sourceFile);

            byte[] btKey = Encoding.Default.GetBytes(mykey);
            byte[] btIV = Encoding.Default.GetBytes(myiv);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] btFile = File.ReadAllBytes(sourceFile);

            using (FileStream fs = new FileStream(destFile, FileMode.Create, FileAccess.Write))
            {
                try
                {
                    using (CryptoStream cs = new CryptoStream(fs, des.CreateEncryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(btFile, 0, btFile.Length);
                        cs.FlushFinalBlock();
                    }
                }
                catch
                {
                    throw;
                }

                finally
                {
                    fs.Close();
                }
            }
        }

        /// <summary>  
        /// Encrypts the file.  
        /// 对文件内容进行DES加密，加密后覆盖掉原来的文件  
        /// </summary>  
        /// <param name="sourceFile">The source file.待加密的文件的绝对路径</param>  
        public static void EncryptFile(string sourceFile)
        {
            EncryptFile(sourceFile, sourceFile);
        }

        /// <summary>  
        /// Decrypts the file.  
        /// 对文件内容进行DES解密  
        /// </summary>  
        /// <param name="sourceFile">The source file.待解密的文件绝对路径</param>  
        /// <param name="destFile">The dest file.解密后的文件保存的绝对路径</param>  
        public static void DecryptFile(string sourceFile, string destFile)
        {
            if (!File.Exists(sourceFile)) throw new FileNotFoundException("指定的文件路径不存在！", sourceFile);
            byte[] btKey = Encoding.Default.GetBytes(mykey);
            byte[] btIV = Encoding.Default.GetBytes(myiv);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] btFile = File.ReadAllBytes(sourceFile);

            using (FileStream fs = new FileStream(destFile, FileMode.Create, FileAccess.Write))
            {
                try
                {
                    using (CryptoStream cs = new CryptoStream(fs, des.CreateDecryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(btFile, 0, btFile.Length);
                        cs.FlushFinalBlock();
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    fs.Close();
                }
            }
        }

        /// <summary>  
        /// Decrypts the file.  
        /// 对文件内容进行DES解密，加密后覆盖掉原来的文件.  
        /// </summary>  
        /// <param name="sourceFile">The source file.待解密的文件的绝对路径.</param>  
        public static void DecryptFile(string sourceFile)
        {
            DecryptFile(sourceFile, sourceFile);
        }
    }
        #endregion
}
