using HarmonyLib;

namespace BepInEx5Plugins.Ash.Alisa.FrameCounter.HarmonyPatches
{
	[HarmonyPatch(typeof(Drawer01Interaction), "Awake")]
	public class Drawer01Interaction_Awake
	{
		// Add a frameCounter component if necessary.
		public static void Postfix(Drawer01Interaction __instance)
		{
			__instance.theCamera?.AddComponentUnique<frameCounter>();
		}
	}
}
