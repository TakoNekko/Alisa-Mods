using HarmonyLib;

namespace BepInEx5Plugins.Ash.Alisa.FrameCounter.HarmonyPatches
{
	[HarmonyPatch(typeof(Puzzle01Interaction), "Start")]
	public class Puzzle01Interaction_Start
	{
		// Add a frameCounter component if necessary.
		public static void Postfix(Puzzle01Interaction __instance)
		{
			__instance.camera01?.AddComponentUnique<frameCounter>();
			__instance.camera02?.AddComponentUnique<frameCounter>();
			__instance.camera03?.AddComponentUnique<frameCounter>();
		}
	}
}
