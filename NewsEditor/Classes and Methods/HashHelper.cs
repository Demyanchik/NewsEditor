using System.Text;
using System.Security.Cryptography;

namespace NewsEditor.Methods
{
    public class HashHelper
    {
        static string ByteArrayToString(byte[] arrInput)
        {
            int i;

            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length; i++)
            {
                //Формируем строку в шестнадцатеричном виде
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }
        public static string GetHashCode(string text)
        {
            //Create a byte array from source data.
            byte[] tmpSource = UTF8Encoding.UTF8.GetBytes(text);
            //Compute hash based on source data.
            byte[] tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            return ByteArrayToString(tmpHash);
        }
    }
}
