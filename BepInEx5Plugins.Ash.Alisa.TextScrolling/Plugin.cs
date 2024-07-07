using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;

namespace BepInEx5Plugins.Ash.Alisa.TextScrolling
{
	[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
	public class Plugin : BaseUnityPlugin
	{
		private readonly ConfigEntry<float> typeSpeed;

		private Plugin()
		{
			typeSpeed = Config.Bind("TextScrolling", "Type Speed", 1f, "Multiply the speed of the type writer animation by this amount.");
			
			Config.SettingChanged += Config_SettingChanged;

			ApplySettings();
		}

		private void Config_SettingChanged(object sender, EventArgs e)
		{
			ApplySettings();
		}

		private void ApplySettings()
		{
			HarmonyPatches.TextBoxManager_Update.typeSpeed = typeSpeed.Value;
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
