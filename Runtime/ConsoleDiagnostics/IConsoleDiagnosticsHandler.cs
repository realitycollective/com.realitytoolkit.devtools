// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace RealityToolkit.DevTools.ConsoleDiagnostics
{
    public interface IConsoleDiagnosticsHandler : IDiagnosticsHandler
    {
        /// <summary>
        /// A new log entry was received.
        /// </summary>
        void OnLogReceived(ConsoleEventData eventData);
    }
}
