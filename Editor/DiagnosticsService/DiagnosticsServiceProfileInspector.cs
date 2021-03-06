// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.﻿

using RealityCollective.Editor.Extensions;
using RealityToolkit.DevTools.DiagnosticsService;
using RealityToolkit.Editor.Profiles;
using UnityEditor;
using UnityEngine;

namespace RealityToolkit.DevTools.Editor.DiagnosticsService
{
    [CustomEditor(typeof(DiagnosticsServiceProfile))]
    public class DiagnosticsServiceProfileInspector : MixedRealityServiceProfileInspector
    {
        private SerializedProperty diagnosticsWindowPrefab;
        private SerializedProperty showDiagnosticsWindowOnStart;

        private readonly GUIContent generalSettingsFoldoutHeader = new GUIContent("General Settings");

        protected override void OnEnable()
        {
            base.OnEnable();

            diagnosticsWindowPrefab = serializedObject.FindProperty(nameof(diagnosticsWindowPrefab));
            showDiagnosticsWindowOnStart = serializedObject.FindProperty(nameof(showDiagnosticsWindowOnStart));
        }

        public override void OnInspectorGUI()
        {
            RenderHeader("Diagnostics monitors system resources and performance inside an application during development.");

            serializedObject.Update();

            if (diagnosticsWindowPrefab.FoldoutWithBoldLabelPropertyField(generalSettingsFoldoutHeader))
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(showDiagnosticsWindowOnStart);
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Space();

            serializedObject.ApplyModifiedProperties();

            base.OnInspectorGUI();
        }
    }
}
