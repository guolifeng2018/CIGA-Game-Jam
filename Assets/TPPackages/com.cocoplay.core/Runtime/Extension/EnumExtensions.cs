using System;

namespace TC.Core
{
	public static class EnumExtensions
	{
		public static bool ToEnum<TEnum> (this string value, out TEnum result) where TEnum : struct
		{
			return EnumUtil.ToEnum (value, out result);
		}
	}
}