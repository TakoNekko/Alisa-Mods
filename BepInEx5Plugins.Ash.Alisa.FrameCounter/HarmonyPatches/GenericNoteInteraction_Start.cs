using HarmonyLib;

namespace BepInEx5Plugins.Ash.Alisa.FrameCounter.HarmonyPatches
{
	[HarmonyPatch(typeof(GenericNoteInteraction), "Start")]
	public class GenericNoteInteraction_Start
	{
		// Add a frameCounter component if necessary.
		public static void Postfix(GenericNoteInteraction __instance)
		{
			__instance.myCamera?.AddComponentUnique<frameCounter>();
		}
	}
}
