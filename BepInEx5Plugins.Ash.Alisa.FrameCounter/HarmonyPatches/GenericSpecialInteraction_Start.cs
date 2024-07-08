using HarmonyLib;

namespace BepInEx5Plugins.Ash.Alisa.FrameCounter.HarmonyPatches
{
	[HarmonyPatch(typeof(GenericSpecialInteraction), "Start")]
	public class GenericSpecialInteraction_Start
	{
		// Add a frameCounter component if necessary.
		public static void Postfix(GenericSpecialInteraction __instance)
		{
			__instance.specialCamera?.AddComponentUnique<frameCounter>();
		}
	}
}
