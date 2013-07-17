using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.UI.CSS.Expressions;
using Xemio.GameLibrary.UI.CSS.Parsers;
using Xemio.GameLibrary.UI.Widgets;
using Xemio.GameLibrary.UI.Widgets.Base;

namespace Xemio.GameLibrary.UI.CSS.Namespaces
{
    public class Namespace : INamespace
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Namespace"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Namespace(string name)
        {
            this.Name = name;

            this._parser = new NamespaceParser();
            this._expressions = new List<IExpression>(this._parser.Parse(name));
        }
        #endregion

        #region Fields
        private readonly NamespaceParser _parser;
        private readonly List<IExpression> _expressions; 
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

        #region Implementation of INamespace
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Gets the depth.
        /// </summary>
        public int Depth
        {
            get { return this._expressions.Count; }
        }
        /// <summary>
        /// Determines whether the namespace contains the specified widget.
        /// </summary>
        /// <param name="widget">The widget.</param>
        public bool Contains(Widget widget)
        {
            List<Widget> widgetTree = new List<Widget>();

            Widget currentWidget = widget;
            while (currentWidget != null)
            {
                widgetTree.Add(currentWidget);
                currentWidget = currentWidget.Parent as Widget;
            }

            widgetTree.Reverse();
            for (int i = 0; i < widgetTree.Count && i < this._expressions.Count; i++)
            {
                IExpression expression = this._expressions[i];

                bool matchesExpression = expression.Matches(widgetTree[i]);
                bool matchesState = expression.State == widgetTree[i].State;
                bool notNoneState = expression.State != WidgetState.None;
                
                if (!matchesExpression && !matchesState && notNoneState)
                {
                    return false;
                }
            }

            return true;
        }
        #endregion
    }
}
