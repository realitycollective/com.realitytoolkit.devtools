// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace RealityToolkit.DevTools.DiagnosticsService.FrameDiagnostics
{
    public interface IFrameDiagnosticsDataProvider : IDiagnosticsDataProvider
    {
        /// <summary>
        /// The last computed GPU frame rate.
        /// </summary>
        int GPUFrameRate { get; }

        /// <summary>
        /// The last computed CPU frame rate.
        /// </summary>
        int CPUFrameRate { get; }
    }
}
