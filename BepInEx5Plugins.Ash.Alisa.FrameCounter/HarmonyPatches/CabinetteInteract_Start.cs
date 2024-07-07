using HarmonyLib;

namespace BepInEx5Plugins.Ash.Alisa.FrameCounter.HarmonyPatches
{
	[HarmonyPatch(typeof(CabinetteInteract), "Start")]
	public class CabinetteInteract_Start
	{
		// Add a frameCounter component if necessary.
		public static void Postfix(CabinetteInteract __instance)
		{
			__instance.thePuzzleCam?.AddComponentUnique<frameCounter>();
			__instance.myCam?.AddComponentUnique<frameCounter>();
			__instance.cabinetteCam?.AddComponentUnique<frameCounter>();
		}
	}
}
