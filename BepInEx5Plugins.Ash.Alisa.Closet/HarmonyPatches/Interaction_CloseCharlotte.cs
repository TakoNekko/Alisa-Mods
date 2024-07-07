using HarmonyLib;

namespace BepInEx5Plugins.Ash.Alisa.Closet.HarmonyPatches
{
	[HarmonyPatch(typeof(Interaction), "CloseCharlotte")]
	public class Interaction_CloseCharlotte
	{
		// Disable button selection auto fix.
		public static void Prefix(Interaction __instance
			, ref EquipmentMenu ___equipmentMenuScript
			)
		{
			if (___equipmentMenuScript
				&& !___equipmentMenuScript.buttonLock)
			{
				ClosetButtonsSorting_Update.checkPending = false;
			}
		}
	}
}
