namespace HexViewer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;

    public class Application
    {
        private Renderer renderer;

        private ConsoleSettings settingsAtStartup;

        public List<FileTab> fileTabs;

        private KeyboardWatcher watcher;

        private int currentFileTabIndex;

        public Application()
        {
            this.settingsAtStartup = new ConsoleSettings();
            this.renderer = new Renderer();
            this.watcher = new KeyboardWatcher();
            this.watcher.OnKeyPressed += this.WatcherOnKeyPressed;
            this.fileTabs = new List<FileTab>();
        }

        public int CurrentFileTabIndex
        {
            get
            {
                return this.currentFileTabIndex;
            }

            set
            {
                if (value < this.fileTabs.Count && value >= 0)
                {
                    this.currentFileTabIndex = value;
                }
            }
        }

        public void Run()
        {
            Console.Title = "Hex Viewer ©Benjamin BOGNER, 2022";
            Console.CursorVisible = false;
            this.SetConsoleFullscreenWithoutScrollBar();

            if (Console.LargestWindowWidth < 82)
            {
                Console.WriteLine("The Consoles window must have a minimum width of 82 characters. Exiting the application...");
                return;
            }

            this.watcher.Start();
        }

        private void SetConsoleFullscreenWithoutScrollBar()
        {
            Console.WindowHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.LargestWindowWidth;
            Console.BufferHeight = Console.LargestWindowHeight;
            Console.BufferWidth = Console.LargestWindowWidth;
        }

        private void WatcherOnKeyPressed(object sender, OnKeyPressedEventArgs e)
        {
            if (e.Modifiers == ConsoleModifiers.Control && e.Key == ConsoleKey.O)
            {
                this.ExecuteOpenFileCommand();
            }
            else if (e.Modifiers == ConsoleModifiers.Control && e.Key == ConsoleKey.W)
            {
                this.ExecuteCloseFileCommand();
            }
            else if (e.Modifiers == ConsoleModifiers.Control && e.Key == ConsoleKey.S)
            {
                this.ExecuteSaveApplicationStateCommand();
            }
            else if (e.Modifiers == ConsoleModifiers.Control && e.Key == ConsoleKey.R)
            {
                this.ExecuteRestoreApplicationStateCommand();
            }

            this.ExecuteNavigationEvaluation(e.Key);
        }

        private void ExecuteRestoreApplicationStateCommand()
        {
            using (FileStream fileStream = new FileStream("ApplicationState.dat", FileMode.Open))
            {
                SurrogateSelector selector = new SurrogateSelector();
                selector.AddSurrogate(typeof(Application), new StreamingContext(StreamingContextStates.All),
                                                           new ApplicationSerialisationSurrogate());

                IFormatter formatter = new BinaryFormatter();
                formatter.SurrogateSelector = selector;
                Application application = (Application)formatter.Deserialize(fileStream);

                this.fileTabs = application.fileTabs;
                this.CurrentFileTabIndex = application.CurrentFileTabIndex;
                
                foreach (var tab in application.fileTabs)
                {
                    tab.LinesRefreshed += this.OnLinesRefreshed;
                }

                if (this.fileTabs.Any())
                {
                    this.renderer.PrintFrame();
                    this.SwitchTab(this.CurrentFileTabIndex + 1);
                }

                fileStream.Close();
            }

            this.RefreshTab();
        }

        private void ExecuteSaveApplicationStateCommand()
        {
            using (FileStream fileStream = new FileStream("ApplicationState.dat", FileMode.Create,
                                                                                  FileAccess.Write,
                                                                                  FileShare.Read))
            {
                SurrogateSelector selector = new SurrogateSelector();
                selector.AddSurrogate(typeof(Application), new StreamingContext(StreamingContextStates.All),
                                                           new ApplicationSerialisationSurrogate());

                IFormatter formatter = new BinaryFormatter();
                formatter.SurrogateSelector = selector;
                formatter.Serialize(fileStream, this);
                fileStream.Close();
            }

        }

        private void ExecuteCloseFileCommand()
        {
            if (this.fileTabs.Any())
            {
                this.fileTabs[this.currentFileTabIndex].LinesRefreshed -= this.OnLinesRefreshed;
                this.fileTabs.RemoveAt(this.currentFileTabIndex);
                this.CurrentFileTabIndex = this.fileTabs.Count - 1;
                this.RefreshTab();
            }
        }

        private void ExecuteNavigationEvaluation(ConsoleKey key)
        {
            if (key == ConsoleKey.Escape)
            {
                this.ExecuteExitApplicationCommand();
            }

            if (!this.fileTabs.Any())
            {
                return;
            }

            switch (key)
            {
                case ConsoleKey.PageUp:
                    this.fileTabs[this.currentFileTabIndex].ScrollUpOnePage();
                    break;
                case ConsoleKey.PageDown:
                    this.fileTabs[this.currentFileTabIndex].ScrollDownOnePage();
                    break;
                case ConsoleKey.UpArrow:
                    this.fileTabs[this.currentFileTabIndex].ScrollUpOneRow();
                    break;
                case ConsoleKey.DownArrow:
                    this.fileTabs[this.currentFileTabIndex].ScrollDownOneRow();
                    break;
                case ConsoleKey.F1:
                    this.SwitchTab(1);
                    break;
                case ConsoleKey.F2:
                    this.SwitchTab(2);
                    break;
                case ConsoleKey.F3:
                    this.SwitchTab(3);
                    break;
                case ConsoleKey.F4:
                    this.SwitchTab(4);
                    break;
                case ConsoleKey.F5:
                    this.SwitchTab(5);
                    break;
                case ConsoleKey.F6:
                    this.SwitchTab(6);
                    break;
                case ConsoleKey.F7:
                    this.SwitchTab(7);
                    break;
                case ConsoleKey.F8:
                    this.SwitchTab(8);
                    break;
                case ConsoleKey.F9:
                    this.SwitchTab(9);
                    break;
                case ConsoleKey.F10:
                    this.SwitchTab(10);
                    break;
                case ConsoleKey.F11:
                    this.SwitchTab(11);
                    break;
                case ConsoleKey.F12:
                    this.SwitchTab(12);
                    break;
                default:
                    break;
            }
        }

        private void SwitchTab(int tabNumber)
        {
            if (tabNumber - 1 < 0 || tabNumber - 1 > this.fileTabs.Count - 1)
            {
                return;
            }

            this.CurrentFileTabIndex = tabNumber - 1;
            this.RefreshTab();
        }

        private void ExecuteExitApplicationCommand()
        {
            this.settingsAtStartup.Restore();
            Environment.Exit(0);
        }

        private void RefreshTab()
        {
            if (!this.fileTabs.Any())
            {
                Console.Clear();
                return;
            }

            this.renderer.Print(this.fileTabs[this.currentFileTabIndex], this.currentFileTabIndex);
        }

        private void ExecuteOpenFileCommand()
        {
            Console.Clear();
            Console.CursorVisible = true;

            string path;

            if (TryGetFilePath(out path))
            {
                Console.Clear();
                Console.CursorVisible = false;

                FileTab newFileTab = new FileTab(path);
                newFileTab.LinesRefreshed += this.OnLinesRefreshed;
                this.renderer.PrintFrame();
                this.fileTabs.Add(newFileTab);
                this.CurrentFileTabIndex = this.fileTabs.Count - 1;
                newFileTab.ReadFromFile(0);
            }
        }

        private bool TryGetFilePath(out string path)
        {
            Console.WriteLine("Please enter the path of the file including the file name!");
            path = Console.ReadLine();
            return File.Exists(path);
        }

        private void OnLinesRefreshed(object sender, LinesRefreshedEventArgs e)
        {
            this.renderer.Print(e.FileTab, this.currentFileTabIndex);
        }
    }
}