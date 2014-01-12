using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Layouts
{
    public class EachElement<T, TItem> : ILayoutElement
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EachElement{T, TItem}" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="elementTag">The element tag.</param>
        /// <param name="selector">The selector.</param>
        /// <param name="layout">The layout.</param>
        public EachElement(string tag, string elementTag, Expression<Func<T, IEnumerable<TItem>>> selector, PersistenceLayout<TItem> layout)
        {
            this.Tag = tag;
            this.ElementTag = elementTag;

            this.Selector = selector;
            this.Layout = layout;

            this._function = selector.Compile();
        }
        #endregion

        #region Fields
        private readonly Func<T, IEnumerable<TItem>> _function;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the tag.
        /// </summary>
        public string Tag { get; private set; }
        /// <summary>
        /// Gets the element tag.
        /// </summary>
        public string ElementTag { get; private set; }
        /// <summary>
        /// Gets the selector.
        /// </summary>
        public Expression<Func<T, IEnumerable<TItem>>> Selector { get; private set; }
        /// <summary>
        /// Gets the layout.
        /// </summary>
        public PersistenceLayout<TItem> Layout { get; private set; } 
        #endregion

        #region Implementation of ILayoutElement

        public void Write(IFormatWriter writer, object value)
        {
            using (writer.Section(this.Tag))
            {
                IEnumerable<TItem> items = this._function((T)value);

                writer.WriteInteger("Length", items.Count());
                foreach (TItem item in this._function((T)value))
                {
                    using (writer.Section(this.ElementTag))
                    {
                        this.Layout.Write(writer, item);
                    }
                }
            }
        }

        public void Read(IFormatReader reader, object value)
        {
            using (reader.Section(this.Tag))
            {
            }
        }

        #endregion
    }
}
