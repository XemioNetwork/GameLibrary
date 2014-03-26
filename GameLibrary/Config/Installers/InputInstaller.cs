using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Input.Adapters;

namespace Xemio.GameLibrary.Config.Installers
{
    public class InputInstaller : AbstractInstaller
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="InputInstaller"/> class.
        /// </summary>
        public InputInstaller()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="InputInstaller" /> class.
        /// </summary>
        /// <param name="createPlayerInput">if set to <c>true</c> [create player input].</param>
        public InputInstaller(bool createPlayerInput)
        {
            this.CreatePlayerInput = createPlayerInput;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether to create a default player input.
        /// </summary>
        public bool CreatePlayerInput { get; set; }
        #endregion

        #region Overrides of AbstractInstaller
        /// <summary>
        /// Installs this instance.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="catalog">The catalog.</param>
        public override void Install(Configuration configuration, IComponentCatalog catalog)
        {
            var inputManager = new InputManager();

            if (this.CreatePlayerInput)
            {
                PlayerInput playerInput = inputManager.CreatePlayerInput();

                playerInput.Attach(new MouseAdapter());
                playerInput.Attach(new KeyboardAdapter());
            }

            catalog.Install(inputManager);
        }
        #endregion
    }
}
