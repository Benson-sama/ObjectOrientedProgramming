namespace LinuxCommandTop
{
    using System;
    using System.Diagnostics;

    public class SafeProcess
    {
        private Process process;

        public SafeProcess(Process process)
        {
            this.Process = process;
        }

        public Process Process
        {
            get
            {
                return this.process;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "The specified value must not be null.");
                }

                this.process = value;
            }
        }

        public string Id
        {
            get
            {
                try
                {
                    return this.Process.Id.ToString();
                }
                catch (Exception)
                {
                    return "--";
                }
            }
        }

        public TimeSpan TotalProcessorTime
        {
            get
            {
                try
                {
                    return this.Process.TotalProcessorTime;
                }
                catch (Exception)
                {
                    return TimeSpan.Zero;
                }
            }
        }
    }
}