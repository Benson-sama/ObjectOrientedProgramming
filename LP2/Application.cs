namespace LP2
{
    using System;
    using System.IO;
    using System.Diagnostics;

    public class Application
    {
        public void Run()
        {
            ProcessMonitor processMonitor = new ProcessMonitor();
            processMonitor.PollInterval = TimeSpan.FromSeconds(1);

            processMonitor.ProcessStarted += (sender, e) =>
            {
                Console.WriteLine("New process started:");
                Console.WriteLine($"      Name: {e.SafeProcess.ProcessName}");
                Console.WriteLine($"Executable: {e.SafeProcess.MainModule.FileName}");
                Console.WriteLine($"        Id: {e.SafeProcess.Id}");
                Console.WriteLine($" StartTime: {e.SafeProcess.StartTime}\n");
            };

            processMonitor.ProcessStopped += (sender, e) =>
            {
                Console.WriteLine("Process stopped:");
                Console.WriteLine($"      Name: {e.SafeProcess.ProcessName}");
                Console.WriteLine($"Executable: {e.SafeProcess.MainModule.FileName}");
                Console.WriteLine($"        Id: {e.SafeProcess.Id}");
                Console.WriteLine($" StartTime: {e.SafeProcess.StartTime}");
                Console.WriteLine($"  ExitTime: {e.SafeProcess.ExitTime}");
                Console.WriteLine($"  ExitCode: {e.SafeProcess.ExitCode}\n");
            };

            processMonitor.Start();

            this.ProcessNextInput();
        }

        private void ProcessNextInput()
        {
            while (true)
            {
                string[] splittedInput = this.GetInput();

                if (splittedInput.Length != 2)
                    continue;

                try
                {
                    if (splittedInput[0] == "start")
                    {
                        File.Exists(splittedInput[1]);
                        Process process = new Process();
                        process.StartInfo.FileName = splittedInput[1];
                        process.Start();
                    }
                    else if (splittedInput[0] == "stop")
                    {
                        int processId = Convert.ToInt32(splittedInput[1]);
                        Process process = Process.GetProcessById(processId);
                        process.Kill();
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("The specified process-id must not contain any characters.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private string[] GetInput()
        {
            string input = Console.ReadLine();
            input = input.ToLower();

            return input.Split(' ');
        }
    }
}