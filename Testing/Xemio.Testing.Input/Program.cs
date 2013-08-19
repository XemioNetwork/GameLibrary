using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Input;

namespace Xemio.Testing.Input
{
    class Program
    {
        static void Main(string[] args)
        {
            Form form = new Form();
            Button button = new Button();
            button.Click += (sender, eventArgs) =>
                                {
                                    string a = "3";
                                };
            
            form.Controls.Add(button);
            
            XGL.Run(form, XGL.Configure()
                .DefaultInput()
                .DisableSplashScreen()
                .BuildConfiguration());
        }
    }
}
