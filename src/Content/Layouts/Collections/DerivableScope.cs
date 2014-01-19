using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Content.Layouts.Collections
{
    [Flags]
    public enum DerivableScope
    {
        None = 0,
        Collection = 1,
        Element = 2,
        All = Collection | Element
    }
}
