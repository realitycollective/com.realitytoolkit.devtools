// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityCollective.ServiceFramework.Attributes;
using RealityCollective.ServiceFramework.Definitions;
using RealityCollective.ServiceFramework.Definitions.Platforms;
using UnityEngine;

namespace RealityToolkit.DevTools.ConsoleDiagnostics
{
    /// <summary>
    /// Console diagnostics service module mirrors the Unity console and digests logs so the
    /// diagnostics system can work with it.
    /// </summary>
    [RuntimePlatform(typeof(AllPlatforms))]
    [System.Runtime.InteropServices.Guid("06916F29-4640-475E-8BF6-313C6B831FCF")]
    public class ConsoleDiagnosticsServiceModule : BaseDiagnosticsServiceModule
    {
        /// <inheritdoc />
        public ConsoleDiagnosticsServiceModule(string name, uint priority, BaseProfile profile, IDiagnosticsService parentService)
            : base(name, priority, profile, parentService)
        {
        }

        /// <inheritdoc />
        public override void Enable()
        {
            base.Enable();

            if (DiagnosticsService != null)
            {
                Application.logMessageReceived += DiagnosticsService.RaiseLogReceived;
            }
        }

        /// <inheritdoc />
        public override void Disable()
        {
            base.Disable();

            if (DiagnosticsService != null)
            {
                Application.logMessageReceived -= DiagnosticsService.RaiseLogReceived;
            }
        }
    }
}