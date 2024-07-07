using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace BepInEx5Plugins.Ash.Alisa.Closet.HarmonyPatches
{
	[HarmonyPatch(typeof(ClosetButtonsSorting), "Update")]
	public class ClosetButtonsSorting_Update
	{
		public static float checkDelay = 1f;

		public static bool checkPending;

		public static float timer;

		private static FieldInfo ButtonSelected_selected;

		public static bool Prepare(MethodBase original)
		{
			if (original is null)
			{
				try
				{
					ButtonSelected_selected = typeof(ButtonSelected).GetField("selected", BindingFlags.NonPublic | BindingFlags.Instance);
				}
				catch (Exception exception)
				{
					Console.WriteLine(exception);
					return false;
				}
			}

			return true;
		}

		// Auto fix button selection if necessary.
		public static void Postfix(ClosetButtonsSorting __instance)
		{
			if (!checkPending)
			{
				return;
			}

			var equipmentMenu = __instance.GetComponent<EquipmentMenu>();

			if (!equipmentMenu || !equipmentMenu.charlotteIsActive)
			{
				return;
			}

			if (timer >= checkDelay)
			{
				timer -= checkDelay;

				if (!HasSelection())
				{
					SelectionFix();
				}
			}

			timer += Time.deltaTime;
		}

		public static bool HasSelection()
		{
			foreach (var buttonSelected in Object.FindObjectsOfType<ButtonSelected>())
			{
				if ((bool)ButtonSelected_selected.GetValue(buttonSelected))
				{
					return true;
				}
			}

			return false;
		}

		public static void SelectionFix()
		{
			checkPending = false;

			if (!EventSystem.current)
			{
				return;
			}

			var selectedObject = EventSystem.current.currentSelectedGameObject;

			if (!selectedObject)
			{
				return;
			}

			Console.WriteLine("ClosetButtonsSorting_Update.SelectionFix: Forcibly reset selection to game object " + selectedObject);

			var buttonSelected = selectedObject.GetComponent<ButtonSelected>();

			if (!buttonSelected)
			{
				return;
			}

			if (buttonSelected.slotButton01 || buttonSelected.itemButtonSlot01)
			{
				buttonSelected.slot_Button01?.GetComponent<Button>()?.Select();
			}
			else if (buttonSelected.slotButton02 || buttonSelected.itemButtonSlot02)
			{
				buttonSelected.slot_Button02?.GetComponent<Button>()?.Select();
			}
			else if (buttonSelected.slotButton03 || buttonSelected.itemButtonSlot03)
			{
				buttonSelected.slot_Button03?.GetComponent<Button>()?.Select();
			}
		}
	}
}
