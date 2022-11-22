namespace LinuxCommandTop
{
    using System;
    using System.Diagnostics;

    public class Application
    {
        private KeyboardWatcher watcher;

        private Renderer renderer;

        private SafeProcessManager safeProcessManager;

        public Application(string[] args)
        {
            this.Renderer = new Renderer();
            this.SafeProcessManager = new SafeProcessManager();
            this.Watcher = new KeyboardWatcher();
            this.Watcher.OnKeyPressed += WatcherOnKeyPressed;
        }

        private KeyboardWatcher Watcher
        {
            get
            {
                return this.watcher;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "The specified value must not be null");
                }

                this.watcher = value;
            }
        }

        private Renderer Renderer
        {
            get
            {
                return this.renderer;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "The specified value must not be null");
                }

                this.renderer = value;
            }
        }

        private SafeProcessManager SafeProcessManager
        {
            get
            {
                return this.safeProcessManager;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "The specified value must not be null");
                }

                this.safeProcessManager = value;
            }
        }

        public void Run()
        {
            this.Watcher.Start();

            this.Renderer.Print(this.SafeProcessManager.SafeProcesses);

            // Test total processor time (Non-accessable time is missing in this value).
            // This code does not belong here.
            Console.WriteLine($"Total processor time: " +
                              $"{this.SafeProcessManager.GetTotalProcessorTime().ToString("c")}");
        }

        private void WatcherOnKeyPressed(object sender, OnKeyPressedEventArgs e)
        {
            if (e.Key == ConsoleKey.C && e.Modifiers == ConsoleModifiers.Control)
            {
                Environment.Exit(0);
            }

            switch (e.Key)
            {
                case ConsoleKey.Q:
                    Environment.Exit(0);
                    break;
                case ConsoleKey.H:
                    break;
                case ConsoleKey.Z:
                    break;
                case ConsoleKey.P:
                    break;
                case ConsoleKey.M:
                    break;
                case ConsoleKey.N:
                    break;
                case ConsoleKey.A:
                    break;
                case ConsoleKey.T:
                    break;
                case ConsoleKey.U:
                    break;
                case ConsoleKey.W:
                    break;
                case ConsoleKey.K:
                    break;
                case ConsoleKey.L:
                    break;
                case ConsoleKey.D1:
                    break;
                default:
                    break;
            }
        }
    }
}