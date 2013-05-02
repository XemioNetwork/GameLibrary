using Xemio.GameLibrary.UI.CSS.Namespaces;
using Xemio.GameLibrary.UI.Widgets;

namespace Xemio.GameLibrary.UI.CSS.Expressions
{
    public class IdExpression : IExpression
    {
        #region Implementation of ILinkable<string>
        /// <summary>
        /// Gets the identifier for the current instance.
        /// </summary>
        public string Identifier
        {
            get { return "IdExpression"; }
        }
        #endregion

        #region Implementation of INamespaceExpression
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public WidgetState State { get; set; }
        /// <summary>
        /// Determines wether the expression matches the specified widget.
        /// </summary>
        /// <param name="widget">The widget.</param>
        public bool Matches(Widget widget)
        {
            return widget.Id == this.Name;
        }
        /// <summary>
        /// Determines whether the specified expression is a valid expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        public bool IsExpression(string expression)
        {
            return expression.StartsWith("#") && expression.Length >= 2;
        }
        /// <summary>
        /// Parses the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        public void Parse(string expression)
        {
            this.Name = expression.Substring(1);
        }
        #endregion

        #region Object Member
        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return this.Name;
        }
        #endregion
    }
}
