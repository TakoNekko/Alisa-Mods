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

		private readonly ConfigEntry<bool> allowEscapedLineBreak;

		private readonly ConfigEntry<string> lineBreakReplacement;

		private readonly ConfigEntry<bool> stripLineBreaks;
		
		private Plugin()
		{
			typeSpeed = Config.Bind("TextScrolling", "Type Speed", 1f, "Multiply the speed of the type writer animation by this amount.");
			allowEscapedLineBreak = Config.Bind("TextScrolling", "Allow Escaped Line Break", true, "If enabled, convert \\@ to @.");
			stripLineBreaks = Config.Bind("TextScrolling", "Strip Line Breaks", false, "If enabled, remove manually inserted line breaks.");
			lineBreakReplacement = Config.Bind("TextScrolling", "Line Break Replacement", " ", "If 'Strip Line Breaks' is true, replace line breaks with this value.");
			
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
			HarmonyPatches.TextBoxManager_Update.allowEscapedLineBreak = allowEscapedLineBreak.Value;
			HarmonyPatches.TextBoxManager_Update.stripLineBreaks = stripLineBreaks.Value;
			HarmonyPatches.TextBoxManager_Update.lineBreakReplacement = lineBreakReplacement.Value;
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
