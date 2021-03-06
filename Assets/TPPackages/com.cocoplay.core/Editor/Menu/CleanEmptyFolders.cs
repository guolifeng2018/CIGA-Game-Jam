﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace TC.Core.Editor
{
	public static class CleanEmptyFolders
	{
		[MenuItem (EditorPreferences.MENU_NAME_COMMON + "Clean Empty Folders", false, EditorPreferences.MENU_PRIORITY_COMMON + 1)]
		private static void Clean ()
		{
			List<string> emptyFolders;
			var isEmpty = IsEmptyFolder (Application.dataPath, out emptyFolders);
			if (isEmpty) {
				return;
			}

			foreach (var emptyFolder in emptyFolders) {
				Debug.LogWarningFormat ("delete empty folder: ---- {0}", emptyFolder);
				Directory.Delete (emptyFolder, true);

				var meta = emptyFolder + ".meta";
				if (File.Exists (meta)) {
					File.Delete (meta);
				}
			}

			AssetDatabase.Refresh ();
		}

		private static bool IsEmptyFolder (string folderPath, out List<string> emptySubFolders)
		{
			emptySubFolders = new List<string> ();
			if (ContainValidFolder (folderPath, out emptySubFolders)) {
				return false;
			}

			return !ContainValidFile (folderPath);
		}

		private static bool ContainValidFile (string folderPath)
		{
			var files = Directory.GetFiles (folderPath);
			return files.Any (file => !file.EndsWith (".meta") && !file.EndsWith (".DS_Store") && !file.EndsWith ("Thumbs.db"));
		}

		private static bool ContainValidFolder (string folderPath, out List<string> emptyFolders)
		{
			var contained = false;

			var folders = Directory.GetDirectories (folderPath);
			emptyFolders = new List<string> (folders.Length);
			foreach (var folder in folders) {
				List<string> emptySubFolders;
				if (IsEmptyFolder (folder, out emptySubFolders)) {
					emptyFolders.Add (folder);
					continue;
				}

				if (emptySubFolders.Count > 0) {
					emptyFolders.AddRange (emptySubFolders);
				}
				contained = true;
			}

			return contained;
		}
	}
}