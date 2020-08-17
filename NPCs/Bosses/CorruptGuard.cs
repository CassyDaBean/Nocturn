using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nocturn.Projectiles;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Terraria.Graphics.Shaders;

namespace Nocturn.NPCs.Bosses
{
    public class CorruptGuard : ModNPC
    {
        int center
        {
            get => (int)npc.ai[1];
            set => npc.ai[1] = value;
        }

        public float Velo = 0f; //Velocity of the NPC
        public float Velo2 = 0f;
        public bool sHOOT = false; //Shoot the Spear
        public bool turnthefuckaround = false; //For testing movements
        public int Timercool = 0;
        public byte Shootcount = 0;
        public bool Teleport = false;
        public byte TeleportCool = 0;
        public byte Telepos = 0;
        public int Colorev = 0;


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Corrupt Guard");

            NPCID.Sets.TrailCacheLength[npc.type] = 20;
            NPCID.Sets.TrailingMode[npc.type] = 0;

        }
        public override void SetDefaults()
        {
            sHOOT = false;
            Velo = 0f;
            turnthefuckaround = false;
            Velo2 = 0f;
            Timercool = 200;
            Shootcount = 0;
            Teleport = false;
            TeleportCool = 0;
            Colorev = npc.alpha + 255;

            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.lifeMax = 2000;
            npc.knockBackResist = 0f;
            npc.defense = 23;
            npc.rotation = 0f;
            npc.damage = 34;
            npc.aiStyle = -1;
            npc.alpha = 0;
            npc.width = 70;
            npc.height = 70;
            
        }

        


        #region Predraw
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D Base = Main.npcTexture[npc.type];
            Texture2D Glow = mod.GetTexture("Misc/CorruptGuardGlow");
            Texture2D Glow2 = mod.GetTexture("Misc/CorruptGuardGlow2");
            lightColor = new Color(255, 255, 255, Colorev);
            
            //spriteBatch.Draw(Glow2, npc.Center - Main.screenPosition, npc.frame, color2, npc.rotation, new Vector2(npc.width * 0.5f, npc.height * 0.5f), 1f, SpriteEffects.None, 0f);
            //spriteBatch.Draw(Glow, npc.Center - Main.screenPosition, npc.frame, color, npc.rotation, new Vector2(npc.width * 0.5f, npc.height * 0.5f), 1f, SpriteEffects.None, 0f);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
            Vector2 drawOrigin = new Vector2(npc.width* .5f, npc.height * .5f);

            for (int k = 0; k < npc.oldPos.Length; k++)
            {
                Vector2 drawPos = npc.oldPos[k]- Main.screenPosition + drawOrigin + new Vector2(0f, npc.gfxOffY);
                Color color2 = new Color(Colorev, Colorev, Colorev, Colorev) * ((float)(npc.oldPos.Length - k) / (float)npc.oldPos.Length);
                Color color = new Color(npc.alpha , npc.alpha, npc.alpha, npc.alpha) * ((float)(npc.oldPos.Length - k) / (float)npc.oldPos.Length);
                
                spriteBatch.Draw(Glow2, drawPos, npc.frame, color2, npc.rotation, drawOrigin, 1f, SpriteEffects.None, 0f);
                spriteBatch.Draw(Glow, npc.position - Main.screenPosition + drawOrigin, npc.frame, color, npc.rotation, drawOrigin, 0.005f +2f, SpriteEffects.None, 0f);
                
            }
            //spriteBatch.Draw(Glow2, npc.Center - Main.screenPosition, npc.frame, color2, npc.rotation, new Vector2(npc.width * 0.5f, npc.height * 0.5f), 1f, SpriteEffects.None, 0f);
            //spriteBatch.Draw(Glow, npc.Center - Main.screenPosition, npc.frame, color, npc.rotation, new Vector2(npc.width * 0.5f, npc.height * 0.5f), 1f, SpriteEffects.None, 0f);
            
            spriteBatch.End();
            spriteBatch.Begin();
            spriteBatch.Draw(Base, npc.position - Main.screenPosition + drawOrigin, npc.frame, lightColor, npc.rotation, drawOrigin, 1f, SpriteEffects.None, 0f);
            return false;

        }

        #endregion



        public override void AI()
        {
            Player player = Main.player[npc.target];
            Vector2 delta = Main.player[npc.target].Center - npc.Center;
            npc.TargetClosest(true);
            
            npc.velocity.Y = (player.Center.Y - npc.Center.Y - 250)*Velo;
            npc.velocity.X = (player.Center.X - npc.Center.X) * Velo2;
            npc.rotation = npc.velocity.X * 0.5f;
            Velo = 0.045f;
            Lighting.AddLight(npc.Center, 2, 1, 1);

            #region Basic movement
            if (Velo2 >= 6f && !turnthefuckaround)
            {
                turnthefuckaround = true;
            }
            if (Velo2 <= -6f && turnthefuckaround)
            {
                turnthefuckaround = false;
            }
            if (!turnthefuckaround)
            {
                Velo2 = 0.05f;
            }
            if (turnthefuckaround)
            {
                Velo2 = 0.02f;
            }
            #endregion

            #region Timer for things ig
            Timercool -= 1;
            if (Timercool <= 0)
            {
                Timercool = 150;
                //Shootcount += 1;
                TeleportCool += 1;
                Teleport = false;
                Telepos = (byte)Main.rand.Next(0, 4);
            }
            if (Shootcount == 0)
                sHOOT = false;
            if (Shootcount == 1)
                sHOOT = true;
            if (Shootcount >= 2)
                Shootcount = 0;
            if (TeleportCool == 0)
            {
                Teleport = false;
                npc.alpha -= 10;
            }
                
            if (TeleportCool == 1)
            {
                Teleport = true;
                npc.alpha += 10;
                Colorev -= 10;
            }
            if (TeleportCool == 2)                
            {
                npc.alpha -= 5;
                Teleport = true;
                Colorev += 5;
            }
            if (TeleportCool == 3)
            {
                Teleport = false;
                TeleportCool = 0;
            }

            #region Lighting (oh boy here we go)
            //preventing the alpha from going over
            if (npc.alpha > 255)
            {
                npc.alpha = 255;
                Colorev = 0;
            }
            if (npc.alpha < 0)
            {
                npc.alpha = 0;
                Colorev = 255;
            }


            #endregion

            #region Particles
            if (Timercool < 50 && !Teleport)
            {
                if (Main.rand.NextFloat() < 0.2894737f)
                {
                    Dust dust;
                    // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                    Vector2 position = new Vector2(npc.Center.X - 45, npc.Center.Y -45);
                    dust = Terraria.Dust.NewDustDirect(position, 100, 105, 226, 0f, 0f, 197, new Color(0, 192, 255), 1f);
                    dust.noGravity = true;
                    dust.noLight = true;
                    dust.fadeIn = 1.065789f;
                }

            }


            #endregion

            #endregion

            #region Shoot code
            if (sHOOT)
            {
                for (int i = 0; i < 1; i++)
                {
                    int proj = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, delta.X*0.05f, delta.Y*0.05f, ProjectileType<Tip>(), 30, 3f, Main.myPlayer);

                    Tip arm = Main.projectile[proj].modProjectile as Tip;
                    arm.CorG= npc.whoAmI;
                }
                sHOOT = false;
                Shootcount = 0;
            }
            #endregion

            #region Teleport code
            if (Teleport)
            {
                //npc.alpha = 0;
                if (Telepos == 0 && npc.alpha >= 200)
                {
                    npc.velocity = new Vector2(0f, 0f);
                    Shootcount = 1;
                    TeleportCool = 2;
                }
                if (Telepos == 1 && npc.alpha >= 200)
                {
                    npc.position.X = player.Center.X - 500;
                    npc.position.Y = player.Center.Y - 45;
                    npc.velocity = new Vector2 (0f, 0f);
                    Shootcount = 1;
                    TeleportCool = 2;
                }
                if (Telepos == 2 && npc.alpha >= 200)
                {
                    npc.position.X = player.Center.X + 500;
                    npc.position.Y = player.Center.Y - 45;
                    npc.velocity = new Vector2(0f, 0f);
                    Shootcount = 1;
                    TeleportCool = 2;
                }
                if (Telepos == 3 && npc.alpha >= 200)
                {
                    #region Shoot spears
                    for (int i = 0; i < 4; i++)
                    {
                        int proj = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, delta.X* 0.04f, delta.Y * 0.04f, ProjectileType<LaserArrow>(), 30, 3f, Main.myPlayer);                        
                    }


                    #endregion
                    npc.position.X = player.Center.X - 400;
                        npc.position.Y = player.Center.Y - 250;
                        npc.velocity = new Vector2(0f, 0f);
                    TeleportCool = 2;
                                                               
                }

                Teleport = false;
                
                
            }
            #endregion
        }
    }
}
