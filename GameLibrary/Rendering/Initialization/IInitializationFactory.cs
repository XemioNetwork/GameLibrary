using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Rendering.Effects;
using Xemio.GameLibrary.Rendering.Effects.Processors;
using Xemio.GameLibrary.Rendering.Fonts;
using Xemio.GameLibrary.Rendering.Shapes;
using Xemio.GameLibrary.Rendering.Shapes.Factories;

namespace Xemio.GameLibrary.Rendering.Initialization
{
    public interface IInitializationFactory
    {
        /// <summary>
        /// Gets the texture writer.
        /// </summary>
        IContentWriter CreateTextureWriter();
        /// <summary>
        /// Gets the texture reader.
        /// </summary>
        IContentReader CreateTextureReader();
        /// <summary>
        /// Gets the render manager.
        /// </summary>
        IRenderManager CreateRenderManager();
        /// <summary>
        /// Gets the render factory.
        /// </summary>
        IRenderFactory CreateRenderFactory();
        /// <summary>
        /// Creates the shape factory.
        /// </summary>
        IShapeFactory CreateShapeFactory();
        /// <summary>
        /// Creates the text rasterizer.
        /// </summary>
        ITextRasterizer CreateTextRasterizer();
        /// <summary>
        /// Creates the effect processors.
        /// </summary>
        IEnumerable<IEffectProcessor> CreateEffectProcessors();
    }
}
