
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Nocturn.Items.Swords
{
    public class GigaSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gigamesh Sword");
            Tooltip.SetDefault("A blade crafted from the strong Carapace of a Gigaworm.");
        }

        public override void SetDefaults()
        {
            item.damage = 40;
            item.melee = true;
            item.width = 34;
            item.height = 44;
            item.useTime = 15;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.knockBack = 6;
            item.value = 10000;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.GetItem("GigawormCarapace"), 5);
            recipe.AddIngredient(mod.GetItem("Wormblood"), 4);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
