using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.UI.Widgets;

namespace Xemio.GameLibrary.UI.CSS.Namespaces
{
    public interface INamespaceExpression : ILinkable<string>
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
