using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Nocturn.NPCs.Bosses.CGC
{
    public class CConsol : ModNPC
    {
        #region Int in Tin
        int ChangeCool = 0;

        #endregion

        #region F's in chat bois (Floats)
        float IconSize = 1f;
        #endregion

        #region Better take a Byte outa this
        byte IconNumber = 0;
        byte SpawnCGC = 0;
        #endregion

        #region Leaf-Booler
        bool IconPulse = false;
        bool TurnintoABossnow = false;
        #endregion
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cyber Console");

        }
        public override void SetDefaults()
        {
            IconNumber = 0;
            IconPulse = false;
            IconSize = 1f;
            SpawnCGC = 0;
            ChangeCool = 0;
            TurnintoABossnow = false;


            npc.width = 126;
            npc.height = 74;
            npc.dontTakeDamage = false;
            npc.defense = 14;
            npc.damage = 0;
            npc.chaseable = false;
            npc.boss = false;
            npc.behindTiles = false;
            npc.aiStyle = -1;
            npc.lifeMax = 3500;
            npc.knockBackResist = 0f;

        }

        #region Predraw
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D Base = Main.npcTexture[npc.type];
            Texture2D Icon1 = mod.GetTexture("NPCs/Bosses/CGC/CIcon1");
            Texture2D Icon2 = mod.GetTexture("NPCs/Bosses/CGC/CIcon2");
            Texture2D Icon3 = mod.GetTexture("NPCs/Bosses/CGC/CIcon3");
            Texture2D Light = mod.GetTexture("Misc/CConsolLight");
            Texture2D GlowMask = mod.GetTexture("Misc/CGlow");
            Color LightColor = new Color(255 /2, 205/2, 136/2, 0);
            Vector2 drawOrigin = new Vector2(npc.width * .5f, npc.height * .5f);
            Vector2 IconPos = new Vector2(npc.Center.X, npc.Center.Y - 75);
            Rectangle Icon1Rec = new Rectangle(0, 0, 48, 36);
            Vector2 Icon1Origin = new Vector2(Icon1Rec.Width * .5f, Icon1Rec.Height * .5f);
            Rectangle Icon2Rec = new Rectangle(0, 0, 54, 50);
            Vector2 Icon2Origin = new Vector2(Icon2Rec.Width * .5f, Icon2Rec.Height * .5f);
            Rectangle LightRec = new Rectangle(0, 0, 94, 98);
            Vector2 LightOrigin = new Vector2(LightRec.Width * .5f, LightRec.Height * .5f);
            Vector2 LightPos = new Vector2(npc.Center.X, npc.Center.Y - 58);

            spriteBatch.Draw(Base, npc.position - Main.screenPosition + drawOrigin, npc.frame, drawColor, 0f, drawOrigin, 1f, SpriteEffects.None, 0f);

            if (IconNumber == 0)
            {
                spriteBatch.Draw(Icon1, IconPos - Main.screenPosition, Icon1Rec, Color.White, 0f, Icon1Origin, IconSize, SpriteEffects.None, 0f);
                spriteBatch.Draw(Light, LightPos - Main.screenPosition, LightRec, LightColor, 0f, LightOrigin, 1f, SpriteEffects.None, 0f);
            }           
                


            if (IconNumber == 1)
            {
                spriteBatch.Draw(Icon2, IconPos - Main.screenPosition, Icon2Rec, Color.White, 0f, Icon2Origin, IconSize, SpriteEffects.None, 0f);
                spriteBatch.Draw(Light, LightPos - Main.screenPosition, LightRec, LightColor, 0f, LightOrigin, 1f, SpriteEffects.None, 0f);
            }
            spriteBatch.Draw(GlowMask, npc.position - Main.screenPosition + drawOrigin, npc.frame, Color.White, 0f, drawOrigin, 1f, SpriteEffects.None, 0f);

            return false;
        }
        #endregion

        #region AI
        public override void AI()
        {
            #region Icon pulse
            if (IconSize < 1.1f && !IconPulse)
            {
                IconSize += 0.005f;
            }
            if (IconSize >= 1.1f && !IconPulse)
            {
                IconPulse = true;
            }
            if (IconPulse)
                IconSize -= 0.005f;
            if (IconSize <= 0.8f)
            {
                IconPulse = false;
            }
            #endregion

            Lighting.AddLight(npc.Center, 2, 1, 1);

            if (npc.life < npc.lifeMax)
            {
                TurnintoABossnow = true;

            }
            if (TurnintoABossnow)
            {
                npc.boss = true;
                IconNumber = 1;
            }
        }
        #endregion
    }
}
