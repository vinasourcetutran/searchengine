using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace RLM.Core.Framework.Utility
{
    public class EncodeHelper
    {
        #region Md5
        public static string GetMd5Hash(string pwd)
        {
            MD5 md5Hasher = MD5.Create();

            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(pwd));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
        public static bool Md5HashEqual(string input, string hash)
        {
            string hashOfInput = GetMd5Hash(input);

            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return (0 == comparer.Compare(hashOfInput, hash));
        }
        #endregion
    }
}
