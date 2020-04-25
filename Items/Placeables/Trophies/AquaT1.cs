using Nocturn.Tiles.Trophy;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Nocturn.Items.Placeables.Trophies
{
    class AquaT1 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aquafuros Trophy");
            Tooltip.SetDefault("Well played Terrarian.");
        }
        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 34;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.value = 50000;
            item.rare = 1;
            item.createTile = TileType<AquaTroph1>();
            item.placeStyle = 0;
        }

    }
}
