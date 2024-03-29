﻿// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityCollective.ServiceFramework.Definitions;
using RealityCollective.ServiceFramework.Modules;

namespace RealityToolkit.DevTools
{
    /// <summary>
    /// Abstract base implementation for <see cref="IDiagnosticsServiceModule"/>s.
    /// Provides needed implementations to register and unregister diagnostics handlers.
    /// </summary>
    public abstract class BaseDiagnosticsServiceModule : BaseServiceModule, IDiagnosticsServiceModule
    {
        /// <inheritdoc />
        protected BaseDiagnosticsServiceModule(string name, uint priority, BaseProfile profile, IDiagnosticsService parentService)
            : base(name, priority, profile, parentService)
        {
            DiagnosticsService = parentService;
        }

        protected IDiagnosticsService DiagnosticsService;
    }
}