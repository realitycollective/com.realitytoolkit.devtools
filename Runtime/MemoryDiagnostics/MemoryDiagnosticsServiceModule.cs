// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityCollective.ServiceFramework.Attributes;
using RealityCollective.ServiceFramework.Definitions;
using RealityCollective.ServiceFramework.Definitions.Platforms;
using UnityEngine.Profiling;

namespace RealityToolkit.DevTools.MemoryDiagnostics
{
    /// <summary>
    /// Diagnostics service module for memory diagnostics. E.g. provides information about used application memory.
    /// </summary>
    [RuntimePlatform(typeof(AllPlatforms))]
    [System.Runtime.InteropServices.Guid("9F9C6912-DD68-4010-8B4A-B7B01B6AD77B")]
    public class MemoryDiagnosticsServiceModule : BaseDiagnosticsServiceModule
    {
        /// <inheritdoc />
        public MemoryDiagnosticsServiceModule(string name, uint priority, BaseProfile profile, IDiagnosticsService parentService)
            : base(name, priority, profile, parentService)
        {
        }

        private ulong lastMemoryUsage;
        private ulong peakMemoryUsage;
        private ulong lastMemoryLimit;

        #region IMixedRealityService Implementation

        /// <inheritdoc />
        public override void LateUpdate()
        {
            base.LateUpdate();

            var systemMemorySize = (ulong)Profiler.GetTotalReservedMemoryLong();

            if (lastMemoryUsage != systemMemorySize)
            {
                if (systemMemorySize > lastMemoryLimit)
                {
                    DiagnosticsService.RaiseMemoryLimitChanged(new MemoryLimit(systemMemorySize));
                    lastMemoryLimit = systemMemorySize;
                }
            }

            var currentMemoryUsage = (ulong)Profiler.GetTotalAllocatedMemoryLong();

            if (currentMemoryUsage != lastMemoryUsage)
            {
                DiagnosticsService.RaiseMemoryUsageChanged(new MemoryUsage(currentMemoryUsage));
                lastMemoryUsage = currentMemoryUsage;
            }

            if (lastMemoryUsage > peakMemoryUsage)
            {
                DiagnosticsService.RaiseMemoryPeakChanged(new MemoryPeak(peakMemoryUsage));
                peakMemoryUsage = lastMemoryUsage;
            }
        }

        #endregion IMixedRealityService Implementation
    }
}