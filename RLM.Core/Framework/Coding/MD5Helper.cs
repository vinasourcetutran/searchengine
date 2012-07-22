using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace RLM.Core.Framework.Coding
{
    public class MD5Helper
    {
        /// <summary>
        /// Return md5 encoded string of input string
        /// </summary>
        /// <param name="pwd">input string</param>
        /// <returns></returns>
        public static string GetMd5Hash(string input)
        {
            MD5 md5Hasher = MD5.Create();

            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
        /// <summary>
        /// Compare plain text string and md5 encoded string
        /// </summary>
        /// <param name="input">plain text string</param>
        /// <param name="hash">md5 encode string</param>
        /// <returns>zero: equal, nozero: not equal </returns>
        public static bool Md5HashEqual(string input, string hash)
        {
            string hashOfInput = GetMd5Hash(input);

            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return (0 == comparer.Compare(hashOfInput, hash));
        }
    }
}
