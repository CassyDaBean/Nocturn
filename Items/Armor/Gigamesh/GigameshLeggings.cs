using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Nocturn.Items.WR;

namespace Nocturn.Items.Armor.Gigamesh
{

    [AutoloadEquip(EquipType.Legs)]
    public class GigameshLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {

            DisplayName.SetDefault("Gigamesh Leggings");
            Tooltip.SetDefault("A strong and durable pair of leggings made from a Gigaworms Carapace.");

        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 14;
            item.value = 10000;
            item.rare = 2;
            item.defense = 19;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.GetItem("GigawormCarapace"), 13);
            recipe.AddIngredient(mod.GetItem("Wormblood"), 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}