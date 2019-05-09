using System;

namespace ToggleSwitch.Utils
{
	// Token: 0x02000005 RID: 5
	internal static class HelperExtensions
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00003318 File Offset: 0x00001518
		public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
		{
			if (val.CompareTo(min) < 0)
			{
				return min;
			}
			if (val.CompareTo(max) <= 0)
			{
				return val;
			}
			return max;
		}
	}
}
