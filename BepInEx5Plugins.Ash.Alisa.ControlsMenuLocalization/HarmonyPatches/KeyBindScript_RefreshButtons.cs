using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace BepInEx5Plugins.Ash.Alisa.ControlsMenuLocalization.HarmonyPatches
{
	[HarmonyPatch(typeof(KeyBindScript), "RefreshButtons")]
	public class KeyBindScript_RefreshButtons
	{
		public static readonly Regex splitRegex = new Regex("([A-Z]+(?![a-z])|[A-Z][a-z]+|[0-9]+|[a-z]+)");

		// Ensure settings is assigned to a valid value.
		public static void Prefix(KeyBindScript __instance
			, ref Settings ___settings
			)
		{
			if (___settings is null)
			{
				Console.WriteLine("KeyBindScript_RefreshButtons.Prefix: settings is null! Auto fixing...");

				___settings = ProgressManager.instance.GetComponent<Settings>();
			}
		}

		// Apply custom localization to key buttons.
		public static void Postfix(KeyBindScript __instance
			, ref Settings ___settings
			)
		{
			Console.WriteLine("KeyBindScript_RefreshButtons.Postfix: Localizing labels");

			__instance.action.text = GetString(___settings.keys["Action"], ___settings);
			__instance.cancel.text = GetString(___settings.keys["Cancel"], ___settings);
			__instance.sprint.text = GetString(___settings.keys["Sprint"], ___settings);
			__instance.switchWeapon.text = GetString(___settings.keys["SwitchWeapon"], ___settings);
			__instance.aim.text = GetString(___settings.keys["Aim"], ___settings);
			__instance.submit.text = GetString(___settings.keys["Submit"], ___settings);
			__instance.select.text = GetString(___settings.keys["Select"], ___settings);
			__instance.special.text = GetString(___settings.keys["Special"], ___settings);
			__instance.reload.text = GetString(___settings.keys["Reload"], ___settings);
		}

		public static string GetString(KeyCode key, Settings settings)
		{
			var strings = (Dictionary<KeyCode, string>)null;

			if (settings.language == 0)
			{
				strings = KeyCodeLocalization.strings;
			}
			else if (settings.language == 1)
			{
				strings = KeyCodeLocalization.strings_French;
			}
			else if (settings.language == 2)
			{
				strings = KeyCodeLocalization.strings_Italian;
			}
			else if (settings.language == 3)
			{
				strings = KeyCodeLocalization.strings_Japanese;
			}
			else
			{
				strings = KeyCodeLocalization.strings_Custom;
			}

			if (strings is null)
			{
				Console.WriteLine("KeyBindScript_RefreshButtons.GetString: localization strings not ready yet!");
			}

			if (strings != null
				&& strings.ContainsKey(key))
			{
				return strings[key];
			}
			else
			{
#pragma warning disable Harmony003 // Harmony non-ref patch parameters modified
				var name = key.ToString();
#pragma warning restore Harmony003 // Harmony non-ref patch parameters modified

				if (settings.xboxType)
				{
					name = name
						.Replace("JoystickButton0", "ButtonA")
						.Replace("JoystickButton1", "ButtonB")
						.Replace("JoystickButton2", "ButtonX")
						.Replace("JoystickButton3", "ButtonY")
						.Replace("JoystickButton4", "ButtonLB")
						.Replace("JoystickButton5", "ButtonRB")
						.Replace("JoystickButton6", "ButtonBack")
						.Replace("JoystickButton7", "ButtonStart")
						.Replace("JoystickButton8", "ButtonLS")
						.Replace("JoystickButton9", "ButtonRS")
						;
				}

				name = name
					.Replace("Alpha", "")
					.Replace("Joystick", "")
					;

				return string.Join(" ", splitRegex.Split(name));
			}
		}
	}
}
