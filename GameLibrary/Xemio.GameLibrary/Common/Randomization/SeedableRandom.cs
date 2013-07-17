using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Common.Randomization
{
    public class SeedableRandom : RandomProxy, ISeedable
    {
        #region Fields
        private int _seed;
        #endregion

        #region ISeedable Member
        /// <summary>
        /// Gets or sets the seed.
        /// </summary>
        public int Seed
        {
            get { return this._seed; }
            set
            {
                this._seed = value;
                this._random = new System.Random(this._seed);
            }
        }
        #endregion
    }
}
