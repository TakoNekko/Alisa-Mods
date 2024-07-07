using HarmonyLib;
using Object = UnityEngine.Object;

namespace BepInEx5Plugins.Ash.Alisa.TextScrolling.HarmonyPatches
{
	[HarmonyPatch(typeof(TextBoxManager), "EnableTextBox")]
	public class TextBoxManager_EnableTextBox
	{
		// Replace call to TextScroll.
		public static bool Prefix(TextBoxManager __instance
			, ref bool ___cantSkip
			)
		{
			___cantSkip = true;
			__instance.isActive = true;

			var interaction = Object.FindObjectOfType<Interaction>();

			if (interaction)
			{
				interaction.interactionIsActive = true;
			}

			if (__instance.isScrollingText)
			{
				__instance.StartCoroutine(TextBoxManager_Update.TextScroll(__instance, __instance.textLines[__instance.currentLine]));
			}

			return false;
		}
	}
}
