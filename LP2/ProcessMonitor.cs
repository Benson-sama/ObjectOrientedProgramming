namespace LP2
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;

    public class ProcessMonitor
    {
        private List<SafeProcess> safeProcesses;

        public event EventHandler<ProcessMonitorEventArgs> ProcessStarted;

        public event EventHandler<ProcessMonitorEventArgs> ProcessStopped;

        private Thread thread;

        private ProcessMonitorThreadArguments threadArguments;

        public ProcessMonitor()
        {
            this.threadArguments = new ProcessMonitorThreadArguments();
        }

        public TimeSpan PollInterval
        {
            get;
            set;
        }

        public List<SafeProcess> SafeProcesses
        {
            get
            {
                return this.safeProcesses;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "The specified value must not be null.");
                }

                this.safeProcesses = value;
            }
        }

        private void FireProcessStarted(object sender, ProcessMonitorEventArgs e)
        {
            if (this.ProcessStarted != null)
            {
                e.SafeProcess.Refresh();
                this.ProcessStarted(sender, e);
            }
        }

        private void FireProcessStopped(object sender, ProcessMonitorEventArgs e)
        {
            if (this.ProcessStopped != null)
            {
                e.SafeProcess.Refresh();
                this.ProcessStopped(sender, e);
            }
        }

        public void Start()
        {
            if (this.thread != null && this.thread.IsAlive)
            {
                return;
            }

            this.threadArguments.Exit = false;
            this.thread = new Thread(this.Worker);
            this.thread.Start(this.threadArguments);
        }

        public void Stop()
        {
            if (this.thread == null || !this.thread.IsAlive)
            {
                return;
            }

            this.threadArguments.Exit = true;
        }

        /// <summary>
        /// Represents the worker thread that monitors process activity.
        /// </summary>
        /// <param name="data">The thread arguments.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Is thrown if the specified instance for data is not of type <see cref="ProcessMonitorThreadArguments"/>.
        /// </exception>
        private void Worker(object data)
        {
            if (!(data is ProcessMonitorThreadArguments))
            {
                throw new ArgumentOutOfRangeException(
                    nameof(data),
                    $"The specified data must be an instance of the {nameof(ProcessMonitorThreadArguments)} class.");
            }

            ProcessMonitorThreadArguments args = (ProcessMonitorThreadArguments)data;

            this.SafeProcesses = this.GetSafeProcesses();

            while (!args.Exit)
            {
                var newSafeProcesses = this.GetSafeProcesses();
                var startedProcesses = this.GetStartedProcesses(this.SafeProcesses, newSafeProcesses);
                var stoppedProcesses = this.GetStoppedProcesses(this.SafeProcesses, newSafeProcesses);

                startedProcesses.ForEach(process => this.FireProcessStarted(this, new ProcessMonitorEventArgs(process)));
                stoppedProcesses.ForEach(process => this.FireProcessStopped(this, new ProcessMonitorEventArgs(process)));

                this.SafeProcesses = newSafeProcesses;
                Thread.Sleep(this.PollInterval);
            }
        }

        private List<SafeProcess> GetStartedProcesses(List<SafeProcess> oldProcesses, List<SafeProcess> newProcesses)
        {
            List<SafeProcess> startedProcesses = new List<SafeProcess>();

            foreach (var newProcess in newProcesses)
            {
                bool isContainedInBoth = false;

                foreach (var oldProcess in oldProcesses)
                {
                    if (oldProcess.Id == newProcess.Id)
                    {
                        isContainedInBoth = true;
                        break;
                    }
                }

                if (!isContainedInBoth)
                {
                    startedProcesses.Add(newProcess);
                }
            }

            return startedProcesses;
        }

        private List<SafeProcess> GetStoppedProcesses(List<SafeProcess> oldProcesses, List<SafeProcess> newProcesses)
        {
            List<SafeProcess> stoppedProcesses = new List<SafeProcess>();

            foreach (var oldProcess in oldProcesses)
            {
                bool isContainedInBoth = false;

                foreach (var newProcess in newProcesses)
                {
                    if (oldProcess.Id == newProcess.Id)
                    {
                        isContainedInBoth = true;
                        break;
                    }
                }

                if (!isContainedInBoth)
                {
                    stoppedProcesses.Add(oldProcess);
                }
            }

            return stoppedProcesses;
        }

        private List<SafeProcess> GetSafeProcesses()
        {
            var processes = Process.GetProcesses().ToList<Process>();
            List<SafeProcess> safeProcesses = new List<SafeProcess>();

            processes.ForEach(process => safeProcesses.Add(new SafeProcess(process)));

            return safeProcesses;
        }
    }
}