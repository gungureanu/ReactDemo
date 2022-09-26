using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.IBusinessLogic
{
    public interface ICryptographyService
    {
        String Encrypt(String strDecryptedString);
        String Decrypt(String strEncryptedString);
        String EncryptWithKey(String strDecryptedString, String strKey);
        String DecryptWithKey(String strEncryptedString, String strKey);
    }
}
