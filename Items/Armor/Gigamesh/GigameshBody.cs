/*
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Nocturn.Items.WR;

namespace Nocturn.Items.Armor.Gigamesh
{

    [AutoloadEquip(EquipType.Body)]
    public class GigameshBody : ModItem
    {
        public override void SetStaticDefaults()
        {
            
            DisplayName.SetDefault("Gigamesh breastplate");
            Tooltip.SetDefault("A strong and durable breastplate made from a Gigaworms Carapace.");
                
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 20;
            item.value = 10000;
            item.rare = 2;
            item.defense = 26;
        }

       
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.GetItem("GigawormCarapace"), 20);
            recipe.AddIngredient(mod.GetItem("Wormblood"), 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}*/