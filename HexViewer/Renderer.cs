namespace HexViewer
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class Renderer
    {
        public void Print(FileTab fileTab, int currentIndex)
        {
            Console.SetCursorPosition(2, 1);
            Console.Write($"File: {currentIndex + 1}");
            Console.SetCursorPosition(12, 1);
            Console.Write($"Name: {fileTab.Path.ShortenPath(Console.WindowWidth - 20)}");
            this.Print(fileTab.Lines);
        }

        public void Print(List<Line> lines)
        {
            Console.SetCursorPosition(2, 3);

            foreach (var line in lines)
            {
                this.Print(line);
            }
        }

        public void PrintFrame()
        {
            this.PrintCharacterAtPosition('┌', 0, 0);
            this.PrintCharacterAtPosition('┐', Console.LargestWindowWidth - 1, 0);
            this.PrintCharacterAtPosition('└', 0, Console.LargestWindowHeight - 2);
            this.PrintCharacterAtPosition('┘', Console.LargestWindowWidth - 1, Console.LargestWindowHeight - 2);
            this.PrintCharacterAtPosition('├', 0, 2);
            this.PrintCharacterAtPosition('┬', 10, 0);
            this.PrintCharacterAtPosition('│', 10, 1);
            this.PrintCharacterAtPosition('┴', 10, 2);
            this.PrintCharacterAtPosition('┤', Console.LargestWindowWidth - 1, 2);
            this.PrintFrameLines();
        }

        private void PrintFrameLines()
        {
            for (int i = 1; i < Console.LargestWindowWidth - 1; i++)
            {
                this.PrintCharacterAtPosition('─', i, Console.LargestWindowHeight - 2);

                if (i == 10)
                {
                    continue;
                }

                this.PrintCharacterAtPosition('─', i, 0);
                this.PrintCharacterAtPosition('─', i, 2);
            }

            for (int i = 1; i < Console.LargestWindowHeight - 2; i++)
            {
                if (i == 2)
                {
                    continue;
                }

                this.PrintCharacterAtPosition('│', 0, i);
                this.PrintCharacterAtPosition('│', Console.LargestWindowWidth - 1, i);
            }
        }

        private void PrintCharacterAtPosition(char character, int left, int top)
        {
            int previousLeft = Console.CursorLeft;
            int previousTop = Console.CursorTop;

            Console.SetCursorPosition(left, top);
            Console.Write(character);
            Console.SetCursorPosition(previousLeft, previousTop);
        }

        private void Print(Line line)
        {
            int count = 0;
            Console.Write($"{line.Offset.ToString("X8")}  ");

            foreach (var item in line.HexContent)
            {
                if (count == 16)
                {
                    Console.Write("    ");
                }

                if (count % 2 == 0 && count != 0)
                {
                    Console.Write(" ");
                }

                Console.Write(item);
                count++;
            }

            Console.Write($"    {Regex.Escape(line.DecimalContent)}                                        ");
            Console.SetCursorPosition(2, Console.CursorTop + 1);
        }
    }
}