//-----------------------------------------------------------
// <copyright file="Program.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the Program class.</summary>
//-----------------------------------------------------------
namespace ExitTheRoomRiddle
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    class Program
    {
        static void Main(string[] args)
        {
            // First output.
            AppDomain.CurrentDomain.ExecuteAssembly((@"C:\Temp\OOP\ExitTheRoomSolver\Source\ExitTheRoom.exe"));

            // Second output. Execute the application in another domain.
            AppDomain myDomain = AppDomain.CreateDomain("newDomain");
            myDomain.ExecuteAssembly(@"C:\Temp\OOP\ExitTheRoomSolver\Source\ExitTheRoom.exe");

            // Get the ExitTheRoom assembly with their types and methods.
            Assembly assembly = myDomain.GetAssemblies().Last();
            var daeneryType = assembly.GetTypes().First();
            var daeneryMethods = daeneryType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static);
            var daeneryInstance = Activator.CreateInstance(daeneryType);

            var birthyearMethod = daeneryMethods.First(p => p.Name.Contains("1912"));

            // Third output.
            string returnedBrother = Convert.ToString(birthyearMethod.Invoke(daeneryInstance, new object[] { new object(), new ObsoleteAttribute(), 100 }));
            var returnedBrotherMethod = daeneryMethods.First(p => p.Name.Contains(returnedBrother));

            // Fourth output.
            try
            {
                returnedBrotherMethod.Invoke(daeneryInstance, new object[] { new Func<int>(() => throw new Exception()) });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
            }

            // Fifth and sixth output.
            using (Stream fs = assembly.GetManifestResourceStream(assembly.GetManifestResourceNames().First()))
            {
                // Load assembly from resource files stream.
                byte[] content = new byte[fs.Length];
                fs.Read(content, 0, content.Length);
                Assembly embeddedAssembly = Assembly.Load(content);

                // Instantiate the Main method and invoke it.
                Type embeddedAssemblyType = embeddedAssembly.GetTypes().First();
                var embeddedInstance = Activator.CreateInstance(embeddedAssemblyType);
                var embeddedMethod = embeddedAssemblyType.GetMethods(BindingFlags.Static | BindingFlags.NonPublic).First();
                embeddedMethod.Invoke(embeddedInstance, new object[] { null });

                // Get the final message and print it.
                var embeddedMember = embeddedAssemblyType.GetFields(BindingFlags.Static | BindingFlags.NonPublic).First();
                Console.WriteLine(embeddedMember.GetValue(new object()));

                fs.Close();
            }

            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey(true);
        }
    }
}
