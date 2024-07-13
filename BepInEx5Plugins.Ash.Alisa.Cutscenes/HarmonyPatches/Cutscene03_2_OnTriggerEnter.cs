using HarmonyLib;
using System;
using UnityEngine;

namespace BepInEx5Plugins.Ash.Alisa.Cutscenes.HarmonyPatches
{
	[HarmonyPatch(typeof(Cutscene03_2), "OnTriggerEnter", new Type[] { typeof(Collider) })]
	public class Cutscene03_2_OnTriggerEnter
	{
		// TODO
	}
}
