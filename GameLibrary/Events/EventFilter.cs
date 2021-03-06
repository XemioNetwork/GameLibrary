﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Events
{
    public class EventFilter<T> where T : class, IEvent
    {
        #region Properties
        /// <summary>
        /// Gets the action that will be executed.
        /// </summary>
        public Action<T> Action { get; private set; }
        /// <summary>
        /// Gets the filters.
        /// </summary>
        public IList<Func<T, bool>> Filters { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EventFilter{T}"/> class.
        /// </summary>
        /// <param name="action">The execute action.</param>
        private EventFilter(Action<T> action)
        {
            this.Filters = new List<Func<T, bool>>();
            this.Action = action;
        }
        #endregion

        #region Static Methods
        /// <summary>
        /// Creates a new EventFilter executing the given method.
        /// </summary>
        /// <param name="action">The execute action.</param>
        public static EventFilter<T> For(Action<T> action)
        {
            return new EventFilter<T>(action);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Applies a filter function to this EventFilter.
        /// The ex
        /// </summary>
        /// <param name="filter">The filter.</param>
        public EventFilter<T> WithCondition(Func<T, bool> filter)
        {
            this.Filters.Add(filter);
            return this;
        }
        /// <summary>
        /// Applies a filter function to this EventFilter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        public EventFilter<T> AndCondition(Func<T, bool> filter)
        {
            return this.WithCondition(filter);
        }
        /// <summary>
        /// Creates the configured filter.
        /// </summary>
        public Action<T> Create()
        {
            return parameter =>
                       {
                           if (this.Filters.All(filter => filter(parameter)))
                           {
                               this.Action(parameter);
                           }
                       };
        }
        #endregion
    }
}
