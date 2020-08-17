using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
using Nocturn.Items.Guns;
using Nocturn.NPCs;

namespace Nocturn.UI
{

    internal class BaseUI : UIState
    {
        public DragableUIPanel BG;
        public SmithBG SBG;
       // public static bool Visible;
        public override void OnInitialize()
        {
            // Here we define our container UIElement. In DragableUIPanel.cs, you can see that DragableUIPanel is a UIPanel with a couple added features

            BG = new DragableUIPanel();
            BG.SetPadding(0);
            BG.Left.Set(10f, 0f);
            BG.Top.Set(400f, 0f);
            BG.Width.Set(490f, 0f);
            BG.Height.Set(302f, 0f);

            SBG = new SmithBG(GetTexture("Nocturn/UI/SmithUI"));
            SBG.Left.Set(0, 0f);
            SBG.Top.Set(0, 0f);
            SBG.Width.Set(490, 0f);
            SBG.Height.Set(302, 0f);

            BG.Append(SBG);
            Append(BG);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            // This prevents drawing unless we are using an ExampleDamageItem
            if (Main.LocalPlayer.talkNPC == -1 || Main.npc[Main.LocalPlayer.talkNPC].type != NPCType<SkeletonSmith>())
            {
                GetInstance<Nocturn>().Smith.SetState(null);
                var modPlayer = Main.LocalPlayer.GetModPlayer<NocturnPlayer>();
                return;
            }
                base.Draw(spriteBatch);
            
            
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            Main.HidePlayerCraftingMenu = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (Main.LocalPlayer.talkNPC == -1 || Main.npc[Main.LocalPlayer.talkNPC].type != NPCType<SkeletonSmith>())
            {
                GetInstance<Nocturn>().Smith.SetState(null);
                var modPlayer = Main.LocalPlayer.GetModPlayer<NocturnPlayer>();
                base.Update(gameTime);
            }
        }
    }
}
