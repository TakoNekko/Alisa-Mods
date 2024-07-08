using HarmonyLib;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BepInEx5Plugins.Ash.Alisa.WakeUpCutsceneSkip.HarmonyPatches
{
	[HarmonyPatch(typeof(WakeUpCutscene), "SkipCutscene")]
	public class WakeUpCutscene_SkipCutscene
	{
		// Replace call to SkippingCutscene.
		public static void Postfix(WakeUpCutscene __instance
			, ref GameObject ___player
			, ref GameObject ___rayPoint
			, ref GameObject ___blopshadow
			, ref EquipmentMenu ___playerUI
			, ref Animator ___fadeControl
			, ref AudioSource ___audioSource
			, ref ActivateTextAtLine ___textComponent
			)
		{
			__instance.StopAllCoroutines();
			__instance.StopAllCoroutines();
			__instance.StopAllCoroutines();
			__instance.StartCoroutine(SkippingCutscene(__instance, ___player, ___rayPoint, ___blopshadow, ___playerUI, ___fadeControl, ___audioSource, ___textComponent));
		}

		// Reset the skip trigger then wait before fading out.
		private static IEnumerator SkippingCutscene(WakeUpCutscene cutscene
			, GameObject player
			, GameObject rayPoint
			, GameObject blopshadow
			, EquipmentMenu playerUI
			, Animator fadeControl
			, AudioSource audioSource
			, ActivateTextAtLine textComponent
			)
		{
			fadeControl.SetBool("Fade", true);
			yield return new WaitForSeconds(0.4f);
			audioSource.Stop();
			audioSource.spatialBlend = 0.9f;
			audioSource.volume = 1f;
			cutscene.blackBarsCanvas.GetComponentInChildren<Animator>().SetBool("Fade", false);
			GameObject.FindWithTag("Subtitles").GetComponent<Text>().text = string.Empty;
			textComponent.theTextBox.theText = GameObject.FindWithTag("Text").GetComponent<Text>();
			textComponent.theTextBox.DisableTextBox();
			ProgressManager.instance.data.cutscene_WakeUp = false;
			player.GetComponentInChildren<Animator>().SetTrigger("Skip");
			player.GetComponentInChildren<Animator>().SetBool("WakeUpCutscene", false);
			blopshadow.SetActive(true);
			player.GetComponent<Collider>().enabled = true;
			yield return new WaitForSeconds(1f);

			player.GetComponentInChildren<Animator>().ResetTrigger("Skip");
			yield return new WaitForSeconds(0.6f);

			fadeControl.SetBool("Fade", false);

			player.GetComponent<AlisaMovement>().enabled = true;
			player.GetComponent<WeaponControl>().enabled = true;
			rayPoint.GetComponent<Interaction>().interactionIsActive = false;
			GameObject.FindWithTag("PlayerUI").GetComponent<EquipmentMenu>().enabled = true;
			playerUI.canOpenMenu = true;
			yield return new WaitForSeconds(0.5f);
			blopshadow.SetActive(true);
			cutscene.gameObject.SetActive(false);
		}
	}
}
