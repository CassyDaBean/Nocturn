using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Nocturn.Projectiles.Wep
{
    public class Spike : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.aiStyle = 1;
            projectile.scale = 1f;
            projectile.penetrate = -1;
            projectile.timeLeft = 1000;
            projectile.tileCollide = true;
            projectile.friendly = true;
            projectile.rotation = 0.1f;
        }
        public override void AI()
        {
            if (Main.mouseLeftRelease)
            {
                projectile.velocity.Y = projectile.velocity.Y + 0.2f; // 0.1f for arrow gravity, 0.4f for knife gravity
                if (projectile.velocity.Y > 6f) // This check implements "terminal velocity". We don't want the projectile to keep getting faster and faster. Past 16f this projectile will travel through blocks, so this check is useful.
                {
                    projectile.velocity.Y = 4f;
                    projectile.velocity.X += projectile.direction;

                }
            }
            if (Main.mouseLeft)
            {
                projectile.velocity.Y = projectile.velocity.Y + 0.2f; // 0.1f for arrow gravity, 0.4f for knife gravity
                if (projectile.velocity.Y > 0.5f) // This check implements "terminal velocity". We don't want the projectile to keep getting faster and faster. Past 16f this projectile will travel through blocks, so this check is useful.
                {
                    projectile.velocity.Y = 0f;
                    projectile.velocity.X = 0f;
                }
            }
        }

        private static float X(float t,
float x0, float x1, float x2, float x3)
        {
            return (float)(
                x0 * Math.Pow((1 - t), 3) +
                x1 * 3 * t * Math.Pow((1 - t), 2) +
                x2 * 3 * Math.Pow(t, 2) * (1 - t) +
                x3 * Math.Pow(t, 3)
            );
        }
        private static float Y(float t,
            float y0, float y1, float y2, float y3)
        {
            return (float)(
                 y0 * Math.Pow((1 - t), 3) +
                 y1 * 3 * t * Math.Pow((1 - t), 2) +
                 y2 * 3 * Math.Pow(t, 2) * (1 - t) +
                 y3 * Math.Pow(t, 3)
             );
        }


        public void DrawHead(SpriteBatch spriteBatch, string headTexture, string glowMaskTexture, NPC head, Color drawColor, Vector2 ifYouReallyWantToo)
        {
            if (projectile.active)
            {
                string neckTex = "Projectiles/Wep/Spikelaser2";
                Texture2D neckTex2D = mod.GetTexture(neckTex);
                Vector2 neckOrigin = new Vector2(projectile.Center.X, projectile.Center.Y);
                Vector2 connector = ifYouReallyWantToo;
                float chainsPerUse = 0.015f;
                for (float i = 0; i <= 1; i += chainsPerUse)
                {
                    Vector2 distBetween;
                    float projTrueRotation;
                    if (i != 0)
                    {
                        distBetween = new Vector2(X(i, neckOrigin.X, (connector.X - neckOrigin.X) * 0.33f + neckOrigin.X, (connector.X - neckOrigin.X) * 0.86f + neckOrigin.X, connector.X) -
                        X(i - chainsPerUse, neckOrigin.X, (connector.X - neckOrigin.X) / 3 + neckOrigin.X, (connector.X - neckOrigin.X) * 0.86f + neckOrigin.X, connector.X),
                        Y(i, neckOrigin.Y, (neckOrigin.Y + connector.Y) / 2 + 300, (neckOrigin.Y + connector.Y) / 2 - 300, connector.Y) -
                        Y(i - chainsPerUse, neckOrigin.Y, (neckOrigin.Y + 300), (neckOrigin.Y - 300), connector.Y));
                        projTrueRotation = distBetween.ToRotation() - (float)Math.PI / 2;
                        spriteBatch.Draw(neckTex2D, new Vector2(X(i, neckOrigin.X, (connector.X - neckOrigin.X) / 3 + neckOrigin.X, (connector.X - neckOrigin.X) * 0.86f + neckOrigin.X, connector.X) - Main.screenPosition.X, Y(i, neckOrigin.Y, (neckOrigin.Y + connector.Y) / 2 + 300, (neckOrigin.Y + connector.Y) / 2 - 300, connector.Y) - Main.screenPosition.Y),
                        new Rectangle(0, 0, neckTex2D.Width, neckTex2D.Height), new Color(255, 220, 0, 1), projTrueRotation,
                        new Vector2(neckTex2D.Width * 0.5f, neckTex2D.Height * 0.5f), 0.7f + i, SpriteEffects.None, 0f);
                    }
                }
                //  spriteBatch.Draw(neckTex2D, new Vector2(head.Center.X - Main.screenPosition.X, head.Center.Y - Main.screenPosition.Y), head.frame, drawColor, head.rotation, new Vector2(36 * 0.5f, 32 * 0.5f), 1f, SpriteEffects.None, 0f);
                //spriteBatch.Draw(mod.GetTexture(glowMaskTexture), new Vector2(head.Center.X - Main.screenPosition.X, head.Center.Y - Main.screenPosition.Y), head.frame, Color.White, head.rotation, new Vector2(36 * 0.5f, 32 * 0.5f), 1f, SpriteEffects.None, 0f);
            }

        }
    }      
}
