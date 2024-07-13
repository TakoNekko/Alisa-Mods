using HarmonyLib;
using System;
using System.Collections;
using UnityEngine;

namespace BepInEx5Plugins.Ash.Alisa.EquipmentMenuFix.HarmonyPatches
{
	[HarmonyPatch(typeof(Interaction), "CloseMenu")]
	public class Interaction_CloseMenu
	{
		// Make all concerned buttons non interactable, and restore them after a delay.
		public static void Prefix(Interaction __instance
			, ref EquipmentMenu ___equipmentMenuScript
			)
		{
			if (!___equipmentMenuScript.buttonLock)
			{
				Console.WriteLine("Patching exit button");

				var canvasGroup = ___equipmentMenuScript.GetComponent<CanvasGroup>();

				if (canvasGroup)
				{
					canvasGroup.interactable = false;

					__instance.StartCoroutine(ClosingMenu(___equipmentMenuScript, canvasGroup));
				}
			}
		}

		public static IEnumerator ClosingMenu(EquipmentMenu equipment, CanvasGroup canvasGroup)
		{
			yield return new WaitForSeconds(0.4f);
			yield return new WaitForSeconds(0.5f);

			System.Console.WriteLine("Restoring buttons");

			canvasGroup.interactable = true;
		}
	}
}
