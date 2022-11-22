namespace Slides
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    class HeavyCalculator
    {
        private object consoleLocker;

        public HeavyCalculator()
        {
            consoleLocker = new object();
        }

        public void Run()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Main() invoked at thread {0}", Thread.CurrentThread.ManagedThreadId);
            var t = ProcessAsync();
            t.Wait();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Press any key to exit the program");
            Console.ReadKey();
        }

        /// <summary>
        /// Does some processing.
        /// </summary>
        /// <returns>The Task that represents the execution of this method.</returns>
        private async Task ProcessAsync()
        {
            lock(this.consoleLocker)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("ProcessAsync() invoked at thread {0}", Thread.CurrentThread.ManagedThreadId);
            }

            Task[] tasks = new Task[4];
            for (int i = 0; i < tasks.Length; i++)
                tasks[i] = HeavyCalculateAsync((3 * i) + 256 * 1024 * 1024);
            int result = HeavyCalculate(5 * 1024 * 1024);

            lock(this.consoleLocker)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("ProcessAsync() heavy work done");
            }

            foreach (Task t in tasks)
                await t;

            lock (this.consoleLocker)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("ProcessAsync() await done");
            }
        }

        /// <summary>
        /// Does some heavy calculation asynchronously.
        /// </summary>
        /// <param name="value">Some value for the heavy calculation.</param>
        /// <returns>The result of the heavy calculation.</returns>
        private async Task HeavyCalculateAsync(int value)
        {
            lock (this.consoleLocker)
            {
                Console.ForegroundColor = (ConsoleColor)(value % 15) + 1;
                Console.WriteLine("HeavyCalculateAsync() with value {0} invoked at thread {1}",
                value, Thread.CurrentThread.ManagedThreadId);
            }

            await Task.Run<int>(() => { return HeavyCalculate(value); });
            
            lock (this.consoleLocker)
            {
                Console.ForegroundColor = (ConsoleColor)(value % 15) + 1;
                Console.WriteLine("HeavyCalculateAsync() with value {0} invoked at thread {1}, waiting done",
                value, Thread.CurrentThread.ManagedThreadId);
            }
        }

        /// <summary>
        /// Does some heavy calculations.
        /// </summary>
        /// <param name="value">Some value for the heavy calculation.</param>
        /// <returns>The result of the heavy calculation.</returns>
        private int HeavyCalculate(int value)
        {
            lock (this.consoleLocker)
            {
                Console.ForegroundColor = (ConsoleColor)(value % 15) + 1;
                Console.WriteLine("HeavyCalculate() with value {0} invoked at thread {1}",
                value, Thread.CurrentThread.ManagedThreadId);
            }

            int result = 0;

            for (int i = 0; i < value; i++)
                result += i;

            lock (this.consoleLocker)
            {
                Console.ForegroundColor = (ConsoleColor)(value % 15) + 1;
                Console.WriteLine("HeavyCalculate() with value {0} invoked at thread {1}, calculation done",
                value, Thread.CurrentThread.ManagedThreadId);
            }
            return result;
        }
    }
}
