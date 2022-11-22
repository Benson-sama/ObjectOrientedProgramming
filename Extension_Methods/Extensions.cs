//--------------------------------------------------------------
// <copyright file="Extensions.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the Extensions class.</summary>
//--------------------------------------------------------------
namespace ExtensionMethods
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the <see cref="Extensions"/> class.
    /// </summary>
    public static class Extensions
    {
        public static void Print<TSource>(this IEnumerable<TSource> items)
        {
            foreach (TSource item in items)
            {
                Console.Write($"{item}, ");
            }
        }

        public static IEnumerable<TSource> Append<TSource>(this IEnumerable<TSource> items, TSource newItem)
        {
            foreach (TSource item in items)
            {
                yield return item;
            }

            yield return newItem;
        }

        public static bool MyAny<TSource>(this IEnumerable<TSource> source)
        {
            if (source.GetEnumerator().MoveNext())
            {
                return true;
            }

            return false;
        }

        public static IEnumerable<TSource> MySkip<TSource>(this IEnumerable<TSource> items, int count)
        {
            IEnumerator<TSource> enumerator = items.GetEnumerator();

            while (count > 0)
            {
                enumerator.MoveNext(); // exception abfangen 
                count--;
            }

            if (count <= 0)
            {
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
            }
        }

        public static IEnumerable<TSource> MyTake<TSource>(this IEnumerable<TSource> items, int count)
        {
            foreach (TSource item in items)
            {
                if (count == 0)
                {
                    break;
                }

                yield return item;
                count--;

            }
        }

    }
}
