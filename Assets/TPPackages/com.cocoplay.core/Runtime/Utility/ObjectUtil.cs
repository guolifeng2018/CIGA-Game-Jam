using UnityEngine;

namespace TC.Core
{
	public static class ObjectUtil
	{
		#region Get/Add Component

		public static T GetOrAddComponent<T> (GameObject go) where T : Component
		{
			var com = go.GetComponent<T> ();
			if (com == null) {
				com = go.AddComponent<T> ();
				if (com == null) {
					Debug.LogErrorFormat ("GameObjectUtil->GetOrAddComponent: component [{0}] can NOT be added.", typeof(T).Name);
				}
			}

			return com;
		}

		public static T GetOrAddComponent<T> (Component com) where T : Component
		{
			return GetOrAddComponent<T> (com.gameObject);
		}

		public static Component GetOrAddComponent (GameObject go, System.Type type)
		{
			var com = go.GetComponent (type);
			if (com == null) {
				com = go.AddComponent (type);
				if (com == null) {
					Debug.LogErrorFormat ("GameObjectUtil->GetOrAddComponent: component [{0}] can NOT be added.", type.Name);
				}
			}

			return com;
		}

		public static Component GetOrAddComponent (Component com, System.Type type)
		{
			return GetOrAddComponent (com.gameObject, type);
		}

		#endregion


		#region Remove Component

		public static void RemoveComponent<T> (GameObject go) where T : Component
		{
			var component = go.GetComponent<T> ();
			if (component != null) {
				Object.Destroy (component);
			}
		}

		public static void RemoveComponent<T> (Component com) where T : Component
		{
			RemoveComponent<T> (com.gameObject);
		}

		public static void RemoveComponent (GameObject go, System.Type type)
		{
			var component = go.GetComponent (type);
			if (component != null) {
				Object.Destroy (component);
			}
		}

		public static void RemoveComponent (Component com, System.Type type)
		{
			RemoveComponent (com.gameObject, type);
		}

		#endregion


		#region Instantiate

		public static GameObject Instantiate (GameObject prefabGo, Transform parent = null,
			TransformUtil.StayOption stayOption = TransformUtil.StayOption.Local)
		{
			if (prefabGo == null) {
				return null;
			}

			switch (stayOption) {
			case TransformUtil.StayOption.Local:
				return Object.Instantiate (prefabGo, parent, false);

			case TransformUtil.StayOption.World:
				return Object.Instantiate (prefabGo, parent, true);

			default: // Reset
				var go = Object.Instantiate (prefabGo, parent, false);
				TransformUtil.Reset (go.transform);
				return go;
			}
		}

		public static GameObject Instantiate (string prefabResPath, Transform parent = null,
			TransformUtil.StayOption stayOption = TransformUtil.StayOption.Local)
		{
			var prefabGo = Resources.Load<GameObject> (prefabResPath);
			return Instantiate (prefabGo, parent, stayOption);
		}

		public static T Instantiate<T> (T prefabCom, Transform parent = null, TransformUtil.StayOption stayOption = TransformUtil.StayOption.Local)
			where T : Component
		{
			if (prefabCom == null) {
				return null;
			}

			switch (stayOption) {
			case TransformUtil.StayOption.Local:
				return Object.Instantiate (prefabCom, parent, false);

			case TransformUtil.StayOption.World:
				return Object.Instantiate (prefabCom, parent, true);

			default: // Reset
				var com = Object.Instantiate (prefabCom, parent, false);
				TransformUtil.Reset (com.transform);
				return com;
			}
		}

		public static T Instantiate<T> (GameObject prefabGo, Transform parent = null, TransformUtil.StayOption stayOption = TransformUtil.StayOption.Local)
			where T : Component
		{
			if (prefabGo == null) {
				return null;
			}

			var prefabCom = prefabGo.GetComponent<T> ();
			return Instantiate (prefabCom, parent, stayOption);
		}

		public static T Instantiate<T> (string prefabResPath, Transform parent = null, TransformUtil.StayOption stayOption = TransformUtil.StayOption.Local)
			where T : Component
		{
			var prefabGo = Resources.Load<GameObject> (prefabResPath);
			return Instantiate<T> (prefabGo, parent, stayOption);
		}

		#endregion


		#region Instantiate/Create

		public static GameObject InstantiateOrCreate (GameObject prefabGo, Transform parent = null,
			TransformUtil.StayOption stayOption = TransformUtil.StayOption.Local)
		{
			if (prefabGo != null) {
				return Instantiate (prefabGo, parent, stayOption);
			}

			var go = new GameObject ();
			TransformUtil.SetParent (go.transform, parent, stayOption);
			return go;
		}

		public static GameObject InstantiateOrCreate (string prefabResPath, Transform parent = null,
			TransformUtil.StayOption stayOption = TransformUtil.StayOption.Local)
		{
			var prefabGo = Resources.Load<GameObject> (prefabResPath);
			return InstantiateOrCreate (prefabGo, parent, stayOption);
		}

		public static T InstantiateOrCreate<T> (T prefabCom, Transform parent = null, TransformUtil.StayOption stayOption = TransformUtil.StayOption.Local)
			where T : Component
		{
			if (prefabCom != null) {
				return Instantiate (prefabCom, parent, stayOption);
			}

			return InstantiateOrCreate<T> ((GameObject) null, parent, stayOption);
		}

		public static T InstantiateOrCreate<T> (GameObject prefabGo, Transform parent = null,
			TransformUtil.StayOption stayOption = TransformUtil.StayOption.Local)
			where T : Component
		{
			var go = InstantiateOrCreate (prefabGo, parent, stayOption);
			return GetOrAddComponent<T> (go);
		}

		public static T InstantiateOrCreate<T> (string prefabResPath, Transform parent = null,
			TransformUtil.StayOption stayOption = TransformUtil.StayOption.Local)
			where T : Component
		{
			var prefabGo = Resources.Load<GameObject> (prefabResPath);
			return InstantiateOrCreate<T> (prefabGo, parent, stayOption);
		}

		#endregion
	}
}