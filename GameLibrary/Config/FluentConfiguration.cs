using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Config
{
    public class FluentConfiguration : IConfigurationAccess
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FluentConfiguration"/> class.
        /// </summary>
        public FluentConfiguration() : this(new Configuration())
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="FluentConfiguration"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public FluentConfiguration(Configuration configuration)
        {
            this._configuration = configuration;
        }
        #endregion

        #region Fields
        private readonly Configuration _configuration;
        #endregion

        #region Implementation of IConfigurationAccess
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        Configuration IConfigurationAccess.Configuration
        {
            get { return this._configuration; }
        }
        #endregion
    }
}
