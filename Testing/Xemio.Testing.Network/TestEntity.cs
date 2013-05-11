using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;

namespace Xemio.Testing.Network
{
    public class TestEntity : Entity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TestEntity"/> class.
        /// </summary>
        public TestEntity()
        {
            this.IsSynced = true;
            this.IsCreationSynced = true;

            this.Components.Add(new TestComponent(this));
        }
        #endregion
    }
}
