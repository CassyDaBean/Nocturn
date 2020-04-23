/*
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Nocturn.Items.WR;

namespace Nocturn.Items.Armor.Gigamesh
{

    [AutoloadEquip(EquipType.Head)]
    public class GigameshHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {

            DisplayName.SetDefault("Gigamesh Helmet");
            Tooltip.SetDefault("A strong and durable Helmet made from a Gigaworms Carapace.");

        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 22;
            item.value = 10000;
            item.rare = 2;
            item.defense = 16;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<GigameshBody>() && legs.type == ItemType<GigameshLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            
            player.allDamage += 0.6f;
            /* Here are the individual weapon class bonuses.
			player.meleeDamage -= 0.2f;
			player.thrownDamage -= 0.2f;
			player.rangedDamage -= 0.2f;
			player.magicDamage -= 0.2f;
			player.minionDamage -= 0.2f;
			*/
/* }

 public override void AddRecipes()
 {
     ModRecipe recipe = new ModRecipe(mod);
     recipe.AddIngredient(mod.GetItem("GigawormCarapace"), 10);
     recipe.AddIngredient(mod.GetItem("Wormblood"), 2);
     recipe.AddTile(TileID.Anvils);
     recipe.SetResult(this);
     recipe.AddRecipe();
 }
}
}*/