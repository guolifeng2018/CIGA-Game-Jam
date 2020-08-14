using UnityEngine;

namespace TC.Core
{
	public static class MathUtil
	{
		/// <summary>
		/// similar with Mathf.Approximately, only more error range
		/// </summary>
		public static bool Approximately (float v1, float v2, float errorRange = 0.0001f)
		{
			return Mathf.Abs (v1 - v2) <= errorRange;
		}

		/// <summary>
		/// ensure that the angle is within -180 to 180 range.
		/// </summary>
		public static float WrapAngle (float angle)
		{
			while (angle > 180f)
				angle -= 360f;
			while (angle < -180f)
				angle += 360f;
			return angle;
		}
	}
}