using HarmonyLib;
using System;
using System.Collections;

namespace BepInEx5Plugins.Ash.Alisa.ControlsMenuLocalization.HarmonyPatches
{
	[HarmonyPatch(typeof(PauseMenu), "ButtonConfigButton")]
	public class PauseMenu_ButtonConfigButton
	{
		// Refresh localization.
		public static void Postfix(PauseMenu __instance
			, ref KeyBindScript ___keyBind
			)
		{
			Console.WriteLine("PauseMenu_ButtonConfigButton.Postfix: Refreshing buttons");

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
