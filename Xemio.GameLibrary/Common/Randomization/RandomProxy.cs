using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Common.Randomization
{
    public class RandomProxy : IRandom
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RandomProxy"/> class.
        /// </summary>
        public RandomProxy() : this((int)DateTime.Now.Ticks)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="RandomProxy"/> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public RandomProxy(string seed) : this(seed.GetHashCode())
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="RandomProxy"/> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public RandomProxy(int seed)
        {
            this._random = new System.Random(seed);
        }
        #endregion

        #region Fields
        protected System.Random _random;
        #endregion

        #region IRandom Member
        /// <summary>
        /// Returns a random integer value.
        /// </summary>
        /// <returns></returns>
        public int Next()
        {
            return this.Next(int.MinValue, int.MaxValue);
        }
        /// <summary>
        /// Returns a random integer value, that is smaller than the specified maximum.
        /// </summary>
        /// <param name="max">The max.</param>
        /// <returns></returns>
        public int Next(int max)
        {
            return this.Next(0, max);
        }
        /// <summary>
        /// Returns a random integer value, that is within the defined range.
        /// </summary>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns></returns>
        public int Next(int min, int max)
        {
            return this._random.Next(min, max);
        }
        /// <summary>
        /// Returns a random double value between 0.0 and 1.0.
        /// </summary>
        /// <returns></returns>
        public double NextDouble()
        {
            return this._random.NextDouble();
        }
        /// <summary>
        /// Returns a random float value between 0.0f and 1.0f.
        /// </summary>
        /// <returns></returns>
        public float NextFloat()
        {
            return (float)this.NextDouble();
        }
        /// <summary>
        /// Returns a random boolean value.
        /// </summary>
        /// <returns></returns>
        public bool NextBoolean()
        {
            return this.NextBoolean(0.5);
        }
        /// <summary>
        /// Returns a random boolean value that has a chance of the specified probability to be true.
        /// </summary>
        /// <param name="probability">The probability between 0.0 and 1.0.</param>
        /// <returns></returns>
        public bool NextBoolean(double probability)
        {
            return this.NextDouble() <= probability;
        }
        /// <summary>
        /// Returns a random byte array.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public byte[] NextBytes(int count)
        {
            byte[] buffer = new byte[count];
            this._random.NextBytes(buffer);

            return buffer;
        }
        #endregion
    }
}
