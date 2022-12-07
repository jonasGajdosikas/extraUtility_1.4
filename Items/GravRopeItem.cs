using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace ExtraUtility.Items
{
    [Autoload(false)]
    public class GravRopeItem : ModItem
    {
        readonly int RopeItemID;
        readonly int RopeTileID;
        readonly string name;
        readonly string displayName;
        readonly string textureName;
        protected override bool CloneNewInstances => true;
        public override string Texture => textureName;
        public override string Name => name;
        public GravRopeItem(AntigravRope newRope)
        {
            RopeItemID = newRope.RopeItemID;
            RopeTileID = newRope.RopeTileID;
            name = newRope.itemName;
            displayName = newRope.displayName;
            textureName = newRope.textureName;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(RopeItemID);
            Item.createTile = RopeTileID;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(displayName);
            Tooltip.SetDefault($"{{$ItemTooltip.{ItemID.Search.GetName(RopeItemID)}}}\n"
            + $"{{$Mods.ExUtil.GravRope.AntigravityTooltip}}");
            SacrificeTotal = 100;
        }
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            //spriteBatch.Draw(Mod.Assets.Request<Texture2D>("ItemImages/SkullOverlay").Value, position, null, Color.White, 0f, origin, scale, SpriteEffects.None, 0);
            spriteBatch.Draw(TextureAssets.Item[RopeItemID].Value, position, null, drawColor, 0f, origin, scale, SpriteEffects.FlipVertically, 0);
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe(99).
                AddIngredient(RopeItemID, 99).
                AddIngredient(ItemID.GravitationPotion).
                Register();
        }
    }
    public class AntigravRope
    {
        public int RopeItemID;
        public int RopeTileID;
        public string itemName;
        public string displayName;
        public string textureName;
        public AntigravRope(int ropeItemID, int ropeTileID, string itemName)
        {
            RopeItemID = ropeItemID;
            RopeTileID = ropeTileID;
            this.itemName = itemName;
            displayName = $"{{$Mods.ExUtil.GravRope.Antigravity}} {{$ItemName.{ItemID.Search.GetName(ropeItemID)}}}";
            textureName = $"Terraria/Images/Item_{ropeItemID}";
        }
    }
}
