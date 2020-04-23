using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Nocturn.Items
{
    public class Aquabag : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
        }

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 32;
            item.height = 32;
            item.rare = 11;
            item.expert = true;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {

            int choice = Main.rand.Next(7);
            if (choice == 0)
            {
                player.QuickSpawnItem(mod.ItemType("AquafurosMask"));
            }
            else if (choice == 1)
            {
                player.QuickSpawnItem(mod.ItemType("AquaT1"));
            }
            if (choice != 1)
            { 
                
            }
            player.QuickSpawnItem(mod.ItemType("Aquamace"));
            player.QuickSpawnItem(mod.ItemType("AquaSpitter"));
        }

        public override int BossBagNPC => mod.NPCType("Aquafuros");
    }
}