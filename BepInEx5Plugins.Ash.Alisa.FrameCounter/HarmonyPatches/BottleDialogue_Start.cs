using HarmonyLib;

namespace BepInEx5Plugins.Ash.Alisa.FrameCounter.HarmonyPatches
{
	[HarmonyPatch(typeof(BottleDialogue), "Start")]
	public class BottleDialogue_Start
	{
		// Add a frameCounter component if necessary.
		public static void Postfix(BottleDialogue __instance)
		{
			__instance.myCamera?.AddComponentUnique<frameCounter>();
		}
	}
}
