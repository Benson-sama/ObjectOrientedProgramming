namespace LP1
{
    using System;
    using System.Collections;

    class Queue<T> : IEnumerable
    {
        private T[] elements;

        private int totalnumberOfElements;

        public Queue(int numberOfElements)
        {
            this.TotalNumberOfElements = numberOfElements;
            this.Elements = new T[numberOfElements];
            this.CurrentlyStoredElements = 0;
        }

        // Adding elements is only valid through the AddElement method.
        private T[] Elements
        {
            get
            {
                return this.elements;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value), "The specified value must not be null.");
                if (value.Length == 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "The specified queue size must be greater than 0");

                this.elements = value;
            }
        }

        // Assuming the number of elements can only be set when initialising the class.
        public int TotalNumberOfElements
        {
            get
            {
                return this.totalnumberOfElements;
            }

            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "The specified value must be greater than zero.");

                this.totalnumberOfElements = value;
            }
        }

        private int CurrentlyStoredElements
        {
            get;
            set;
        }

        private int OldestElementIndex
        {
            get;
            set;
        }

        public void Add(T element)
        {
            // If empty space available, else replaced old entry.
            if (this.CurrentlyStoredElements < this.TotalNumberOfElements)
            {
                if (this.CurrentlyStoredElements == 0)
                {
                    this.OldestElementIndex = 0;
                }

                this.Elements[this.CurrentlyStoredElements] = element;
                this.CurrentlyStoredElements++;
            }
            else
            {
                this.Elements[this.OldestElementIndex] = element;

                if (this.OldestElementIndex < this.TotalNumberOfElements - 1)
                {
                    this.OldestElementIndex++;
                }
                else
                {
                    this.OldestElementIndex = 0;
                }
            }
        }

        public IEnumerator GetEnumerator()
        {
            return new QueueEnumerator<T>(this.Elements, this.OldestElementIndex);
        }
    }
}
