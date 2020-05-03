using Terraria;
using Terraria.ModLoader;
using Nocturn.NPCs.Bosses.Aquafuros.AquaMain.AquaE;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace Nocturn.NPCs.Bosses.Aquafuros.AquaMain
{
    public class AquaTrans : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aquafuros");
            Main.projFrames[projectile.type] = 17;
        }
        public override void SetDefaults()
        {
            projectile.damage = 0;
            projectile.width = 120;
            projectile.height = 118;
            projectile.penetrate = -1;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 900;
            projectile.alpha = 0;
        }
        public override void AI()
        {
            if (++projectile.frameCounter >= 6)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 17)
                {
                    int AF = NPC.NewNPC((int)projectile.Center.X, (int)projectile.Center.Y, NPCType<AquafurosE>());
                    projectile.timeLeft = 0;
                }
            }
            //projectile.alpha += 10;
            projectile.velocity.X *= 0.00f;
            projectile.velocity.Y -= .0f;
        }
    }
}
