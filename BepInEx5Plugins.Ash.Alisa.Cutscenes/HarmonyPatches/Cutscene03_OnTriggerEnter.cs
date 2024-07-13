using HarmonyLib;
using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

namespace BepInEx5Plugins.Ash.Alisa.Cutscenes.HarmonyPatches
{
	[HarmonyPatch(typeof(Cutscene03), "OnTriggerEnter", new Type[] { typeof(Collider) })]
	public class Cutscene03_OnTriggerEnter
	{
		private static MethodInfo Cutscene03_StartSubtitles;
		private static MethodInfo Cutscene03_Voices;
		private static MethodInfo Cutscene03_WeaponReady;

		private static FieldInfo Cutscene03_readyToSkip;
		private static FieldInfo Cutscene03_moveThePlayer;

		public static bool Prepare(MethodBase original)
		{
			if (original is null)
			{
				try
				{
					Cutscene03_StartSubtitles = typeof(Cutscene03).GetMethod("StartSubtitles", BindingFlags.NonPublic | BindingFlags.Instance);
					Cutscene03_Voices = typeof(Cutscene03).GetMethod("Voices", BindingFlags.NonPublic | BindingFlags.Instance);
					Cutscene03_WeaponReady = typeof(Cutscene03).GetMethod("WeaponReady", BindingFlags.NonPublic | BindingFlags.Instance);

					Cutscene03_readyToSkip = typeof(Cutscene03).GetField("readyToSkip", BindingFlags.NonPublic | BindingFlags.Instance);
					Cutscene03_moveThePlayer = typeof(Cutscene03).GetField("moveThePlayer", BindingFlags.NonPublic | BindingFlags.Instance);
				}
				catch (Exception exception)
				{
					Console.WriteLine(exception);
					return false;
				}
			}

			return true;
		}

		public static bool Prefix(Cutscene03 __instance, Collider col
			, ref GameObject ___player
			, ref Animator ___playerAnim
			, ref Animator ___puppetAnim
			, ref BoxCollider ___myCollision
			, ref GameObject ___interaction
			, ref EquipmentMenu ___equipment
			, ref WeaponControl ___weaponControl
			, ref AlisaMovement ___movement
			, ref Settings ___settings
			, ref Canvas ___blackBarsCanvas
			, ref bool ___cutsceneIsPlaying
			, ref GameObject ___camera01
			, ref GameObject ___camera02
			, ref GameObject ___camera03
			, ref GameObject ___camera04
			, ref GameObject ___camera05
			, ref GameObject ___camera06
			, ref GameObject ___camera07
			, ref GameObject ___camera08
			, ref GameObject ___camera09
			, ref MusicPlayer ___musicPlayer
			)
		{
			if (!___cutsceneIsPlaying
				&& !___movement.quickTurning
				&& !___weaponControl.isReloading
				&& !___playerAnim.GetCurrentAnimatorStateInfo(0).IsTag("Swap")
				&& !___playerAnim.GetCurrentAnimatorStateInfo(0).IsTag("SwapHold")
				&& !___playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Dodging")
				&& !___playerAnim.GetCurrentAnimatorStateInfo(0).IsName("DodgeLand")
				&& col.gameObject.tag == "Player")
			{
				__instance.StartCoroutine(PlayCutscene(__instance
					, ___player
					, ___playerAnim
					, ___puppetAnim
					, ___interaction
					, ___equipment
					, ___weaponControl
					, ___movement
					, ___blackBarsCanvas
					, ___camera01
					, ___camera02
					, ___camera03
					, ___camera04
					, ___camera05
					, ___camera06
					, ___camera07
					, ___camera08
					, ___camera09
					, ___musicPlayer
					, Cutscene03_WeaponReady
					, Cutscene03_readyToSkip
					, Cutscene03_moveThePlayer
			));
				__instance.StartCoroutine((IEnumerator)Cutscene03_StartSubtitles.Invoke(__instance, null));

				if (___settings.voice == 0)
				{
					__instance.StartCoroutine((IEnumerator)Cutscene03_Voices.Invoke(__instance, null));
				}

				___myCollision.enabled = false;
				___cutsceneIsPlaying = true;
				___movement.cantMove = true;
				___movement.enabled = false;
				___weaponControl.weaponSwitched = false;
				___playerAnim.SetFloat("inputV", 0f);
				___playerAnim.SetFloat("inputH", 0f);
				___playerAnim.SetBool("sprinting", false);
			}

			return false;
		}

		private static IEnumerator PlayCutscene(Cutscene03 cutscene
			, GameObject player
			, Animator playerAnim
			, Animator puppetAnim
			, GameObject interaction
			, EquipmentMenu equipment
			, WeaponControl weaponControl
			, AlisaMovement movement
			, Canvas blackBarsCanvas
			, GameObject camera01
			, GameObject camera02
			, GameObject camera03
			, GameObject camera04
			, GameObject camera05
			, GameObject camera06
			, GameObject camera07
			, GameObject camera08
			, GameObject camera09
			, MusicPlayer musicPlayer
			, MethodInfo Cutscene03_WeaponReady
			, FieldInfo Cutscene03_readyToSkip
			, FieldInfo Cutscene03_moveThePlayer
			)
		{
			equipment.disablePauseMenu = true;

			yield return new WaitForSeconds(0.1f);

			equipment.canOpenMenu = false;
			player.GetComponent<CapsuleCollider>().enabled = false;
			interaction.GetComponent<Interaction>().interactionIsActive = true;
			weaponControl.disabled = true;
			Cutscene03_WeaponReady.Invoke(cutscene, null);

			yield return new WaitForSeconds(0.4f);

			blackBarsCanvas.GetComponentInChildren<Animator>().SetBool("Fade", true);
			camera01.SetActive(false);
			camera02.SetActive(false);
			camera03.SetActive(false);
			camera04.SetActive(false);
			camera05.SetActive(false);
			camera06.SetActive(false);
			camera07.SetActive(false);
			camera08.SetActive(true);

			blackBarsCanvas.worldCamera = camera08.GetComponent<Camera>();

			player.transform.position = new Vector3(6.437f, 0.8f, -9.615f);
			player.transform.rotation = Quaternion.Euler(0f, -153.666f, 0f);
			puppetAnim.SetBool("Cutscene03", true);
			ProgressManager.instance.data.cutscene03 = false;

			yield return new WaitForSeconds(0.2f);

			Cutscene03_readyToSkip.SetValue(cutscene, true);

			yield return new WaitForSeconds(1.5f);

			camera07.SetActive(true);
			camera08.SetActive(false);
			blackBarsCanvas.worldCamera = camera07.GetComponent<Camera>();

			playerAnim.SetBool("Cutscene03", true);

			yield return new WaitForSeconds(1.25f);

			camera07.SetActive(false);
			camera08.SetActive(true);
			blackBarsCanvas.worldCamera = camera08.GetComponent<Camera>();

			puppetAnim.SetTrigger("CutsceneNext");

			yield return new WaitForSeconds(2.5f);

			camera07.SetActive(false);
			camera08.SetActive(false);
			camera09.SetActive(true);
			blackBarsCanvas.worldCamera = camera09.GetComponent<Camera>();

			yield return new WaitForSeconds(0.38f);

			playerAnim.SetTrigger("CutsceneNext");

			yield return new WaitForSeconds(1.55f);

			Cutscene03_moveThePlayer.SetValue(cutscene, true);

			yield return new WaitForSeconds(1.25f);

			musicPlayer.PlayPolThemeSong();

			yield return new WaitForSeconds(0.3f);

			camera09.SetActive(false);
			camera07.SetActive(false);
			camera08.SetActive(true);
			blackBarsCanvas.worldCamera = camera08.GetComponent<Camera>();

			puppetAnim.SetTrigger("CutsceneNext");

			yield return new WaitForSeconds(0.1f);

			Cutscene03_moveThePlayer.SetValue(cutscene, false);

			yield return new WaitForSeconds(7f);

			camera07.SetActive(true);
			camera08.SetActive(false);
			blackBarsCanvas.worldCamera = camera07.GetComponent<Camera>();

			playerAnim.SetTrigger("CutsceneNext");

			yield return new WaitForSeconds(7.3f);

			camera07.SetActive(false);
			camera08.SetActive(true);
			blackBarsCanvas.worldCamera = camera08.GetComponent<Camera>();

			puppetAnim.SetTrigger("CutsceneNext");

			yield return new WaitForSeconds(16.85f);

			camera07.SetActive(true);
			camera08.SetActive(false);
			blackBarsCanvas.worldCamera = camera07.GetComponent<Camera>();

			playerAnim.SetTrigger("CutsceneNext");

			yield return new WaitForSeconds(2.715f);

			camera07.SetActive(false);
			camera08.SetActive(true);
			blackBarsCanvas.worldCamera = camera08.GetComponent<Camera>();

			puppetAnim.SetTrigger("CutsceneNext");
			playerAnim.SetBool("Cutscene03", false);
			puppetAnim.SetBool("Cutscene03", false);
			Cutscene03_readyToSkip.SetValue(cutscene, false);

			yield return new WaitForSeconds(3.35f);

			playerAnim.SetTrigger("CutsceneNext");
			camera03.SetActive(true);
			camera07.SetActive(false);
			camera08.SetActive(false);
			blackBarsCanvas.worldCamera = camera03.GetComponent<Camera>();

			puppetAnim.SetTrigger("CutsceneNext");
			blackBarsCanvas.GetComponentInChildren<Animator>().SetBool("Fade", false);

			yield return new WaitForSeconds(0.2f);

			movement.enabled = true;
			movement.cantMove = false;
			player.GetComponent<CapsuleCollider>().enabled = true;
			weaponControl.disabled = false;
		}
	}
}
