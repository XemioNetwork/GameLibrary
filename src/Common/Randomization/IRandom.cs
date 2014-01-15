using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Common.Randomization
{
    public interface IRandom
    {
        /// <summary>
        /// Returns a random integer value.
        /// </summary>
        /// <returns></returns>
        int Next();
        /// <summary>
        /// Returns a random integer value, that is smaller than the specified maximum.
        /// </summary>
        /// <param name="max">The max.</param>
        /// <returns></returns>
        int Next(int max);
        /// <summary>
        /// Returns a random integer value, that is within the defined range.
        /// </summary>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns></returns>
        int Next(int min, int max);
        /// <summary>
        /// Returns a random double value between 0.0 and 1.0.
        /// </summary>
        /// <returns></returns>
        double NextDouble();
        /// <summary>
        /// Returns a random float value between 0.0f and 1.0f.
        /// </summary>
        /// <returns></returns>
        float NextFloat();
        /// <summary>
        /// Returns a random boolean value.
        /// </summary>
        /// <returns></returns>
        bool NextBoolean();
        /// <summary>
        /// Returns a random boolean value that has a chance of the specified probability to be true.
        /// </summary>
        /// <param name="probability">The probability between 0.0 and 1.0.</param>
        /// <returns></returns>
        bool NextBoolean(double probability);
        /// <summary>
        /// Returns a random byte array.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        byte[] NextBytes(int count);
    }
}
