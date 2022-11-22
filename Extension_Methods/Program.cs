//-----------------------------------------------------------
// <copyright file="Program.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the Program class.</summary>
//-----------------------------------------------------------
namespace ExtensionMethods
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents the <see cref="Program"/> class.
    /// </summary>
    class Program
    {
        /// <summary>
        /// This is the entry point of the application.
        /// </summary>
        /// <param name="args">Possibly specified command line arguments.</param>
        static void Main(string[] args)
        {
            /* - Print: alle Elemente werden mit Beistrich getrennt.
               - Append: an ein Element werden mehrere Elemente dran gehängt.
               - MyAny, MySkip, MyTake, MyToList -> wie org.
               - Hop -> 1,2,3,4,5 => 2,4,6, ...
               - Pick -> {1{3,4,5}}, {2, {1, 3,4}, ....
               - Reorder -> Alle Möglichkeiten anzeigen wie man was anordnen kann.
               - Propagate -> (+2) = 1,3,5,7 ... true, false, true false,... */

            string[] str = { "", "", "" };

            int[] numbers = { 1, 2, 3, 4, 5 };

            /* numbers.Print();
               numbers.Append(10).Print();
               Console.WriteLine(numbers.MyAny());
               Console.WriteLine(numbers.MyAny()); */

            IEnumerable<int> test = numbers.OrderByDescending(x => x).MyTake(2);
            foreach (int testnumber in test)
            {
                Console.WriteLine(testnumber);
            }

            Console.ReadKey(true);
        }
    }
}