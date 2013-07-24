namespace Xemio.GameLibrary.Math.Collision.Sources
{
    public class StaticCollisionSource : ICollisionSource
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="StaticCollisionSource"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public StaticCollisionSource(float x, float y)
        {
            this.Position = new Vector2(x, y);
        }
        #endregion
        
        #region Implementation of ICollisionSource
        /// <summary>
        /// Gets the position.
        /// </summary>
        public Vector2 Position { get; private set; }
        #endregion
    }
}
