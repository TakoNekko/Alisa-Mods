using HarmonyLib;

namespace BepInEx5Plugins.Ash.Alisa.FrameCounter.HarmonyPatches
{
	[HarmonyPatch(typeof(DisplayCaseInteraction), "Awake")]
	public class DisplayCaseInteraction_Awake
	{
		// Add a frameCounter component if necessary.
		public static void Postfix(DisplayCaseInteraction __instance)
		{
			__instance.theCamera?.AddComponentUnique<frameCounter>();
		}
	}
}
