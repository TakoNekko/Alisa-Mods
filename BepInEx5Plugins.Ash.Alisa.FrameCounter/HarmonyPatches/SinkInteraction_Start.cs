using HarmonyLib;

namespace BepInEx5Plugins.Ash.Alisa.FrameCounter.HarmonyPatches
{
	[HarmonyPatch(typeof(SinkInteraction), "Start")]
	public class SinkInteraction_Start
	{
		// Add a frameCounter component if necessary.
		public static void Postfix(SinkInteraction __instance)
		{
			__instance.camera01?.AddComponentUnique<frameCounter>();
			__instance.camera02?.AddComponentUnique<frameCounter>();
		}
	}
}
