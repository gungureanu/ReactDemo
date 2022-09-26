using BusinessLogic.IBusinessLogic;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLogic.BLImplementation
{
    public class CryptographyService : ICryptographyService
    {
        #region Cryptography Functions
        //private const String DEFAULTKEY = "OERGRZWHM2018";
        private const String DEFAULTKEY = "REACTDEMO@2022";
        private String mstrErrorString = String.Empty;
        private String mstrOutputString = String.Empty;

        static Byte[] GetMD5Hash(String strKey)
        {
            MD5CryptoServiceProvider objHashMD5 = null;
            Byte[] objPwdhash;
            try
            {
                objHashMD5 = new MD5CryptoServiceProvider();
                objPwdhash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strKey));

            }
            catch (Exception ex)
            {
                //  throw new Exception("MD5_ERROR Crypto.Vb::GetMD5Hash() Unable to generate MD5 hash.");
                throw ex;
            }
            finally
            {
                if (objHashMD5 != null)
                {
                    objHashMD5.Clear();
                }
            }
            return objPwdhash;
        }
        public String Encrypt(String strDecryptedString)
        {
            try
            {
                return EncryptWithKey(strDecryptedString, DEFAULTKEY).Replace("+", "^^").Replace("=", "~~");
            }
            catch (Exception ex)
            {

                throw ex;
            }
         
        }
        public String Decrypt(String strEncryptedString)
        {
            try
            {
                return DecryptWithKey(strEncryptedString.Replace("^^", "+").Replace("~~", "="), DEFAULTKEY);
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }
        public String EncryptWithKey(String strDecryptedString, String strKey)
        {
            String strEncrypted = String.Empty;
            TripleDESCryptoServiceProvider objDES = null;
            Byte[] objBuff;
            try
            {
                objDES = new TripleDESCryptoServiceProvider();
                if (strKey.Length > 0)
                {
                    objDES.Key = GetMD5Hash(strKey);
                }
                objDES.Mode = CipherMode.ECB;
                objBuff = ASCIIEncoding.ASCII.GetBytes(strDecryptedString);

                strEncrypted = Convert.ToBase64String(objDES.CreateEncryptor().TransformFinalBlock(objBuff, 0, objBuff.Length));

            }
            catch (Exception ex)
            {
                // throw new Exception("CRYPTO_ERROR Crypto.cs::EncryptWithKey() Error encrypting string.");
                throw ex;
            }
            finally
            {
                if (objDES != null)
                {
                    objDES.Clear();
                }
            }
            return strEncrypted;

        }
        public String DecryptWithKey(String strEncryptedString, String strKey)
        {
            String strDecrypted = String.Empty;
            TripleDESCryptoServiceProvider objDES = null;
            Byte[] objBuff;
            try
            {
                if (strKey.Length == 0)
                {
                    throw new Exception("Invalid key supplied. Unable to decrypt.");
                }
                objDES = new TripleDESCryptoServiceProvider();
                if (strKey.Length > 0)
                {
                    objDES.Key = GetMD5Hash(strKey);
                }
                objDES.Mode = CipherMode.ECB;
                objBuff = Convert.FromBase64String(strEncryptedString);
                strDecrypted = ASCIIEncoding.ASCII.GetString(objDES.CreateDecryptor().TransformFinalBlock(objBuff, 0, objBuff.Length));
            }
            catch (Exception ex)
            {
                //throw new Exception("CRYPTO_ERROR Crypto.cs::DecryptWithKey() Error decrypting string.");
                throw ex;
            }
            finally
            {
                if (objDES != null)
                {
                    objDES.Clear();
                }
            }
            return strDecrypted;
        }

        #endregion encryption password
    }
}
