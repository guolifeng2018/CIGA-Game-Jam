using System.Collections;
using UnityEngine;

namespace TC.Core
{
	public static class WaitUtil
	{
		public static IEnumerator WaitForTime (float time, System.Action action = null)
		{
			yield return new WaitForSeconds (time);

			if (action != null) {
				action ();
			}
		}

		public static IEnumerator WaitForRealTime (float time, System.Action action = null)
		{
			yield return new WaitForSecondsRealtime (time);

			if (action != null) {
				action ();
			}
		}

		public static IEnumerator WaitForFrame (int frameCount, System.Action action = null)
		{
			while (frameCount-- > 0) {
				yield return new WaitForEndOfFrame ();
			}

			if (action != null) {
				action ();
			}
		}

		public static IEnumerator WaitWhile (System.Func<bool> predicate, System.Action action = null)
		{
			yield return new WaitWhile (predicate);

			if (action != null) {
				action ();
			}
		}

		public static IEnumerator WaitUntil (System.Func<bool> predicate, System.Action action = null)
		{
			yield return new WaitUntil (predicate);

			if (action != null) {
				action ();
			}
		}

		public static IEnumerator WaitForYield (IEnumerator enumerator, System.Action action = null)
		{
			yield return enumerator;

			if (action != null) {
				action ();
			}
		}

		public static IEnumerator WaitForYield (YieldInstruction yieldInst, System.Action action = null)
		{
			yield return yieldInst;

			if (action != null) {
				action ();
			}
		}
	}
}