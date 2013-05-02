﻿using System;

namespace Xemio.GameLibary.Script
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
        /// <param name="a">A.</param>
        public TestCommand(int a)
        {
            _a = a;
        }
        #endregion

        private int _a;

        #region Implementation of ILinkable<string>
        /// <summary>
        /// Gets the identifier for the current instance.
        /// </summary>
        public string Identifier
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
            Console.WriteLine(_a);
        }
        #endregion
    }
}
