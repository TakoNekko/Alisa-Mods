using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;

namespace BepInEx5Plugins.Ash.Alisa.Shortcuts
{
	[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
	public class Plugin : BaseUnityPlugin
	{
		private static ConfigEntry<bool> fadeOut;

		private Plugin()
		{
			try
			{
				fadeOut = Config.Bind("Quick Quit Game", "Fade Out", true);

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
			HarmonyPatches.Settings_Update.fadeOut = fadeOut.Value;
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
