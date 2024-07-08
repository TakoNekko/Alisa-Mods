using HarmonyLib;

namespace BepInEx5Plugins.Ash.Alisa.FrameCounter.HarmonyPatches
{
	[HarmonyPatch(typeof(DeadDollInteraction), "Start")]
	public class DeadDollInteraction_Start
	{
		// Add a frameCounter component if necessary.
		public static void Postfix(DeadDollInteraction __instance)
		{
			__instance.theCamera?.AddComponentUnique<frameCounter>();
		}
	}
}
