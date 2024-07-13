using HarmonyLib;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BepInEx5Plugins.Ash.Alisa.EquipmentMenuFix.HarmonyPatches
{
	[HarmonyPatch(typeof(EquipmentMenuSlotButtonSelect), "OnSelect", new Type[] { typeof(BaseEventData) })]
	public class EquipmentMenuSlotButtonSelect_OnSelect
	{
		// Prevent processing when button is not interactable.
		public static bool Prefix(EquipmentMenuSlotButtonSelect __instance, BaseEventData eventData)
		{
			try
			{
				var button = __instance.GetComponent<Button>();

				if (!button)
				{
					return true;
				}

				if (button && !button.interactable)
				{
					return false;
				}

				if (button)
				{
					var canvasGroup = button.transform.parent?.parent?.GetComponent<CanvasGroup>();

					if (canvasGroup && !canvasGroup.interactable)
					{
						return false;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			return true;
		}
	}
}
