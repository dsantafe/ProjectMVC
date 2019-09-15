using System;
using System.Text;

namespace ProjectMVC.Logica.BL
{
    public class Utils
    {
        /// <summary>
        /// Base64Encode
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Base64Decode
        /// </summary>
        /// <param name="base64EndodedData"></param>
        /// <returns></returns>
        public string Base64Decode(string base64EndodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EndodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
