// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace RealityToolkit.DevTools.FrameDiagnostics
{
    public interface IFrameDiagnosticsHandler : IDiagnosticsHandler
    {
        /// <summary>
        /// Raised when the <see cref="IDiagnosticsSystem"/> frame rate has changed.
        /// </summary>
        void OnFrameRateChanged(FrameEventData eventData);

        /// <summary>
        /// A frame target was missed.
        /// </summary>
        void OnFrameMissed(FrameEventData eventData);
    }
}
