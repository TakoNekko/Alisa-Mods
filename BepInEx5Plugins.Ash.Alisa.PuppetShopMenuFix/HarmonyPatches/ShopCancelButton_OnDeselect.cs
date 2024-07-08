using HarmonyLib;
using System;
using UnityEngine.EventSystems;

namespace BepInEx5Plugins.Ash.Alisa.PuppetShopMenuFix.HarmonyPatches
{
	[HarmonyPatch(typeof(ShopCancelButton), "OnDeselect", new Type[] { typeof(BaseEventData) })]
	public class ShopCancelButton_OnDeselect
	{
		// Enable button selection auto fix.
		public static void Prefix(ShopCancelButton __instance, BaseEventData eventData)
		{
			PuppetShopMenu_Update.checkPending = true;
		}
	}
}
