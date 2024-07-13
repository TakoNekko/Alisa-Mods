using HarmonyLib;
using System;
using System.Collections;
using UnityEngine;

namespace BepInEx5Plugins.Ash.Alisa.EquipmentMenuFix.HarmonyPatches
{
	[HarmonyPatch(typeof(InventoryTabButtons), "KeysButton")]
	public class InventoryTabButtons_KeysButton
	{
		// Make all concerned buttons non interactable, and restore them after a delay.
		public static void Prefix(InventoryTabButtons __instance
			, ref EquipmentMenu ___equipment
			)
		{
			if (!___equipment.buttonLock)
			{
				Console.WriteLine("Patching keys button");

				var canvasGroup = ___equipment.GetComponent<CanvasGroup>();

				if (canvasGroup)
				{
					canvasGroup.interactable = false;

					__instance.StartCoroutine(RestoreInteraction(___equipment, canvasGroup));
				}
			}
		}

		public static IEnumerator RestoreInteraction(EquipmentMenu equipment, CanvasGroup canvasGroup)
		{
			yield return new WaitForSeconds(0.5f);

			Console.WriteLine("Restoring buttons");

			canvasGroup.interactable = true;
		}
	}
}
