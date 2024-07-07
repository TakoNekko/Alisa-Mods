using HarmonyLib;
using System;
using UnityEngine;

namespace BepInEx5Plugins.Ash.Alisa.Stairs.HarmonyPatches
{
	[HarmonyPatch(typeof(Interaction), "FixedUpdate")]
	public class Interaction_FixedUpdate
	{
		public static float stairsWalkSpeed = 2f;

		public static bool ignoreAnimation = false;

		public static bool newGamePlusOnly = false;

		// Add speed modifier when moving on stairs.
		private static bool Prefix(Interaction __instance, ref float? __state
			, ref GameObject ___player
			, ref bool ___playerMovingDown
			, ref bool ___playerMovingUp
			, ref Animator ___anim
			)
		{
			__state = null;

			if (newGamePlusOnly
				&& ProgressManager.instance.data.newGamePlus < 1)
			{
				return true;
			}

			try
			{
				___anim.SetBool("WalkingDownStairs", ___playerMovingDown);
				___anim.SetBool("WalkingUpStairs", ___playerMovingUp);

				if (!ignoreAnimation)
				{
					__state = ___anim.speed;
				}

				if (___playerMovingDown)
				{
					if (!ignoreAnimation)
					{
						__state = stairsWalkSpeed;
					}
					___player.transform.Translate(stairsWalkSpeed * Vector3.forward * 0.7f * Time.fixedDeltaTime);
					___player.transform.Translate(stairsWalkSpeed * Vector3.down * 0.475f * Time.fixedDeltaTime);
					___player.GetComponent<Rigidbody>().velocity = Vector3.zero;
				}

				if (___playerMovingUp)
				{
					if (!ignoreAnimation)
					{
						__state = stairsWalkSpeed;
					}
					___player.transform.Translate(stairsWalkSpeed * Vector3.forward * 0.7f * Time.fixedDeltaTime);
					___player.transform.Translate(stairsWalkSpeed * Vector3.up * 0.48f * Time.fixedDeltaTime);
					___player.GetComponent<Rigidbody>().velocity = Vector3.zero;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			return false;
		}

		// Modify the animation speed if necessary.
		private static void Postfix(Interaction __instance, ref float? __state
			, ref Animator ___anim
			)
		{
			try
			{
				__state = null;

				if (__state.HasValue)
				{
					___anim.speed = __state.Value;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}
	}
}
