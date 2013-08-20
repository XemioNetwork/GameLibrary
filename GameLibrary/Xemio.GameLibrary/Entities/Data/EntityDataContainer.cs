using Xemio.GameLibrary.Common;

namespace Xemio.GameLibrary.Entities.Data
{
    public abstract class EntityDataContainer
    {
        #region Singleton
        /// <summary>
        /// Gets the empty instance.
        /// </summary>
        public static EntityDataContainer Empty
        {
            get { return Singleton<EmptyDataContainer>.Value; }
        }
        #endregion
    }
}
