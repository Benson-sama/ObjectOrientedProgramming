namespace LP1
{
    using System;
    using System.Collections;

    public class QueueEnumerator<T> : IEnumerator
    {
        private T[] queue;

        public QueueEnumerator(T[] queue, int oldestElementIndex)
        {
            this.Queue = queue;
            this.OldestElementIndex = oldestElementIndex;
            this.CurrentIndex = oldestElementIndex - 1;
            this.NumberOfElementsIterated = 0;
        }

        object IEnumerator.Current
        {
            get
            {
                return this.Queue[this.CurrentIndex];
            }
        }

        private int OldestElementIndex
        {
            get;
            set;
        }

        private int NumberOfElementsIterated
        {
            get;
            set;
        }

        public T Current
        {
            get
            {
                return this.Queue[this.CurrentIndex];
            }
        }

        private int CurrentIndex
        {
            get;
            set;
        }

        private T[] Queue
        {
            get
            {
                return this.queue;
            }

            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value), "The specified argument must not be null.");

                this.queue = value;
            }
        }

        public void Dispose()
        {
            // Use garbage collector.
        }

        public bool MoveNext()
        {
            if (this.Queue.Length == 0)
            {
                return false;
            }

            if (this.NumberOfElementsIterated == this.Queue.Length)
            {
                return false;
            }
            else if (this.CurrentIndex == this.Queue.Length - 1)
            {
                this.CurrentIndex = 0;
                this.NumberOfElementsIterated++;
                return true;
            }
            else
            {
                this.CurrentIndex++;
                this.NumberOfElementsIterated++;
                return true;
            }
        }

        public void Reset()
        {
            this.CurrentIndex = this.OldestElementIndex - 1;
            this.NumberOfElementsIterated = 0;
        }
    }
}