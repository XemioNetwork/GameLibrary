using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Input
{
    public class InputReference
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="InputReference" /> class.
        /// </summary>
        /// <param name="inputManager">The input manager.</param>
        /// <param name="playerIndex">Index of the player.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="associatedIds">The associated ids.</param>
        internal InputReference(InputManager inputManager, int playerIndex, string id, IEnumerable<string> associatedIds)
        {
            this._inputManager = inputManager;
            this._playerIndex = playerIndex;

            this.Id = id;
            this.AssociatedIds = associatedIds;
        }
        #endregion

        #region Fields
        private readonly InputManager _inputManager;
        private readonly int _playerIndex;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the key identifier.
        /// </summary>
        public string Id { get; private set; }
        /// <summary>
        /// Gets the associated identifiers.
        /// </summary>
        public IEnumerable<string> AssociatedIds { get; private set; }
        /// <summary>
        /// Gets the player input.
        /// </summary>
        public PlayerInput PlayerInput
        {
            get { return this._inputManager[this._playerIndex]; }
        }
        /// <summary>
        /// Gets a value indicating whether the key is pressed.
        /// </summary>
        public bool IsPressed
        {
            get { return this.IsActive && !this.WasPreviouslyActive; }
        }
        /// <summary>
        /// Gets a value indicating whether the key is released.
        /// </summary>
        public bool IsReleased
        {
            get { return !this.IsActive && this.WasPreviouslyActive; }
        }
        /// <summary>
        /// Gets a value indicating whether the key is down.
        /// </summary>
        public bool IsActive
        {
            get { return this.AssociatedIds.Any(id => this.PlayerInput.IsActive(id)); }
        }
        /// <summary>
        /// Gets a value indicating whether the key was previously active.
        /// </summary>
        public bool WasPreviouslyActive
        {
            get { return this.AssociatedIds.Any(id => this.PlayerInput.WasPreviouslyActive(id)); }
        }
        /// <summary>
        /// Gets the value.
        /// </summary>
        public float Value
        {
            get { return this.AssociatedIds.Sum(id => this.PlayerInput[id].Value); }
        }
        #endregion
    }
}
