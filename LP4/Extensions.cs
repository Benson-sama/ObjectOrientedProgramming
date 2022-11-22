namespace LP4
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class Extensions
    {
        public static IEnumerable<MethodInfo> GetPipelineBuilderMethods(this List<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
            {
                foreach (var method in assembly.GetPipelineBuilderMethods())
                {
                    yield return method;
                }
            }
        }

        public static IEnumerable<MethodInfo> GetPipelineBuilderMethods(this Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (!type.IsPublic)
                {
                    continue;
                }

                foreach (var method in type.GetMethods())
                {
                    if (!method.IsPublic)
                    {
                        continue;
                    }

                    IEnumerable<ZeroParametersAttribute> zeroParametersAttributes = method.GetCustomAttributes<ZeroParametersAttribute>();

                    if (zeroParametersAttributes.Any())
                    {
                        yield return method;
                    }

                    IEnumerable<OneParameterAttribute> oneParametersAttributes = method.GetCustomAttributes<OneParameterAttribute>();

                    if (oneParametersAttributes.Any())
                    {
                        yield return method;
                    }
                }
            }
        }
    }
}
