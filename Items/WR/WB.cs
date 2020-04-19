
using Terraria;
using Terraria.ID;

using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Nocturn.Items.WR
{
    public class Wormblood : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("the remaining blood drained from a worm.");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 24;
            item.maxStack = 999;

        }
    }
} 

