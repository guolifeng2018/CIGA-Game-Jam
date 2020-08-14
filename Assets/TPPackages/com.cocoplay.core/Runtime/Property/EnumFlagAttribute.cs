using UnityEngine;

namespace TC.Core
{
	public class EnumFlagAttribute : PropertyAttribute
	{
		public readonly string DisplayName;

		public EnumFlagAttribute ()
		{
		}

		public EnumFlagAttribute (string name)
		{
			DisplayName = name;
		}
	}
}