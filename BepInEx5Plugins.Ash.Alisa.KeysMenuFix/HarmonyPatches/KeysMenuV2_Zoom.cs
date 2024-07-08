using HarmonyLib;
using UnityEngine;

namespace BepInEx5Plugins.Ash.Alisa.KeysMenuFix.HarmonyPatches
{
	[HarmonyPatch(typeof(KeysMenuV2), "Zoom")]
	public class KeysMenuV2_Zoom
	{
		// Check if currentSelectedItem is null before referencing it.
		public static bool Prefix(KeysMenuV2 __instance
			, ref Settings ___settings
			, ref bool ___moving
			, ref int ___zoom
			, ref Vector3 ___zoomedIn
			, ref float ___moveSpeed
			, ref AudioSource ___audioSource
			)
		{
			if (__instance.currentSelectedItem
				&& __instance.currentSelectedItem.name == "KeyRing"
				&& Input.GetKeyDown(___settings.keys["Action"])
				&& ___zoom == 0
				&& !___moving)
			{
				___zoom = 1;
				___moving = true;
				__instance.currentSelectedItem.transform.rotation = Quaternion.identity;
				___audioSource.PlayOneShot(__instance.keyRingZoomInSound, 1f);
			}
			else if (__instance.currentSelectedItem
				&& __instance.currentSelectedItem.name == "KeyRing"
				&& Input.GetKeyDown(___settings.keys["Cancel"])
				&& ___zoom == 2)
			{
				___zoom = 3;
				___audioSource.PlayOneShot(__instance.keyRingZoomOutSound, 1f);
			}

			if (___zoom == 1)
			{
				__instance.rayPoint.localPosition = Vector3.MoveTowards(__instance.rayPoint.localPosition, ___zoomedIn, ___moveSpeed * Time.deltaTime);

				if (__instance.rayPoint.localPosition == ___zoomedIn)
				{
					___zoom = 2;
				}
			}
			else if (___zoom == 3)
			{
				__instance.rayPoint.localPosition = Vector3.MoveTowards(__instance.rayPoint.localPosition, Vector3.zero, ___moveSpeed * Time.deltaTime);

				if (__instance.rayPoint.localPosition == Vector3.zero)
				{
					___zoom = 0;
					___moving = false;
				}
			}

			if (__instance.currentSelectedItem
				&& __instance.currentSelectedItem.name == "KeyRing"
				&& ___zoom == 0
				&& !__instance.keyRingTitle.activeSelf)
			{
				__instance.keyRingTitle.SetActive(true);
			}
			else if ((__instance.currentSelectedItem
				&& __instance.currentSelectedItem.name != "KeyRing" || ___zoom != 0)
				&& __instance.keyRingTitle.activeSelf)
			{
				__instance.keyRingTitle.SetActive(false);
			}

			return false;
		}
	}
}
