using HarmonyLib;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BepInEx5Plugins.Ash.Alisa.ControlsMenuLocalization.HarmonyPatches
{
	[HarmonyPatch(typeof(KeyBindScript), "DpadNames")]
	public class KeyBindScript_DpadNames
	{
		// Apply custom localization to axii buttons.
		public static void Postfix(KeyBindScript __instance
			, ref Settings ___settings
			)
		{
			Console.WriteLine("KeyBindScript_DpadNames.Postfix: Refreshing buttons");

			if (___settings.keyboardAlt != 0)
			{
				return;
			}

			if (___settings.horizontalAxis == 0)
			{
				__instance.leftButton.transform.GetChild(0).GetComponent<Text>().text = KeyBindScript_RefreshButtons.GetString(KeyCode.LeftArrow, ___settings);
				__instance.rightButton.transform.GetChild(0).GetComponent<Text>().text = KeyBindScript_RefreshButtons.GetString(KeyCode.RightArrow, ___settings);
			}
			else
			{
				__instance.leftButton.transform.GetChild(0).GetComponent<Text>().text = GetString(___settings.horizontalAxis * -1, ___settings);
				__instance.rightButton.transform.GetChild(0).GetComponent<Text>().text = GetString(___settings.horizontalAxis, ___settings);
			}

			if (___settings.verticalAxis == 0)
			{
				__instance.upButton.transform.GetChild(0).GetComponent<Text>().text = KeyBindScript_RefreshButtons.GetString(KeyCode.UpArrow, ___settings);
				__instance.downButton.transform.GetChild(0).GetComponent<Text>().text = KeyBindScript_RefreshButtons.GetString(KeyCode.DownArrow, ___settings);
			}
			else
			{
				__instance.upButton.transform.GetChild(0).GetComponent<Text>().text = GetString(___settings.verticalAxis, ___settings);
				__instance.downButton.transform.GetChild(0).GetComponent<Text>().text = GetString(___settings.verticalAxis * -1, ___settings);
			}
		}

		public static string GetString(int axis, Settings settings)
		{
			var negative = axis < 0;
			var strings = (List<string>)null;

			if (settings.language == 0)
			{
				strings = AxisLocalization.strings;
			}
			else if (settings.language == 1)
			{
				strings = AxisLocalization.strings_French;
			}
			else if (settings.language == 2)
			{
				strings = AxisLocalization.strings_Italian;
			}
			else if (settings.language == 3)
			{
				strings = AxisLocalization.strings_Japanese;
			}
			else
			{
				strings = AxisLocalization.strings_Custom;
			}

			if (strings is null)
			{
				Console.WriteLine("KeyBindScript_DpadNames.GetString: localization strings not ready yet!");
			}

			if (axis < 0)
			{
#pragma warning disable Harmony003 // Harmony non-ref patch parameters modified
				axis = -axis;
#pragma warning restore Harmony003 // Harmony non-ref patch parameters modified
			}

			if (strings != null
				&& (axis - 1) < strings.Count / 2)
			{
				return strings[((axis - 1) * 2) + (negative ? 0 : 1)];
			}
			else
			{
				return "Axis " + axis + " " + (negative ? "-" : "+");
			}
		}
	}
}
