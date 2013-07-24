using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Math.Collision.Sources;

namespace Xemio.GameLibrary.Math.Collision.Events
{
    public class CollisionEvent : IEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CollisionEvent"/> class.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        public CollisionEvent(ICollisionSource first, ICollisionSource second)
        {
            this.First = first;
            this.Second = second;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the first.
        /// </summary>
        public ICollisionSource First { get; private set; }
        /// <summary>
        /// Gets the second.
        /// </summary>
        public ICollisionSource Second { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Determines whether this collision is an intersection of the specified types.
        /// </summary>
        /// <typeparam name="TFirst">The type of the first.</typeparam>
        /// <typeparam name="TSecond">The type of the second.</typeparam>
        public bool IsIntersectionOf<TFirst, TSecond>()
        {
            return
                this.First.GetType() == typeof (TFirst) &&
                this.Second.GetType() == typeof (TSecond) ||
                this.First.GetType() == typeof(TSecond) &&
                this.Second.GetType() == typeof(TFirst);
        }
        /// <summary>
        /// Gets a collision source for the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public T Get<T>()
        {
            if (this.First is T)
                return (T)this.First;

            if (this.Second is T)
                return (T)this.Second;

            return default(T);
        }
        #endregion
    }
}
