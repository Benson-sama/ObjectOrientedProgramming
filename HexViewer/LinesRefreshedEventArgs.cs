namespace HexViewer
{
    using System;
    using System.Collections.Generic;

    public class LinesRefreshedEventArgs : EventArgs
    {
        private FileTab fileTab;

        public LinesRefreshedEventArgs(FileTab fileTab)
        {
            this.FileTab = fileTab;
        }

        public FileTab FileTab
        {
            get
            {
                return this.fileTab;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "The specified value must not be null.");
                }

                this.fileTab = value;
            }
        }
    }
}