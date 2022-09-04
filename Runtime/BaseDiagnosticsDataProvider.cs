// Copyright (c) Reality Collective. All rights reserved.
// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityCollective.ServiceFramework.Definitions;
using RealityCollective.ServiceFramework.Providers;

namespace RealityToolkit.DevTools
{
    /// <summary>
    /// Abstract base implementation for diagnostics data providers. Provides needed implementations to register and unregister
    /// diagnostics handlers.
    /// </summary>
    public abstract class BaseDiagnosticsDataProvider : BaseServiceDataProvider, IDiagnosticsDataProvider
    {
        /// <inheritdoc />
        protected BaseDiagnosticsDataProvider(string name, uint priority, BaseProfile profile, IDiagnosticsService parentService)
            : base(name, priority, profile, parentService)
        {
            DiagnosticsService = parentService;
        }

        protected IDiagnosticsService DiagnosticsService;
    }
}