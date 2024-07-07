using HarmonyLib;
using UnityEngine;

namespace BepInEx5Plugins.Ash.Alisa.FatDollCollision.HarmonyPatches
{
	[HarmonyPatch(typeof(BigDollBehaviour), "FixedUpdate")]
	public class BigDollBehaviour_FixedUpdate
	{
		// Store dead state for postfix.
		private static void Prefix(BigDollBehaviour __instance, out bool __state)
		{
			__state = __instance.enemyHealth <= 0f && !__instance.enemyIsDead;
		}

		// Disable physical collider of fat dolls when they die.
		private static void Postfix(BigDollBehaviour __instance, bool __state)
		{
			if (__state != (__instance.enemyHealth <= 0f && __instance.enemyIsDead))
			{
				if (__instance.transform.Find("FatDoll"))
				{
					var collider = __instance.transform.Find("Armature/Root/TorsoBone")?.GetComponent<BoxCollider>();

					if (collider)
					{
						collider.enabled = false;
					}
				}

				return;
			}
		}
	}
}
