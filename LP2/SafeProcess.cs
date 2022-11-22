namespace LP2
{
    using System;
    using System.Diagnostics;

    public class SafeProcess
    {
        private Process process;

        private SafeProcessModule mainModule;

        public SafeProcess(Process process)
        {
            this.Process = process;
            this.SetId();
        }

        private Process Process
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

        public SafeProcessModule MainModule
        {
            get
            {
                return this.mainModule;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "The specified value must not be null.");
                }

                this.mainModule = value;
            }
        }

        public string ProcessName
        {
            get;
            set;
        }

        public string Id
        {
            get;
            private set;
        }

        public string StartTime
        {
            get;
            private set;
        }

        public string ExitTime
        {
            get;
            private set;
        }

        public string ExitCode
        {
            get;
            private set;
        }

        public void Refresh()
        {
            this.MainModule = new SafeProcessModule(this.Process);
            this.SetProcessName();
            this.SetId();
            this.SetStartTime();
            this.SetExitTime();
            this.SetExitCode();
        }

        private void SetId()
        {
            try
            {
                this.Id = this.Process.Id.ToString();
            }
            catch (InvalidOperationException)
            {
                this.Id = "no permission";
            }
            catch (PlatformNotSupportedException)
            {
                this.Id = "no permission";
            }
        }

        private void SetProcessName()
        {
            try
            {
                this.ProcessName = this.Process.ProcessName;
            }
            catch (Exception)
            {
                this.ProcessName = "no permission";
            }
        }

        private void SetStartTime()
        {
            try
            {
                this.StartTime = this.Process.StartTime.ToLongTimeString();
            }
            catch (Exception)
            {
                this.StartTime = "no permission";
            }
        }

        public void SetExitTime()
        {
            try
            {
                if (!this.Process.HasExited)
                {
                    return;
                }

                this.ExitTime = this.Process.ExitTime.ToLongTimeString();
            }
            catch (Exception)
            {
                this.ExitTime = "no permission";
            }
        }

        public void SetExitCode()
        {
            try
            {
                if (!this.Process.HasExited)
                {
                    return;
                }

                this.ExitCode = this.Process.ExitCode.ToString();
            }
            catch (Exception)
            {
                this.ExitCode = "no permission";
            }
        }
    }
}