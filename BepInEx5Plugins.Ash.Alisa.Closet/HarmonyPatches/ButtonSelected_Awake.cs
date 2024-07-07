using HarmonyLib;
using System;
using UnityEngine;

namespace BepInEx5Plugins.Ash.Alisa.Closet.HarmonyPatches
{
	[HarmonyPatch(typeof(ButtonSelected), "Awake")]
	public class ButtonSelected_Awake
	{
		// Forcibly assign null fields to appopriate values if possible.
		public static void Postfix(ButtonSelected __instance)
		{
			if (!__instance.slot_Button01)
			{
				Console.WriteLine("ButtonSelected_Awake.Postfix: Force initialization of slot_Button01");

				__instance.slot_Button01 = __instance.buttonSet01?.transform.parent?.Find("SlotButtons")?.Find("Slot01")?.gameObject;
			}

			if (!__instance.slot_Button02)
			{
				Console.WriteLine("ButtonSelected_Awake.Postfix: Force initialization of slot_Button02");

				__instance.slot_Button02 = __instance.buttonSet02?.transform.parent?.Find("SlotButtons")?.Find("Slot02")?.gameObject;
			}

			if (!__instance.slot_Button03)
			{
				Console.WriteLine("ButtonSelected_Awake.Postfix: Force initialization of slot_Button03");

				__instance.slot_Button03 = __instance.buttonSet03?.transform.parent?.Find("SlotButtons")?.Find("Slot03")?.gameObject;
			}

			if (!__instance.myHighlightedBackground)
			{
				Console.WriteLine("ButtonSelected_Awake.Postfix: Force initialization of myHighlightedBackground");

				var target = (GameObject)null;

				if (__instance.slotButton01)
				{
					target = __instance.buttonSet01;
				}
				else if (__instance.slotButton02)
				{
					target = __instance.buttonSet02;
				}
				else if (__instance.slotButton03)
				{
					target = __instance.buttonSet03;
				}

				if (target)
				{
					for (var i = 0; i < target.transform.childCount; ++i)
					{
						var child = target.transform.GetChild(i);

						if (child.name.StartsWith("ClosetSlotBackground"))
						{
							__instance.myHighlightedBackground = child.gameObject;
							break;
						}
					}
				}
			}
		}
	}
}
