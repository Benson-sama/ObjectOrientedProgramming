namespace LP4
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    class PipelineBuilder
    {
        private List<MethodInfo> methods;

        private List<MethodInfo> methodPipeline;

        public PipelineBuilder()
        {
            this.methods = new List<MethodInfo>();
            this.methodPipeline = new List<MethodInfo>();
        }

        public void Run()
        {
            if (this.LoadMethods())
            {
                Console.WriteLine("Methods loaded.");
            }
            else
            {
                Console.WriteLine("Could not load methods.");
            }

            this.PrintAllMethods();
            this.GetUserInput();
        }

        private void GetUserInput()
        {
            Console.WriteLine("\nPlease enter the index of a method to add to the pipeline or hit enter to execute it.");

            while (true)
            {
                string input = Console.ReadLine();

                if (input == string.Empty)
                {
                    Console.WriteLine("Executing Pipeline. (Not implemented yet, nothing to expect here.");
                }

                int selectedMethodIndex = 0;
                int.TryParse(input, out selectedMethodIndex);
                Console.Clear();
                this.PrintAllMethods();
                this.AddMethodToPipeline(selectedMethodIndex);
                this.PrintPipelineStatus();
            }
        }

        private void PrintPipelineStatus()
        {
            Console.WriteLine("Pipeline status:");
            this.methodPipeline.ForEach(p => Console.WriteLine(p));
        }

        private void AddMethodToPipeline(int index)
        {
            if (index <= 0 || index > this.methods.Count)
            {
                Console.WriteLine("The specified index is not valid");
                return;
            }

            this.methodPipeline.Add(this.methods[index - 1]);
        }

        private void PrintAllMethods()
        {
            int i = 1;

            foreach (var method in this.methods)
            {
                Console.WriteLine($"{i++} : {method}");
                var descriptions = method.GetCustomAttributes().Where(p => p.GetType() == typeof(DescriptionAttribute));
                descriptions.ToList().ForEach(p => Console.WriteLine($"\t {p.ToString()}"));
            }

            Console.WriteLine();
        }

        private bool LoadMethods()
        {
            List<Assembly> assemblies = this.LoadAssemblies("plugins");

            if (!assemblies.Any())
            {
                return false;
            }

            this.methods = assemblies.GetPipelineBuilderMethods().ToList();

            return true;
        }

        private List<Assembly> LoadAssemblies(string filePath)
        {
            List<Assembly> assemblies = new List<Assembly>();

            if (!Directory.Exists(filePath))
            {
                return assemblies;
            }

            string[] files;

            try
            {
                files = Directory.GetFiles(filePath);
            }
            catch (Exception)
            {
                Console.WriteLine("Could not access the the folder 'plugins' or one of its members.");
                return assemblies;
            }

            foreach (var file in files)
            {
                try
                {
                    var assembly = Assembly.LoadFrom(file);
                    assemblies.Add(assembly);
                }
                catch (Exception)
                {
                    Console.WriteLine($"Could not load assembly: {file}");
                    continue;
                }
            }

            return assemblies;
        }
    }
}
