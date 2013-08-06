using System;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Sound;

namespace Xemio.GameLibrary
{
    internal class FluentConfiguration : Configuration
    {
        #region Fields
        private IGraphicsInitializer _graphicsInitializer;
        private ISoundInitializer _soundInitializer;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether to register the default components.
        /// </summary>
        public bool RegisterDefaultComponents { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Sets the graphics initializer.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        public void SetInitializer(IGraphicsInitializer initializer)
        {
            this._graphicsInitializer = initializer;
        }
        /// <summary>
        /// Sets the sound initializer.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        public void SetInitializer(ISoundInitializer initializer)
        {
            this._soundInitializer = initializer;
        }
        /// <summary>
        /// Registers the default components.
        /// </summary>
        public override void RegisterComponents()
        {
            if (this.RegisterDefaultComponents)
            {
                base.RegisterComponents();
            }
        }
        #endregion

        #region Overrides of Configuration
        /// <summary>
        /// Gets or sets the graphics initializer.
        /// </summary>
        public override IGraphicsInitializer GraphicsInitializer
        {
            get { return this._graphicsInitializer; }
        }
        /// <summary>
        /// Gets or sets the sound initializer.
        /// </summary>
        public override ISoundInitializer SoundInitializer
        {
            get { return this._soundInitializer; }
        }
        #endregion
    }
}