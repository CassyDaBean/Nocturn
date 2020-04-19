using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Nocturn.Items.WR
{
    public class GigawormCarapace : ModItem
    {
        public override void SetStaticDefaults()
        {
            
            Tooltip.SetDefault("A large, hard and flexible naturally grown shell from a Gigaworm");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 38;
            item.maxStack = 999;

        }
    }
} 

