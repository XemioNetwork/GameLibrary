using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Common.Collections;

namespace Xemio.GameLibrary.Components
{
    public interface IComponentCatalog : ICatalog<IComponent>, IEnumerable<IComponent>
    {
        /// <summary>
        /// Constructs all loaded components.
        /// </summary>
        void Construct();
    }
}
