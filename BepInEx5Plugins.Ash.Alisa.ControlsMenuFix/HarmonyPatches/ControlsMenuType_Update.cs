using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BepInEx5Plugins.Ash.Alisa.ControlsMenuFix.HarmonyPatches
{
	[HarmonyPatch(typeof(ControlsMenuType), "Update")]
	public class ControlsMenuType_Update
	{
		private static FieldInfo MainMenu_audioSource;

		private static FieldInfo PauseMenu_audioSource;

		public static bool Prepare(MethodBase original)
		{
			if (original is null)
			{
				try
				{
					MainMenu_audioSource = typeof(MainMenu).GetField("audioSource", BindingFlags.NonPublic | BindingFlags.Instance);
					PauseMenu_audioSource = typeof(PauseMenu).GetField("audioSource", BindingFlags.NonPublic | BindingFlags.Instance);
				}
				catch (Exception exception)
				{
					Console.WriteLine(exception);
					return false;
				}
			}

			return true;
		}

		// Check if audioSource is null before referencing it,
		// automatically assign it to an appopriate value if necessary.
		public static bool Prefix(ControlsMenuType __instance
				, ref Settings ___settings
				, ref bool ___buttonPressed
			)
		{
			if (!__instance.audioSource)
			{
				Console.WriteLine("<" + __instance.name + ">" + "ControlsMenuType_Update.Prefix: audioSource is null! Auto fixing...");

				if (__instance.mainMenu)
				{
					__instance.audioSource = (AudioSource)MainMenu_audioSource.GetValue(__instance.mainMenu);
				}
				else if (__instance.pauseMenu)
				{
					__instance.audioSource = (AudioSource)PauseMenu_audioSource.GetValue(__instance.pauseMenu);
				}
				else
				{
					Console.WriteLine("<" + __instance.name + ">" + "ControlsMenuType_Update.Prefix: Both main and pause menu references are null!");
				}
			}

			if (!(EventSystem.current.currentSelectedGameObject == __instance.gameObject))
			{
				return false;
			}

			if (!___buttonPressed)
			{
				if (Input.GetAxis("Horizontal") != 0f
					|| Input.GetAxis(___settings.customHorizontalAxis) != 0f)
				{
					if (!__instance.xboxControllerType)
					{
						if (__instance.mainMenu)
						{
							__instance.mainMenu.ButtonTypeSwitch();
						}
						else if (__instance.pauseMenu)
						{
							__instance.pauseMenu.ButtonTypeSwitch();
						}

						if (__instance.audioSource)
						{
							__instance.audioSource.PlayOneShot(__instance.moveSelected, 1f);
						}
					}
					else
					{
						if (__instance.mainMenu)
						{
							__instance.mainMenu.XboxControlTypeSwitch();
						}
						else if (__instance.pauseMenu)
						{
							__instance.pauseMenu.XboxControlTypeSwitch();
						}

						if (__instance.audioSource)
						{
							__instance.audioSource.PlayOneShot(__instance.moveSelected, 1f);
						}
					}

					___buttonPressed = true;
				}

				if (___settings.keyboardAlt > 0)
				{
					if (Input.GetAxis("Horizontal_WASD") == 0f)
					{
						return false;
					}

					if (!__instance.xboxControllerType)
					{
						if (__instance.mainMenu)
						{
							__instance.mainMenu.ButtonTypeSwitch();
						}
						else if (__instance.pauseMenu)
						{
							__instance.pauseMenu.ButtonTypeSwitch();
						}

						if (__instance.audioSource)
						{
							__instance.audioSource.PlayOneShot(__instance.moveSelected, 1f);
						}
					}
					else
					{
						if (__instance.mainMenu)
						{
							__instance.mainMenu.XboxControlTypeSwitch();
						}
						else if (__instance.pauseMenu)
						{
							__instance.pauseMenu.XboxControlTypeSwitch();
						}

						if (__instance.audioSource)
						{
							__instance.audioSource.PlayOneShot(__instance.moveSelected, 1f);
						}
					}

					___buttonPressed = true;
				}
				else
				{
					if (___settings.keyboardAlt >= 0
						|| Input.GetAxis("Horizontal_ZQSD") == 0f)
					{
						return false;
					}

					if (!__instance.xboxControllerType)
					{
						if (__instance.mainMenu)
						{
							__instance.mainMenu.ButtonTypeSwitch();
						}
						else if (__instance.pauseMenu)
						{
							__instance.pauseMenu.ButtonTypeSwitch();
						}

						if (__instance.audioSource)
						{
							__instance.audioSource.PlayOneShot(__instance.moveSelected, 1f);
						}
					}
					else
					{
						if (__instance.mainMenu)
						{
							__instance.mainMenu.XboxControlTypeSwitch();
						}
						else if (__instance.pauseMenu)
						{
							__instance.pauseMenu.XboxControlTypeSwitch();
						}

						if (__instance.audioSource)
						{
							__instance.audioSource.PlayOneShot(__instance.moveSelected, 1f);
						}
					}

					___buttonPressed = true;
				}
			}
			else if (Input.GetAxis("Horizontal") == 0f
				&& Input.GetAxis(___settings.customHorizontalAxis) == 0f
				&& Input.GetAxis("Horizontal_WASD") == 0f
				&& Input.GetAxis("Horizontal_ZQSD") == 0f)
			{
				___buttonPressed = false;
			}

			return false;
		}
	}
}
