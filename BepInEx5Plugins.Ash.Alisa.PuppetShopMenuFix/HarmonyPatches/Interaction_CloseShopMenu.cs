using HarmonyLib;

namespace BepInEx5Plugins.Ash.Alisa.PuppetShopMenuFix.HarmonyPatches
{
	[HarmonyPatch(typeof(Interaction), "CloseShopMenu")]
	public class Interaction_CloseShopMenu
	{
		// Disable button selection auto fix.
		public static void Prefix(Interaction __instance
			//, ref EquipmentMenu ___equipmentMenuScript
			)
		{
			//if (!___equipmentMenuScript.buttonLock)
			{
				PuppetShopMenu_Update.checkPending = false;
			}
		}
	}
}
