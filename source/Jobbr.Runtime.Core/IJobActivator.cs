﻿using System;

namespace Jobbr.Runtime.Core
{
    /// <summary>
    /// Represents an interface for the range of the dependencies.
    /// </summary>
    public interface IJobActivator
    {
        /// <summary>
        /// Retrieves a service from the scope.
        /// </summary>
        /// <param name="serviceType">The service to be retrieved.</param>
        /// <returns>The retrieved service.</returns>
        object Activate(Type serviceType);
    }
}
