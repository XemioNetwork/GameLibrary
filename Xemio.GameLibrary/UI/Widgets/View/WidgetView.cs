using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.UI.Widgets.Base;

namespace Xemio.GameLibrary.UI.Widgets.View
{
    public class WidgetView
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="WidgetView"/> class.
        /// </summary>
        /// <param name="widget">The widget.</param>
        public WidgetView(Widget widget)
        {
            this._widget = widget;
            this.States = new List<WidgetViewState>();
        }
        #endregion

        #region Fields
        private readonly Widget _widget;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the states.
        /// </summary>
        public List<WidgetViewState> States { get; private set; }
        /// <summary>
        /// Gets the current view state.
        /// </summary>
        public WidgetViewState Current
        {
            get
            {
                var combinedState = new WidgetViewState(this._widget.State);
                var viewStates = this.GetStates(this._widget.State);

                if (viewStates.Count == 0)
                {
                    viewStates = this.GetStates(WidgetState.Normal);
                }

                foreach (WidgetViewState viewState in viewStates)
                {
                    combinedState.Instructions.AddRange(viewState.Instructions);
                }

                return combinedState;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the view states for the specified widget state.
        /// </summary>
        /// <param name="state">The state.</param>
        public List<WidgetViewState> GetStates(WidgetState state)
        {
            var viewStates = this.States
                .Where(v => v.Visible && v.State == state)
                .ToList();

            return viewStates;
        } 
        #endregion
    }
}
