using System;
using UnityEngine;

namespace TC.Core
{
	public abstract class OptionalValueBase
	{
	}

	[Serializable]
	public class OptionalValue<T> : OptionalValueBase
	{
		public OptionalValue ()
		{
			inUse = false;
			value = default(T);
		}

		public OptionalValue (T value)
		{
			inUse = true;
			this.value = value;
		}

		[SerializeField]
		private bool inUse = false;

		public bool InUse {
			get { return inUse; }
			set { inUse = value; }
		}

		[SerializeField]
		private T value;

		public T Value {
			get {
				if (inUse) {
					return value;
				}

				LogNotInUse ();
				return default(T);
			}
			set {
				if (inUse) {
					this.value = value;
					return;
				}

				LogNotInUse ();
			}
		}

		public override string ToString ()
		{
			return string.Format ("InUse: {0}, Value: {1}", inUse, value);
		}

		private void LogNotInUse ()
		{
			Debug.LogWarningFormat ("{0}: NOT in use!", GetType ());
		}

		public bool ValueIsEquals (OptionalValue<T> other)
		{
			if (other == null) {
				return false;
			}

			if (InUse != other.InUse) {
				return false;
			}

			if (!InUse) {
				return true;
			}

			if (Value == null && other.Value == null) {
				return true;
			}
			if (Value == null || other.Value == null) {
				return false;
			}

			return ValueNonNullIsEquals (other.Value);
		}

		public static bool ValueIsEquals (OptionalValue<T> p1, OptionalValue<T> p2)
		{
			if (p1 == null) {
				return p2 == null;
			}

			return p1.ValueIsEquals (p2);
		}

		protected virtual bool ValueNonNullIsEquals (T otherValue)
		{
			return value.Equals (otherValue);
		}
	}

	[Serializable]
	public class OptionalInt : OptionalValue<int>
	{
		public OptionalInt ()
		{
		}

		public OptionalInt (int value) : base (value)
		{
		}
	}

	[Serializable]
	public class OptionalFloat : OptionalValue<float>
	{
		public OptionalFloat ()
		{
		}

		public OptionalFloat (float value) : base (value)
		{
		}
	}

	[Serializable]
	public class OptionalString : OptionalValue<string>
	{
		public OptionalString ()
		{
		}

		public OptionalString (string value) : base (value)
		{
		}
	}

	[Serializable]
	public class OptionalVector2 : OptionalValue<Vector2>
	{
		public OptionalVector2 ()
		{
		}

		public OptionalVector2 (Vector2 value) : base (value)
		{
		}
	}

	[Serializable]
	public class OptionalVector3 : OptionalValue<Vector3>
	{
		public OptionalVector3 ()
		{
		}

		public OptionalVector3 (Vector3 value) : base (value)
		{
		}
	}

	[Serializable]
	public class OptionalColor : OptionalValue<Color>
	{
		public OptionalColor ()
		{
		}

		public OptionalColor (Color value) : base (value)
		{
		}
	}

	[Serializable]
	public class OptionalLayerMask : OptionalValue<LayerMask>
	{
		public OptionalLayerMask ()
		{
		}

		public OptionalLayerMask (LayerMask value) : base (value)
		{
		}

		public OptionalLayerMask (int value) : base (value)
		{
		}
	}

	[Serializable]
	public class OptionalRect : OptionalValue<Rect>
	{
		public OptionalRect ()
		{
		}

		public OptionalRect (Rect value) : base (value)
		{
		}
	}

	[Serializable]
	public class OptionalSprite : OptionalValue<Sprite>
	{
		public OptionalSprite ()
		{
		}

		public OptionalSprite (Sprite value) : base (value)
		{
		}
	}
}