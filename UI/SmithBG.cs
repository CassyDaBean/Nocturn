using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;
using Terraria.ModLoader;



namespace Nocturn.UI
{
    // This DragableUIPanel class inherits from UIPanel. 
    // Inheriting is a great tool for UI design. By inheriting, we get the background drawing for free from UIPanel
    // We've added some code to allow the panel to be dragged around. 
    // We've also added some code to ensure that the panel will bounce back into bounds if it is dragged outside or the screen resizes.
    // UIPanel does not prevent the player from using items when the mouse is clicked, so we've added that as well.
    internal class SmithBG : UIImage
    {

        public SmithBG(Texture2D texture) : base(texture)
        {
            //texture = ModContent.GetTexture("Nocturn/UI/SmithUI");
            return;
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

        }
    }
}
