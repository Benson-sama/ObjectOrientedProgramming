namespace LinuxCommandTop
{
    using System;
    using System.Diagnostics;
    using System.Collections.Generic;

    public class SafeProcessManager
    {
        private TimeSpan totalProcessorTime;
        private int numberOfProcessesRunning;
        private int numberOfProcessesSleeping;
        private List<SafeProcess> safeProcesses;

        public SafeProcessManager()
        {
            this.SafeProcesses = GetSafeProcessList();
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

        public TimeSpan GetTotalProcessorTime()
        {
            this.totalProcessorTime = TimeSpan.Zero;

            foreach (var process in this.SafeProcesses)
            {
                this.totalProcessorTime += process.TotalProcessorTime;
            }

            return this.totalProcessorTime;
        }

        private List<SafeProcess> GetSafeProcessList()
        {
            Process[] processesArray = Process.GetProcesses();
            List<SafeProcess> processesList = new List<SafeProcess>();

            foreach (var process in processesArray)
            {
                processesList.Add(new SafeProcess(process));
            }

            return processesList;
        }
    }
}