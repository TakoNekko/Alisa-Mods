using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;

namespace BepInEx5Plugins.Ash.Alisa.Stairs
{
	[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
	public class Plugin : BaseUnityPlugin
	{
		private static ConfigEntry<bool> ignoreAnimation;

		private static ConfigEntry<bool> newGamePlusOnly;

		private static ConfigEntry<float> stairsWalkSpeed;

		private Plugin()
		{
			try
			{
				stairsWalkSpeed = Config.Bind("Stairs", "Walk Speed", 2f);
				ignoreAnimation = Config.Bind("Stairs", "Ignore Animation", false);
				newGamePlusOnly = Config.Bind("Stairs", "New Game Plus Only", false);

				Config.SettingChanged += Config_SettingChanged;

				ApplySettings();
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception);
			}
		}

		private static void Config_SettingChanged(object sender, EventArgs e)
		{
			ApplySettings();
		}

		private static void ApplySettings()
		{
			HarmonyPatches.Interaction_FixedUpdate.stairsWalkSpeed = stairsWalkSpeed.Value;
			HarmonyPatches.Interaction_FixedUpdate.ignoreAnimation = ignoreAnimation.Value;
			HarmonyPatches.Interaction_FixedUpdate.newGamePlusOnly = newGamePlusOnly.Value;
		}

		private void Awake()
		{
			try
			{
				Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

				var harmony = new Harmony(Info.Metadata.GUID);

				harmony.PatchAll();
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception);
			}
		}
	}
}
