using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Layouts
{
    public abstract class BaseElement : ILayoutElement
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseElement" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        protected BaseElement(string tag, Func<object, object> getAction, Action<object, object> setAction)
        {
            this.Tag = tag;
            this.GetAction = getAction;
            this.SetAction = setAction;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the tag.
        /// </summary>
        protected string Tag { get; private set; }
        /// <summary>
        /// Gets the read action.
        /// </summary>
        protected Func<object, object> GetAction { get; private set; }
        /// <summary>
        /// Gets the write action.
        /// </summary>
        protected Action<object, object> SetAction { get; private set; }
        #endregion
        
        #region Implementation of ILayoutElement
        /// <summary>
        /// Writes property for the specified container.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="container">The container.</param>
        public abstract void Write(IFormatWriter writer, object container);
        /// <summary>
        /// Reads the property for the specified container.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="container">The container.</param>
        public abstract void Read(IFormatReader reader, object container);
        #endregion
    }
}
