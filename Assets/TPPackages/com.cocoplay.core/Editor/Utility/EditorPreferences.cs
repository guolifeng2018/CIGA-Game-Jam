namespace TC.Core.Editor
{
	public static class EditorPreferences
	{
		// menu name
		public const string MENU_NAME_ASSETS = "Assets/";
		public const string MENU_NAME_ROOT = "TC/";
		public const string MENU_NAME_COMMON = MENU_NAME_ROOT + "Common/";
		public const string MENU_NAME_DEBUG = MENU_NAME_ROOT + "Debug/";

		// menu priority
		public const int MENU_PRIORITY_COMMON = 0;
		public const int MENU_PRIORITY_DEBUG = 10;
		public const int MENU_PRIORITY_MAX = 2000;

		// asset path
		public const string GAME_RESOURCE_ROOT_DIRECTORY = "Assets/_Game/Resources/";
	}
}