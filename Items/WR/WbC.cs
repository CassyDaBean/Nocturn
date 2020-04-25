
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
using Nocturn.NPCs.WE;



namespace Nocturn.Items.WR
{
	public class WormbloodChalice : ModItem
	{
		public override void SetStaticDefaults() {
            DisplayName.SetDefault("Wormblood Chalice");
            Tooltip.SetDefault("Filled to the brim with the blood of worms!"
                + "\n Use to summon the Worm Regime!"
                + "\n (WIP)");

		}

		public override void SetDefaults() {
			item.width = 26;
			item.height = 44;
			item.maxStack = 20;
			item.value = 100;
			item.rare = 1;         
            item.useTime = 45;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.useAnimation = 20;
            item.consumable = true;

            item.shoot = ModContent.ProjectileType<Nothing>();
        }
       
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<GeneralBossSpawn>(),ModContent.NPCType<WEvent>(), knockBack, player.whoAmI);
            return false;
        }
        public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Wormblood"),5 );
            recipe.AddIngredient(ItemID.GoldBar, 15);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 1);
			recipe.AddRecipe();
			

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Wormblood"),5 );
            recipe.AddIngredient(ItemID.PlatinumBar, 15);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();            
        }
	}
}
