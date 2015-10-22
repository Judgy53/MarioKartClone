using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

public class EncryptDecrypt// : MonoBehaviour 
{
    private static readonly string PrivateKey = SystemInfo.deviceUniqueIdentifier.Replace("-", string.Empty);

    /*void Start()
    {
        string test = "Ceci est un test";
        Debug.Log(test);
        string encrypted = EncryptData(test);
        Debug.Log(encrypted);
        string decrypted = DecryptData(encrypted);
        Debug.Log (decrypted);
    }
    */

    #region encrypt_decrypt
    public static string EncryptData(string toEncrypt)
    {
#if UNITY_WP8
		return toEncrypt;
#else
        byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);
        RijndaelManaged rDel = CreateRijndaelManaged();
        ICryptoTransform cTransform = rDel.CreateEncryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
#endif
    }

    public static string DecryptData(string toDecrypt)
    {
#if UNITY_WP8
		return toDecrypt;
#else
        byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);
        RijndaelManaged rDel = CreateRijndaelManaged();
        ICryptoTransform cTransform = rDel.CreateDecryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
        return Encoding.UTF8.GetString(resultArray);
#endif
    }

#if !UNITY_WP8
    private static RijndaelManaged CreateRijndaelManaged()
    {
        byte[] keyArray = Encoding.UTF8.GetBytes(PrivateKey);
        var result = new RijndaelManaged();

        var newKeysArray = new byte[16];
        Array.Copy(keyArray, 0, newKeysArray, 0, 16);

        result.Key = newKeysArray;
        result.Mode = CipherMode.ECB;
        result.Padding = PaddingMode.PKCS7;
        return result;
    }
#endif
    #endregion
}
