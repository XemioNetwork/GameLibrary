using Xemio.GameLibrary.Network.Synchronization;

namespace Xemio.GameLibrary.Entities.Data
{
    public class EmptyDataContainer : EntityDataContainer
    {
        #region Overrides of EntityDataContainer
        /// <summary>
        /// Synchronizes to the specified storage.
        /// </summary>
        /// <param name="storage">The storage.</param>
        public override void Synchronize(SynchronizationStorage storage)
        {
        }
        #endregion
    }
}
