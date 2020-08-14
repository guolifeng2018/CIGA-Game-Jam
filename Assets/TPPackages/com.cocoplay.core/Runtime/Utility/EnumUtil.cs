using System;

namespace TC.Core
{
	public static class EnumUtil
	{
		public static bool ToEnum<TEnum> (string value, out TEnum result) where TEnum : struct
		{
			var success = true;
			try {
				result = (TEnum)Enum.Parse (typeof(TEnum), value);
			} catch (Exception) {
				result = default (TEnum);
				success = false;
			}
			return success;
		}
	}
}