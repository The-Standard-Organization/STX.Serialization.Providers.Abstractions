// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using STX.SPAL.Abstractions;
using System;
using System.Linq;
using System.Reflection;

namespace STX.SPAL.Core
{
    public partial class SPALOrchestrationService
    {
        private static Type[] GetInterfaceImplementations<T>(
            Assembly assembly,
            Type concreteTypeProvider,
            string spalId)
            where T : ISPALProvider
        {
            Type spalInterfaceType = typeof(T);

            Type[] implementations =
                assembly
                    .GetExportedTypes()
                    .Where(exportedType =>
                        exportedType.GetInterfaces()
                            .Any(interfaceType =>
                                //@interface is T
                                interfaceType.Assembly.FullName == spalInterfaceType.Assembly.FullName
                                    && interfaceType.FullName == spalInterfaceType.FullName))
                     .Where(exportedType =>
                        (concreteTypeProvider == null
                            && string.IsNullOrEmpty(spalId))
                        || (concreteTypeProvider == null
                            && !string.IsNullOrEmpty(spalId)
                            && exportedType.Namespace == spalId)
                        || (concreteTypeProvider != null
                            && concreteTypeProvider.FullName == exportedType.FullName
                            && string.IsNullOrEmpty(spalId))
                        || (concreteTypeProvider != null
                            && concreteTypeProvider.FullName == exportedType.FullName
                            && !string.IsNullOrEmpty(spalId)
                            && exportedType.Namespace == spalId))
                    .ToArray();

            return implementations;
        }

        private static Type[] GetExportedTypesFromAssemblyPath<T>(
            string assemblyPath,
            Type concreteTypeProvider,
            string spalId)
            where T : ISPALProvider
        {
            Assembly applicationAssembly = Assembly.LoadFrom(assemblyPath);

            return GetInterfaceImplementations<T>(applicationAssembly, concreteTypeProvider, spalId);
        }

        private static Type[] GetExportedTypesFromAssembliesPaths<T>(Type concreteTypeProvider, string spalId)
            where T : ISPALProvider
        {
            string[] applicationAssembliesPaths = GetApplicationAssemblies();
            Type[] exportedTypesOfT =
                applicationAssembliesPaths
                    .SelectMany(applicationAssemblyPath =>
                        GetExportedTypesFromAssemblyPath<T>(applicationAssemblyPath, concreteTypeProvider, spalId))
                    .ToArray();

            return exportedTypesOfT;
        }
    }
}
