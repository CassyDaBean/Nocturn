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
			Tooltip.SetDefault("Filled to the brim with the blood of worms!");
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
            

            /*
			// Start a new Recipe. (Prepend with "ModRecipe " if 1st recipe in code block.)
			recipe = new ModRecipe(mod);
			// Add a Vanilla Ingredient. 
			// Look up ItemIDs: https://github.com/bluemagic123/tModLoader/wiki/Vanilla-Item-IDs
			// To specify more than one ingredient, use multiple recipe.AddIngredient() calls.
			recipe.AddIngredient(ItemID.DirtBlock);
			// An optional 2nd argument will specify a stack of the item. 
			recipe.AddIngredient(ItemID.Acorn, 10);
			// We can also specify the current item as an ingredient
			recipe.AddIngredient(this, 2);
			// Add a Mod Ingredient. Do not attempt ItemID.EquipMaterial, it's not how it works.
			recipe.AddIngredient(mod, "EquipMaterial", 3);
			// an alternate approach to the above.
			recipe.AddIngredient(mod.ItemType("EquipMaterial"), 3);
			// RecipeGroups allow you create a recipe that accepts items from a group of similar ingredients. For example, all varieties of Wood are in the vanilla "Wood" Group
			recipe.AddRecipeGroup("Wood"); // check here for other vanilla groups: https://github.com/bluemagic123/tModLoader/wiki/ModRecipe#public-void-addrecipegroupstring-name-int-stack--1
			// Here is using a mod recipe group. Check out ExampleMod.AddRecipeGroups() to see how to register a recipe group.
			recipe.AddRecipeGroup("ExampleMod:ExampleItem", 2);
			// To specify a crafting station, specify a tile. Look up TileIDs: https://github.com/bluemagic123/tModLoader/wiki/Vanilla-Tile-IDs
			recipe.AddTile(TileID.WorkBenches);
			// A mod Tile example. To specify more than one crafting station, use multiple recipe.AddTile() calls.
			recipe.AddTile(mod, "ExampleWorkbench");
			// There is a limit of 14 ingredients and 14 tiles to a recipe.
			// Special
			// Water, Honey, and Lava are not tiles, there are special bools for those. Also needSnowBiome. Water also specifies that it works with Sinks.
			recipe.needHoney = true;
			// Set the result of the recipe. You can use stack here too. Since this is in a ModItem class, we can use "this" to specify this item as the result.
			recipe.SetResult(this, 999); // or, for a vanilla result, recipe.SetResult(ItemID.Muramasa);
			// Finish your recipe
			recipe.AddRecipe();
			*/
        }
	}
}
