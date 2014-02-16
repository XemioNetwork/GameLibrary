using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Network.Logic;
using Xemio.GameLibrary.Network.Syncput.Packages;
using Xemio.GameLibrary.Network.Syncput.Turns;

namespace Xemio.GameLibrary.Network.Syncput.Logic.Client
{
    using Client = Xemio.GameLibrary.Network.Client;

    public class StartGameClientLogic : ClientLogic<StartGamePackage>
    {
        #region Methods
        /// <summary>
        /// Called when the game will be started.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        public override void OnReceive(IClient client, StartGamePackage package)
        {
            Syncput syncput = XGL.Components.Get<Syncput>();
            syncput.GameStartIndex = package.StartIndex;
        }
        #endregion
    }
}
