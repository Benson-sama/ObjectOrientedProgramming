namespace HexViewer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class FileTab
    {
        private string path;

        private List<Line> lines;

        public event EventHandler<LinesRefreshedEventArgs> LinesRefreshed;

        public FileTab(string path)
        {
            this.Lines = new List<Line>();
            this.Path = path;
        }

        public string Path
        {
            get
            {
                return this.path;
            }

            set
            {
                if (!File.Exists(value))
                {
                    throw new ArgumentException(nameof(value), "The specified file does not exist.");
                }

                this.path = value;
            }
        }

        public List<Line> Lines
        {
            get
            {
                return this.lines;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "The specified argument must not be null.");
                }

                this.lines = value;
            }
        }

        public void ReadFromFile(int index)
        {
            this.Lines.Clear();

            using (FileStream fileStream = new FileStream(this.Path, FileMode.Open))
            {
                fileStream.Seek(index, SeekOrigin.Begin);
                byte[] content = new byte[16];

                for (int i = 0; i < Console.LargestWindowHeight - 5; i++)
                {
                    int bytesRead = fileStream.Read(content, 0, content.Length);

                    if (bytesRead == 0)
                    {
                        this.Lines.Add(new Line(System.Text.Encoding.UTF8.GetBytes(string.Empty), index + i * 16));
                        continue;
                    }

                    this.Lines.Add(new Line(content, index + i * 16));
                }

                fileStream.Close();
            }

            this.FireLinesRefreshed();
        }

        public void ScrollDownOnePage()
        {
            int numberOfLines = Console.LargestWindowHeight - 5;
            int newOffset = this.Lines[this.Lines.Count - 1].Offset + 16;
            this.ReadFromFile(newOffset);
        }

        public void ScrollUpOnePage()
        {
            int numberOfLines = Console.LargestWindowHeight - 5;
            int newOffset = this.Lines[0].Offset - 16 * numberOfLines;

            if (this.Lines[0].Offset == 0)
            {
                return;
            }
            else if (this.Lines[0].Offset < numberOfLines * 16)
            {
                this.ReadFromFile(0);
            }
            else
            {
                this.ReadFromFile(newOffset);
            }
        }

        public void ScrollDownOneRow()
        {
            int offset = this.Lines[this.Lines.Count - 1].Offset + 16;

            using (FileStream fileStream = new FileStream(this.Path, FileMode.Open))
            {
                fileStream.Seek(offset, SeekOrigin.Begin);
                byte[] content = new byte[16];

                fileStream.Read(content, 0, content.Length);
                this.Lines.Add(new Line(content, offset));
                this.Lines.RemoveAt(0);
                fileStream.Close();
            }

            this.FireLinesRefreshed();
        }

        public void ScrollUpOneRow()
        {
            int offset = this.Lines[0].Offset - 16;

            if (offset < 0)
            {
                return;
            }

            using (FileStream fileStream = new FileStream(this.Path, FileMode.Open))
            {
                fileStream.Seek(offset, SeekOrigin.Begin);
                byte[] content = new byte[16];

                fileStream.Read(content, 0, content.Length);
                this.Lines.RemoveAt(this.Lines.Count - 1);
                this.Lines = this.Lines.Prepend(new Line(content, offset)).ToList();
                fileStream.Close();
            }

            this.FireLinesRefreshed();
        }

        private void FireLinesRefreshed()
        {
            if (this.LinesRefreshed != null)
            {
                this.LinesRefreshed(this, new LinesRefreshedEventArgs(this));
            }
        }
    }
}