//-----------------------------------------------------------------------
// <copyright file="ProcessMonitorThreadArguments.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the ProcessMonitorThreadArguments class.</summary>
//-----------------------------------------------------------------------
namespace LP2
{
    /// <summary>
    /// Represents the <see cref="ProcessMonitorThreadArguments"/> class.
    /// </summary>
    public class ProcessMonitorThreadArguments
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessMonitorThreadArguments"/> class.
        /// </summary>
        public ProcessMonitorThreadArguments()
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