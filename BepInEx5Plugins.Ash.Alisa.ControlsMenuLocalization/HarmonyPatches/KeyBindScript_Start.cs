using HarmonyLib;
using System;

namespace BepInEx5Plugins.Ash.Alisa.ControlsMenuLocalization.HarmonyPatches
{
	[HarmonyPatch(typeof(KeyBindScript), "Start")]
	public class KeyBindScript_Start
	{
		// Load custom localization files.
		public static void Prefix(KeyBindScript __instance)
		{
			try
			{
				Console.WriteLine("KeyBindScript_Start.Prefix: Loading localization tables");

				KeyCodeLocalization.Load();
				AxisLocalization.Load();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		// Refresh buttons.
		public static void Postfix(KeyBindScript __instance)
		{
			Console.WriteLine("KeyBindScript_Start.Postfix: Refresh buttons");

			__instance.RefreshButtons();
		}
	}
}
