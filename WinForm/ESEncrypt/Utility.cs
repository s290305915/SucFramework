using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Security.Cryptography;
//using Eval3;
using System.IO;
using System.Web;

namespace ESEncrypt
{
    public static class Utility
    {
        public class SymmetricEncryptor
        {
            private string _siteType = "0";//yaxin

            public string SiteType
            {
                set { _siteType = value; }
                get { return _siteType; }
            }


            private byte[] _key;
            private byte[] _vector;
            public static string sKey = "asia123?";
            public SymmetricEncryptor(string key, string vector)
            {
                InitBuffer(ref _key, key);
                InitBuffer(ref _vector, vector);
            }

            private void InitBuffer(ref byte[] buffer, string value)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    buffer = Convert.FromBase64String(value);
                }
                else
                {
                    buffer = new byte[8];
                    new Random().NextBytes(buffer);
                }
            }

            private bool IsEncrypted(string value)
            {
                try
                {
                    Convert.FromBase64String(value);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public string Encrypt(string value)
            {
                if (IsEncrypted(value))
                    return value;

                SymmetricAlgorithm sa = new DESCryptoServiceProvider();
                ICryptoTransform ct = sa.CreateEncryptor(_key, _vector);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                    {
                        byte[] buffer = Encoding.UTF8.GetBytes(value);
                        cs.Write(buffer, 0, buffer.Length);
                        cs.FlushFinalBlock();
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
            }

            public string Decrypt(string value)
            {
                if (!IsEncrypted(value))
                    return value;

                try
                {
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                    byte[] inputByteArray = new byte[value.Length / 2];
                    for (int x = 0; x < value.Length / 2; x++)
                    {
                        int i = (Convert.ToInt32(value.Substring(x * 2, 2), 16));
                        inputByteArray[x] = (byte)i;
                    }

                    des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                    des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();

                    StringBuilder ret = new StringBuilder();

                    return HttpContext.Current.Server.UrlDecode(System.Text.Encoding.Default.GetString(ms.ToArray()));
                }
                catch (Exception e)
                {
                    e.ToString(); // disable CS0168
                    return value;
                }
            }
        }

        public static string Encrypt(string text)
        {
            char[] data = text.ToCharArray();
            byte[] buffer = new byte[data.Length];
            StringBuilder sb = new StringBuilder();
            HashAlgorithm sha = new SHA1CryptoServiceProvider();

            for (int i = 0; i < data.Length; ++i)
                buffer[i] = (byte)data[i];

            buffer = sha.ComputeHash(buffer);
            foreach (byte b in buffer)
                sb.AppendFormat("{0:X2}", b);

            return sb.ToString();
        }

        public static string GetMd5HashCode(string input)
        {
            MD5 md5 = MD5.Create();
            byte[] data = md5.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                result.AppendFormat("{0:X2}", data[i]);
            }

            return result.ToString();
        }

        public static int GetStringHashCode(string text)
        {
            return (text == null ? 0 : text.GetHashCode());
        }

        private static char[] constant ={ '0','1','2','3','4','5','6','7','8','9',   
             'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};

        public static string GenerateRandomString(int length)
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            Random random = new Random();
            str.Append(constant.Skip(36).Take(26).ToArray()[random.Next(26)]);
            for (int i = 0; i < length - 1; i++)
            {
                str.Append(constant.Take(36).ToArray()[random.Next(36)]);
            }
            return str.ToString();
        }

        public static string GenerateRandomVerifyCode(int length)
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            Random random = new Random();
            str.Append(constant.Skip(10).Take(26).ToArray()[random.Next(26)]);
            for (int i = 0; i < length - 1; i++)
            {
                str.Append(constant.Take(10).ToArray()[random.Next(10)]);
            }
            return str.ToString();
        }
    }
}
