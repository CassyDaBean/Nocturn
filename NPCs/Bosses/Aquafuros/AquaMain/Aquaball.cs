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
    public class Aquaball : ModNPC
    {


        int _despawn = 0;
        public static int aiballcount = 0;
        float _blip = -1;
        float _dist = 0;
        float _distRate = .1f;
        Vector2 _origin;
       
        public override void SetStaticDefaults()
        {
                      DisplayName.SetDefault("Aqua Ball");
                      Main.npcFrameCount[npc.type] = 1;
        }
        public override void SetDefaults()
        {
            _despawn = 0;
            aiballcount = 0;
            _distRate = 3;
            _dist = 50;
            _blip = -1;
            npc.width = 14;
            npc.height = 14;
            npc.damage = 10;
            npc.defense = 12;
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
            //Emerald Ball movement 1 (and hopefully it will work) 
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
            targetPos.X = _origin.X + _dist * (float)Math.Cos(_blip) - npc.width / 2;
            targetPos.Y = _origin.Y + _dist * (float)Math.Sin(_blip);
            npc.position = targetPos;
            if(_dist > 400)
            {

                npc.active = false;
                _distRate += 1;
                aiballcount += 1;
            }
            npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) + 1.57f / 2;
            


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
            if (_dist > 100)
            {
                npc.active = false;
                npc.life = 0;
            }

           


        


        }                              
    }
}
