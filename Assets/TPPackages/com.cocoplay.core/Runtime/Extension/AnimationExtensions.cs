using UnityEngine;

namespace TC.Core
{
	public static class AnimationExtensions
	{
		#region Animation

		public static AnimationState GetAnimState (this Animation anim, string clipName = null)
		{
			return AnimationUtil.GetAnimState (anim, clipName);
		}

		public static void Sample (this Animation anim, float time, string clipName = null, bool normalized = false)
		{
			AnimationUtil.Sample (anim, time, clipName, normalized);
		}

		#endregion


		#region Animator

		public static void Sample (this Animator animator, float time, string stateName, bool normalized = false)
		{
			AnimationUtil.Sample (animator, time, stateName, normalized);
		}

		#endregion
	}
}