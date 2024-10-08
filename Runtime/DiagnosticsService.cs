﻿// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityCollective.ServiceFramework.Attributes;
using RealityCollective.ServiceFramework.Definitions.Platforms;
using RealityCollective.ServiceFramework.Definitions.Utilities;
using RealityCollective.ServiceFramework.Services;
using RealityCollective.Utilities.Extensions;
using RealityToolkit.DevTools.ConsoleDiagnostics;
using RealityToolkit.DevTools.FrameDiagnostics;
using RealityToolkit.DevTools.MemoryDiagnostics;
using RealityToolkit.Player;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

namespace RealityToolkit.DevTools
{
    /// <summary>
    /// The default implementation of the <see cref="IDiagnosticsService"/>
    /// </summary>
    [RuntimePlatform(typeof(AllPlatforms))]
    [System.Runtime.InteropServices.Guid("2044B5AE-8F50-4B66-B508-D8087356C140")]
    public class DiagnosticsService : BaseEventService, IDiagnosticsService
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="profile">Diagnostics system configuration profile.</param>
        public DiagnosticsService(string name, uint priority, DiagnosticsServiceProfile profile)
            : base(name, priority, profile)
        {
            this.profile = profile;
        }

        private readonly DiagnosticsServiceProfile profile;

        private FrameEventData frameEventData;
        private MemoryEventData memoryEventData;
        private ConsoleEventData consoleEventData;

        private Transform rigTransform = null;

        private Transform RigTransform
        {
            get
            {
                if (rigTransform == null)
                {
                    rigTransform = ServiceManager.Instance.TryGetService<IPlayerService>(out var playerService)
                        ? playerService.PlayerRig.RigTransform
                        : Camera.main.transform.parent;
                }
                return rigTransform;
            }
        }

        #region IService Implementation

        /// <inheritdoc />
        public override void Initialize()
        {
            base.Initialize();

            if (!Application.isPlaying)
            {
                return;
            }

            var currentEventSystem = EventSystem.current;
            frameEventData = new FrameEventData(currentEventSystem);
            consoleEventData = new ConsoleEventData(currentEventSystem);
            memoryEventData = new MemoryEventData(currentEventSystem);

            if (profile.ShowDiagnosticsWindowOnStart == AutoStartBehavior.AutoStart)
            {
                IsWindowEnabled = true;
            }
        }

        /// <inheritdoc />
        public override void Destroy()
        {
            if (diagnosticsWindow.IsNotNull())
            {
                Unregister(diagnosticsWindow);
            }

            diagnosticsWindow.Destroy();

            if (!diagnosticsRoot.IsNull() &&
                !diagnosticsRoot.gameObject.IsNull())
            {
                diagnosticsRoot.gameObject.Destroy();
            }
        }

        #endregion IService Implementation

        #region IDiagnosticsSystem Implementation

        private Transform diagnosticsRoot = null;

        /// <inheritdoc />
        public Transform DiagnosticsRoot
        {
            get
            {
                if (diagnosticsRoot.IsNull())
                {
                    diagnosticsRoot = new GameObject("Diagnostics").transform;
                    diagnosticsRoot.transform.SetParent(RigTransform, false);
                }

                return diagnosticsRoot;
            }
        }

        private GameObject diagnosticsWindow = null;

        /// <inheritdoc />
        public GameObject DiagnosticsWindow
        {
            get
            {
                if (diagnosticsWindow.IsNull())
                {
                    diagnosticsWindow = Object.Instantiate(profile.DiagnosticsWindowPrefab, DiagnosticsRoot);
                    Register(diagnosticsWindow);
                }

                return diagnosticsWindow;
            }
        }

        /// <inheritdoc />
        public string ApplicationSignature => $"{Application.productName} v{Application.version}";

        private bool isWindowEnabled = false;

        /// <inheritdoc />
        public bool IsWindowEnabled
        {
            get => DiagnosticsWindow.activeInHierarchy && isWindowEnabled;
            set
            {
                if (isWindowEnabled == value)
                {
                    return;
                }

                isWindowEnabled = value;
                DiagnosticsWindow.SetActive(isWindowEnabled);
            }
        }

        #region Console Events

        /// <inheritdoc />
        public void RaiseLogReceived(string message, string stackTrace, LogType logType)
        {
            consoleEventData.Initialize(message, stackTrace, logType);
            HandleEvent(consoleEventData, OnLogReceived);
        }

        private static readonly ExecuteEvents.EventFunction<IConsoleDiagnosticsHandler> OnLogReceived =
            delegate (IConsoleDiagnosticsHandler handler, BaseEventData eventData)
            {
                var casted = ExecuteEvents.ValidateEventData<ConsoleEventData>(eventData);
                handler.OnLogReceived(casted);
            };

        #endregion Console Events

        #region Frame Events

        /// <inheritdoc />
        public void RaiseMissedFramesChanged(bool[] missedFrames)
        {
            frameEventData.Initialize(missedFrames);
            HandleEvent(frameEventData, OnFrameMissed);
        }

        private static readonly ExecuteEvents.EventFunction<IFrameDiagnosticsHandler> OnFrameMissed =
            delegate (IFrameDiagnosticsHandler handler, BaseEventData eventData)
            {
                var casted = ExecuteEvents.ValidateEventData<FrameEventData>(eventData);
                handler.OnFrameMissed(casted);
            };

        /// <inheritdoc />
        public void RaiseFrameRateChanged(int frameRate, bool isGPU)
        {
            frameEventData.Initialize(frameRate, isGPU);
            HandleEvent(frameEventData, OnFrameRateChanged);
        }

        private static readonly ExecuteEvents.EventFunction<IFrameDiagnosticsHandler> OnFrameRateChanged =
            delegate (IFrameDiagnosticsHandler handler, BaseEventData eventData)
            {
                var casted = ExecuteEvents.ValidateEventData<FrameEventData>(eventData);
                handler.OnFrameRateChanged(casted);
            };

        #endregion Frame Events

        #region Memory Events

        /// <inheritdoc />
        public void RaiseMemoryLimitChanged(MemoryLimit currentMemoryLimit)
        {
            memoryEventData.Initialize(currentMemoryLimit);
            HandleEvent(memoryEventData, OnMemoryLimitChanged);
        }

        private static readonly ExecuteEvents.EventFunction<IMemoryDiagnosticsHandler> OnMemoryLimitChanged =
            delegate (IMemoryDiagnosticsHandler handler, BaseEventData eventData)
            {
                var casted = ExecuteEvents.ValidateEventData<MemoryEventData>(eventData);
                handler.OnMemoryLimitChanged(casted);
            };

        /// <inheritdoc />
        public void RaiseMemoryUsageChanged(MemoryUsage currentMemoryUsage)
        {
            memoryEventData.Initialize(currentMemoryUsage);
            HandleEvent(memoryEventData, OnMemoryUsageChanged);
        }

        private static readonly ExecuteEvents.EventFunction<IMemoryDiagnosticsHandler> OnMemoryUsageChanged =
            delegate (IMemoryDiagnosticsHandler handler, BaseEventData eventData)
            {
                var casted = ExecuteEvents.ValidateEventData<MemoryEventData>(eventData);
                handler.OnMemoryUsageChanged(casted);
            };

        /// <inheritdoc />
        public void RaiseMemoryPeakChanged(MemoryPeak peakMemoryUsage)
        {
            memoryEventData.Initialize(peakMemoryUsage);
            HandleEvent(memoryEventData, OnMemoryPeakChanged);
        }

        private static readonly ExecuteEvents.EventFunction<IMemoryDiagnosticsHandler> OnMemoryPeakChanged =
            delegate (IMemoryDiagnosticsHandler handler, BaseEventData eventData)
            {
                var casted = ExecuteEvents.ValidateEventData<MemoryEventData>(eventData);
                handler.OnMemoryPeakChanged(casted);
            };

        #endregion Memory Events

        #endregion IDiagnosticsSystem Implementation
    }
}
