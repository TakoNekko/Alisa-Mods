using HarmonyLib;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace BepInEx5Plugins.Ash.Alisa.Shortcuts.HarmonyPatches
{
	[HarmonyPatch(typeof(Settings), "Update")]
	public class Settings_Update
	{
		public static bool fadeOut = true;

		private static bool locked = false;

		// Quit game upon pressing a combination of keys.
		private static bool Prefix(Settings __instance)
		{
			if (!locked
				&& Input.GetKey(__instance.keys["Action"])
				&& Input.GetKey(__instance.keys["Sprint"])
				&& Input.GetKey(__instance.keys["Cancel"])
				&& Input.GetKey(__instance.keys["SwitchWeapon"])
				&& Input.GetKey(__instance.keys["Submit"])
				&& Input.GetKey(__instance.keys["Select"]))
			{
				locked = true;

				__instance.StartCoroutine(QuitGame());

				return false;
			}

			return true;
		}

		private static IEnumerator QuitGame()
		{
			GameObject.FindWithTag("MusicPlayer")?.GetComponent<MusicPlayer>()?.StopMusic();

			if (fadeOut)
			{
				GameObject.FindWithTag("FadeEffect")?.GetComponent<Animator>()?.SetBool("Fade", true);

				yield return new WaitForSeconds(1.5f);
			}

			Camera.main.gameObject.SetActive(false);

			Object.Destroy(GameObject.FindWithTag("MusicPlayer"));
			Object.Destroy(ProgressManager.instance.gameObject);
			Object.Destroy(GameObject.FindWithTag("Player"));
			Object.Destroy(GameObject.FindWithTag("PlayerUIHolder"));
			Object.Destroy(GameObject.FindWithTag("RenderTextureSet"));
			Object.Destroy(GameObject.Find("MakeUpSet"));

			locked = false;

			SceneManager.LoadScene("InfoSplashScreen");
		}
	}
}
