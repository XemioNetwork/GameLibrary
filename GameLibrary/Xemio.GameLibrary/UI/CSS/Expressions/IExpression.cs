using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.UI.Widgets;
using Xemio.GameLibrary.UI.Widgets.Base;

namespace Xemio.GameLibrary.UI.CSS.Expressions
{
    public interface IExpression : ILinkable<string>
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        WidgetState State { get; set; }
        /// <summary>
        /// Determines wether the expression matches the specified widget.
        /// </summary>
        /// <param name="widget">The widget.</param>
        bool Matches(Widget widget);
        /// <summary>
        /// Determines whether the specified expression is a valid expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        bool IsExpression(string expression);
        /// <summary>
        /// Parses the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        void Parse(string expression);
    }
}
