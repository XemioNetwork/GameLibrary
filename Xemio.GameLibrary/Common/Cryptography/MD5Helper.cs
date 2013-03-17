using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Xemio.GameLibrary.Common.Cryptography
{
    public static class MD5Helper
    {
        #region Methods
        /// <summary>
        /// Computes the md5 hash for the specified data array.
        /// </summary>
        /// <param name="data">The data.</param>
        public static string ComputeHash(byte[] data)
        {
            MD5 md5 = MD5.Create();

            byte[] hash = md5.ComputeHash(data);
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                builder.Append(hash[i].ToString("X2"));
            }

            string result = builder.ToString();
            result = result.ToLower();

            return result;
        }
        /// <summary>
        /// Computes the MD5 hash for the specified file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public static string ComputeHash(string fileName)
        {
            return MD5Helper.ComputeHash(File.ReadAllBytes(fileName));
        }
        #endregion
    }
}
