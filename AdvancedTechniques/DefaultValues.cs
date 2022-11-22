namespace AdvancedTechniques
{
    using System;

    public class DefaultValues
    {
        public void Run()
        {
            Console.WriteLine(Test(1));
            Console.WriteLine(Test(2, 6.9));
            Console.WriteLine(Test(i: 15)); // Keine Ahnung was das genau macht.
            Console.WriteLine(Test(fuckingWert: 20, i: 2));
        }

        private double Test(int i, double fuckingWert = 4.20)
        {
            i++;
            return fuckingWert;
        }
    }
}
