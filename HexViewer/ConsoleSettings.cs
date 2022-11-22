namespace HexViewer
{
    using System;

    public class ConsoleSettings
    {
        public ConsoleSettings()
        {
            this.Capture();
        }

        public int WindowWidth
        {
            get;
            set;
        }

        public int WindowHeight
        {
            get;
            set;
        }

        public ConsoleColor BackgroundColour
        {
            get;
            set;
        }

        public ConsoleColor ForegroundColour
        {
            get;
            set;
        }

        public bool CursorVisible
        {
            get;
            set;
        }

        public void Capture()
        {
            this.WindowHeight = Console.WindowHeight;
            this.WindowWidth = Console.WindowWidth;
            this.BackgroundColour = Console.BackgroundColor;
            this.ForegroundColour = Console.ForegroundColor;
            this.CursorVisible = Console.CursorVisible;
        }

        public void Restore()
        {
            Console.WindowHeight = this.WindowHeight;
            Console.WindowWidth = this.WindowWidth;
            Console.BackgroundColor = this.BackgroundColour;
            Console.ForegroundColor = this.ForegroundColour;
            Console.CursorVisible = this.CursorVisible;
        }
    }
}