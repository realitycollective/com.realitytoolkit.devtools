// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace RealityToolkit.DevTools.DiagnosticsService.FrameDiagnostics
{
    public interface IFrameDiagnosticsHandler : IDiagnosticsHandler
    {
        /// <summary>
        /// Raised when the <see cref="IMixedRealityDiagnosticsSystem"/> frame rate has changed.
        /// </summary>
        void OnFrameRateChanged(FrameEventData eventData);

        /// <summary>
        /// A frame target was missed.
        /// </summary>
        void OnFrameMissed(FrameEventData eventData);
    }
}
