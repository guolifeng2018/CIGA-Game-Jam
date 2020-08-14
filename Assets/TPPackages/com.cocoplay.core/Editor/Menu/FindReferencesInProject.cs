using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace TC.Core.Editor
{
	public static class FindProject
	{
#if UNITY_EDITOR_OSX

		[MenuItem (EditorPreferences.MENU_NAME_ASSETS + "[TC] Find References In Project", false, EditorPreferences.MENU_PRIORITY_MAX)]
		private static void FindProjectReferences ()
		{
			var appDataPath = Application.dataPath;
			var output = "";
			var selectedAssetPath = AssetDatabase.GetAssetPath (Selection.activeObject);
			var references = new List<string> ();

			var guid = AssetDatabase.AssetPathToGUID (selectedAssetPath);

			var psi = new System.Diagnostics.ProcessStartInfo {
				WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized,
				FileName = "/usr/bin/mdfind",
				Arguments = "-onlyin " + Application.dataPath + " " + guid,
				UseShellExecute = false,
				RedirectStandardOutput = true,
				RedirectStandardError = true
			};

			var process = new System.Diagnostics.Process { StartInfo = psi };

			process.OutputDataReceived += (sender, e) => {
				if (string.IsNullOrEmpty (e.Data))
					return;

				var relativePath = "Assets" + e.Data.Replace (appDataPath, "");

				// skip the meta file of whatever we have selected
				if (relativePath == selectedAssetPath + ".meta")
					return;

				references.Add (relativePath);
			};

			var errorOutput = string.Empty;
			process.ErrorDataReceived += (sender, e) => {
				if (string.IsNullOrEmpty (e.Data))
					return;

				errorOutput += "Error: " + e.Data + "\n";
			};
			process.Start ();
			process.BeginOutputReadLine ();
			process.BeginErrorReadLine ();

			process.WaitForExit (2000);

			output += errorOutput;
			foreach (var file in references) {
				output += file + "\n";
				Debug.Log (file, AssetDatabase.LoadMainAssetAtPath (file));
			}

			Debug.LogWarning (references.Count + " references found for object " + Selection.activeObject.name + "\n\n" + output);
		}

#endif
	}
}