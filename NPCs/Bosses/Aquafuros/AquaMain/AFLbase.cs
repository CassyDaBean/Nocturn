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

namespace Nocturn.NPCs.Bosses.Aquafuros.AquaMain
{
    public class AFLbase : ModNPC
    {


        int _despawn = 0;
        public static int aiballcount = 0;
        float _blip = -1;
        float _dist = 0;
        float _distRate = .1f;
        Vector2 _origin;

        public override void SetStaticDefaults()
        {
          //  DisplayName.SetDefault("Aquaflame");
         //   Main.npcFrameCount[npc.type] = 6;
        }
        public override void SetDefaults()
        {
            _despawn = 0;
            aiballcount = 0;
            _distRate = 3;
            _dist = 50;
            _blip = -1;
            npc.width = 2;
            npc.height = 2;
            npc.damage = 0;
            npc.defense = 0;
            npc.lifeMax = 1;
            npc.value = 0f;
            npc.knockBackResist = 0f;
            npc.aiStyle = 0;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.buffImmune[24] = true;
            npc.dontTakeDamage = true;

        }


        public override void AI()
        {
            //modified Aquaball code for testing 
            if (_blip == -1 && aiballcount == 0)
            {
               
                _blip = npc.ai[1] * 6.28f / 8;
                _origin = npc.position;
            }
            _blip += 3.14f / 120;
            _dist += _distRate;
            _distRate += .5f;
            float divisions = 6.28f / 8;
            Vector2 targetPos;
            targetPos.X = _origin.X;
            targetPos.Y = _origin.Y;
            npc.position = targetPos;
            if (_dist > 400)
            {
                //aiballcount = 1;
                _distRate -= 1;
            }
               if(aiballcount == 0)
            {

                for (int k = 0; k < 10; k++)
                {


                    int f = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AFL"));
                    Main.npc[f].ai[0] = npc.whoAmI;
                    Main.npc[f].ai[1] = k;
                }

                float speed = 10; // pixels per second
                npc.velocity = (Main.player[npc.target].Center - npc.Center).SafeNormalize(Vector2.Zero) * speed;
                
                
                Player player = Main.player[npc.target];
            }
           



            //Should despawn after the player dies (hopefully)
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

           


        


        }                              
    }
}
