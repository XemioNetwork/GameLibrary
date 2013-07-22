using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Rendering.GDIPlus;

namespace Xemio.Testing.TileEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            Form form = new Form();

            XGL.Initialize(new GDIGraphicsInitializer());
            XGL.Run(form.Handle, 400, 300, 30);
        }
    }
}
