using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Nocturn;
using Nocturn.Items.Boss.Aqua;
using Nocturn.Items;
using Nocturn.Items.Placeables.Trophies;
using Nocturn.Items.Armor;
using Terraria.GameContent;

namespace Nocturn.Support
{
    internal class WeakReferences
    {
        public static void GoModSupport()
        {
            HPBarSupport();
            BCL();
        }
        private static void HPBarSupport()
        {
            Mod yabhb = ModLoader.GetMod("FKBossHealthBar");
            if (yabhb != null)
            {
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                Nocturn.instance.GetTexture("HPBars/AquaHPStart"),
                Nocturn.instance.GetTexture("HPBars/AquaHPMid"),
                Nocturn.instance.GetTexture("HPBars/AquaHPEnd"),
                Nocturn.instance.GetTexture("HPBars/AquaHPFill"));
                yabhb.Call("hbSetColours",
                    Color.Aqua,
                    Color.Aqua,
                    Color.Aqua);
                yabhb.Call("hbSetMidBarOffset", -30, 0);
                yabhb.Call("hbSetBossHeadCentre", 21, 21);
                yabhb.Call("hbSetFillDecoOffsetSmall", 10);
                yabhb.Call("hbFinishSingle", Nocturn.instance.NPCType("Aquafuros"));
            }
        }
        private static void BCL()
        {
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            Nocturn mod = Nocturn.instance;
            if (bossChecklist != null)
            {
                #region Aquafuros
                bossChecklist.Call("AddBoss", 3.5f, mod.NPCType("Aquafuros"), mod,
                    Lang.BossCheck("Aquafuros1"),
                    (Func<bool>)(() => NocturnWorld.YeetedAquafuros),
                    ModContent.ItemType<Aquachal>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Placeables.Trophies.AquaT1>(),
                        ModContent.ItemType<AquafurosMask>()

                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Aquabag>(),
                        ModContent.ItemType<Items.Bows.AquaSpitter>(),
                        ModContent.ItemType<Items.Swords.Aquamace>()
                    },
                    Lang.BossCheck("UseA") + "[i: " + ModContent.ItemType<Aquachal>() + "]",
                    Lang.BossCheck("AquaInfo1"),
                    "Nocturn/Support/BCL/AquaCheck",
                    "Nocturn/NPCs/Bosses/Aquafuros/AquaMain/Aquafuros_Head_Boss");
                #endregion

            }
        }
    }
}