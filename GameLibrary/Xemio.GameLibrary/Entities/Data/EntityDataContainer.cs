using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Network.Synchronization;

namespace Xemio.GameLibrary.Entities.Data
{
    public abstract class EntityDataContainer
    {
        #region Singleton
        /// <summary>
        /// Gets the empty instance.
        /// </summary>
        [ExcludeSync]
        public static EntityDataContainer Empty
        {
            get { return Singleton<EmptyDataContainer>.Value; }
        }
        #endregion
    }
}
