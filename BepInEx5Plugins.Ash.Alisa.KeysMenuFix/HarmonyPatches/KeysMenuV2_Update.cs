using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;

namespace BepInEx5Plugins.Ash.Alisa.KeysMenuFix.HarmonyPatches
{
	[HarmonyPatch(typeof(KeysMenuV2), "Update")]
	public class KeysMenuV2_Update
	{
		private static MethodInfo KeysMenuV2_SelectedItem;
		private static MethodInfo KeysMenuV2_Move;
		private static MethodInfo KeysMenuV2_Zoom;
		private static MethodInfo KeysMenuV2_CloseKeysMenu;
		private static MethodInfo KeysMenuV2_ItemFunction;
		private static MethodInfo KeysMenuV2_KeyRingUpdate;
		
		public static bool Prepare(MethodBase original)
		{
			if (original is null)
			{
				try
				{
					KeysMenuV2_SelectedItem = typeof(KeysMenuV2).GetMethod("SelectedItem", BindingFlags.NonPublic | BindingFlags.Instance);
					KeysMenuV2_Move = typeof(KeysMenuV2).GetMethod("Move", BindingFlags.NonPublic | BindingFlags.Instance);
					KeysMenuV2_Zoom = typeof(KeysMenuV2).GetMethod("Zoom", BindingFlags.NonPublic | BindingFlags.Instance);
					KeysMenuV2_CloseKeysMenu = typeof(KeysMenuV2).GetMethod("CloseKeysMenu", BindingFlags.NonPublic | BindingFlags.Instance);
					KeysMenuV2_ItemFunction = typeof(KeysMenuV2).GetMethod("ItemFunction", BindingFlags.NonPublic | BindingFlags.Instance);
					KeysMenuV2_KeyRingUpdate = typeof(KeysMenuV2).GetMethod("KeyRingUpdate", BindingFlags.NonPublic | BindingFlags.Instance);
				}
				catch (Exception exception)
				{
					Console.WriteLine(exception);
					return false;
				}
			}

			return true;
		}

		// Call the Zoom method even if currentSelectedItem is null.
		public static void Postfix(KeysMenuV2 __instance)
		{
			if (!__instance.locked)
			{
				__instance.RayCheckItem();
				KeysMenuV2_SelectedItem.Invoke(__instance, null);
				KeysMenuV2_Move.Invoke(__instance, null);
				KeysMenuV2_Zoom.Invoke(__instance, null);

				if (__instance.currentSelectedItem)
				{
					KeysMenuV2_CloseKeysMenu.Invoke(__instance, null);
					KeysMenuV2_ItemFunction.Invoke(__instance, null);
				}

				if (__instance.keyRing_Obj.activeSelf)
				{
					KeysMenuV2_KeyRingUpdate.Invoke(__instance, null);
				}
			}

			if (__instance.locked
				&& __instance.currentSelectedItem)
			{
				__instance.currentSelectedItem.transform.rotation = Quaternion.identity;
			}

			if (!__instance.itemList.itemsTotal[0].activeSelf
				&& __instance.titleNone.activeSelf)
			{
				__instance.titleNone.SetActive(false);
			}
		}
	}
}
