// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.IO;
using System.Linq;
using System.Reflection;

namespace STX.SPAL.Core
{
    internal partial class SPALOrchestrationService
    {
        private static Assembly[] GetDependantAssemblies(
            Assembly assembly,
            int currentDepth = 0,
            int maximumDepth = DEFAULT_MAXIMUM_DEPTH_ANALISYS)
        {
            return assembly
                .GetReferencedAssemblies()
                .SelectMany(referencedAssemblyName =>
                {
                    Assembly referencedAssembly = Assembly.Load(referencedAssemblyName);

                    return currentDepth < maximumDepth
                        ? GetDependantAssemblies(referencedAssembly, currentDepth + 1, maximumDepth)
                        : new Assembly[] { referencedAssembly };
                })
                .ToArray();
        }

        private static Assembly[] GetAllAssemblies(Assembly rootAssembly)
        {
            Assembly[] assemblies =
                GetDependantAssemblies(rootAssembly)
                .Distinct()
                .ToArray();

            return assemblies;
        }

        private static string[] GetApplicationAssemblies()
        {
            string applicationPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            return Directory.GetFiles(applicationPath, "*.dll");
        }
    }

}
