// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityCollective.Editor.Utilities;
using RealityCollective.Extensions;
using RealityToolkit.Editor;
using RealityToolkit.Editor.Utilities;
using System.IO;
using UnityEditor;

namespace RealityToolkit.DevTools.Editor
{
    [InitializeOnLoad]
    internal static class DevToolsPackageInstaller
    {
        private static readonly string DefaultPath = $"{MixedRealityPreferences.ProfileGenerationPath}DeveloperTools";
        private static readonly string HiddenPath = Path.GetFullPath($"{PathFinderUtility.ResolvePath<IPathFinder>(typeof(DevToolsPathFinder)).ForwardSlashes()}{Path.DirectorySeparatorChar}{MixedRealityPreferences.HIDDEN_PACKAGE_ASSETS_PATH}");

        static DevToolsPackageInstaller()
        {
            EditorApplication.delayCall += CheckPackage;
        }

        [MenuItem("Reality Toolkit/Packages/Install Deveveloper Tools Package Assets...", true)]
        private static bool ImportPackageAssetsValidation()
        {
            return !Directory.Exists($"{DefaultPath}{Path.DirectorySeparatorChar}");
        }

        [MenuItem("Reality Toolkit/Packages/Install Developer Tools Package Assets...")]
        private static void ImportPackageAssets()
        {
            EditorPreferences.Set($"{nameof(DevToolsPackageInstaller)}.Assets", false);
            EditorApplication.delayCall += CheckPackage;
        }

        private static void CheckPackage()
        {
            if (!EditorPreferences.Get($"{nameof(DevToolsPackageInstaller)}.Assets", false))
            {
                EditorPreferences.Set($"{nameof(DevToolsPackageInstaller)}.Assets", PackageInstaller.TryInstallAssets(HiddenPath, DefaultPath));
            }
        }
    }
}
