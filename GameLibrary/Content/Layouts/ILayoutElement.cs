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
        /// <summary>
        /// Writes partial data for the container into the specified format.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="container">The container.</param>
        void Write(IFormatWriter writer, object container);
        /// <summary>
        /// Reads partial data for the container from the specified format.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="container">The container.</param>
        void Read(IFormatReader reader, object container);
    }
}
