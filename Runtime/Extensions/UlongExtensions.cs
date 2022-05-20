// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace RealityToolkit.DevTools.Extensions
{
    /// <summary>
    /// Diagnostics utilities.
    /// </summary>
    public static class UlongExtensions
    {
        /// <summary>
        /// Converts bytes to megabytes.
        /// </summary>
        /// <param name="bytes">Amount of bytes.</param>
        /// <returns>Amount of megabytes.</returns>
        public static float ToMegabytes(this ulong bytes) => bytes / 1024.0f / 1024.0f;
    }
}