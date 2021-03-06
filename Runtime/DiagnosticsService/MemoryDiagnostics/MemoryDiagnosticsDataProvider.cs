// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityToolkit.Definitions;
using UnityEngine.Profiling;

namespace RealityToolkit.DevTools.DiagnosticsService.MemoryDiagnostics
{
    /// <summary>
    /// Diagnostics data provider for memory diagnostics. E.g. provides information about used application memory.
    /// </summary>
    [System.Runtime.InteropServices.Guid("9F9C6912-DD68-4010-8B4A-B7B01B6AD77B")]
    public class MemoryDiagnosticsDataProvider : BaseDiagnosticsDataProvider
    {
        /// <inheritdoc />
        public MemoryDiagnosticsDataProvider(string name, uint priority, BaseMixedRealityProfile profile, IDiagnosticsService parentService)
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