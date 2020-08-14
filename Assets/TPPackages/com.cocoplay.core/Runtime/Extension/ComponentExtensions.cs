using UnityEngine;

namespace TC.Core
{
	public static class ComponentExtensions
	{
		#region Get/Add Component

		public static T GetOrAddComponent<T> (this Component com) where T : Component
		{
			return ObjectUtil.GetOrAddComponent<T> (com);
		}

		public static Component GetOrAddComponent (this Component com, System.Type type)
		{
			return ObjectUtil.GetOrAddComponent (com, type);
		}

		#endregion


		#region Remove

		public static void RemoveComponent<T> (this Component com) where T : Component
		{
			ObjectUtil.RemoveComponent<T> (com);
		}

		public static void RemoveComponent (this Component com, System.Type type)
		{
			ObjectUtil.RemoveComponent (com, type);
		}

		#endregion
	}
}