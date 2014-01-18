using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Common.Randomization
{
    public class CryptoRandom : IRandom
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CryptoRandom"/> class.
        /// </summary>
        public CryptoRandom()
        {
            this._cryptoProvider = new RNGCryptoServiceProvider();
        }
        #endregion

        #region Fields
        private readonly RNGCryptoServiceProvider _cryptoProvider;
        #endregion
        
        #region Implementation of IRandom
        /// <summary>
        /// Returns a random integer value.
        /// </summary>
        public int Next()
        {
            return BitConverter.ToInt32(this.NextBytes(4), 0);
        }
        /// <summary>
        /// Returns a random integer value, that is smaller than the specified maximum.
        /// </summary>
        /// <param name="max">The max.</param>
        public int Next(int max)
        {
            int value = this.Next();
            double multiplier = max / (double)int.MaxValue;

            return (int)(value * multiplier);
        }
        /// <summary>
        /// Returns a random integer value, that is within the defined range.
        /// </summary>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        public int Next(int min, int max)
        {
            return min + this.Next(max - min);
        }
        /// <summary>
        /// Returns a random double value between 0.0 and 1.0.
        /// </summary>
        public double NextDouble()
        {
            const double multiplier = 1.0 / double.MaxValue;
            double value = System.Math.Abs(BitConverter.ToDouble(this.NextBytes(8), 0));

            return value * multiplier;
        }
        /// <summary>
        /// Returns a random float value between 0.0f and 1.0f.
        /// </summary>
        public float NextFloat()
        {
            return (float)this.NextDouble();
        }
        /// <summary>
        /// Returns a random boolean value.
        /// </summary>
        public bool NextBoolean()
        {
            return this.NextBoolean(0.5);
        }
        /// <summary>
        /// Returns a random boolean value that has a chance of the specified probability to be true.
        /// </summary>
        /// <param name="probability">The probability between 0.0 and 1.0.</param>
        public bool NextBoolean(double probability)
        {
            return this.NextDouble() < probability;
        }
        /// <summary>
        /// Returns a random byte array.
        /// </summary>
        /// <param name="count">The count.</param>
        public byte[] NextBytes(int count)
        {
            var result = new byte[count];
            this._cryptoProvider.GetBytes(result);

            return result;
        }
        #endregion
    }
}
