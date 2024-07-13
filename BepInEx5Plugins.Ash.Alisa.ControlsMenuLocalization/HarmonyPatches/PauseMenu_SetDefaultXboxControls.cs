using HarmonyLib;
using System;
using UnityEngine;

namespace BepInEx5Plugins.Ash.Alisa.ControlsMenuLocalization.HarmonyPatches
{
	[HarmonyPatch(typeof(PauseMenu), "SetDefaultXboxControls")]
	public class PauseMenu_SetDefaultXboxControls
	{
		// Refresh localization.
		public static void Postfix(PauseMenu __instance
			, ref KeyBindScript ___keyBind
			)
		{
			if (Application.platform != RuntimePlatform.OSXPlayer)
			{
				Console.WriteLine("PauseMenu_SetDefaultXboxControls.Postfix: Refreshing buttons");

				if (___keyBind)
				{
					___keyBind.RefreshButtons();
				}
			}
		}
	}
}
