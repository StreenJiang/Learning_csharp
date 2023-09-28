using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Learning_csharp {
    internal class Encryption {
        public void Run() {
            // 不可逆加密
            // 1. MD5加密算法
            // <1> 规则：
            //   1) MD5公开算法，hash散列计算；任何语言实现后都一样，通用的。本质是通过hash散列算法计算，可对数据信息进行加密，也可对文件生成文件的摘要。
            //   2) 相同的源数据信息加密后的结果是一样的
            //   3) 不同长度的内容加密后结果都是32位的
            //   4) 源数据信息差别很小，加密后结果差别很大
            //   5) 不管文件多大，生成的摘要都是32位的
            //   6) 文件内容有一点改动，结果变化非常大
            //   7) 文件内容不变，名字变了，结果不变
            // <2> 作用：
            //   1) 防篡改：发送文档时加上文档的MD5摘要，一旦文档内容被改，则新的摘要与原始摘要不一致，说明文件内容有改动
            //              数据与MD5密文一起发送，接收端对数据本身再做一次MD5加密，与密文对比，一致则说明数据没有被篡改（？篡改数据的时候再把密文一起改了呢）
            //   2) 密码保护：数据库只存密文，用户输入的时候通过MD5加密后再对比，即可保证不存真实密码也能达到密码验证的需求
            //   3) 防止抵赖：用作数字签名。将文件内容做MD5摘要，然后交由第三方进行保障，那么这个内容就是属于作者的，无法抵赖
            // <3> 争议：
            //   实质上，MD5只是一种针对源数据的哈希算法（hash），翻译过来是散列算法，是一类把任意数据转换为定长（或给定长度）的数据的算法统称。
            //   MD5可以把给定数据通过hash算法计算后得到一个散列值，不可逆。部分人认为不可逆的不算加密。
            string testStr = "this is a test string, used for MD5 encryption.";
            Console.WriteLine("data before MD5 encrypting: {0}", testStr);
            byte[] bytesAfterEncrypting = MD5.HashData(Encoding.UTF8.GetBytes(testStr));
            //Console.WriteLine("MD5 bytesAfterEncrypting: {0}", BitConverter.ToString(bytesAfterEncrypting).Replace("-", ""));
            Console.WriteLine("MD5 bytesAfterEncrypting: {0}", Convert.ToBase64String(bytesAfterEncrypting, 0, bytesAfterEncrypting.Length));

            // 对称可逆加密
            // 1. DES
            // <1> 规则：
            //   1) 加密过程可逆；有一组成套的加密解密算法，且算法完全公开，不同编程语言的应用也完全相同
            //   2) 有一组加密解密Key - 秘钥，且秘钥完全相同。知道加密解密算法，无法推算出秘钥
            //   3) 加密和解密需要秘钥，且同一个数据，加密解密时使用的秘钥相同
            // <2> 特点：
            //   1) 加密解密速度超级快
            //   2) 容易泄露秘钥，因为加密解密使用的秘钥相同。相对于非对称可逆加密，安全性低
            //   3) 原文长，加密后的密文也长
            // <3> 作用：
            //   1) 互联网传输加密数据
            //   2) 防止抵赖
            //   3) JWT鉴权授权
            // 根据最新的库文件发现，DES已经不在库中了，查了资料后发现，大概率是被AES取代了，用法也差不多
            string AESKey = "this is a key for AES encryption. just a test key.";
            string testStr2 = "this is a test string, used for AES encryption.";
            Console.WriteLine("data before AES encrypting: {0}", testStr2);
            string testStr2AfterEncrypting = Encrypt(testStr2, AESKey);
            Console.WriteLine("AES testStr2AfterEncrypting: {0}", testStr2AfterEncrypting);
            string testStr2AfterRestoring = Decrypt(testStr2AfterEncrypting, AESKey);
            Console.WriteLine("AES testStr2AfterRestoring: {0}", testStr2AfterRestoring);

            // 非对称可逆加密
            // 1. RSA
            // <1> 规则：
            //   1) 有一套公开的加密解密算法
            //   2) 有一组加密解密Key，但加密Key和解密Key不一样。无法通过加密算法推算Key。具有私有Key和公有Key之分。
            //   *  在其他实现中，是私有Key加密，公有Key解密，这个是为了能够做签名。在C#中的视线，提供的API是公有Key加密，私有Key解密。
            // <2> 特点：
            //   1) 原文短，加密后密文很长
            //   2) 加密速度相对较慢
            //   3) 安全性超强，因为私钥公钥不一样
            //   4) 私钥里面是包含公钥的（这点老师居然不写在这里？？？？）
            // <3> 作用：
            //   1) 数据的安全传输
            //   2) 直接支持的加密解密API，还无法完成签名（？？没看懂）
            //   *  签名：实际在数字签名的时候，是需要私钥加密，公钥解密的（反正没看懂，这老师说到啥？）
            // <4> 数字签名：
            //   1) 私钥签名
            //   2) 公钥验签（我的理解是，私钥里既然包括公钥的信息，那么私钥做的签名，公钥虽然无法解密，但是可以做到验证的效果）
            // <5> 使用场景（简单应用。这条是自己写的）：
            //   1) 公钥加密，私钥解密：数据加密
            //   2) 私钥加密，公钥验证：数字签名
            // PEM秘钥
            RSAKey key = new RSAKey();
            RSAGeneratePEMKey(1024, ref key);
            Console.WriteLine("RSA private key: {0}", key.PrivateKey);
            Console.WriteLine("RSA public key: {0}", key.PublicKey);
            string dataBeforeEncrypting = "this is a test string for RSA testing.";
            Console.WriteLine("RAS encryption dataBeforeEncrypting: {0}", dataBeforeEncrypting);
            // 场景1：公钥加密，私钥解密
            string dataAfterEncrypting = RSAEncrypt(dataBeforeEncrypting, key.PublicKey);
            Console.WriteLine("RSA encryption dataAfterEncrypting: {0}", dataAfterEncrypting);
            string dataAfterDecrypting = RSADecrypt(dataAfterEncrypting, key.PrivateKey);
            Console.WriteLine("RSA decryption dataAfterDecrypting: {0}", dataAfterDecrypting);


        }

        static string Encrypt(string plainText, string key) {  
            byte[] encryptedData;  
            using (Aes aes = Aes.Create()) {  
                aes.Key = StringToBytes(key);  
                aes.IV = StringToBytes(key);  
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);  
                using (MemoryStream ms = new MemoryStream()) {  
                    ms.Write(aes.IV, 0, aes.IV.Length);  
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write)) {  
                        using (StreamWriter sw = new StreamWriter(cs)) {  
                            sw.Write(plainText);  
                        }  
                    }  
                    encryptedData = ms.ToArray();  
                }  
            }  
            return Convert.ToBase64String(encryptedData, 0, encryptedData.Length);  
        }  
      
        static string Decrypt(string cipherText, string key) {  
            string plaintext;  
            using (Aes aes = Aes.Create()) {  
                aes.Key = StringToBytes(key);  
                aes.IV = StringToBytes(key);  
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);  
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(cipherText))) {  
                    ms.Read(aes.IV, 0, aes.IV.Length);  
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read)) {  
                        using (StreamReader sr = new StreamReader(cs)) {  
                            plaintext = sr.ReadToEnd();  
                        }  
                    }  
                }  
            }  
            return plaintext;  
        }

        static byte[] StringToBytes(string str) {  
            byte[] bytes = new byte[16]; // 16字节的字节数组  
            int length = str.Length;  
      
            // 如果字符串长度超过16字节，则丢弃超长部分  
            if (length > 16) {  
                length = 16;  
            }  
      
            // 将字符串转换为字节并复制到字节数组中  
            Encoding.UTF8.GetBytes(str.Substring(0, length), 0, length, bytes, 0);  
      
            // 如果字符串长度不够16字节，则用全0补齐
            for (int i = length; i < 16; i++) {  
                bytes[i] = 0;  
            }  
      
            return bytes;  
        }  

        // XML秘钥
        public static void RSAGeneratePEMKey(int keySize, ref RSAKey key) {
            using (RSACryptoServiceProvider rsaSP = new RSACryptoServiceProvider(keySize)) {
                key.PrivateKey = rsaSP.ToXmlString(true);
                key.PublicKey =rsaSP.ToXmlString(false);
            }
        }
        // RSA加密
        public static string RSAEncrypt(string sourceStr, string keyStr) {
            string resultStr;
            using (RSA rsa = RSA.Create()) {
                rsa.FromXmlString(keyStr);
                // 将源数据转换成字节数组
                byte[] dataBytes = Encoding.UTF8.GetBytes(sourceStr);
                // 加密操作
                resultStr = Convert.ToBase64String(rsa.Encrypt(dataBytes, RSAEncryptionPadding.Pkcs1));
            }
            return resultStr;
        }
        // RSA解密
        public static string RSADecrypt(string sourceStr, string keyStr) {
            string resultStr;
            using (RSA rsa = RSA.Create()) {
                rsa.FromXmlString(keyStr);
                // 将源数据转换成字节数组（这里有个大坑：加密的时候先通过UTF-8转码，然后再转成base64，所以这里应该先转base64再用UTF-8转回去）
                byte[] dataBytes = Convert.FromBase64String(sourceStr);
                // 解密操作
                resultStr = Encoding.UTF8.GetString(rsa.Decrypt(dataBytes, RSAEncryptionPadding.Pkcs1));
            }
            return resultStr;
        }
    }

    public class RSAKey {
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
    }

    
}
