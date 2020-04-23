using Terraria.ModLoader;

namespace Nocturn.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class AquafurosMask : ModItem
	{
		public override void SetDefaults() {
			item.width = 20;
			item.height = 20;
			item.rare = 1;
			item.vanity = true;
		}

		public override bool DrawHead() {
			return false;
		}
	}
}