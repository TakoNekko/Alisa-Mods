using HarmonyLib;
using UnityEngine.SceneManagement;

namespace BepInEx5Plugins.Ash.Alisa.PuppetShopDemoPrices.HarmonyPatches
{
	[HarmonyPatch(typeof(PuppetShopMenu), "BuyM1")]
	public class PuppetShopMenu_BuyM1
	{
		public static bool IsDemo
			=> SceneManager.sceneCountInBuildSettings == 20;

		// Temporarily give the player enough toothwheels to buy the M1 if necessary.
		public static void Prefix(PuppetShopMenu __instance, out bool __state)
		{
			__state = false;

			if (IsDemo
				&& ProgressManager.instance.data.currentToothWheels >= 22
				&& !ProgressManager.instance.data.m1Sold)
			{
				ProgressManager.instance.data.currentToothWheels += 10;
				__state = true;
			}
		}

		// Adjust the amount of recorded used toothwheels if necessary.
		public static void Postfix(PuppetShopMenu __instance, bool __state)
		{
			if (__state)
			{
				ProgressManager.instance.data.usedToothwheels -= 10;
			}
		}
	}
}
