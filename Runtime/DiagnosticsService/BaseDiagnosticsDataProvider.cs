// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityToolkit.Definitions;
using RealityToolkit.Services;

namespace RealityToolkit.DevTools.DiagnosticsService
{
    /// <summary>
    /// Abstract base implementation for diagnostics data providers. Provides needed implementations to register and unregister
    /// diagnostics handlers.
    /// </summary>
    public abstract class BaseDiagnosticsDataProvider : BaseDataProvider, IDiagnosticsDataProvider
    {
        /// <inheritdoc />
        protected BaseDiagnosticsDataProvider(string name, uint priority, BaseMixedRealityProfile profile, IDiagnosticsService parentService)
            : base(name, priority, profile, parentService)
        {
            DiagnosticsService = parentService;
        }

        protected IDiagnosticsService DiagnosticsService;
    }
}