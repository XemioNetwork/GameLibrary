using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Xemio.GameLibrary.Common.Cryptography
{
    public static class CrytographyHelper
    {
        #region Methods
        /// <summary>
        /// Computes the md5 hash for the specified data array.
        /// </summary>
        /// <param name="algorithm">The algorithm.</param>
        /// <param name="data">The data.</param>
        public static string ComputeHash(HashAlgorithm algorithm, byte[] data)
        {
            byte[] hash = algorithm.ComputeHash(data);
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
        /// <param name="algorithm">The algorithm.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static string ComputeHash(HashAlgorithm algorithm, string fileName)
        {
            return CrytographyHelper.ComputeHash(algorithm, File.ReadAllBytes(fileName));
        }
        #endregion
    }
}
