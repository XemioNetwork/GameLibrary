using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Xemio.GameLibrary.Rendering.Surfaces
{
    public class WindowSurface : ISurface
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowSurface"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public WindowSurface(int width, int height)
        {
            Form form = new Form
                            {
                                ClientSize = new Size(width, height),
                                Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location),
                                MaximizeBox = false,
                                FormBorderStyle = FormBorderStyle.FixedSingle
                            };

            this.Handle = form.Handle;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowSurface"/> class.
        /// </summary>
        /// <param name="handle">The handle.</param>
        public WindowSurface(IntPtr handle)
        {
            this.Handle = handle;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the handle.
        /// </summary>
        public IntPtr Handle { get; private set; }
        /// <summary>
        /// Gets the control.
        /// </summary>
        public Control Control
        {
            get { return Control.FromHandle(this.Handle); }
        }
        #endregion
        
        #region Implementation of ISurface
        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width
        {
            get { return this.Control.ClientSize.Width; }
        }
        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height
        {
            get { return this.Control.ClientSize.Height; }
        }
        #endregion
    }
}
