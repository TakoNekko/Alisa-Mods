using HarmonyLib;
using System;
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace BepInEx5Plugins.Ash.Alisa.TextScrolling.HarmonyPatches
{
	[HarmonyPatch(typeof(TextBoxManager), "Update")]
	public class TextBoxManager_Update
	{
		public static float typeSpeed = 1f;


		private static FieldInfo TextBoxManager_cancelTyping;

		private static FieldInfo TextBoxManager_cantSkip;

		private static FieldInfo TextBoxManager_skipTimer;

		private static FieldInfo TextBoxManager_typeSpeed;

		private static MethodInfo TextBoxManager_TextLanguage;

		public static bool Prepare(MethodBase original)
		{
			if (original is null)
			{
				try
				{
					TextBoxManager_cancelTyping = typeof(TextBoxManager).GetField("cancelTyping", BindingFlags.NonPublic | BindingFlags.Instance);
					TextBoxManager_cantSkip = typeof(TextBoxManager).GetField("cantSkip", BindingFlags.NonPublic | BindingFlags.Instance);
					TextBoxManager_skipTimer = typeof(TextBoxManager).GetField("skipTimer", BindingFlags.NonPublic | BindingFlags.Instance);
					TextBoxManager_typeSpeed = typeof(TextBoxManager).GetField("typeSpeed", BindingFlags.NonPublic | BindingFlags.Instance);
					TextBoxManager_TextLanguage = typeof(TextBoxManager).GetMethod("TextLanguage", BindingFlags.NonPublic | BindingFlags.Instance);
				}
				catch (Exception exception)
				{
					Console.WriteLine(exception);
					return false;
				}
			}

			return true;
		}

		// Replace call to TextScroll.
		private static bool Prefix(TextBoxManager __instance
				, ref Interaction ___interaction
				, ref Settings ___settings
			)
		{
			TextBoxManager_TextLanguage.Invoke(__instance, null);

			if (!__instance.isActive)
			{
				return false;
			}

			if (!__instance.isScrollingText)
			{
				__instance.theText.text = __instance.textLines[__instance.currentLine];

				if (!__instance.isSubtitles
					&& (Input.GetKeyDown(___settings.keys["Action"])
						|| Input.GetKeyDown(___settings.keys["Cancel"]))
					&& !(bool)TextBoxManager_cantSkip.GetValue(__instance))
				{
					TextBoxManager_cantSkip.SetValue(__instance, true);
					++__instance.currentLine;
				}

				if (__instance.currentLine > __instance.endAtLine)
				{
					__instance.DisableTextBox();
				}
			}

			if (__instance.isScrollingText
				&& (Input.GetKeyDown(___settings.keys["Action"])
					|| Input.GetKeyDown(___settings.keys["Cancel"]))
				)
			{
				if (!__instance.isTyping)
				{
					TextBoxManager_cantSkip.SetValue(__instance, true);
					++__instance.currentLine;

					if (__instance.currentLine > __instance.endAtLine)
					{
						__instance.DisableTextBox();
					}
					else
					{
						__instance.StartCoroutine(TextScroll(__instance, __instance.textLines[__instance.currentLine]));
					}
				}
				else if (__instance.isTyping && !(bool)TextBoxManager_cancelTyping.GetValue(__instance))
				{
					TextBoxManager_cancelTyping.SetValue(__instance, true);
				}
			}

			if (___interaction.watchingDoor)
			{
				__instance.watchDoor = true;
			}
			else
			{
				__instance.watchDoor = false;
			}

			if ((bool)TextBoxManager_cantSkip.GetValue(__instance))
			{
				TextBoxManager_skipTimer.SetValue(__instance, (float)TextBoxManager_skipTimer.GetValue(__instance) + Time.deltaTime);

				if ((float)TextBoxManager_skipTimer.GetValue(__instance) > 0.4f)
				{
					TextBoxManager_cantSkip.SetValue(__instance, false);
					TextBoxManager_skipTimer.SetValue(__instance, 0f);
				}
			}

			return false;
		}

		// Add support for word breaks.
		public static IEnumerator TextScroll(TextBoxManager textBox, string lineOfText)
		{
			foreach (var outline in textBox.theText.GetComponents<Outline>())
			{
				outline.useGraphicAlpha = true;
			}

			textBox.isTyping = true;
			TextBoxManager_cancelTyping.SetValue(textBox, false);

#pragma warning disable Harmony003 // Harmony non-ref patch parameters modified
			lineOfText = lineOfText.Replace("@", "\n");
#pragma warning restore Harmony003 // Harmony non-ref patch parameters modified

			var typeSpeed = (float)TextBoxManager_typeSpeed.GetValue(textBox) * TextBoxManager_Update.typeSpeed;
			var letter = 1;

			while (textBox.isTyping
				&& !(bool)TextBoxManager_cancelTyping.GetValue(textBox)
				&& letter < lineOfText.Length - 1)
			{
				textBox.theText.text = lineOfText.Substring(0, letter) + "<color=#00000000>" + lineOfText.Substring(letter) + "</color>";
				++letter;

				yield return new WaitForSeconds(typeSpeed);
			}

			textBox.theText.text = lineOfText;
			textBox.isTyping = false;
			TextBoxManager_cancelTyping.SetValue(textBox, false);
		}
	}
}