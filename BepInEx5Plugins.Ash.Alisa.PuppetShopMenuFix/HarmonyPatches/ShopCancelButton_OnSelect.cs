using HarmonyLib;
using System;
using UnityEngine.EventSystems;

namespace BepInEx5Plugins.Ash.Alisa.PuppetShopMenuFix.HarmonyPatches
{
	[HarmonyPatch(typeof(ShopCancelButton), "OnSelect", new Type[] { typeof(BaseEventData) })]
	public class ShopCancelButton_OnSelect
	{
		// Disable button selection auto fix.
		public static void Prefix(ShopCancelButton __instance, BaseEventData eventData)
		{
			PuppetShopMenu_Update.checkPending = false;
		}
	}
}
