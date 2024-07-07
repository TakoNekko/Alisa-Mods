using HarmonyLib;
using System;
using UnityEngine;

namespace BepInEx5Plugins.Ash.Alisa.PauseMenuFix.HarmonyPatches
{
	[HarmonyPatch(typeof(PauzeGame), "HardPauzeOn")]
	public class PauzeGame_HardPauzeOn
	{
		// Move playerWeapon and playerMovement assignments to prevent causing null reference exceptions.
		public static bool Prefix(PauzeGame __instance
			, ref AlisaAttributes ___attributes
			)
		{
			__instance.pauzed = true;

			var enemies = GameObject.FindGameObjectsWithTag("Enemy");

			__instance.enemyControllers = new EnemyController[enemies.Length];

			for (var i = 0; i < enemies.Length; ++i)
			{
				__instance.enemyControllers[i] = enemies[i].GetComponent<EnemyController>();
				__instance.enemyControllers[i].pauzed = true;
			}

			__instance.player = GameObject.FindWithTag("Player");
			__instance.interact = GameObject.FindWithTag("RayCastPoint").GetComponent<Interaction>();

			if (!__instance.menu)
			{
				__instance.menu = GameObject.FindWithTag("PlayerUI").GetComponent<EquipmentMenu>();
			}

			if (__instance.menu)
			{
				__instance.menu.canOpenMenu = false;
			}

			if (__instance.player)
			{
				__instance.playerWeapon = __instance.player.GetComponent<WeaponControl>();
				__instance.playerMovement = __instance.player.GetComponent<AlisaMovement>();

				if (__instance.playerWeapon)
				{
					__instance.playerWeapon.disabled = true;
				}

				if (__instance.playerMovement)
				{
					__instance.playerMovement.disabled = true;
				}

				if (__instance.interact)
				{
					__instance.interact.disabled = true;
				}

				___attributes = __instance.player.GetComponent<AlisaAttributes>();
				___attributes.animPaused = true;
			}
			else
			{
				Console.WriteLine("PauzeGame_HardPauzeOn.Prefix: player is null!");

				__instance.playerWeapon = null;
				__instance.playerMovement = null;
				___attributes = null;
			}

			if (__instance.player && !__instance.quickUi)
			{
				__instance.quickUi = GameObject.Find("/PlayerUIHolder/PlayerUI/QuickUI/UI_Base").GetComponent<QuickUIController>();
			}

			if (__instance.quickUi)
			{
				__instance.quickUi.returningPos = true;
			}

			return false;
		}
	}
}