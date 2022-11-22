namespace LinuxCommandTop
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class Renderer
    {
        public void Print(List<SafeProcess> safeProcesses)
        {
            if (safeProcesses == null)
                return;

            // Print column description.
            Console.WriteLine("PID");

            // Prototype for listing all processes.
            int count = 0;
            foreach (var process in safeProcesses)
            {
                Console.SetCursorPosition(0, count + 1);
                Console.WriteLine(process.Id);
                Console.SetCursorPosition(10, count + 1);
                Console.WriteLine(process.Process.ProcessName);
                count++;
            }
        }

        public void PrintHeader(Process[] processes)
        {
            string commandHeader = "top - ";
            commandHeader += DateTime.UtcNow.ToString("HH:mm:ss");
            Console.WriteLine(commandHeader);
        }
    }
}