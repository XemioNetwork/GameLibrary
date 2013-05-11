using Xemio.GameLibrary.UI.Widgets.Base;

namespace Xemio.GameLibrary.UI.Widgets.View.ViewStates.Button
{
    internal class ButtonView : WidgetView
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonView"/> class.
        /// </summary>
        /// <param name="widget">The widget.</param>
        public ButtonView(Widget widget) : base(widget)
        {
            this.States.Add(new ButtonStateNormal(widget));
            this.States.Add(new ButtonStateHover(widget));
        }
        #endregion
    }
}
