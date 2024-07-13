using HarmonyLib;
using System;

namespace BepInEx5Plugins.Ash.Alisa.ControlsMenuLocalization.HarmonyPatches
{
	[HarmonyPatch(typeof(KeyBindScript), "CheckForDirectionOverlap")]
	public class KeyBindScript_CheckForDirectionOverlap
	{
		// Refresh localization.
		public static void Postfix(KeyBindScript __instance)
		{
			Console.WriteLine("KeyBindScript_CheckForDirectionOverlap.Postfix: Refreshing buttons");

			__instance.RefreshButtons();
		}
	}
}
