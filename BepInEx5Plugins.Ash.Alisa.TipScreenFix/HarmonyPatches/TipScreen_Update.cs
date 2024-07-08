using HarmonyLib;
using UnityEngine.SceneManagement;

namespace BepInEx5Plugins.Ash.Alisa.TipScreenFix.HarmonyPatches
{
	[HarmonyPatch(typeof(TipScreen), "Update")]
	public class TipScreen_Update
	{
		// Forcibly disable combat tip if player is no longer in the lounge.
		private static void Postfix(TipScreen __instance)
		{
			if (__instance.tip02IsPossible
				&& !SceneManager.GetActiveScene().name.StartsWith("Lounge"))
			{
				__instance.tip02IsPossible = false;
			}
		}
	}
}
