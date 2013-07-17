using System;

namespace Xemio.GameLibrary.Script
{
    public class TestCommand : ICommand
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TestCommand"/> class.
        /// </summary>
        public TestCommand()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TestCommand"/> class.
        /// </summary>
        /// <param name="value">A.</param>
        public TestCommand(int value)
        {
            this._value = value;
        }
        #endregion

        #region Fields
        private int _value;
        #endregion

        #region Implementation of ILinkable<string>
        /// <summary>
        /// Gets the identifier for the current instance.
        /// </summary>
        public string Id
        {
            get { return "Test"; }
        }
        #endregion

        #region Implementation of ICommand
        /// <summary>
        /// Gets a value indicating whether this <see cref="ICommand"/> is active.
        /// </summary>
        public bool Active
        {
            get { return false; }
        }
        /// <summary>
        /// Executes this command.
        /// </summary>
        public void Execute()
        {
            Console.WriteLine(this._value);
        }
        #endregion
    }
}
