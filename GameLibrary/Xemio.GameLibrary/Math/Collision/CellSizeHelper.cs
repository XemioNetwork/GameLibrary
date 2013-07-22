using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Math.Collision
{
    public static class CellSizeHelper
    {
        #region Methods
        /// <summary>
        /// Validates the size of the cell.
        /// </summary>
        /// <param name="cellSize">Size of the cell.</param>
        public static void ValidateCellSize(int cellSize)
        {
            BitArray bitArray = new BitArray(
                BitConverter.GetBytes(cellSize));

            int trueValues = bitArray.Cast<bool>().Count(bit => bit);
            bool lastBit = bitArray[bitArray.Length - 1];

            if (trueValues > 1 || lastBit)
            {
                throw new ArgumentOutOfRangeException("cellSize",
                    "The specified cell size has to be a square number of 2.");
            }
        }
        #endregion
    }
}
