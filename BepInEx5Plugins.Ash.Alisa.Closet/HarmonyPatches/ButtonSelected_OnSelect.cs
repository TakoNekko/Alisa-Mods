using HarmonyLib;
using System;
using UnityEngine.EventSystems;

namespace BepInEx5Plugins.Ash.Alisa.Closet.HarmonyPatches
{
	[HarmonyPatch(typeof(ButtonSelected), "OnSelect", new Type[] { typeof(BaseEventData) })]
	public class ButtonSelected_OnSelect
	{
		// Disable button selection auto fix.
		public static void Prefix(ButtonSelected __instance, BaseEventData eventData)
		{
			ClosetButtonsSorting_Update.checkPending = false;
		}
	}
}
