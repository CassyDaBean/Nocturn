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


namespace Nocturn.Projectiles.Bosses.Aqua
{
    class WaterA : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Water Arrow");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[projectile.type] = 1;        //The recording mode
        }

        public override void SetDefaults()
        {
            projectile.friendly = false;
            projectile.aiStyle = 1;
            projectile.width = 14;
            projectile.height = 30;
            projectile.alpha = 0;
            projectile.timeLeft = 600;
            projectile.penetrate = -1;
            projectile.hostile = true;
            projectile.magic = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            aiType = ProjectileID.Bullet;
        }
        

            public override Color? GetAlpha(Color lightColor) // color of glow
        {
            return Color.Azure;
        }
    }
}

