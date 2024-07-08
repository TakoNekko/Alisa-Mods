using HarmonyLib;

namespace BepInEx5Plugins.Ash.Alisa.FrameCounter.HarmonyPatches
{
	[HarmonyPatch(typeof(BathInteraction), "Start")]
	public class BathInteraction_Start
	{
		// Add a frameCounter component if necessary.
		public static void Postfix(BathInteraction __instance)
		{
			__instance.cam01?.AddComponentUnique<frameCounter>();
			__instance.cam02?.AddComponentUnique<frameCounter>();
		}
	}
}
