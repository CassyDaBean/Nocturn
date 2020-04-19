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

        public float tvel = 0f;
        int _despawn = 0;
        public static int aiballcount = 0;
        float _blip = -1;
        float _dist = 0;
        float _distRate = .1f;
        Vector2 _origin;
        Vector2 _Origin2;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aquaflame");
            Main.npcFrameCount[npc.type] = 6;
        }
        public override void SetDefaults()
        {
            _despawn = 0;
            aiballcount = 0;
            _distRate = 3;
            _dist = 50;
            _blip = -1;
            npc.width = 26;
            npc.height = 204;
            npc.damage = 30;
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
            targetPos.X = _origin.X + _dist * (float)Math.Cos(_blip) - npc.width / 2;
            targetPos.Y = _origin.Y + _dist * (float)Math.Sin(_blip);
            npc.position = targetPos;
            if (_dist > 400)
            {
                aiballcount = 1;
                _distRate -= 1;
            }
            if (aiballcount == 1)
            {
                Player player = Main.player[npc.target];
                _Origin2 = player.Center;
            }
                float dist = Vector2.Distance(npc.Center, Main.player[npc.target].Center);
                tvel = dist / 70;
                Vector2 Targetpos;
                Targetpos.X = _Origin2.X + tvel;
                Targetpos.Y = _Origin2.Y + tvel;
               
                npc.position = Targetpos;
                float speed = 20; // pixels per second
                npc.velocity = (Main.player[npc.target].Center - npc.Center).SafeNormalize(Vector2.Zero) * speed;

               // Player player = Main.player[npc.target];


               



            
           



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
