namespace AdvancedTechniques
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            DefaultValues defaultValues = new DefaultValues();
            defaultValues.Run();

            Complex c1 = new Complex(1, 2.0);
            Complex c2 = new Complex(1, 2.0);

            // Instanzen Addiert
            var c3 = c1 + c2;
            Console.WriteLine(c3); // 1 + 1 = 2, 2 + 2 = 4;

            Console.ReadLine();

        }

        List<int> Filter(List<int> items, Predicate<int> predicate)
        {
            var result = new List<int>();
            foreach (int number in items)
            {
                if (number > 3)
                {
                    result.Add(items[number]);
                }
            }

            return result;
        }



        private static void PersonVergleich()
        {
            // Bsp. 1:
            var p1 = new Person
            {
                FirstName = "Garbage",
                LastName = "OOP",
                Age = 22
            };

            // Bsp. 2:
            var p2 = new
            {
                FirstName = "What the fuck",
                LastName = "is wrong with you, c#",
                Gender = "Apache Attack Helicopter",
                AssType = "Gorilla buttcheeks"
            };

            // Zwischen Bsp. 1 und Bsp. 2 ist ein gigantischer unterschied. 
            // In Bsp. 2 kann man temporär eine "Art" Klasse machen, jedoch kann man 
            //      ALLE Properties reinschreiben und benutzen wie man lustig ist.
            //      Somit hat Bsp. 2 NICHTS mit Bsp. 1 zu tun, welches sich an die Klasse
            //      Person hält.
        }
    }
}
