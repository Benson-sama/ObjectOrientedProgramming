//-----------------------------------------------------------------------
// <copyright file="KeyboardWatcherThreadArguments.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the KeyboardWatcherThreadArguments class.</summary>
//-----------------------------------------------------------------------
namespace HexViewer
{
    /// <summary>
    /// Represents the <see cref="KeyboardWatcherThreadArguments"/> class.
    /// </summary>
    public class KeyboardWatcherThreadArguments
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardWatcherThreadArguments"/> class.
        /// </summary>
        public KeyboardWatcherThreadArguments()
        {
            this.Exit = false;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the keyboard watcher shall exit or not.
        /// </summary>
        /// <value>The exit boolean for the thread.</value>
        public bool Exit
        {
            get;
            set;
        }
    }
}