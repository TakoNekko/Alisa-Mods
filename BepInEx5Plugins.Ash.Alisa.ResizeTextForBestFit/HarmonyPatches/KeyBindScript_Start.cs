using HarmonyLib;
using UnityEngine.UI;

namespace BepInEx5Plugins.Ash.Alisa.ResizeTextForBestFit.HarmonyPatches
{
	[HarmonyPatch(typeof(KeyBindScript), "Start")]
	public class KeyBindScript_Start
	{
		public static int maxSize = 18;

		public static bool singleOutline = true;

		// Set every text child components to fit automatically.
		public static void Postfix(KeyBindScript __instance)
		{
			foreach (var text in __instance.GetComponentsInChildren<Text>())
			{
				text.resizeTextForBestFit = true;
				text.resizeTextMaxSize = maxSize;

				if (singleOutline)
				{
					var outlines = text.GetComponents<Outline>();

					for (var i = 1; i < outlines.Length; ++i)
					{
						var outline = outlines[i];

						outline.enabled = false;
					}
				}
			}
		}
	}
}
