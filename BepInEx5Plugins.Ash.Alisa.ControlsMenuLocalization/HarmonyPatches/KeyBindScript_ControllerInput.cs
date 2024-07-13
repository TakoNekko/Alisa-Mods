using HarmonyLib;
using System;

namespace BepInEx5Plugins.Ash.Alisa.ControlsMenuLocalization.HarmonyPatches
{
	[HarmonyPatch(typeof(KeyBindScript), "ControllerInput")]
	public class KeyBindScript_ControllerInput
	{
		// Refresh localization.
		public static void Postfix(KeyBindScript __instance)
		{
			Console.WriteLine("KeyBindScript_ControllerInput.Postfix: Refreshing buttons");

			__instance.RefreshButtons();
		}
	}
}
