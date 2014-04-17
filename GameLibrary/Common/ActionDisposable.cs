using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Common
{
    public class ActionDisposable : IDisposable
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionDisposable"/> class.
        /// </summary>
        /// <param name="preAction">The pre action.</param>
        /// <param name="postAction">The post action.</param>
        public ActionDisposable(Action preAction, Action postAction) : this(postAction)
        {
            preAction();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionDisposable"/> class.
        /// </summary>
        /// <param name="method">The method.</param>
        public ActionDisposable(Action method)
        {
            this.Method = method;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the method beeing executed.
        /// </summary>
        public Action Method { get; private set; }
        #endregion

        #region Static Properties
        /// <summary>
        /// Gets an empty disposable.
        /// </summary>
        public static IDisposable Empty
        {
            get { return new ActionDisposable(() => { }); }
        }
        #endregion

        #region Static Methods
        /// <summary>
        /// Combines the specified actions.
        /// </summary>
        /// <param name="actions">The actions.</param>
        public static ActionDisposable Combine(params Action[] actions)
        {
            return new ActionDisposable(() =>
            {
                foreach (Action action in actions)
                {
                    action();
                }
            });
        }
        #endregion

        #region Implementation of IDisposable
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            this.Method();
        }
        #endregion
    }
}
