using HarmonyLib;

namespace BepInEx5Plugins.Ash.Alisa.FrameCounter.HarmonyPatches
{
	[HarmonyPatch(typeof(Puzzle02Interaction), "Start")]
	public class Puzzle02Interaction_Start
	{
		// Add a frameCounter component if necessary.
		public static void Postfix(Puzzle02Interaction __instance)
		{
			__instance.camera01?.gameObject.AddComponentUnique<frameCounter>();
			__instance.puzzleCamera?.AddComponentUnique<frameCounter>();
		}
	}
}
