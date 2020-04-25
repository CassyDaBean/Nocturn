
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Nocturn.Items.Swords
{
    public class Aquamace : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aqua Mace");
            Tooltip.SetDefault("The weapon of a lone, young Averix.");
        }

        public override void SetDefaults()
        {
            item.damage = 30;
            item.melee = true;
            item.width = 44;
            item.height = 44;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.knockBack = 6;
            item.value = 10000;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }
    }
}
