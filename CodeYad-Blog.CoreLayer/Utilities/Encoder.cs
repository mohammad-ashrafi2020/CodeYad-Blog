using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeYad_Blog.CoreLayer.Utilities
{
    public static class Encoder
    {
        public static string EncodeToMd5(this string text) //Encrypt using MD5   
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            var originalBytes = Encoding.Default.GetBytes(text);
            var encodedBytes = md5.ComputeHash(originalBytes);
            return BitConverter.ToString(encodedBytes);
        }
        /// <summary>
        /// If the first parameter was equal to the second parameter Return True (in md5 type)
        /// </summary>
        /// <param name="firstParam">This Is a Md5 Text</param>
        /// <param name="secondParam">This is a String Text</param>
        /// <returns></returns>
        public static bool IsCompareMd5Text(this string md5Text, string secondParam) //Encrypt using MD5   
        {
            secondParam = secondParam.EncodeToMd5();
            return md5Text == secondParam;
        }
        public static bool IsMD5(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                return false;
            }
            return Regex.IsMatch(input, "^[0-9a-fA-F]{32}$", RegexOptions.Compiled);
        }

    }
}
