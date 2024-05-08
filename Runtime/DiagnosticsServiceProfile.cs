// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityCollective.ServiceFramework.Definitions;
using RealityCollective.ServiceFramework.Definitions.Utilities;
using RealityCollective.Utilities.Attributes;
using UnityEngine;

namespace RealityToolkit.DevTools
{
    /// <summary>
    /// Configuration profile settings for setting up diagnostics.
    /// </summary>
    public class DiagnosticsServiceProfile : BaseServiceProfile<IDiagnosticsServiceModule>
    {
        [Prefab]
        [SerializeField]
        [Tooltip("The prefab instantiated to visualize diagnostics data.")]
        private GameObject diagnosticsWindowPrefab = null;

        /// <summary>
        /// The prefab instantiated to visualize diagnostics data.
        /// </summary>
        public GameObject DiagnosticsWindowPrefab
        {
            get => diagnosticsWindowPrefab;
            private set => diagnosticsWindowPrefab = value;
        }

        [SerializeField]
        [Tooltip("Should the diagnostics window be opened on application start?")]
        private AutoStartBehavior showDiagnosticsWindowOnStart = AutoStartBehavior.ManualStart;

        /// <summary>
        /// Should the diagnostics window be opened on application start?
        /// </summary>
        public AutoStartBehavior ShowDiagnosticsWindowOnStart => showDiagnosticsWindowOnStart;
    }
}