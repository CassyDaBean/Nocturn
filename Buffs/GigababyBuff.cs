using Terraria;
using Terraria.ModLoader;
using Nocturn.Projectiles.SW;
using static Terraria.ModLoader.ModContent;
namespace Nocturn.Buffs
{
    class GigababyBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Gigaworm Baby");
            Description.SetDefault("A cute baby Gigaworm is following you!");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            NocturnPlayer modPlayer = player.GetModPlayer<NocturnPlayer>();
            if (player.ownedProjectileCounts[ProjectileType<GigababyHead>()] >= 0)
            {
                player.buffTime[buffIndex] = 18000;
                modPlayer.GigaBab = true;
            }
            if (!modPlayer.GigaBab)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
                return;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;
            }
        }
    }
}

