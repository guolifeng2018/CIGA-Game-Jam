using UnityEditor;

namespace TC.Core.Editor
{
	public class DebugMenuItems
	{
		#region Debug Settings

		private const string SETTINGS_ASSET_PATH = EditorPreferences.GAME_RESOURCE_ROOT_DIRECTORY + DebugSettings.RESOURCE_PATH + ".asset";

		[MenuItem (EditorPreferences.MENU_NAME_DEBUG + "Debug Settings ...", false, EditorPreferences.MENU_PRIORITY_DEBUG)]
		public static void FocusAssetObject ()
		{
			AssetEditorUtil.FocusScriptableObject<DebugSettings> (SETTINGS_ASSET_PATH);
		}

		#endregion
	}
}