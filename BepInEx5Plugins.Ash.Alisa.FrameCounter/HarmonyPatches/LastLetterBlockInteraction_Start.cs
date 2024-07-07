using HarmonyLib;

namespace BepInEx5Plugins.Ash.Alisa.FrameCounter.HarmonyPatches
{
	[HarmonyPatch(typeof(LastLetterBlockInteraction), "Start")]
	public class LastLetterBlockInteraction_Start
	{
		// Add a frameCounter component if necessary.
		public static void Postfix(LastLetterBlockInteraction __instance)
		{
			__instance.myCamera?.AddComponentUnique<frameCounter>();
		}
	}
}
