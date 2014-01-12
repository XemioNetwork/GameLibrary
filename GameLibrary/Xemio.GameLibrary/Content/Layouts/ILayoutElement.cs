using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Layouts
{
    public interface ILayoutElement
    {
        void Write(IFormatWriter writer, object value);
        void Read(IFormatReader reader, object value);
    }
}
