using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;

namespace BepInEx5Plugins.Ash.Alisa.HudAnimationFix.HarmonyPatches
{
	[HarmonyPatch(typeof(QuickUIController), "Update")]
	public class QuickUIController_Update
	{
		private static MethodInfo QuickUIController_MeleeTarget;
		private static MethodInfo QuickUIController_FireBallTarget;
		private static MethodInfo QuickUIController_DualPistolTarget;
		private static MethodInfo QuickUIController_LMGTarget;
		private static MethodInfo QuickUIController_Target;
		private static MethodInfo QuickUIController_CurrentWeapon;
		private static MethodInfo QuickUIController_HealthBar;
		private static MethodInfo QuickUIController_NotHiddenCheck;

		public static bool Prepare(MethodBase original)
		{
			if (original is null)
			{
				try
				{
					QuickUIController_MeleeTarget = typeof(QuickUIController).GetMethod("MeleeTarget", BindingFlags.NonPublic | BindingFlags.Instance);
					QuickUIController_FireBallTarget = typeof(QuickUIController).GetMethod("FireBallTarget", BindingFlags.NonPublic | BindingFlags.Instance);
					QuickUIController_DualPistolTarget = typeof(QuickUIController).GetMethod("DualPistolTarget", BindingFlags.NonPublic | BindingFlags.Instance);
					QuickUIController_LMGTarget = typeof(QuickUIController).GetMethod("LMGTarget", BindingFlags.NonPublic | BindingFlags.Instance);
					QuickUIController_Target = typeof(QuickUIController).GetMethod("Target", BindingFlags.NonPublic | BindingFlags.Instance);
					QuickUIController_CurrentWeapon = typeof(QuickUIController).GetMethod("CurrentWeapon", BindingFlags.NonPublic | BindingFlags.Instance);
					QuickUIController_HealthBar = typeof(QuickUIController).GetMethod("HealthBar", BindingFlags.NonPublic | BindingFlags.Instance);
					QuickUIController_NotHiddenCheck = typeof(QuickUIController).GetMethod("NotHiddenCheck", BindingFlags.NonPublic | BindingFlags.Instance);
				}
				catch (Exception exception)
				{
					Console.WriteLine(exception);
					return false;
				}
			}

			return true;
		}

		// Immediately return the quickbar if the animation was stopped before it could finish.
		public static void Postfix(QuickUIController __instance
			, ref WeaponControl ___weaponControl
			, ref RectTransform ___myTransform
			, ref Vector3 ___hidePos
			, ref Vector3 ___showPos
			, ref Interaction ___interact
			, ref PauzeGame ___pauze
			)
		{
			if ((ProgressManager.instance.data.slot01Weapon == 3 && !___weaponControl.weaponSwitched) || (ProgressManager.instance.data.slot02Weapon == 3 && ___weaponControl.weaponSwitched) || (ProgressManager.instance.data.slot01Weapon == 0 && !___weaponControl.weaponSwitched) || (ProgressManager.instance.data.slot02Weapon == 0 && ___weaponControl.weaponSwitched) || (ProgressManager.instance.data.slot01Weapon == -1 && !___weaponControl.weaponSwitched) || (ProgressManager.instance.data.slot02Weapon == -1 && ___weaponControl.weaponSwitched) || (ProgressManager.instance.data.slot01Weapon == 12 && !___weaponControl.weaponSwitched) || (ProgressManager.instance.data.slot02Weapon == 12 && ___weaponControl.weaponSwitched) || (ProgressManager.instance.data.slot01Weapon == 13 && !___weaponControl.weaponSwitched) || (ProgressManager.instance.data.slot02Weapon == 13 && ___weaponControl.weaponSwitched))
			{
				QuickUIController_MeleeTarget.Invoke(__instance, null);
			}
			else if ((ProgressManager.instance.data.slot01Weapon == -3 && !___weaponControl.weaponSwitched) || (ProgressManager.instance.data.slot02Weapon == -3 && ___weaponControl.weaponSwitched))
			{
				QuickUIController_FireBallTarget.Invoke(__instance, null);
			}
			else if ((ProgressManager.instance.data.slot01Weapon == 10 && !___weaponControl.weaponSwitched) || (ProgressManager.instance.data.slot02Weapon == 10 && ___weaponControl.weaponSwitched) || (ProgressManager.instance.data.slot01Weapon == 14 && !___weaponControl.weaponSwitched) || (ProgressManager.instance.data.slot02Weapon == 14 && ___weaponControl.weaponSwitched))
			{
				QuickUIController_DualPistolTarget.Invoke(__instance, null);
			}
			else if ((ProgressManager.instance.data.slot01Weapon == 11 && !___weaponControl.weaponSwitched) || (ProgressManager.instance.data.slot02Weapon == 11 && ___weaponControl.weaponSwitched))
			{
				QuickUIController_LMGTarget.Invoke(__instance, null);
			}
			else
			{
				QuickUIController_Target.Invoke(__instance, null);
			}

			QuickUIController_CurrentWeapon.Invoke(__instance, null);
			QuickUIController_HealthBar.Invoke(__instance, null);
			QuickUIController_NotHiddenCheck.Invoke(__instance, null);

			if (___pauze.pauzed || ___pauze.softPauzed)
			{
				__instance.returningPos = true;
			}

			if (___weaponControl.isAiming)
			{
				__instance.StopAllCoroutines();

				// return only when it's in full position.
				__instance.returningPos = false;

				if (___myTransform.localPosition != ___showPos)
				{
					___myTransform.localPosition = Vector3.MoveTowards(___myTransform.localPosition, ___showPos, 2000f * Time.deltaTime);
				}
				else
				{
					___myTransform.localPosition = ___showPos;
				}
			}
			else
			{
				if (___myTransform.localPosition != ___showPos)
				{
					__instance.returningPos = true;
				}
			}

			if (___weaponControl.isAiming)
			{
				__instance.isActive = true;
			}
			else
			{
				__instance.isActive = false;
			}

			if (__instance.returningPos)
			{
				if (___myTransform.localPosition != ___hidePos)
				{
					___myTransform.localPosition = Vector3.MoveTowards(___myTransform.localPosition, ___hidePos, 2000f * Time.deltaTime);
				}
				else
				{
					___myTransform.localPosition = ___hidePos;
					__instance.returningPos = false;
				}
			}

			if (___interact.interactionIsActive)
			{
				__instance.returningPos = true;
			}
		}
	}
}
