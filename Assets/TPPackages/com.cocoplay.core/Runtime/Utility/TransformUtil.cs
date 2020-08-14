using UnityEngine;

namespace TC.Core
{
	public static class TransformUtil
	{
		#region Position

		public static void SetPosX (Transform trans, float x)
		{
			var pos = trans.position;
			pos.x = x;
			trans.position = pos;
		}

		public static void SetPosY (Transform trans, float y)
		{
			var pos = trans.position;
			pos.y = y;
			trans.position = pos;
		}

		public static void SetPosZ (Transform trans, float z)
		{
			var pos = trans.position;
			pos.z = z;
			trans.position = pos;
		}

		public static void SetLocalPosX (Transform trans, float x)
		{
			var pos = trans.localPosition;
			pos.x = x;
			trans.localPosition = pos;
		}

		public static void SetLocalPosY (Transform trans, float y)
		{
			var pos = trans.localPosition;
			pos.y = y;
			trans.localPosition = pos;
		}

		public static void SetLocalPosZ (Transform trans, float z)
		{
			var pos = trans.localPosition;
			pos.z = z;
			trans.localPosition = pos;
		}

		public static void AddPosX (Transform trans, float x)
		{
			trans.position += new Vector3 (x, 0, 0);
		}

		public static void AddPosY (Transform trans, float y)
		{
			trans.position += new Vector3 (0, y, 0);
		}

		public static void AddPosZ (Transform trans, float z)
		{
			trans.position += new Vector3 (0, 0, z);
		}

		public static void AddLocalPosX (Transform trans, float x)
		{
			trans.localPosition += new Vector3 (x, 0, 0);
		}

		public static void AddLocalPosY (Transform trans, float y)
		{
			trans.localPosition += new Vector3 (0, y, 0);
		}

		public static void AddLocalPosZ (Transform trans, float z)
		{
			trans.localPosition += new Vector3 (0, 0, z);
		}

		#endregion


		#region Child

		public static void RemoveAllChildren (Transform trans)
		{
			foreach (Transform child in trans) {
				Object.Destroy (child.gameObject);
			}
		}

		public static void SetChildLayer (Transform trans, int layer)
		{
			for (var i = 0; i < trans.childCount; ++i) {
				var child = trans.GetChild (i);
				child.gameObject.layer = layer;
				SetChildLayer (child, layer);
			}
		}

		public static void SetSelfAndChildLayer (Transform trans, int layer)
		{
			trans.gameObject.layer = layer;
			SetChildLayer (trans, layer);
		}

		#endregion


		#region Parent

		public enum StayOption
		{
			Local,
			World,
			Reset
		}

		public static void SetParent (Transform trans, Transform parent, StayOption stayOption = StayOption.Local)
		{
			switch (stayOption) {
			case StayOption.Local:
				trans.SetParent (parent, false);
				break;
			case StayOption.World:
				trans.SetParent (parent, true);
				break;
			default:
				trans.SetParent (parent, false);
				Reset (trans);
				break;
			}
		}

		public static void Reset (Transform trans)
		{
			trans.localPosition = Vector3.zero;
			trans.localRotation = Quaternion.identity;
			trans.localScale = Vector3.one;
		}

		#endregion
	}
}