using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Rendering.Effects
{
    [Flags]
    public enum FlipEffectOptions
    {
        None = 0,
        X = 1,
        Y = 2,
        Both = X | Y
    }
}
