using HarmonyLib;
using System;
using UnityEngine;

namespace BepInEx5Plugins.Ash.Alisa.Cutscenes.HarmonyPatches
{
	[HarmonyPatch(typeof(Cutscene03_3), "OnTriggerEnter", new Type[] { typeof(Collider) })]
	public class Cutscene03_3_OnTriggerEnter
	{
		// TODO
	}
}
