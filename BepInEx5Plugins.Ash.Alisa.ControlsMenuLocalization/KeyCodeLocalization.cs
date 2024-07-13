using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace BepInEx5Plugins.Ash.Alisa.ControlsMenuLocalization
{
	public class KeyCodeLocalization
	{
		public static readonly Dictionary<KeyCode, string> strings = new Dictionary<KeyCode, string>();
		public static readonly Dictionary<KeyCode, string> strings_French = new Dictionary<KeyCode, string>();
		public static readonly Dictionary<KeyCode, string> strings_Italian = new Dictionary<KeyCode, string>();
		public static readonly Dictionary<KeyCode, string> strings_Japanese = new Dictionary<KeyCode, string>();
		public static readonly Dictionary<KeyCode, string> strings_Custom = new Dictionary<KeyCode, string>();

		public static void Load()
		{
			try
			{
				ReadText(Path.Combine(Application.streamingAssetsPath, "Text"), "", strings);
				ReadText(Path.Combine(Application.streamingAssetsPath, "Text"), "_French", strings_French);
				ReadText(Path.Combine(Application.streamingAssetsPath, "Text"), "_Italian", strings_Italian);
				ReadText(Path.Combine(Application.streamingAssetsPath, "Text"), "_Japanese", strings_Japanese);
				ReadText(Path.Combine(Application.streamingAssetsPath, "CustomLanguage/Text"), "", strings_Custom);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		private static void ReadText(string basePath, string suffix, Dictionary<KeyCode, string> items)
		{
			if (items.Count > 0)
			{
				return;
			}

			var path = Path.Combine(basePath, "Keys" + suffix + ".txt");

			if (!File.Exists(path))
			{
				var enums = Enum.GetValues(typeof(KeyCode));
				var lines = new List<string>(enums.Length);

				Array.Sort(enums);

				foreach (var value in enums)
				{
					var name = ((KeyCode)value).ToString();

					if (lines.Contains(name))
					{
						continue;
					}

					lines.Add(name);
				}

				Directory.CreateDirectory(basePath);
				File.WriteAllLines(path, lines.ToArray());

				return;
			}
			else
			{
				var i = 0;
				var enums = Enum.GetValues(typeof(KeyCode));
				var lines = File.ReadAllLines(path);

				Array.Sort(enums);

				foreach (var value in enums)
				{
					if (items.ContainsKey((KeyCode)value))
					{
						continue;
					}

					items.Add((KeyCode)value, lines[i++]);
				}
			}
		}
	}
}
