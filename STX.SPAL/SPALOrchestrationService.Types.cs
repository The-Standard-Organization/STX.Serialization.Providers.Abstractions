// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;

namespace STX.SPAL
{
    internal partial class SPALOrchestrationService
    {
        private static Type[] GetInterfaceImplementations(
            Assembly assembly,
            Type spalInterfaceType,
            Type concreteProviderType,
            string spalId)
        {
            ValidateSPALInterfaceType(spalInterfaceType);

            Type[] implementations =
                assembly
                    .GetExportedTypes()
                    .Where(exportedType =>
                        exportedType
                            .GetInterfaces()
                            .Any(interfaceType =>
                                interfaceType.Assembly.FullName == spalInterfaceType.Assembly.FullName
                                    && interfaceType.FullName == spalInterfaceType.FullName))
                     .Where(exportedType =>
                        (concreteProviderType == null
                            && string.IsNullOrEmpty(spalId))
                        || (concreteProviderType == null
                            && !string.IsNullOrEmpty(spalId)
                            && exportedType.Namespace == spalId)
                        || (concreteProviderType != null
                            && concreteProviderType.FullName == exportedType.FullName
                            && string.IsNullOrEmpty(spalId))
                        || (concreteProviderType != null
                            && concreteProviderType.FullName == exportedType.FullName
                            && !string.IsNullOrEmpty(spalId)
                            && exportedType.Namespace == spalId))
                    .ToArray();

            return implementations;
        }

        private static Type[] GetExportedTypesFromAssemblyPath(
            string assemblyPath,
            Type spalInterfaceType,
            Type concreteTypeProvider,
            string spalId)
        {
            Assembly applicationAssembly = Assembly.LoadFrom(assemblyPath);

            return GetInterfaceImplementations(applicationAssembly, spalInterfaceType, concreteTypeProvider, spalId);
        }

        private static Type[] GetExportedTypesFromAssembliesPaths(Type spalInterfaceType, Type concreteTypeProvider, string spalId)
        {
            string[] applicationAssembliesPaths = GetApplicationAssemblies();
            Type[] exportedTypesOfT =
                applicationAssembliesPaths
                    .SelectMany(applicationAssemblyPath =>
                        GetExportedTypesFromAssemblyPath(applicationAssemblyPath, spalInterfaceType, concreteTypeProvider, spalId))
                    .ToArray();

            return exportedTypesOfT;
        }
    }
}
