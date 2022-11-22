namespace HexViewer
{
    using System;
    using System.Text;

    public class Line
    {
        private int offset;

        private byte[] content;

        private string hexContent;

        private string decimalContent;

        public Line(byte[] data, int offset)
        {
            this.Content = data;
            this.Offset = offset;
            this.hexContent = BitConverter.ToString(data).Replace("-", string.Empty);
            this.decimalContent = Encoding.UTF8.GetString(data);
        }

        public int Offset
        {
            get
            {
                return this.offset;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The specified value must be greater than zero.");
                }

                this.offset = value;
            }
        }

        public byte[] Content
        {
            get
            {
                return this.content;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "The specified value must not be null.");
                }

                this.content = value;
            }
        }

        public string HexContent
        {
            get
            {
                return this.hexContent;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "The specified value must not be null.");
                }

                this.hexContent = value;
            }
        }

        public string DecimalContent
        {
            get
            {
                return this.decimalContent;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "The specified value must not be null.");
                }

                this.decimalContent = value;
            }
        }
    }
}