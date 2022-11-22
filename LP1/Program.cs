namespace LP1
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            Queue<string> myQueue = new Queue<string>(3);

            myQueue.Add("Peter");
            myQueue.Add("Erich");
            myQueue.Add("Gustav");

            Console.WriteLine("Adding Peter, Erich and Gustav to the empty queue:");
            foreach (string element in myQueue)
            {
                Console.WriteLine(element);
            }

            myQueue.Add("Neumann");
            myQueue.Add("Bachmann");

            Console.WriteLine("\nAdding Neumann and Bachmann to the queue.");
            foreach (string element in myQueue)
            {
                Console.WriteLine(element);
            }

            Console.ReadKey(true);
        }
    }
}
