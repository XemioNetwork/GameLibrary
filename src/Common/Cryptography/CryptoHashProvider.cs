using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Xemio.GameLibrary.Common.Cryptography
{
    public static class CryptoHashProvider
    {
        #region Methods
        /// <summary>
        /// Computes the hash for the specified data array using the specified hash algorithm.
        /// </summary>
        /// <param name="data">The data.</param>
        public static string ComputeHash<T>(byte[] data) where T : HashAlgorithm
        {
            T hashAlgorithm = (T)HashAlgorithm.Create(typeof(T).ToString());

            byte[] hash = hashAlgorithm.ComputeHash(data);
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                builder.Append(hash[i].ToString("X2"));
            }

            return builder.ToString().ToLower();
        }
        /// <summary>
        /// Computes the hash for the specified file using the specified hash algorithm.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static string ComputeHash<T>(string fileName) where T : HashAlgorithm
        {
            return CryptoHashProvider.ComputeHash<T>(File.ReadAllBytes(fileName));
        }
        #endregion
    }
}
