using BepInEx;
using BepInEx.Configuration;
using System;
using UnityEngine;

namespace BepInEx5Plugins.Ash.Alisa.ControlScheme
{
	//[DefaultExecutionOrder(1000)]
	[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
	public class Plugin : BaseUnityPlugin
	{
		public bool isActive;

		public ConfigEntry<bool> IdleOnStart;

		public ConfigEntry<bool> UseDpad;

		public ConfigEntry<KeyboardShortcut> ActionKeyboardShortcut;
		public ConfigEntry<KeyboardShortcut> CancelKeyboardShortcut;
		public ConfigEntry<KeyboardShortcut> SprintKeyboardShortcut;
		public ConfigEntry<KeyboardShortcut> SwitchWeaponKeyboardShortcut;
		public ConfigEntry<KeyboardShortcut> AimKeyboardShortcut;
		public ConfigEntry<KeyboardShortcut> SubmitKeyboardShortcut;
		public ConfigEntry<KeyboardShortcut> SelectKeyboardShortcut;
		public ConfigEntry<KeyboardShortcut> ReloadKeyboardShortcut;
		public ConfigEntry<KeyboardShortcut> SpecialKeyboardShortcut;
		public ConfigEntry<KeyboardShortcut> UpKeyboardShortcut;
		public ConfigEntry<KeyboardShortcut> DownKeyboardShortcut;
		public ConfigEntry<KeyboardShortcut> LeftKeyboardShortcut;
		public ConfigEntry<KeyboardShortcut> RightKeyboardShortcut;

		public ConfigEntry<KeyboardShortcut> ActionJoystickShortcut;
		public ConfigEntry<KeyboardShortcut> CancelJoystickShortcut;
		public ConfigEntry<KeyboardShortcut> SprintJoystickShortcut;
		public ConfigEntry<KeyboardShortcut> SwitchWeaponJoystickShortcut;
		public ConfigEntry<KeyboardShortcut> AimJoystickShortcut;
		public ConfigEntry<KeyboardShortcut> SubmitJoystickShortcut;
		public ConfigEntry<KeyboardShortcut> SelectJoystickShortcut;
		public ConfigEntry<KeyboardShortcut> ReloadJoystickShortcut;
		public ConfigEntry<KeyboardShortcut> SpecialJoystickShortcut;
		public ConfigEntry<KeyboardShortcut> UpJoystickShortcut;
		public ConfigEntry<KeyboardShortcut> DownJoystickShortcut;
		public ConfigEntry<KeyboardShortcut> LeftJoystickShortcut;
		public ConfigEntry<KeyboardShortcut> RightJoystickShortcut;

		private Settings _settings;

		public Settings settings
		{
			get
			{
				if (!_settings)
				{
					_settings = GameObject.Find("ScenePersistenceManager")?.GetComponent<Settings>();
				}

				return _settings;
			}
		}

		private Plugin()
		{
			IdleOnStart = Config.Bind("Shortcuts", "Idle on Start", false, "If true, controls are overriden only when pressing controls on a different control scheme. Otherwise, the plugin will override controls as soon as any input is pressed.");
			UseDpad = Config.Bind("Shortcuts", "Use D-Pad", false, "If true, use the D-Pad instead of the left stick.");
			
			ActionKeyboardShortcut = Config.Bind("Shortcuts", "Keyboard Action", new KeyboardShortcut(KeyCode.LeftControl));
			CancelKeyboardShortcut = Config.Bind("Shortcuts", "Keyboard Cancel", new KeyboardShortcut(KeyCode.Backspace));
			SprintKeyboardShortcut = Config.Bind("Shortcuts", "Keyboard Sprint", new KeyboardShortcut(KeyCode.LeftShift));
			SwitchWeaponKeyboardShortcut = Config.Bind("Shortcuts", "Keyboard SwitchWeapon", new KeyboardShortcut(KeyCode.Tab));
			AimKeyboardShortcut = Config.Bind("Shortcuts", "Keyboard Aim", new KeyboardShortcut(KeyCode.Space));
			SubmitKeyboardShortcut = Config.Bind("Shortcuts", "Keyboard Submit", new KeyboardShortcut(KeyCode.Return));
			SelectKeyboardShortcut = Config.Bind("Shortcuts", "Keyboard Select", new KeyboardShortcut(KeyCode.Escape));
			ReloadKeyboardShortcut = Config.Bind("Shortcuts", "Keyboard Reload", new KeyboardShortcut(KeyCode.R));
			SpecialKeyboardShortcut = Config.Bind("Shortcuts", "Keyboard Special", new KeyboardShortcut(KeyCode.CapsLock));
			UpKeyboardShortcut = Config.Bind("Shortcuts", "Keyboard Up", new KeyboardShortcut(KeyCode.UpArrow));
			DownKeyboardShortcut = Config.Bind("Shortcuts", "Keyboard Down", new KeyboardShortcut(KeyCode.DownArrow));
			LeftKeyboardShortcut = Config.Bind("Shortcuts", "Keyboard Left", new KeyboardShortcut(KeyCode.LeftArrow));
			RightKeyboardShortcut = Config.Bind("Shortcuts", "Keyboard Right", new KeyboardShortcut(KeyCode.RightArrow));

			ActionJoystickShortcut = Config.Bind("Shortcuts", "Joystick Action", new KeyboardShortcut(KeyCode.JoystickButton0));
			CancelJoystickShortcut = Config.Bind("Shortcuts", "Joystick Cancel", new KeyboardShortcut(KeyCode.JoystickButton1));
			SprintJoystickShortcut = Config.Bind("Shortcuts", "Joystick Sprint", new KeyboardShortcut(KeyCode.JoystickButton2));
			SwitchWeaponJoystickShortcut = Config.Bind("Shortcuts", "Joystick SwitchWeapon", new KeyboardShortcut(KeyCode.JoystickButton3));
			AimJoystickShortcut = Config.Bind("Shortcuts", "Joystick Aim", new KeyboardShortcut(KeyCode.JoystickButton5));
			SubmitJoystickShortcut = Config.Bind("Shortcuts", "Joystick Submit", new KeyboardShortcut(KeyCode.JoystickButton7));
			SelectJoystickShortcut = Config.Bind("Shortcuts", "Joystick Select", new KeyboardShortcut(KeyCode.JoystickButton6));
			ReloadJoystickShortcut = Config.Bind("Shortcuts", "Joystick Reload", new KeyboardShortcut(KeyCode.None));
			SpecialJoystickShortcut = Config.Bind("Shortcuts", "Joystick Special", new KeyboardShortcut(KeyCode.None));
			UpJoystickShortcut = Config.Bind("Shortcuts", "Joystick Up", new KeyboardShortcut(KeyCode.None));
			DownJoystickShortcut = Config.Bind("Shortcuts", "Joystick Down", new KeyboardShortcut(KeyCode.None));
			LeftJoystickShortcut = Config.Bind("Shortcuts", "Joystick Left", new KeyboardShortcut(KeyCode.None));
			RightJoystickShortcut = Config.Bind("Shortcuts", "Joystick Right", new KeyboardShortcut(KeyCode.None));
		}

		private void Awake()
		{
			Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
		}

		private void Update()
		{
			try
			{
				if (!settings)
				{
					return;
				}

				var forceCheck = false;

				if (!isActive
					&& !IdleOnStart.Value)
				{
					forceCheck = true;
				}

				if (forceCheck || settings.buttonMapping == 0)
				{
					var axis = Input.GetAxis(settings.customHorizontalAxis);

					if (axis < -0.5f || axis > 0.5f)
					{
						UseJoystickMapping();
						return;
					}

					axis = Input.GetAxis(settings.customVerticalAxis);

					if (axis < -0.5f || axis > 0.5f)
					{
						UseJoystickMapping();
						return;
					}
					
					if (Input.GetKeyDown(ActionJoystickShortcut.Value.MainKey)
						|| Input.GetKeyDown(CancelJoystickShortcut.Value.MainKey)
						|| Input.GetKeyDown(SprintJoystickShortcut.Value.MainKey)
						|| Input.GetKeyDown(SwitchWeaponJoystickShortcut.Value.MainKey)
						|| Input.GetKeyDown(AimJoystickShortcut.Value.MainKey)
						|| Input.GetKeyDown(SubmitJoystickShortcut.Value.MainKey)
						|| Input.GetKeyDown(SelectJoystickShortcut.Value.MainKey)
						|| Input.GetKeyDown(ReloadJoystickShortcut.Value.MainKey)
						|| Input.GetKeyDown(SpecialJoystickShortcut.Value.MainKey)
						|| Input.GetKeyDown(UpJoystickShortcut.Value.MainKey)
						|| Input.GetKeyDown(DownJoystickShortcut.Value.MainKey)
						|| Input.GetKeyDown(LeftJoystickShortcut.Value.MainKey)
						|| Input.GetKeyDown(RightJoystickShortcut.Value.MainKey))
					{
						UseJoystickMapping();
						return;
					}
				}

				if (forceCheck || settings.buttonMapping == 1)
				{
					if (settings.keyboardAlt > 0)
					{
						var axis = Input.GetAxis("Horizontal_WASD");

						if (axis < -0.1f || axis > 0.1f)
						{
							UseKeyboardMapping();
							return;
						}

						axis = Input.GetAxis("Vertical_WASD");

						if (axis < -0.1f || axis > 0.1f)
						{
							UseKeyboardMapping();
							return;
						}
					}
					else if (settings.keyboardAlt < 0)
					{
						var axis = Input.GetAxis("Horizontal_ZQSD");

						if (axis < -0.1f || axis > 0.1f)
						{
							UseKeyboardMapping();
							return;
						}

						axis = Input.GetAxis("Vertical_ZQSD");

						if (axis < -0.1f || axis > 0.1f)
						{
							UseKeyboardMapping();
							return;
						}
					}

					if (Input.GetKeyDown(ActionKeyboardShortcut.Value.MainKey)
						|| Input.GetKeyDown(CancelKeyboardShortcut.Value.MainKey)
						|| Input.GetKeyDown(SprintKeyboardShortcut.Value.MainKey)
						|| Input.GetKeyDown(SwitchWeaponKeyboardShortcut.Value.MainKey)
						|| Input.GetKeyDown(AimKeyboardShortcut.Value.MainKey)
						|| Input.GetKeyDown(SubmitKeyboardShortcut.Value.MainKey)
						|| Input.GetKeyDown(SelectKeyboardShortcut.Value.MainKey)
						|| Input.GetKeyDown(ReloadKeyboardShortcut.Value.MainKey)
						|| Input.GetKeyDown(SpecialKeyboardShortcut.Value.MainKey)
						|| Input.GetKeyDown(UpKeyboardShortcut.Value.MainKey)
						|| Input.GetKeyDown(DownKeyboardShortcut.Value.MainKey)
						|| Input.GetKeyDown(LeftKeyboardShortcut.Value.MainKey)
						|| Input.GetKeyDown(RightKeyboardShortcut.Value.MainKey))
					{
						UseKeyboardMapping();
						return;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}
		
		public void UseKeyboardMapping()
		{
			Console.WriteLine("Changing control scheme to keyboard type.");

			isActive = true;

			settings.buttonMapping = 0;

			settings.keys["Action"] = ActionKeyboardShortcut.Value.MainKey;
			settings.keys["Cancel"] = CancelKeyboardShortcut.Value.MainKey;
			settings.keys["Sprint"] = SprintKeyboardShortcut.Value.MainKey;
			settings.keys["SwitchWeapon"] = SwitchWeaponKeyboardShortcut.Value.MainKey;
			settings.keys["Aim"] = AimKeyboardShortcut.Value.MainKey;
			settings.keys["Submit"] = SubmitKeyboardShortcut.Value.MainKey;
			settings.keys["Select"] = SelectKeyboardShortcut.Value.MainKey;
			settings.keys["Reload"] = ReloadKeyboardShortcut.Value.MainKey;
			settings.keys["Special"] = SpecialKeyboardShortcut.Value.MainKey;
			settings.keys["Up"] = UpKeyboardShortcut.Value.MainKey;
			settings.keys["Down"] = DownKeyboardShortcut.Value.MainKey;
			settings.keys["Left"] = LeftKeyboardShortcut.Value.MainKey;
			settings.keys["Right"] = RightKeyboardShortcut.Value.MainKey;

			settings.SaveSettings();
		}

		public void UseJoystickMapping()
		{
			Console.WriteLine("Changing control scheme to joystick type.");

			isActive = true;

			settings.buttonMapping = 1;

			if (Application.platform != RuntimePlatform.OSXPlayer)
			{
				settings.keys["Action"] = ActionJoystickShortcut.Value.MainKey;
				settings.keys["Cancel"] = CancelJoystickShortcut.Value.MainKey;
				settings.keys["Sprint"] = SprintJoystickShortcut.Value.MainKey;
				settings.keys["SwitchWeapon"] = SwitchWeaponJoystickShortcut.Value.MainKey;
				settings.keys["Aim"] = AimJoystickShortcut.Value.MainKey;
				settings.keys["Submit"] = SubmitJoystickShortcut.Value.MainKey;
				settings.keys["Select"] = SelectJoystickShortcut.Value.MainKey;
				settings.keys["Reload"] = ReloadJoystickShortcut.Value.MainKey;
				settings.keys["Special"] = SpecialJoystickShortcut.Value.MainKey;
				settings.keys["Up"] = UpJoystickShortcut.Value.MainKey;
				settings.keys["Down"] = DownJoystickShortcut.Value.MainKey;
				settings.keys["Left"] = LeftJoystickShortcut.Value.MainKey;
				settings.keys["Right"] = RightJoystickShortcut.Value.MainKey;

				if (settings.horizontalAxis == 0 || settings.verticalAxis == 0)
				{
					if (UseDpad.Value)
					{
						SetDpad();
					}
					else
					{
						SetAnalog();
					}
				}
			}

			settings.SaveSettings();
		}

		public void SetDpad()
		{
			if (Application.platform != RuntimePlatform.OSXPlayer)
			{
				settings.horizontalAxis = 6;
				settings.verticalAxis = 7;
				settings.WhatAxis();
			}
		}

		public void SetAnalog()
		{
			settings.horizontalAxis = 1;
			settings.verticalAxis = -2;
			settings.WhatAxis();
		}
	}
}
