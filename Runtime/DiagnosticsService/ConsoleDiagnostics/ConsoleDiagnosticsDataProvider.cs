// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityToolkit.Definitions;
using UnityEngine;

namespace RealityToolkit.DevTools.DiagnosticsService.ConsoleDiagnostics
{
    /// <summary>
    /// Console diagnostics data providers mirrors the Unity console and digests logs so the
    /// diagnostics system can work with it.
    /// </summary>
    [System.Runtime.InteropServices.Guid("06916F29-4640-475E-8BF6-313C6B831FCF")]
    public class ConsoleDiagnosticsDataProvider : BaseDiagnosticsDataProvider
    {
        /// <inheritdoc />
        public ConsoleDiagnosticsDataProvider(string name, uint priority, BaseMixedRealityProfile profile, IDiagnosticsService parentService)
            : base(name, priority, profile, parentService)
        {
        }

        #region IMixedRealityServce Implementation

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

        #endregion IMixedRealityServce Implementation
    }
}