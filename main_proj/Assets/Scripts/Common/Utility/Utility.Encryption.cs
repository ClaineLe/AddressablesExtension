using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace com.halo.framework
{
    namespace common
    {
        public static partial class Utility
        {
            /// <summary>
            /// 加密解密相关的实用函数。
            /// </summary>
            public static class Encryption
            {
                internal const int QuickEncryptLength = 220;

                /// <summary>
                /// 将 bytes 使用 code 做异或运算的快速版本。
                /// </summary>
                /// <param name="bytes">原始二进制流。</param>
                /// <param name="code">异或二进制流。</param>
                /// <returns>异或后的二进制流。</returns>
                public static byte[] GetQuickXorBytes(byte[] bytes, byte[] code)
                {
                    return GetXorBytes(bytes, 0, QuickEncryptLength, code);
                }

                /// <summary>
                /// 将 bytes 使用 code 做异或运算的快速版本。此方法将复用并改写传入的 bytes 作为返回值，而不额外分配内存空间。
                /// </summary>
                /// <param name="bytes">原始及异或后的二进制流。</param>
                /// <param name="code">异或二进制流。</param>
                public static void GetQuickSelfXorBytes(byte[] bytes, byte[] code)
                {
                    GetSelfXorBytes(bytes, 0, QuickEncryptLength, code);
                }

                /// <summary>
                /// 将 bytes 使用 code 做异或运算。
                /// </summary>
                /// <param name="bytes">原始二进制流。</param>
                /// <param name="code">异或二进制流。</param>
                /// <returns>异或后的二进制流。</returns>
                public static byte[] GetXorBytes(byte[] bytes, byte[] code)
                {
                    if (bytes == null)
                    {
                        return null;
                    }

                    return GetXorBytes(bytes, 0, bytes.Length, code);
                }

                /// <summary>
                /// 将 bytes 使用 code 做异或运算。此方法将复用并改写传入的 bytes 作为返回值，而不额外分配内存空间。
                /// </summary>
                /// <param name="bytes">原始及异或后的二进制流。</param>
                /// <param name="code">异或二进制流。</param>
                public static void GetSelfXorBytes(byte[] bytes, byte[] code)
                {
                    if (bytes == null)
                    {
                        return;
                    }

                    GetSelfXorBytes(bytes, 0, bytes.Length, code);
                }

                /// <summary>
                /// 将 bytes 使用 code 做异或运算。
                /// </summary>
                /// <param name="bytes">原始二进制流。</param>
                /// <param name="startIndex">异或计算的开始位置。</param>
                /// <param name="length">异或计算长度，若小于 0，则计算整个二进制流。</param>
                /// <param name="code">异或二进制流。</param>
                /// <returns>异或后的二进制流。</returns>
                public static byte[] GetXorBytes(byte[] bytes, int startIndex, int length, byte[] code)
                {
                    if (bytes == null)
                    {
                        return null;
                    }

                    int bytesLength = bytes.Length;
                    byte[] results = new byte[bytesLength];
                    Array.Copy(bytes, 0, results, 0, bytesLength);
                    GetSelfXorBytes(results, startIndex, length, code);
                    return results;
                }

                /// <summary>
                /// 将 bytes 使用 code 做异或运算。此方法将复用并改写传入的 bytes 作为返回值，而不额外分配内存空间。
                /// </summary>
                /// <param name="bytes">原始及异或后的二进制流。</param>
                /// <param name="startIndex">异或计算的开始位置。</param>
                /// <param name="length">异或计算长度。</param>
                /// <param name="code">异或二进制流。</param>
                public static void GetSelfXorBytes(byte[] bytes, int startIndex, int length, byte[] code)
                {
                    if (bytes == null)
                    {
                        return;
                    }

                    if (code == null)
                    {
                        throw new Exception("Code is invalid.");
                    }

                    int codeLength = code.Length;
                    if (codeLength <= 0)
                    {
                        throw new Exception("Code length is invalid.");
                    }

                    if (startIndex < 0 || length < 0 || startIndex + length > bytes.Length)
                    {
                        throw new Exception("Start index or length is invalid.");
                    }

                    int codeIndex = startIndex % codeLength;
                    for (int i = startIndex; i < length; i++)
                    {
                        bytes[i] ^= code[codeIndex++];
                        codeIndex %= codeLength;
                    }
                }


                /// <summary>
                /// AES加密 
                /// </summary>
                /// <param name="text">加密字符</param>
                /// <param name="password">加密的密码</param>
                /// <param name="iv">密钥</param>
                /// <returns></returns>
                public static byte[] AESEncrypt(byte[] sourceBytes, string password, string iv)
                {
                    var rijndaelCipher = new RijndaelManaged
                    {
                        Mode = CipherMode.CBC,
                        Padding = PaddingMode.PKCS7,
                        KeySize = 128,
                        BlockSize = 128
                    };
                    var pwdBytes = System.Text.Encoding.Default.GetBytes(password);
                    var keyBytes = new byte[16];
                    var len = pwdBytes.Length;
                    if (len > keyBytes.Length) len = keyBytes.Length;
                    System.Array.Copy(pwdBytes, keyBytes, len);
                    rijndaelCipher.Key = keyBytes;
                    var ivBytes = System.Text.Encoding.Default.GetBytes(iv);
                    rijndaelCipher.IV = ivBytes;
                    var transform = rijndaelCipher.CreateEncryptor();
                    var plainText = sourceBytes;
                    var cipherBytes = transform.TransformFinalBlock(plainText, 0, plainText.Length);
                    return cipherBytes;
                }

                /// <summary>
                /// AES解密
                /// </summary>
                /// <param name="text"></param>
                /// <param name="password"></param>
                /// <param name="iv"></param>
                /// <returns></returns>
                public static byte[] AESDecrypt(byte[] encryptedBytes, string password, string iv)
                {
                    var rijndaelCipher = new RijndaelManaged
                    {
                        Mode = CipherMode.CBC,
                        Padding = PaddingMode.PKCS7,
                        KeySize = 128,
                        BlockSize = 128
                    };
                    var encryptedData = encryptedBytes;
                    var pwdBytes = System.Text.Encoding.Default.GetBytes(password);
                    var keyBytes = new byte[16];
                    var len = pwdBytes.Length;
                    if (len > keyBytes.Length) len = keyBytes.Length;
                    System.Array.Copy(pwdBytes, keyBytes, len);
                    rijndaelCipher.Key = keyBytes;
                    var ivBytes = System.Text.Encoding.Default.GetBytes(iv);
                    rijndaelCipher.IV = ivBytes;
                    var transform = rijndaelCipher.CreateDecryptor();
                    var plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                    return plainText;
                }

                /// <summary>
                /// 异或加密
                /// </summary>
                /// <param name="content"></param>
                /// <param name="password"></param>
                /// <returns></returns>
                public static byte[] XOREncrypt(byte[] sourceByte, string password)
                {
                    var encryptedContent = string.Empty;
                    if (string.IsNullOrEmpty(password))
                    {
                        FrameworkLog.Error("传入密码为空,无法加密");
                        return sourceByte;
                    }
                    var sourceContentArray = sourceByte;
                    var passwordArray = Encoding.Default.GetBytes(password);
                    var passwordLength = passwordArray.Length;
                    for (var i = 0; i < sourceContentArray.Length; i++)
                    {
                        var p = passwordArray[i % passwordLength];
                        sourceContentArray[i] = (byte)(sourceContentArray[i] ^ p);
                    }
                    return sourceContentArray;
                }

                /// <summary>
                /// 异或解密
                /// </summary>
                /// <param name="content"></param>
                /// <param name="password"></param>
                /// <returns></returns>
                public static byte[] XORDecrypt(byte[] encryptedBytes, string password)
                {
                    var sourceContent = string.Empty;
                    if (string.IsNullOrEmpty(password))
                    {
                        FrameworkLog.Error("传入密码为空,无法解密");
                        return encryptedBytes;
                    }
                    var passwordArray = Encoding.Default.GetBytes(password);
                    var passwordLength = passwordArray.Length;
                    for (var i = 0; i < encryptedBytes.Length; i++)
                    {
                        var p = passwordArray[i % passwordLength];
                        encryptedBytes[i] = (byte)(encryptedBytes[i] ^ p);
                    }
                    return encryptedBytes;
                }
            }
        }
    }
}