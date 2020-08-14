using UnityEngine;

namespace TC.Core
{
	public static class AnimationUtil
	{
		#region Animation

		public static AnimationState GetAnimState (Animation anim, string clipName = null)
		{
			var clip = string.IsNullOrEmpty (clipName) ? anim.clip : anim.GetClip (clipName);
			return clip != null ? anim[clip.name] : null;
		}

		public static void Sample (Animation anim, float time, string clipName = null, bool normalized = false)
		{
			var animState = anim.GetAnimState (clipName);
			if (animState == null) {
				return;
			}

			anim.Play (animState.name);
			if (normalized) {
				animState.normalizedTime = time;
			} else {
				animState.time = time;
			}
			animState.enabled = true;
			anim.Sample ();
			animState.enabled = false;
		}

		#endregion


		#region Animator

		public static void Sample (Animator animator, float time, string stateName, bool normalized = false)
		{
			if (normalized) {
				animator.Play (stateName, 0, time);
			} else {
				animator.PlayInFixedTime (stateName, 0, time);
			}
			animator.Update (0);
		}

		#endregion
	}
}