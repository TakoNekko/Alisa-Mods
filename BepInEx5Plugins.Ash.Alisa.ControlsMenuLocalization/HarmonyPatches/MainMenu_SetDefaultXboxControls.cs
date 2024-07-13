using HarmonyLib;
using System;
using UnityEngine;

namespace BepInEx5Plugins.Ash.Alisa.ControlsMenuLocalization.HarmonyPatches
{
	[HarmonyPatch(typeof(MainMenu), "SetDefaultXboxControls")]
	public class MainMenu_SetDefaultXboxControls
	{
		// Refresh localization.
		public static void Postfix(MainMenu __instance
			, ref KeyBindScript ___keyBind
			)
		{
			if (Application.platform != RuntimePlatform.OSXPlayer)
			{
				Console.WriteLine("MainMenu_SetDefaultXboxControls.Postfix: Refreshing buttons");

				if (___keyBind)
				{
					___keyBind.RefreshButtons();
				}
			}
		}
	}
}
