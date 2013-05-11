using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.UI.DataBindings;
using Xemio.GameLibrary.UI.Widgets;

namespace Xemio.Testing.DataBindings
{
    class Program
    {
        static void Main(string[] args)
        {
            TestWidget testWidget = new TestWidget();

            testWidget.Message = "Hallo Welt";
            testWidget.UpdateBindings();

            Console.WriteLine(testWidget.Model.Message);
            Console.ReadLine();
        }
    }

    class TestWidget : Widget
    {
        public TestWidget()
        {
            this.Model = new Model();
            this.Bind(() => this.Message, () => this.Model.Message);
        }

        public Model Model { get; set; }
        public string Message { get; set; }
    }

    class Model
    {
        public string Message { get; set; }
    }
}
