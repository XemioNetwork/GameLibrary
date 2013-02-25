using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Plugins;
using System.Reflection;

namespace Xemio.GameLibrary.Rendering
{
    public static class GraphicsProvider
    {
        #region Methods
        /// <summary>
        /// Sets up the rendering pipeline and creates related components.
        /// </summary>
        /// <param name="providerAssembly">The provider assembly.</param>
        public static void Setup(Assembly providerAssembly)
        {
            PluginLoader<IGraphicsProvider> loader = new PluginLoader<IGraphicsProvider>();
            loader.LoadAssembly(providerAssembly);

            IGraphicsProvider provider = loader.Plugins.FirstOrDefault();
            if (provider == null)
            {
                throw new InvalidOperationException("The requested assembly does not contain any graphics providers.");
            }

            ComponentManager.Instance.Add(new ValueProvider<ITextureFactory>(provider.TextureFactory));
            ComponentManager.Instance.Add(new ValueProvider<IRenderManager>(provider.RenderManager));

            ComponentManager.Instance.Add(new ValueProvider<IGraphicsProvider>(provider));
        }
        #endregion
    }
}
