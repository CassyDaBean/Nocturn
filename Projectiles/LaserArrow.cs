using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;
using Nocturn.NPCs.Bosses;
using Terraria.ID;



namespace Nocturn.Projectiles
{
    public class LaserArrow : ModProjectile
    {
        public bool GLOW = false;


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Corrupt arrow");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.damage = 13;
            projectile.friendly = false;
            projectile.height = 30;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 400;
            projectile.width = 18;
            projectile.alpha = 0;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {

            Texture2D Base = Main.projectileTexture[projectile.type];
            Vector2 drawOrigin = new Vector2(projectile.width * .5f, projectile.height * .5f);

            Color lightcolor = new Color(1f, 1f, 0.2f, 1f);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);

            if (GLOW)
            {
                for (int k = 0; k < projectile.oldPos.Length; k++)
                {
                    Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);

                    Color color = new Color(255, 255, 55, projectile.alpha) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);

                    spriteBatch.Draw(Base, projectile.position - Main.screenPosition + drawOrigin, new Rectangle(0, 0, projectile.width, projectile.height), color, projectile.rotation, drawOrigin, 1f + k, SpriteEffects.None, 0f);

                }
            }
            spriteBatch.Draw(Base, projectile.position - Main.screenPosition + drawOrigin, new Rectangle(0, 0, projectile.width, projectile.height), lightColor, projectile.rotation, drawOrigin, 1f, SpriteEffects.None, 0f);                 
            return false;
        }
        int target;
        public override void AI()
        {
            //Player targetPlayer = Main.player[target];
            Player targetPlayer = Main.player[target];

            projectile.ai[0] += 1f; // Use a timer to wait 15 ticks before applying gravity.
            projectile.ai[1] += 0.5f;
            //projectile.ai[2] = 0;
            //projectile.rotation += 0.08f;
            if (projectile.ai[0] == 15f)
            {
                projectile.ai[0] = 15f;
                projectile.velocity.Y = 0f;
                projectile.velocity.X = 0f;
                projectile.alpha = 4;
                //projectile.rotation += 0.5f;
                
                
            }
            if (projectile.ai[1] >= 15f)
            {

                GLOW = true;
                projectile.ai[0] = 16;
                
                    
                    Vector2 direction = targetPlayer.Center - projectile.Center;
                    float targetAngle = direction.ToRotation();
                projectile.rotation = targetAngle + 1.5f;

                    
                    projectile.velocity = direction.SafeNormalize(Vector2.Zero) * 6f;
                }
                }

            }
                


        }


      
