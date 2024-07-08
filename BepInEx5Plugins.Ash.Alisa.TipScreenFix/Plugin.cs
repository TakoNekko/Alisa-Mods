using BepInEx;
using HarmonyLib;
using System;

namespace BepInEx5Plugins.Ash.Alisa.TipScreenFix
{
	[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
	public class Plugin : BaseUnityPlugin
	{
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
