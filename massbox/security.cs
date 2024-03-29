﻿using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace massbox
{
    class Security
    {
        public class EncryptHelper
        {
            //默认密钥                       
            private static string AESKey = "[MaTrIx56/*.+u?]";
            private static string DESKey = "[.AlC%*]";

            /// <summary> 
            /// AES加密 
            /// </summary>
            public static string AESEncrypt(string value, string _aeskey = null)
            {
                if (string.IsNullOrEmpty(_aeskey))
                {
                    _aeskey = AESKey;
                }
                byte[] keyArray = Encoding.UTF8.GetBytes(_aeskey);
                byte[] toEncryptArray = Encoding.UTF8.GetBytes(value);

                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = rDel.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }

            /// <summary> 
            /// AES解密 
            /// </summary>
            public static string AESDecrypt(string value, string _aeskey = null)
            {
                try
                {
                    if (string.IsNullOrEmpty(_aeskey))
                    {
                        _aeskey = AESKey;
                    }
                    byte[] keyArray = Encoding.UTF8.GetBytes(_aeskey);
                    byte[] toEncryptArray = Convert.FromBase64String(value);

                    RijndaelManaged rDel = new RijndaelManaged();
                    rDel.Key = keyArray;
                    rDel.Mode = CipherMode.ECB;
                    rDel.Padding = PaddingMode.PKCS7;

                    ICryptoTransform cTransform = rDel.CreateDecryptor();
                    byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                    return Encoding.UTF8.GetString(resultArray);
                }
                catch
                {
                    return string.Empty;
                }
            }

            /// <summary> 
            /// DES加密 
            /// </summary>
            public static string DESEncrypt(string value, string _deskey = null)
            {
                if (string.IsNullOrEmpty(_deskey))
                {
                    _deskey = DESKey;
                }
                value += "#ALC-PSW#" + new Random().Next() + DateTime.Now;
                byte[] keyArray = Encoding.UTF8.GetBytes(_deskey);
                byte[] toEncryptArray = Encoding.UTF8.GetBytes(value);

                DESCryptoServiceProvider rDel = new DESCryptoServiceProvider();
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = rDel.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }

            /// <summary> 
            /// DES解密 
            /// </summary>
            public static string DESDecrypt(string value, string _deskey = null)
            {
                try
                {
                    if (string.IsNullOrEmpty(_deskey))
                    {
                        _deskey = DESKey;
                    }
                    byte[] keyArray = Encoding.UTF8.GetBytes(_deskey);
                    byte[] toEncryptArray = Convert.FromBase64String(value);

                    DESCryptoServiceProvider rDel = new DESCryptoServiceProvider();
                    rDel.Key = keyArray;
                    rDel.Mode = CipherMode.ECB;
                    rDel.Padding = PaddingMode.PKCS7;

                    ICryptoTransform cTransform = rDel.CreateDecryptor();
                    byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                    String tmp = Encoding.UTF8.GetString(resultArray);
                    try
                    {
                        return tmp.Split("#ALC-PSW#".ToCharArray())[0];
                    }catch (Exception)
                    {
                        return string.Empty;
                    }
                    
                }
                catch
                {
                    return string.Empty;
                }
            }

            public static string MD5(string value)
            {
                byte[] result = Encoding.UTF8.GetBytes(value);
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] output = md5.ComputeHash(result);
                return BitConverter.ToString(output).Replace("-", "");
            }

            public static string HMACMD5(string value, string hmacKey)
            {
                HMACSHA1 hmacsha1 = new HMACSHA1(Encoding.UTF8.GetBytes(hmacKey));
                byte[] result = System.Text.Encoding.UTF8.GetBytes(value);
                byte[] output = hmacsha1.ComputeHash(result);


                return BitConverter.ToString(output).Replace("-", "");
            }

            /// <summary>
            /// base64编码
            /// </summary>
            /// <returns></returns>
            public static string Base64Encode(string value)
            {
                string result = Convert.ToBase64String(Encoding.Default.GetBytes(value));
                return result;
            }
            /// <summary>
            /// base64解码
            /// </summary>
            /// <returns></returns>
            public static string Base64Decode(string value)
            {
                string result = Encoding.Default.GetString(Convert.FromBase64String(value));
                return result;
            }

            public static string cover(String str)
            {
                int len = str.Length;
                if (len < 6)
                {
                    return "******";
                }
                else
                {
                    return str.Substring(0, 3) + "******" + str.Substring(len - 3, 3);
                }
            }
        }
    }
}
