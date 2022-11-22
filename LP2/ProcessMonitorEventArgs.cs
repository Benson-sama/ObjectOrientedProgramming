namespace LP2
{
    using System;
    using System.Diagnostics;

    public class ProcessMonitorEventArgs : EventArgs
    {
        private SafeProcess safeProcess;

        public ProcessMonitorEventArgs(SafeProcess safeProcess)
        {
            this.SafeProcess = safeProcess;
        }

        public SafeProcess SafeProcess
        {
            get
            {
                return this.safeProcess;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "The specified value must not be null.");
                }

                this.safeProcess = value;
            }
        }
    }
}