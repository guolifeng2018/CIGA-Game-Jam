using UnityEngine;

namespace TC.Core
{
	public static class GameObjectExtensions
	{
		#region Get/Add Component

		public static T GetOrAddComponent<T> (this GameObject go) where T : Component
		{
			return ObjectUtil.GetOrAddComponent<T> (go);
		}

		public static Component GetOrAddComponent (this GameObject go, System.Type type)
		{
			return ObjectUtil.GetOrAddComponent (go, type);
		}

		#endregion


		#region Remove

		public static void RemoveComponent<T> (this GameObject go) where T : Component
		{
			ObjectUtil.RemoveComponent<T> (go);
		}

		public static void RemoveComponent (this GameObject go, System.Type type)
		{
			ObjectUtil.RemoveComponent (go, type);
		}

		#endregion
	}
}