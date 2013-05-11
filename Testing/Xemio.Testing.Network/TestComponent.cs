using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities.Network;
using Xemio.GameLibrary.Math;

namespace Xemio.Testing.Network
{
    public class TestComponent : NetworkComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TestComponent"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public TestComponent(TestEntity entity) : base(entity)
        {
            this.Container = new TestDataContainer();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the test container.
        /// </summary>
        protected TestDataContainer TestContainer
        {
            get { return this.Container as TestDataContainer; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this.Entity.Position += new Vector2(1, 1) * this.TestContainer.Velocity;
            this.TestContainer.Velocity *= 0.96f;
        }
        #endregion
    }
}
