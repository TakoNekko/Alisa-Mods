using HarmonyLib;
using System;
using System.Collections;

namespace BepInEx5Plugins.Ash.Alisa.ControlsMenuLocalization.HarmonyPatches
{
	[HarmonyPatch(typeof(MainMenu), "ButtonConfigButton")]
	public class MainMenu_ButtonConfigButton
	{
		// Refresh localization.
		public static void Postfix(MainMenu __instance
			, ref KeyBindScript ___keyBind
			)
		{
			Console.WriteLine("MainMenu_ButtonConfigButton.Postfix: Refreshing buttons");
			
			if (__instance.locked)
			{
				return;
			}

			if (___keyBind)
			{
				___keyBind.RefreshButtons();
			}
		}
	}
}
