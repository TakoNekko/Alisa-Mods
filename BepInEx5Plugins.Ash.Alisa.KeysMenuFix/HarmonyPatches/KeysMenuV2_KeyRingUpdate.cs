using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;

namespace BepInEx5Plugins.Ash.Alisa.KeysMenuFix.HarmonyPatches
{
	[HarmonyPatch(typeof(KeysMenuV2), "KeyRingUpdate")]
	public class KeysMenuV2_KeyRingUpdate
	{
		private static MethodInfo KeysMenuV2_UseKey01;
		private static MethodInfo KeysMenuV2_UseKey02;
		private static MethodInfo KeysMenuV2_UseKey03;
		private static MethodInfo KeysMenuV2_UseKey04;
		private static MethodInfo KeysMenuV2_UseKey05;
		private static MethodInfo KeysMenuV2_UseKey06;
		private static MethodInfo KeysMenuV2_UseKey07;
		private static MethodInfo KeysMenuV2_UseKey08;

		public static bool Prepare(MethodBase original)
		{
			if (original is null)
			{
				try
				{
					KeysMenuV2_UseKey01 = typeof(KeysMenuV2).GetMethod("UseKey01", BindingFlags.NonPublic | BindingFlags.Instance);
					KeysMenuV2_UseKey02 = typeof(KeysMenuV2).GetMethod("UseKey02", BindingFlags.NonPublic | BindingFlags.Instance);
					KeysMenuV2_UseKey03 = typeof(KeysMenuV2).GetMethod("UseKey03", BindingFlags.NonPublic | BindingFlags.Instance);
					KeysMenuV2_UseKey04 = typeof(KeysMenuV2).GetMethod("UseKey04", BindingFlags.NonPublic | BindingFlags.Instance);
					KeysMenuV2_UseKey05 = typeof(KeysMenuV2).GetMethod("UseKey05", BindingFlags.NonPublic | BindingFlags.Instance);
					KeysMenuV2_UseKey06 = typeof(KeysMenuV2).GetMethod("UseKey06", BindingFlags.NonPublic | BindingFlags.Instance);
					KeysMenuV2_UseKey07 = typeof(KeysMenuV2).GetMethod("UseKey07", BindingFlags.NonPublic | BindingFlags.Instance);
					KeysMenuV2_UseKey08 = typeof(KeysMenuV2).GetMethod("UseKey08", BindingFlags.NonPublic | BindingFlags.Instance);
				}
				catch (Exception exception)
				{
					Console.WriteLine(exception);
					return false;
				}
			}

			return true;
		}

		// Activate/Deactivate key title objects only as necessary.
		public static bool Prefix(KeysMenuV2 __instance
			, ref int ___zoom
			, ref Settings ___settings
			)
		{
			if (ProgressManager.instance.data.puzzle01Done && !__instance.key02.activeSelf)
			{
				ProgressManager.instance.data.keyAmount = 2;
				__instance.key02.SetActive(true);
			}

			if (ProgressManager.instance.data.torsoPuzzleDone && !__instance.key03.activeSelf)
			{
				ProgressManager.instance.data.keyAmount = 3;
				__instance.key03.SetActive(true);
			}

			if (ProgressManager.instance.data.clockPuzzleIsDone && !__instance.key04.activeSelf)
			{
				ProgressManager.instance.data.keyAmount = 4;
				__instance.key04.SetActive(true);
			}

			if (ProgressManager.instance.data.pianoPuzzleDone && !__instance.key05.activeSelf)
			{
				ProgressManager.instance.data.keyAmount = 5;
				__instance.key05.SetActive(true);
			}

			if (ProgressManager.instance.data.libraryPuzzleState == 2 && !__instance.key06.activeSelf)
			{
				ProgressManager.instance.data.keyAmount = 6;
				__instance.key06.SetActive(true);
			}

			if (ProgressManager.instance.data.gardenPuzzleDone && !__instance.key07.activeSelf)
			{
				ProgressManager.instance.data.keyAmount = 7;
				__instance.key07.SetActive(true);
			}

			if (ProgressManager.instance.data.boss03Defeated && !__instance.key08.activeSelf)
			{
				ProgressManager.instance.data.keyAmount = 8;
				__instance.key08.SetActive(true);
			}

			var currentKeyChanged = false;

			if ((!__instance.keyAnim.GetCurrentAnimatorStateInfo(0).IsTag("Idle") || ___zoom != 2)
				&& __instance.currentKey != 0)
			{
				__instance.currentKey = 0;
				currentKeyChanged = true;
			}

			if (ProgressManager.instance.data.keyAmount == 1 && __instance.keyAnim.GetInteger("KeyCount") != 1)
			{
				__instance.keyAnim.SetInteger("KeyCount", 1);
			}
			else if (ProgressManager.instance.data.keyAmount == 2 && __instance.keyAnim.GetInteger("KeyCount") != 2)
			{
				__instance.keyAnim.SetInteger("KeyCount", 2);
			}
			else if (ProgressManager.instance.data.keyAmount == 3 && __instance.keyAnim.GetInteger("KeyCount") != 3)
			{
				__instance.keyAnim.SetInteger("KeyCount", 3);
			}
			else if (ProgressManager.instance.data.keyAmount == 4 && __instance.keyAnim.GetInteger("KeyCount") != 4)
			{
				__instance.keyAnim.SetInteger("KeyCount", 4);
			}
			else if (ProgressManager.instance.data.keyAmount == 5 && __instance.keyAnim.GetInteger("KeyCount") != 5)
			{
				__instance.keyAnim.SetInteger("KeyCount", 5);
			}
			else if (ProgressManager.instance.data.keyAmount == 6 && __instance.keyAnim.GetInteger("KeyCount") != 6)
			{
				__instance.keyAnim.SetInteger("KeyCount", 6);
			}
			else if (ProgressManager.instance.data.keyAmount == 7 && __instance.keyAnim.GetInteger("KeyCount") != 7)
			{
				__instance.keyAnim.SetInteger("KeyCount", 7);
			}
			else if (ProgressManager.instance.data.keyAmount == 8 && __instance.keyAnim.GetInteger("KeyCount") != 8)
			{
				__instance.keyAnim.SetInteger("KeyCount", 8);
			}

			if (___zoom == 2)
			{
				if (__instance.keyAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle1"))
				{
					if (__instance.currentKey != 1)
					{
						__instance.currentKey = 1;
						currentKeyChanged = true;
					}

					if (Input.GetKeyDown(___settings.keys["Action"]))
					{
						KeysMenuV2_UseKey01.Invoke(__instance, null);
					}
				}
				else if (__instance.keyAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle2"))
				{
					if (__instance.currentKey != 2)
					{
						__instance.currentKey = 2;
						currentKeyChanged = true;
					}

					if (Input.GetKeyDown(___settings.keys["Action"]))
					{
						KeysMenuV2_UseKey02.Invoke(__instance, null);
					}
				}
				else if (__instance.keyAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle3"))
				{
					if (__instance.currentKey != 3)
					{
						__instance.currentKey = 3;
						currentKeyChanged = true;
					}

					if (Input.GetKeyDown(___settings.keys["Action"]))
					{
						KeysMenuV2_UseKey03.Invoke(__instance, null);
					}
				}
				else if (__instance.keyAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle8"))
				{
					if (__instance.currentKey != 4)
					{
						__instance.currentKey = 4;
						currentKeyChanged = true;
					}

					if (Input.GetKeyDown(___settings.keys["Action"]))
					{
						KeysMenuV2_UseKey04.Invoke(__instance, null);
					}
				}
				else if (__instance.keyAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle7"))
				{
					if (__instance.currentKey != 5)
					{
						__instance.currentKey = 5;
						currentKeyChanged = true;
					}

					if (Input.GetKeyDown(___settings.keys["Action"]))
					{
						KeysMenuV2_UseKey05.Invoke(__instance, null);
					}
				}
				else if (__instance.keyAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle4"))
				{
					if (__instance.currentKey != 6)
					{
						__instance.currentKey = 6;
						currentKeyChanged = true;
					}

					if (Input.GetKeyDown(___settings.keys["Action"]))
					{
						KeysMenuV2_UseKey06.Invoke(__instance, null);
					}
				}
				else if (__instance.keyAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle5"))
				{
					if (__instance.currentKey != 7)
					{
						__instance.currentKey = 7;
						currentKeyChanged = true;
					}

					if (Input.GetKeyDown(___settings.keys["Action"]))
					{
						KeysMenuV2_UseKey07.Invoke(__instance, null);
					}
				}
				else if (__instance.keyAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle6"))
				{
					if (__instance.currentKey != 8)
					{
						__instance.currentKey = 8;
						currentKeyChanged = true;
					}

					if (Input.GetKeyDown(___settings.keys["Action"]))
					{
						KeysMenuV2_UseKey08.Invoke(__instance, null);
					}
				}
			}

			if (currentKeyChanged)
			{
				__instance.key01Title.SetActive(__instance.currentKey == 1);
				__instance.key02Title.SetActive(__instance.currentKey == 2);
				__instance.key03Title.SetActive(__instance.currentKey == 3);
				__instance.key04Title.SetActive(__instance.currentKey == 4);
				__instance.key05Title.SetActive(__instance.currentKey == 5);
				__instance.key06Title.SetActive(__instance.currentKey == 6);
				__instance.key07Title.SetActive(__instance.currentKey == 7);
				__instance.key08Title.SetActive(__instance.currentKey == 8);
			}

			return false;
		}
	}
}
