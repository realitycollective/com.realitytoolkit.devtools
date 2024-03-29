﻿// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace RealityToolkit.DevTools.MemoryDiagnostics
{
    public struct MemoryPeak
    {
        public MemoryPeak(ulong memoryPeak)
        {
            Value = memoryPeak;
        }

        public readonly ulong Value;

        public static implicit operator ulong(MemoryPeak self) => self.Value;
    }
}