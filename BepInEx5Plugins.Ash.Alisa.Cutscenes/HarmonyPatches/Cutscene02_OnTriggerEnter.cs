using HarmonyLib;
using System;
using UnityEngine;

namespace BepInEx5Plugins.Ash.Alisa.Cutscenes.HarmonyPatches
{
	[HarmonyPatch(typeof(Cutscene02), "OnTriggerEnter", new Type[] { typeof(Collider) })]
	public class Cutscene02_OnTriggerEnter
	{
		// TODO
	}
}
