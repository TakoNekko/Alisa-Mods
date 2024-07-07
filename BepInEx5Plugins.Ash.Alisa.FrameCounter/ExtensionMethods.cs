using UnityEngine;

namespace BepInEx5Plugins.Ash.Alisa.FrameCounter
{
	public static partial class ExtensionMethods
	{
		public static T AddComponentUnique<T>(this GameObject instance) where T : Component
		{
			var t = instance.GetComponent<T>();

			if (!t)
			{
				t = instance.AddComponent<T>();
			}

			return t;
		}
	}
}
