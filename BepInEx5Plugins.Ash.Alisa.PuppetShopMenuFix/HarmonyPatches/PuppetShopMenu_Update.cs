using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace BepInEx5Plugins.Ash.Alisa.PuppetShopMenuFix.HarmonyPatches
{
	[HarmonyPatch(typeof(PuppetShopMenu), "Update")]
	public class PuppetShopMenu_Update
	{
		public static float checkDelay = 1f;

		public static bool checkPending;

		public static float timer;

		private static FieldInfo ShopCancelButton_selected;

		public static bool Prepare(MethodBase original)
		{
			if (original is null)
			{
				try
				{
					ShopCancelButton_selected = typeof(ShopCancelButton).GetField("selected", BindingFlags.NonPublic | BindingFlags.Instance);
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
		public static void Postfix(PuppetShopMenu __instance)
		{
			if (!checkPending)
			{
				return;
			}
				
			if (timer >= checkDelay)
			{
				timer -= checkDelay;

				if (!HasSelection())
				{
					SelectionFix(__instance);
				}
			}

			timer += Time.deltaTime;
		}

		public static bool HasSelection()
		{
			foreach (var button in Object.FindObjectsOfType<ShopCancelButton>())
			{
				if ((bool)ShopCancelButton_selected.GetValue(button))
				{
					return true;
				}
			}

			return false;
		}

		public static void SelectionFix(PuppetShopMenu shop)
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

			Console.WriteLine("PuppetShopMenu_Update.SelectionFix: Forcibly resetting shop selection to game object " + selectedObject);
			
			if (shop.weaponsScreen.activeSelf)
			{
				Console.WriteLine("Selecting first weapons slot");

				shop.firstWeaponsSlot?.GetComponent<Button>()?.Select();
			}
			else if (shop.dressesScreen.activeSelf)
			{
				Console.WriteLine("Selecting first dresses slot");

				shop.firstDressesSlot?.GetComponent<Button>()?.Select();
			}
			else if (shop.itemsScreen.activeSelf)
			{
				Console.WriteLine("Selecting first items slot");

				shop.firstItemsSlot?.GetComponent<Button>()?.Select();
			}
			else if (shop.modScreen.activeSelf)
			{
				Console.WriteLine("Selecting first mod slot");

				shop.firstSlectedMod?.GetComponent<Button>()?.Select();
			}
			else if (shop.rewardScreen.activeSelf)
			{
				Console.WriteLine("Selecting first reward slot");

				shop.rewardsFirstSelected?[0]?.GetComponent<Button>()?.Select();
			}

			else if (shop.buttons.activeSelf)
			{
				Console.WriteLine("Selecting weapons button");

				shop.weaponsButton?.GetComponent<Button>()?.Select();
			}
			else if (shop.buttons2.activeSelf)
			{
				Console.WriteLine("Selecting weapons button 2");

				shop.weaponsButton2?.GetComponent<Button>()?.Select();
			}
			else if (shop.buttons3.activeSelf)
			{
				Console.WriteLine("Selecting modifications button");

				shop.modificationsButton?.GetComponent<Button>()?.Select();
			}
		}
	}
}
