using System;
using UnityEngine;

namespace TC.Core
{
	public abstract class RangeValueBase
	{
	}

	[Serializable]
	public class RangeValue<T> : RangeValueBase
	{
		public RangeValue (T from, T to)
		{
			this.from = from;
			this.to = to;
		}

		public T from;

		public T to;

		public override string ToString ()
		{
			return string.Format ("({0} <-> {1})", from, to);
		}
	}

	[Serializable]
	public class RangeInt : RangeValue<int>
	{
		public RangeInt (int from, int to) : base (from, to)
		{
		}
	}

	[Serializable]
	public class RangeFloat : RangeValue<float>
	{
		public RangeFloat (float from, float to) : base (from, to)
		{
		}
	}

	[Serializable]
	public class RangeVector2 : RangeValue<Vector2>
	{
		public RangeVector2 (Vector2 from, Vector2 to) : base (from, to)
		{
		}
	}

	[Serializable]
	public class RangeVector3 : RangeValue<Vector3>
	{
		public RangeVector3 (Vector3 from, Vector3 to) : base (from, to)
		{
		}
	}

	[Serializable]
	public class RangeColor : RangeValue<Color>
	{
		public RangeColor (Color from, Color to) : base (from, to)
		{
		}
	}
}