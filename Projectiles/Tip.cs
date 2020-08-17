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
using System.IO;
using Terraria.ID;

namespace Nocturn.Projectiles
{
    public class Tip : ModProjectile
    {
        private const string ChainTexturePath = "Nocturn/Projectiles/TipChain";

        int center
        {
            get => (int)projectile.ai[1];
            set => projectile.ai[1] = value;
        }
        public int CorG = -1;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Laser Tip");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;

        }
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.penetrate = -1;
            //projectile.melee = true;
            projectile.hostile = true;
            projectile.damage = 30;
            projectile.tileCollide = false;
            projectile.timeLeft = 30;
        }
        public override void AI()
        {
            

            var player = Main.npc[CorG].position;
            NPC npc = Main.npc[CorG];
            //CorruptGuard modNPC = mod.modNPC as CorruptGuard;
            var CG = new Vector2(NPCType<CorruptGuard>());
            var vectorToNPC = npc.Center - projectile.Center;
            float currentChainLength = vectorToNPC.Length();
            //Vector2 center = Main.npc[modNPC.center].Center;

            //Main.NewText(currentChainLength);

            if (projectile.ai[0] == 0f)
            {
                // This is how far the chain would go measured in pixels
                float maxChainLength = 310f;
                projectile.tileCollide = true;

                if (currentChainLength > maxChainLength)
                {
                    // If we reach maxChainLength, we change behavior.
                    projectile.ai[0] = 1f;
                    projectile.netUpdate = true;
                }

            }
            else if (projectile.ai[0] == 1f)
            {
                // When ai[0] == 1f, the projectile has either hit a tile or has reached maxChainLength, so now we retract the projectile
                float elasticFactorA = 14f / 0.5f;
                float elasticFactorB = 0.9f / 0.05f;
                float maxStretchLength = 300f; // This is the furthest the flail can stretch before being forced to retract. Make sure that this is a bit less than maxChainLength so you don't accidentally reach maxStretchLength on the initial throw.

                if (projectile.ai[1] == 1f)
                    projectile.tileCollide = false;

                if (!projectile.tileCollide)
                    elasticFactorB *= 2f;

                int restingChainLength = 20;

                // If there is tension in the chain, or if the projectile is being forced to retract, give the projectile some velocity towards the player
                if (currentChainLength > restingChainLength || !projectile.tileCollide)
                {
                    var elasticAcceleration = vectorToNPC * elasticFactorA / currentChainLength - projectile.velocity;
                    elasticAcceleration *= elasticFactorB / elasticAcceleration.Length();
                    projectile.velocity *= 0.98f;
                    projectile.velocity += elasticAcceleration;
                }

            }
            Vector2 unit = new Vector2();
            float rotation = 0f;

            float r = unit.ToRotation() + rotation;

            projectile.rotation += 0.5f;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            NPC npc = Main.npc[CorG]; ;
            Texture2D Base = Main.projectileTexture[projectile.type];
            Vector2 mountedCenter = npc.Center;
            Texture2D chainTexture = ModContent.GetTexture(ChainTexturePath);

            var drawPosition = projectile.Center;
            var remainingVectorToPlayer = mountedCenter - drawPosition;

            float rotation = remainingVectorToPlayer.ToRotation() - MathHelper.PiOver2;


            while (true)
            {
                float length = remainingVectorToPlayer.Length();

                // Once the remaining length is small enough, we terminate the loop
                if (length < 25f || float.IsNaN(length))
                    break;

                // drawPosition is advanced along the vector back to the player by 12 pixels
                // 12 comes from the height of ExampleFlailProjectileChain.png and the spacing that we desired between links
                drawPosition += remainingVectorToPlayer * 30 / length;
                remainingVectorToPlayer = mountedCenter - drawPosition;

                // Finally, we draw the texture at the coordinates using the lighting information of the tile coordinates of the chain section
                lightColor = new Color(1f, length, 0.2f, 1) ;
                spriteBatch.Draw(chainTexture, drawPosition - Main.screenPosition, null, lightColor, rotation, chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
            }

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
            Vector2 drawOrigin = new Vector2(projectile.width * .5f, projectile.height * .5f);

            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
               
                Color color = new Color(255, 255, 55, 20) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);

              
                spriteBatch.Draw(Base, projectile.position - Main.screenPosition + drawOrigin, new Rectangle(0,0,projectile.width,projectile.height), color, projectile.rotation, drawOrigin, 1f + k, SpriteEffects.None, 0f);

            }
            

            spriteBatch.End();
            spriteBatch.Begin();
            Color lightcolor = new Color(1f, 1f, 0.2f, 1f);
            spriteBatch.Draw(Base, projectile.position - Main.screenPosition + drawOrigin, new Rectangle(0, 0, projectile.width, projectile.height), lightColor, projectile.rotation, drawOrigin, 1f, SpriteEffects.None, 0f);
            return false;

        }
    }
}
