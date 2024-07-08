using HarmonyLib;

namespace BepInEx5Plugins.Ash.Alisa.FrameCounter.HarmonyPatches
{
	[HarmonyPatch(typeof(ClockInteraction), "Start")]
	public class ClockInteraction_Start
	{
		// Add a frameCounter component if necessary.
		public static void Postfix(ClockInteraction __instance)
		{
			__instance.clockCamera?.AddComponentUnique<frameCounter>();
		}
	}
}
