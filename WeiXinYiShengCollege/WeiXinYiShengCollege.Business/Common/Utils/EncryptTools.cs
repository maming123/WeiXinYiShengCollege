using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Module.Utils
{
    public class EncryptTools
    {
        //private static byte[] cryptKey = { 42, 16, 93, 156, 78, 4, 218, 32 };
        //private static byte[] cryptIV = { 55, 103, 246, 79, 36, 99, 167, 3 };

        private static byte[] cryptIV = { 42, 16, 93, 156, 78, 4, 218, 32 };
        private static byte[] cryptKey = { 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80 };

        /// <summary>
        /// 构造函数
        /// </summary>
        private EncryptTools()
        {
        }
        /// <summary>
        /// 使用DES方法加密字符串DESEncrypt
        /// </summary>
        /// <param name="value">要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string DESEncrypt(string value)
        {
            return DESEncrypt(value, Encoding.UTF8);
        }

        /// <summary>
        /// 使用DES方法加密字符串DESEncrypt
        /// </summary>
        /// <param name="value">要加密的字符串</param>
        /// <param name="encoding">字符编码</param>
        /// <returns>加密后的字符串</returns>
        public static string DESEncrypt(string value, Encoding encoding)
        {
            return DESEncrypt(value, encoding, cryptKey, cryptIV);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">要加密的字符串</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="cryptKey">用于对称算法的密钥</param>
        /// <param name="cryptIV">用于对称算法的初始化向量</param>
        /// <returns></returns>
        public static string DESEncrypt(string value, Encoding encoding, byte[] cryptKey, byte[] cryptIV)
        {
            byte[] data = encoding.GetBytes(value);

            byte[] eData = DESEncrypt(data, cryptKey, cryptIV);

            return Convert.ToBase64String(eData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] DESEncrypt(byte[] data)
        {
            return DESEncrypt(data, cryptKey, cryptIV);
        }

        public static string md5(string str, int code)
        {
            if (code == 16) //16位MD5加密（取32位加密的9~25字符）   
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower().Substring(8, 16);
            }
            else//32位加密   
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
            }
        }   

        /// <summary>
        /// 使用DES对byte数组进行加密
        /// </summary>
        /// <param name="data">要加密的byte数组</param>
        /// <param name="cryptKey">用于对称算法的密钥</param>
        /// <param name="cryptIV">用于对称算法的初始化向量</param>
        /// <returns></returns>
        public static byte[] DESEncrypt(byte[] data, byte[] cryptKey, byte[] cryptIV)
        {
            DESCryptoServiceProvider cryptoProvider = CreateDESCrypto();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateEncryptor(cryptKey, cryptIV), CryptoStreamMode.Write);
            cryptoStream.Write(data, 0, data.Length);
            cryptoStream.FlushFinalBlock();

            Byte[] result = memoryStream.ToArray();

            return result;
        }

        /// <summary>
        /// 使用DES方法加密字符串DESEncrypt
        /// </summary>
        /// <param name="value">要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string DESDecrypt(string value)
        {
            return DESDecrypt(value, Encoding.UTF8);
        }
        /// <summary>
        /// 使用DES方法加密字符串DESEncrypt
        /// </summary>
        /// <param name="value">要加密的字符串</param>
        /// <param name="encoding">字符编码</param>
        /// <returns>加密后的字符串</returns>
        public static string DESDecrypt(string value, Encoding encoding)
        {
            return DESDecrypt(value, encoding, cryptKey, cryptIV);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">要加密的字符串</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="cryptKey">用于对称算法的密钥</param>
        /// <param name="cryptIV">用于对称算法的初始化向量</param>
        /// <returns></returns>
        public static string DESDecrypt(string value, Encoding encoding, byte[] cryptKey, byte[] cryptIV)
        {
            byte[] data = Convert.FromBase64String(value);

            byte[] eData = DESDecrypt(data, cryptKey, cryptIV);

            return encoding.GetString(eData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] DESDecrypt(byte[] data)
        {
            return DESDecrypt(data, cryptKey, cryptIV);
        }



        /// <summary>
        /// 解密byte数组
        /// </summary>
        /// <param name="data">要解密的byte数组</param>
        /// <param name="encoding"></param>
        /// <param name="cryptKey">用于对称算法的密钥</param>
        /// <param name="cryptIV">用于对称算法的初始化向量</param>
        /// <returns></returns>
        public static byte[] DESDecrypt(byte[] data, byte[] cryptKey, byte[] cryptIV)
        {
            DESCryptoServiceProvider cryptoProvider = CreateDESCrypto();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(cryptKey, cryptIV), CryptoStreamMode.Write);
            cryptoStream.Write(data, 0, data.Length);
            cryptoStream.FlushFinalBlock();
            return memoryStream.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static DESCryptoServiceProvider CreateDESCrypto()
        {
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();

            cryptoProvider.KeySize = 64;
            cryptoProvider.BlockSize = 64;

            return cryptoProvider;
        }


        /// <summary>
        /// 加密一个字符串使用Base64方法
        /// </summary>
        /// <param name="value">要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string Base64Encode(string value)
        {
            return Base64Encode(value, Encoding.Default);
        }

        /// <summary>
        /// 加密一个字符串使用Base64方法
        /// </summary>
        /// <param name="value">要加密的字符串</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        public static string Base64Encode(string value, Encoding encoding)
        {
            string encode = "";
            try
            {
                byte[] bytes = encoding.GetBytes(value);
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = value;
            }
            return encode;
        }

        /// <summary>
        /// 解密一个Base64方法加密的字符串，采用Encoding.Default字符编码
        /// </summary>
        /// <param name="value">要解密的字符串</param>
        /// <returns>解密后的字符串</returns>
        public static string Base64Decode(string value)
        {
            return Base64Decode(value, Encoding.Default);
        }

        /// <summary>
        /// 解密一个Base64方法加密的字符串
        /// </summary>
        /// <param name="value">要解密的字符串</param>
        /// <param name="encoding">字符编码</param>
        /// <returns>解密后的字符串</returns>
        public static string Base64Decode(string value, Encoding encoding)
        {
            string decode = "";
            try
            {
                byte[] bytes = Convert.FromBase64String(value);
                decode = encoding.GetString(bytes);
            }
            catch
            {
                decode = value;
            }

            return decode;
        }


        /// <summary>
        /// 移位加密
        /// </summary>
        /// <param name="value">需要加密的字符串</param>
        /// <returns></returns>
        public static string ShiftEncrypt(string value)
        {
            string result = "";

            for (int i = 0; i < value.Length; i++)
            {
                int code = (int)value[i] + 3;
                result = result + Convert.ToChar(code);
            }

            return result;
        }

        /// <summary>
        /// 移位解密
        /// </summary>
        /// <param name="value">需要解密的字符串</param>
        /// <returns></returns>
        public static string ShiftDecrypt(string value)
        {
            string result = "";
            for (int i = 0; i < value.Length; i++)
            {
                int code = (int)value[i] - 3;
                result = result + Convert.ToChar(code);
            }
            return result;

        }


        #region RSA加密解密 add by maming 2014-5-22  主要用于flash抽奖传参加密
        //私有key一般在每个项目的common.PriveteKey中
        //公钥存在flash中 ，可以根据项目每个项目采取不同的公钥私钥

        /// <summary>
        /// RSA 的密钥产生 产生私钥 和公钥 
        /// </summary>
        /// <param name="xmlPrivateKey"></param>
        /// <param name="xmlPublicKey"></param>
        public void RSAKey(out string xmlPrivateKey, out string xmlPublicKey)
        {
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            xmlPrivateKey = rsa.ToXmlString(true);
            xmlPublicKey = rsa.ToXmlString(false);
        }
        /// <summary>
        /// RSA加密 返回Base64编码
        /// RSA加密方式的私有秘钥  .net Framework中提供的RSA算法规定，每次加密的字节数，不能超过密钥的长度值减去11 如果用1024 bits RSA密钥 一次可以加密117个字符
        /// </summary>
        /// <param name="publickey"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public string RSAEncrypt(string publickey, string content)
        {
            //publickey = @"<RSAKeyValue><Modulus>5m9m14XH3oqLJ8bNGw9e4rGpXpcktv9MSkHSVFVMjHbfv+SJ5v0ubqQxa5YjLN4vc49z7SVju8s0X4gZ6AzZTn06jzWOgyPRV54Q4I0DCYadWW4Ze3e+BOtwgVU1Og3qHKn8vygoj40J6U85Z/PTJu3hN1m75Zr195ju7g9v4Hk=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(publickey);

            cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(content), false);

            return Convert.ToBase64String(cipherbytes);
        }

        /// <summary>
        /// RSA解密 
        /// </summary>
        /// <param name="privatekey"></param>
        /// <param name="content">Base64编码的加密字符串</param>
        /// <returns></returns>
        public string RSADecrypt(string privatekey, string content)
        {
            //privatekey = @"<RSAKeyValue><Modulus>5m9m14XH3oqLJ8bNGw9e4rGpXpcktv9MSkHSVFVMjHbfv+SJ5v0ubqQxa5YjLN4vc49z7SVju8s0X4gZ6AzZTn06jzWOgyPRV54Q4I0DCYadWW4Ze3e+BOtwgVU1Og3qHKn8vygoj40J6U85Z/PTJu3hN1m75Zr195ju7g9v4Hk=</Modulus><Exponent>AQAB</Exponent><P>/hf2dnK7rNfl3lbqghWcpFdu778hUpIEBixCDL5WiBtpkZdpSw90aERmHJYaW2RGvGRi6zSftLh00KHsPcNUMw==</P><Q>6Cn/jOLrPapDTEp1Fkq+uz++1Do0eeX7HYqi9rY29CqShzCeI7LEYOoSwYuAJ3xA/DuCdQENPSoJ9KFbO4Wsow==</Q><DP>ga1rHIJro8e/yhxjrKYo/nqc5ICQGhrpMNlPkD9n3CjZVPOISkWF7FzUHEzDANeJfkZhcZa21z24aG3rKo5Qnw==</DP><DQ>MNGsCB8rYlMsRZ2ek2pyQwO7h/sZT8y5ilO9wu08Dwnot/7UMiOEQfDWstY3w5XQQHnvC9WFyCfP4h4QBissyw==</DQ><InverseQ>EG02S7SADhH1EVT9DD0Z62Y0uY7gIYvxX/uq+IzKSCwB8M2G7Qv9xgZQaQlLpCaeKbux3Y59hHM+KpamGL19Kg==</InverseQ><D>vmaYHEbPAgOJvaEXQl+t8DQKFT1fudEysTy31LTyXjGu6XiltXXHUuZaa2IPyHgBz0Nd7znwsW/S44iql0Fen1kzKioEL3svANui63O3o5xdDeExVM6zOf1wUUh/oldovPweChyoAdMtUzgvCbJk1sYDJf++Nr0FeNW1RB1XG30=</D></RSAKeyValue>";
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(privatekey);
            cipherbytes = rsa.Decrypt(Convert.FromBase64String(content), false);

            return Encoding.UTF8.GetString(cipherbytes, 0, cipherbytes.Length);
        }

        #endregion

        #region AES加密 解密 add by maming 2014-12-9

        /// <summary>
        /// AES加密解密
        /// </summary>
        public class AES
        {
            /// <summary>
            /// 获取密钥
            /// </summary>
            private static string Key
            {
                //get { return @")O[NB]6,YF}+efcaj{+oESb9d8>Z'e9M"; }
                get { return @"3015XCFetionAES_"; }
            }

            /// <summary>
            /// 获取向量
            /// </summary>
            private static byte[] IV
            {
                //get { return @")L+/~f4,Ir)b$=pkf"; }
                get { return new byte[] { 112, 150, 156, 39, 8, 166, 46, 177, 153, 238, 13, 98, 79, 42, 40, 110 }; }
            }

            /// <summary>
            /// ///
            /// AES加密
            /// </summary>
            /// <param name="plainStr">明文字符串</param>
            /// <returns>密文</returns>
            public static string AESEncrypt(string plainStr)
            {

                byte[] bKey = Encoding.UTF8.GetBytes(Key);
                //byte[] bIV = Encoding.UTF8.GetBytes(IV);
                byte[] bIV = IV;
                byte[] byteArray = Encoding.UTF8.GetBytes(plainStr);

                string encrypt = null;
                Rijndael aes = Rijndael.Create();
                try
                {
                    using (MemoryStream mStream = new MemoryStream())
                    {
                        using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateEncryptor(bKey, bIV), CryptoStreamMode.Write))
                        {
                            cStream.Write(byteArray, 0, byteArray.Length);
                            cStream.FlushFinalBlock();
                            encrypt = Convert.ToBase64String(mStream.ToArray());
                        }
                    }
                }
                catch
                {

                }
                aes.Clear();

                return encrypt;
            }


            /// <summary>
            /// AES加密
            /// </summary>
            /// <param name="plainStr">明文字符串</param>
            /// <param name="returnNull">加密失败时是否返回 null，false 返回 String.Empty</param>
            /// <returns>密文</returns>
            public static string AESEncrypt(string plainStr, bool returnNull)
            {
                string encrypt = AESEncrypt(plainStr);
                return returnNull ? encrypt : (encrypt == null ? String.Empty : encrypt);
            }

            /// <summary>
            /// AES解密
            /// </summary>
            /// <param name="encryptStr">密文字符串</param>
            /// <returns>明文</returns>
            public static string AESDecrypt(string encryptStr)
            {
                byte[] bKey = Encoding.UTF8.GetBytes(Key);
                //byte[] bIV = Encoding.UTF8.GetBytes(IV);
                byte[] bIV = IV;
                byte[] byteArray = Convert.FromBase64String(encryptStr);

                string decrypt = null;
                Rijndael aes = Rijndael.Create();
                try
                {
                    using (MemoryStream mStream = new MemoryStream())
                    {
                        using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateDecryptor(bKey, bIV), CryptoStreamMode.Write))
                        {
                            cStream.Write(byteArray, 0, byteArray.Length);
                            cStream.FlushFinalBlock();
                            decrypt = Encoding.UTF8.GetString(mStream.ToArray());
                        }
                    }
                }
                catch { }
                aes.Clear();

                return decrypt;
            }

            /// <summary>
            /// AES解密
            /// </summary>
            /// <param name="encryptStr">密文字符串</param>
            /// <param name="returnNull">解密失败时是否返回 null，false 返回 String.Empty</param>
            /// <returns>明文</returns>
            public static string AESDecrypt(string encryptStr, bool returnNull)
            {
                string decrypt = AESDecrypt(encryptStr);
                return returnNull ? decrypt : (decrypt == null ? String.Empty : decrypt);
            }

            /// <summary>
            /// 用于产生IV
            /// </summary>
            private void CreateIVKey()
            {
                //分组加密算法
                RijndaelManaged rijndaelCipher = new RijndaelManaged();
                //设置密钥及密钥向量
                //秘钥Key的長度(128 or 192 or 256位)
                string base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes("2015XCFetionAES_"));
                rijndaelCipher.Key = Convert.FromBase64String(base64);//加解密双方约定好密钥：AESKey
                string key = Encoding.UTF8.GetString(rijndaelCipher.Key);
                rijndaelCipher.GenerateIV();
                byte[] keyIv = rijndaelCipher.IV;

            }
        }

        #endregion

        #region DEC加密解密 add by maming 20150706 采用ECB模式，为了兼容JAVA的无向量模式
        //ECB模式：电子密本方式，这是JAVA封装的DES算法的默认模式 向量是任何值都无所谓
        //CBC模式：密文分组链接方式，这是.NET封装的DES算法的默认模式 需要指定Key 和IV
        /// <summary>
        /// 为配合Java的ECB加密模式 ，向量在这个加密中不起作用
        /// </summary>
        /// <param name="key"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DESEncryptForJava(string key, string str)
        {
            byte[] bKey = new byte[8];
            byte[] bkeyInput =Encoding.UTF8.GetBytes(key);
            for (int i = 0; i < bkeyInput.Length && i<bKey.Length; i++)
            {
                bKey[i] = bkeyInput[i];
            }
            //因使用ECB加密方式 所以向量随意并不影响加密结果
            byte[] bIV = cryptIV;

            byte[] bStr = Encoding.UTF8.GetBytes(str);

            try
            {

                DESCryptoServiceProvider desc = new DESCryptoServiceProvider();
                desc.Padding = PaddingMode.PKCS7;//补位
                desc.Mode = CipherMode.ECB;//CipherMode.CBC
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, desc.CreateEncryptor(bKey, bIV), CryptoStreamMode.Write))
                    {
                        cStream.Write(bStr, 0, bStr.Length);
                        cStream.FlushFinalBlock();
                        StringBuilder ret = new StringBuilder();
                        byte[] res = mStream.ToArray();
                        foreach (byte b in res)
                        {
                            ret.AppendFormat("{0:x2}", b);
                        }
                        return ret.ToString();
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion

    }
}
