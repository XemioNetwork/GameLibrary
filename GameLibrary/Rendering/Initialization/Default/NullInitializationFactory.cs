using System.Collections.Generic;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Rendering.Effects.Processors;
using Xemio.GameLibrary.Rendering.Fonts;
using Xemio.GameLibrary.Rendering.Shapes.Factories;

namespace Xemio.GameLibrary.Rendering.Initialization.Default
{
    public class NullInitializationFactory : IInitializationFactory
    {
        #region Implementation of IGraphicsFactory
        /// <summary>
        /// Gets the texture writer.
        /// </summary>
        public IContentWriter CreateTextureWriter()
        {
            return new NullTextureWriter();
        }
        /// <summary>
        /// Gets the texture reader.
        /// </summary>
        public IContentReader CreateTextureReader()
        {
            return new NullTextureReader();
        }
        /// <summary>
        /// Gets the render manager.
        /// </summary>
        public IRenderManager CreateRenderManager()
        {
            return new NullRenderManager();
        }
        /// <summary>
        /// Gets the render factory.
        /// </summary>
        public IRenderFactory CreateRenderFactory()
        {
            return new NullRenderFactory();
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
            return new NullTextRasterizer();
        }
        /// <summary>
        /// Creates the effect processors.
        /// </summary>
        public IEnumerable<IEffectProcessor> CreateEffectProcessors()
        {
            yield break;
        }
        #endregion
    }
}
