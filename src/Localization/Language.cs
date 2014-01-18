using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace Xemio.GameLibrary.Localization
{
    public class Language : IEquatable<Language>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Language"/> class.
        /// </summary>
        public Language()
        {
            this.Values = new Dictionary<string, string>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the name of the culture.
        /// </summary>
        public string CultureName { get; set; }
        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        public Dictionary<string, string> Values { get; set; }
        #endregion

        #region Overriden Methods
        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        public override bool Equals(object obj)
        {
            var language = obj as Language;
            if (language != null)
                return this.Equals(language);

            return base.Equals(obj);
        }
        #endregion

        #region IEquatable<Language> Member
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(Language other)
        {
            return this.CultureName == other.CultureName;
        }
        #endregion
    }
}
