using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;

namespace BepInEx5Plugins.Ash.Alisa.ResizeTextForBestFit
{
	[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
	public class Plugin : BaseUnityPlugin
	{
		private readonly ConfigEntry<int> maxSize;

		private readonly ConfigEntry<bool> singleOutline;

		private Plugin()
		{
			maxSize = Config.Bind("ResizeTextForBestFit", "Max Size", 18);
			singleOutline = Config.Bind("ResizeTextForBestFit", "Single Outline", true);

			Config.SettingChanged += Config_SettingChanged;

			ApplySettings();
		}

		private void Config_SettingChanged(object sender, EventArgs e)
		{
			ApplySettings();
		}

		private void ApplySettings()
		{
			HarmonyPatches.KeyBindScript_Start.maxSize = maxSize.Value;
			HarmonyPatches.KeyBindScript_Start.singleOutline = singleOutline.Value;
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
