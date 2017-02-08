using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

/// <summary>
/// Java和Net通用的加密解密方法
/// </summary>
public class NetAndJavaDES
{
    //密匙
    static string _QueryStringKey = "xxxxxxxx";
    
    /// <summary>
    /// 加密算法
    /// </summary>
    public static string Encrypt(string QueryString)
    {
        return DESEnCode(QueryString, _QueryStringKey);
    }


    /// <summary>
    /// 解密算法
    /// </summary>
    public static string Decrypt(string QueryString)
    {
        return DESDeCode(QueryString, _QueryStringKey);
    }

    #region DESEnCode DES加密
    /// <summary>
    /// DES加密
    /// </summary>
    /// <param name="pToEncrypt"></param>
    /// <param name="sKey"></param>
    /// <returns></returns>
    public static string DESEnCode(string pToEncrypt, string sKey)
    {
        try
        {
            // string pToEncrypt1 = HttpContext.Current.Server.UrlEncode(pToEncrypt);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.GetEncoding("UTF-8").GetBytes(pToEncrypt);

            //建立加密对象的密钥和偏移量
            //原文使用ASCIIEncoding.ASCII方法的GetBytes方法
            //使得输入密码必须输入英文文本
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }
        catch (Exception)
        {
            //throw new MsgEx("加密數據失敗");
            return "";
        }
    }
    #endregion

    #region DESDeCode DES解密
    /// <summary>
    /// des 解密
    /// </summary>
    /// <param name="pToDecrypt"></param>
    /// <param name="sKey"></param>
    /// <returns></returns>
    public static string DESDeCode(string pToDecrypt, string sKey)
    {
        try
        {
            //    HttpContext.Current.Response.Write(pToDecrypt + "<br>" + sKey);
            //    HttpContext.Current.Response.End();
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder();

            // return HttpContext.Current.Server.UrlDecode(System.Text.Encoding.Default.GetString(ms.ToArray()));
            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }
        catch (Exception)
        {
            //throw new MsgEx("解密數據失敗");
            return "";
        }
    }
    #endregion
}

#region Java
//import javax.crypto.Cipher;
//import javax.crypto.SecretKey;
//import javax.crypto.SecretKeyFactory;
//import javax.crypto.spec.DESKeySpec;
//import javax.crypto.spec.IvParameterSpec;


//public class Des
//{
//    private byte[] desKey;


//    //解密数据
//    public static String decrypt(String message, String key) throws Exception
//    {

//            byte[] bytesrc = convertHexString(message);
//    Cipher cipher = Cipher.getInstance("DES/CBC/PKCS5Padding");
//    DESKeySpec desKeySpec = new DESKeySpec(key.getBytes("UTF-8"));
//    SecretKeyFactory keyFactory = SecretKeyFactory.getInstance("DES");
//    SecretKey secretKey = keyFactory.generateSecret(desKeySpec);
//    IvParameterSpec iv = new IvParameterSpec(key.getBytes("UTF-8"));

//    cipher.init(Cipher.DECRYPT_MODE, secretKey, iv);

//            byte[] retByte = cipher.doFinal(bytesrc);
//            return new String(retByte);
//}

//public static byte[] encrypt(String message, String key)
//            throws Exception
//{
//    Cipher cipher = Cipher.getInstance("DES/CBC/PKCS5Padding");

//    DESKeySpec desKeySpec = new DESKeySpec(key.getBytes("UTF-8"));

//    SecretKeyFactory keyFactory = SecretKeyFactory.getInstance("DES");
//    SecretKey secretKey = keyFactory.generateSecret(desKeySpec);
//    IvParameterSpec iv = new IvParameterSpec(key.getBytes("UTF-8"));
//    cipher.init(Cipher.ENCRYPT_MODE, secretKey, iv);

//        return cipher.doFinal(message.getBytes("UTF-8"));
//}

//public static byte[] convertHexString(String ss)
//{
//    byte digest[] = new byte[ss.length() / 2];
//    for (int i = 0; i < digest.length; i++)
//    {
//        String byteString = ss.substring(2 * i, 2 * i + 2);
//        int byteValue = Integer.parseInt(byteString, 16);
//        digest[i] = (byte)byteValue;
//    }

//    return digest;
//}


//public static void main(String[] args) throws Exception
//{
//    String key = "11111111";
//    String value="aa";
//    String jiami=java.net.URLEncoder.encode(value, "utf-8").toLowerCase();

//    System.out.println("加密数据:"+jiami);
//    String a=toHexString(encrypt(jiami, key)).toUpperCase();


//    System.out.println("加密后的数据为:"+a);
//    String b=java.net.URLDecoder.decode(decrypt(a,key), "utf-8") ;
//    System.out.println("解密后的数据:"+b);

//}


//public static String toHexString(byte b[])
//{
//    StringBuffer hexString = new StringBuffer();
//    for (int i = 0; i < b.length; i++)
//    {
//        String plainText = Integer.toHexString(0xff & b[i]);
//        if (plainText.length() < 2)
//            plainText = "0" + plainText;
//        hexString.append(plainText);
//    }

//    return hexString.toString();
//}

// }
#endregion
