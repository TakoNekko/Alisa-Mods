using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace BepInEx5Plugins.Ash.Alisa.ControlsMenuLocalization
{
	public class AxisLocalization
	{
		public static readonly List<string> strings = new List<string>();
		public static readonly List<string> strings_French = new List<string>();
		public static readonly List<string> strings_Italian = new List<string>();
		public static readonly List<string> strings_Japanese = new List<string>();
		public static readonly List<string> strings_Custom = new List<string>();

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

		private static void ReadText(string basePath, string suffix, List<string> items)
		{
			if (items.Count > 0)
			{
				return;
			}

			var path = Path.Combine(basePath, "Axis" + suffix + ".txt");

			if (!File.Exists(path))
			{
				var lines = new List<string>(24);

				for (var i = 0; i < 12; ++i)
				{
					var axis = "Axis " + (i + 1).ToString();

					lines.Add(axis + " -");
					lines.Add(axis + " +");
				}

				Directory.CreateDirectory(basePath);
				File.WriteAllLines(path, lines.ToArray());

				return;
			}
			else
			{
				var lines = File.ReadAllLines(path);

				if (lines.Length < 24)
				{
					Console.WriteLine("ERROR: Invalid axis text file. Must have 24 lines.");
					return;
				}

				for (var i = 0; i < 24; ++i)
				{
					if (string.IsNullOrEmpty(lines[i]))
					{
						break;
					}

					items.Add(lines[i]);
				}
			}
		}
	}
}

