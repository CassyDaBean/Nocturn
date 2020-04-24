using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Nocturn.Projectiles.Summon;
using Nocturn.NPCs.Bosses.Aquafuros;



namespace Nocturn.Items.Boss.Aqua
{
	public class Aquachal : ModItem
	{
		public override void SetStaticDefaults() {
            DisplayName.SetDefault("Aqua Chalice");
			Tooltip.SetDefault("Use to summon Aquafuros."
                +"\n 'The water in it seems to steadily stir around by itself.\'");
		}

		public override void SetDefaults() {
			item.width = 28;
			item.height = 36;
			item.maxStack = 20;
			item.value = 100;
			item.rare = 1;         
            item.useTime = 45;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.useAnimation = 20;
            item.consumable = true;

            item.shoot = ModContent.ProjectileType<Nothing>();
        }
        //Player player = Main.player;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<GeneralBossSpawn>(),ModContent.NPCType<AquafurosHead>(), knockBack, player.whoAmI);
            return false;
        }
        public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Coral, 3);
			recipe.AddIngredient(ItemID.Wood, 15);
            recipe.AddIngredient(ItemID.Gel, 15);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this, 1);
			recipe.AddRecipe();
			

			
		}
	}
}
