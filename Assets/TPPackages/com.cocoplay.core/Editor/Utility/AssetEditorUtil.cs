using System.IO;
using UnityEditor;
using UnityEngine;

namespace TC.Core.Editor
{
	public static class AssetEditorUtil
	{
		public static void CreateAsset (Object asset, string path)
		{
			if (asset == null) {
				return;
			}

			var folder = Path.GetDirectoryName (path);
			CreateAssetFolder (folder);

			AssetDatabase.CreateAsset (asset, path);
		}

		public static void CreateAssetFolder (string folder)
		{
			if (string.IsNullOrEmpty (folder) || folder == "Assets") {
				return;
			}

			if (AssetDatabase.IsValidFolder (folder)) {
				return;
			}

			var parentFolder = Path.GetDirectoryName (folder);
			CreateAssetFolder (parentFolder);

			var subFolder = Path.GetFileName (folder);
			AssetDatabase.CreateFolder (parentFolder, subFolder);
		}

		public static void FocusScriptableObject<T> (string assetPath) where T : ScriptableObject
		{
			var settingsAsset = AssetDatabase.LoadAssetAtPath<T> (assetPath);

			if (settingsAsset == null) {
				settingsAsset = ScriptableObject.CreateInstance<T> ();
				CreateAsset (settingsAsset, assetPath);
				AssetDatabase.SaveAssets ();
			}

			Selection.activeObject = settingsAsset;
		}
	}
}