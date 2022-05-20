﻿// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace RealityToolkit.DevTools.DiagnosticsService.MemoryDiagnostics
{
    public struct MemoryLimit
    {
        public MemoryLimit(ulong memoryLimit)
        {
            Value = memoryLimit;
        }

        public readonly ulong Value;

        public static implicit operator ulong(MemoryLimit self) => self.Value;
    }
}