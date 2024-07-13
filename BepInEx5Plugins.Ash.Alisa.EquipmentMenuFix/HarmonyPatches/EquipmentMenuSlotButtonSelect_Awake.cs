using HarmonyLib;
using UnityEngine.UI;

namespace BepInEx5Plugins.Ash.Alisa.EquipmentMenuFix.HarmonyPatches
{
	[HarmonyPatch(typeof(EquipmentMenuSlotButtonSelect), "Awake")]
	public class EquipmentMenuSlotButtonSelect_Awake
	{
		// Set the disabled state to be the same as the normal state.
		public static void Postfix(EquipmentMenuSlotButtonSelect __instance)
		{
			var button = __instance.GetComponent<Button>();

			if (button)
			{
				if (button.transition == Selectable.Transition.SpriteSwap)
				{
					var spriteState = button.spriteState;

					spriteState.disabledSprite = button.image.sprite;

					button.spriteState = spriteState;
				}
				else if (button.transition == Selectable.Transition.ColorTint)
				{
					var colorBlock = button.colors;

					colorBlock.disabledColor = colorBlock.normalColor;

					button.colors = colorBlock;
				}
			}
		}
	}
}
