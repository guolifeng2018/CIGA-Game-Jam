using UnityEngine;
using UnityEditor;

namespace TC.Core.Editor
{
	public static class FindMissingScriptsRecursively
	{
		private static int _goCount, _componentCount, _missingCount;

		[MenuItem (EditorPreferences.MENU_NAME_COMMON + "Find Missing Scripts Recursively", false, EditorPreferences.MENU_PRIORITY_COMMON)]
		private static void Find ()
		{
			var gos = Selection.gameObjects;

			if (gos == null || gos.Length <= 0) {
				// no selection, collect from active scene
				var activeScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene ();
				gos = activeScene.GetRootGameObjects ();
			}

			_goCount = 0;
			_componentCount = 0;
			_missingCount = 0;
			foreach (var g in gos) {
				FindInGo (g);
			}
			Debug.LogErrorFormat ("Searched {0} GameObjects, {1} components, found {2} missing", _goCount, _componentCount, _missingCount);
		}

		private static void FindInGo (GameObject go)
		{
			_goCount++;
			var components = go.GetComponents<Component> ();
			for (var i = 0; i < components.Length; i++) {
				_componentCount++;
				if (components [i] != null) {
					continue;
				}

				_missingCount++;
				var s = go.name;
				var t = go.transform;
				while (t.parent != null) {
					var parent = t.parent;
					s = parent.name + "/" + s;
					t = parent;
				}
				Debug.LogWarningFormat (go, "{0} has an empty script attached in position [{1}]", s, i);
			}
			// Now recurse through each child GO (if there are any):
			foreach (Transform childT in go.transform) {
				//Debug.Log("Searching " + childT.name  + " " );
				FindInGo (childT.gameObject);
			}
		}
	}
}