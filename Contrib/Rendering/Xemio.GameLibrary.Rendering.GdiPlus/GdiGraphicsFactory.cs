using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Rendering.Effects.Processors;
using Xemio.GameLibrary.Rendering.Fonts;
using Xemio.GameLibrary.Rendering.GdiPlus.Geometry;
using Xemio.GameLibrary.Rendering.GdiPlus.Processors;
using Xemio.GameLibrary.Rendering.GdiPlus.Serialization;
using Xemio.GameLibrary.Rendering.Initialization;
using Xemio.GameLibrary.Rendering.Shapes.Factories;

namespace Xemio.GameLibrary.Rendering.GdiPlus
{
    public class GdiGraphicsFactory : IGraphicsFactory
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GdiGraphicsFactory"/> class.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        public GdiGraphicsFactory(GdiGraphicsInitializer initializer)
        {
            this._initializer = initializer;
        }
        #endregion

        #region Fields
        private readonly GdiGraphicsInitializer _initializer;
        #endregion

        #region Implementation of IGraphicsFactory
        /// <summary>
        /// Creates the texture writer.
        /// </summary>
        /// <returns></returns>
        public IWriter CreateTextureWriter()
        {
            return new GdiTextureWriter();
        }
        /// <summary>
        /// Creates the texture reader.
        /// </summary>
        /// <returns></returns>
        public IReader CreateTextureReader()
        {
            return new GdiTextureReader();
        }
        /// <summary>
        /// Creates the render manager.
        /// </summary>
        /// <returns></returns>
        public IRenderManager CreateRenderManager()
        {
            return new GdiRenderManager(this._initializer.SmoothingMode, this._initializer.InterpolationMode);
        }
        /// <summary>
        /// Creates the render factory.
        /// </summary>
        /// <returns></returns>
        public IRenderFactory CreateRenderFactory()
        {
            return new GdiRenderFactory();
        }
        /// <summary>
        /// Creates the shape factory.
        /// </summary>
        public IShapeFactory CreateShapeFactory()
        {
            return new ShapeFactory();
        }
        /// <summary>
        /// Creates the text rasterizer.
        /// </summary>
        public ITextRasterizer CreateTextRasterizer()
        {
            return new SpriteFontRasterizer();
        }
        /// <summary>
        /// Creates the effect processors.
        /// </summary>
        public IEnumerable<IEffectProcessor> CreateEffectProcessors()
        {
            yield return new TintEffectProcessor();
            yield return new TranslateEffectProcessor();
            yield return new TranslateToEffectProcessor();
        }
        #endregion
    }
}
