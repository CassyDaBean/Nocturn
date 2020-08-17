using Terraria;
using Terraria.ModLoader;


namespace Nocturn.NPCs.Bosses.Aquafuros.AquaMain
{
    public class AquaDefeat : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aquafuros");
            Main.projFrames[projectile.type] = 13;
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
            if (++projectile.frameCounter >= 13)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 4)
                {
                    projectile.frame = 0;
                }
            }
            projectile.alpha += 10;
            projectile.velocity.X *= 0.00f;
            projectile.velocity.Y -= .0f;
        }
    }
}
