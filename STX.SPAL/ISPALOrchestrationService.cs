// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using STX.SPAL.Abstractions;
using System;

namespace STX.SPAL
{
    public interface ISPALOrchestrationService
    {
        T GetImplementation<T>() where T : ISPALProvider;
        T GetImplementation<T>(Type concreteTypeProvider) where T : ISPALProvider;
        T GetImplementation<T>(string spalId) where T : ISPALProvider;
        T GetImplementation<T>(Type concreteProviderType, string spalId) where T : ISPALProvider;
        T[] GetImplementations<T>(Type concreteProviderType, string spalId) where T : ISPALProvider;
    }
}
