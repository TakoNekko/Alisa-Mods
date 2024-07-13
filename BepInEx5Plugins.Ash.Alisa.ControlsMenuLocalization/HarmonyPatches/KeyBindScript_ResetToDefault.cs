using HarmonyLib;
using System;

namespace BepInEx5Plugins.Ash.Alisa.ControlsMenuLocalization.HarmonyPatches
{
	[HarmonyPatch(typeof(KeyBindScript), "ResetToDefault")]
	public class KeyBindScript_ResetToDefault
	{
		// Refresh localization.
		public static void Postfix(KeyBindScript __instance)
		{
			Console.WriteLine("KeyBindScript_ResetToDefault.Postfix: Refreshing buttons");

			__instance.RefreshButtons();
		}
	}
}
