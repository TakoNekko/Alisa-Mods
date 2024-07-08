using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;

namespace BepInEx5Plugins.Ash.Alisa.PuppetShopMenuFix
{
	[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
	public class Plugin : BaseUnityPlugin
	{
		private static ConfigEntry<float> checkDelay;

		private Plugin()
		{
			try
			{
				checkDelay = Config.Bind("PuppetShop", "Check Delay", 1f);

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
			HarmonyPatches.PuppetShopMenu_Update.checkDelay = checkDelay.Value;
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
