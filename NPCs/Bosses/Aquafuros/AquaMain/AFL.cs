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
using Nocturn.NPCs.Bosses.Aquafuros.AquaMain;



namespace Nocturn.NPCs.Bosses.Aquafuros.AquaMain
{
    public class AFL : ModNPC
    {
        int AnimGo = 0;
        public float tvel = 0f;
        int _despawn = 0;
        public static int aiballcount = 0;
        float _blip = -1;
        float _dist = 0;
        float _distRate = .1f;
        Vector2 _origin;
        int ree = 0;
        public static bool active = true;
        private int center
        {
            get => (int)npc.ai[0];
            set => npc.ai[0] = value;
        }


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aquaflame");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            active = true;
            AnimGo = 0;
            ree = 0;
            _despawn = 0;
            aiballcount = 0;
            _distRate = 3;
            _dist = 90;
            _blip = -1;
            npc.width = 28;
            npc.height = 46;
            npc.damage = 0;
            npc.defense = 12;
            npc.lifeMax = 150;
            npc.value = 0f;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.buffImmune[24] = true;
            npc.dontTakeDamage = false;

        }
        public override void ScaleExpertStats(int numPlayers, float npcLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.25f * npcLifeScale);
            //npc.damage = (int)(npc.damage * 0.6f);
        }

        public override void AI()
        {
            #region NPC Position prt 1
            
            if (_blip == -1 && aiballcount == 0)
            {
                _blip = npc.ai[1] * 6.28f / 8;
                _origin = npc.position;

            }
            _blip += 3.14f / 120;
            AFL modNPC = npc.modNPC as AFL;
            if (modNPC != null)
            {
                Vector2 center = Main.npc[modNPC.center].Center;

                Vector2 _Dist;
                SetPosition(npc);
                //Rot += npc.ai[1] * 0.1f;
                _Dist = npc.position;
                center.X = _Dist.X + 90 * (float)Math.Cos(_blip) - npc.width / 2;
                center.Y = _Dist.Y + 90 * (float)Math.Sin(_blip);
                npc.position = center;
            }
            #endregion

            #region DESPAWN
           
             if (npc.life <= 1)
            {
                active = false;
            }
             if (npc.life >= 2)
            {
                active = true;
            }

            NPC aquafuros = Main.npc[center];
            if (!aquafuros.active || aquafuros.type != NPCType<Aquafuros>())
            {
                
                    npc.life = -1;
                    npc.active = false;
                    return;
                
            }

            if (!Main.player[npc.target].active || Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
                if (!Main.player[npc.target].active || Main.player[npc.target].dead)
                {
                    if (_despawn == 0)
                        _despawn++;
                }
            }
            if (_despawn >= 1)
            {
                _despawn++;
                npc.noTileCollide = true;
                if (_despawn >= 300)
                    npc.active = false;
            }
            if (_dist > 1000)
            {
                npc.active = false;
                npc.life = 0;
            }
            #endregion
        }



        #region NPC Position prt 2: electricboogaloo
        public static void SetPosition(NPC npc)
        {
            float Rotation = 0;
            AFL modNPC = npc.modNPC as AFL;
            if (modNPC != null)
            {
                Vector2 center = Main.npc[modNPC.center].Center;

                float test = -1;
                if (test == -1)
                {
                    Rotation += 0.1f;
                    test = npc.ai[1] * 6.28f / 8;
                }
                

                npc.position = center;
            }
        }
        #endregion


        #region Animation
        public override void FindFrame(int frameHight)
        {

            if (AnimGo == 0)
            {
                npc.frameCounter++;
                if (npc.frame.Y == frameHight * 4)
                {

                    npc.frame.Y = 0;
                }

                if (npc.frameCounter > 4)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += frameHight;
                    if (npc.frame.Y >= frameHight * Main.npcFrameCount[npc.type]) npc.frame.Y = 0;
                }
            }
        }
        #endregion

        #region Color glow thingy
        public override Color? GetAlpha(Color lightColor) // color of glow
        {
            return Color.Aqua;
        }
        #endregion
    }
}
