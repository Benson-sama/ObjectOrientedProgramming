namespace Slides
{
    using System;

    class Program
    {
        /// <summary>
        /// Represents the entry point of our application.
        /// </summary>
        /// <param name="args">Possibly specified command line arguments.</param>
        static void Main(string[] args)
        {
            HeavyCalculator calc = new HeavyCalculator();
            calc.Run();

            int number = 2;

            Func<int, int> square = x => x * x;
            Console.WriteLine(square(5));

            Console.WriteLine("2 times 10, 9 times.");
            for (int i = 0; i < 9; i++)
            {
                Console.WriteLine(number = number.TimesTen());
            }

            Console.WriteLine("Press any key to exit the application.");
            Console.ReadKey();
        }
    }
}
