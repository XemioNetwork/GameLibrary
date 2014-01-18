using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Content.Layouts.Generation
{
    internal class LayoutCache
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LayoutCache"/> class.
        /// </summary>
        public LayoutCache()
        {
            this._layouts = new Dictionary<Type, ILayoutElement>();
        }
        #endregion

        #region Fields
        private readonly Dictionary<Type, ILayoutElement> _layouts; 
        #endregion

        #region Methods
        /// <summary>
        /// Gets the layout for the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        public ILayoutElement Get(Type type)
        {
            lock (this._layouts)
            {
                if (!this._layouts.ContainsKey(type))
                {
                    this._layouts.Add(type, LayoutGenerator.Generate(type));
                }

                return this._layouts[type];
            }
        } 
        #endregion
    }
}
