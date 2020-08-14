using UnityEngine;

namespace TC.Core
{
	public static class TransformExtensions
	{
		#region Position

		public static void SetPosX (this Transform trans, float x)
		{
			TransformUtil.SetPosX (trans, x);
		}

		public static void SetPosY (this Transform trans, float y)
		{
			TransformUtil.SetPosY (trans, y);
		}

		public static void SetPosZ (this Transform trans, float z)
		{
			TransformUtil.SetPosZ (trans, z);
		}

		public static void SetLocalPosX (this Transform trans, float x)
		{
			TransformUtil.SetLocalPosX (trans, x);
		}

		public static void SetLocalPosY (this Transform trans, float y)
		{
			TransformUtil.SetLocalPosY (trans, y);
		}

		public static void SetLocalPosZ (this Transform trans, float z)
		{
			TransformUtil.SetLocalPosZ (trans, z);
		}

		public static void AddPosX (this Transform trans, float x)
		{
			TransformUtil.AddPosX (trans, x);
		}

		public static void AddPosY (this Transform trans, float y)
		{
			TransformUtil.AddPosY (trans, y);
		}

		public static void AddPosZ (this Transform trans, float z)
		{
			TransformUtil.AddLocalPosZ (trans, z);
		}

		public static void AddLocalPosX (this Transform trans, float x)
		{
			TransformUtil.AddLocalPosX (trans, x);
		}

		public static void AddLocalPosY (this Transform trans, float y)
		{
			TransformUtil.AddLocalPosY (trans, y);
		}

		public static void AddLocalPosZ (this Transform trans, float z)
		{
			TransformUtil.AddLocalPosZ (trans, z);
		}

		#endregion


		#region Child

		public static void RemoveAllChildren (this Transform trans)
		{
			TransformUtil.RemoveAllChildren (trans);
		}

		public static void SetChildLayer (this Transform trans, int layer)
		{
			TransformUtil.SetChildLayer (trans, layer);
		}

		public static void SetSelfAndChildLayer (this Transform trans, int layer)
		{
			TransformUtil.SetSelfAndChildLayer (trans, layer);
		}

		#endregion


		#region Parent

		public static void SetParent (this Transform trans, Transform parent, TransformUtil.StayOption stayOption = TransformUtil.StayOption.Local)
		{
			TransformUtil.SetParent (trans, parent, stayOption);
		}

		public static void Reset (this Transform trans)
		{
			trans.localPosition = Vector3.zero;
			trans.localRotation = Quaternion.identity;
			trans.localScale = Vector3.one;
		}

		#endregion
	}
}