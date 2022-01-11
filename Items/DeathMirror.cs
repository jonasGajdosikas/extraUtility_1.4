using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace extraUtility.Items
{
	public class DeathMirror : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Grave Mirror"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Gaze in the mirror to return to your last death point");
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.MagicMirror);
            Item.useAnimation = 90;
            Item.useTime = 90;
			Item.maxStack = 1;
			Item.consumable = false;
			return;
		}

		public override void AddRecipes()
		{
			CreateRecipe().
				AddRecipeGroup("ExUtil:AnyMirror").
				AddRecipeGroup("ExUtil:AnyRichTomb", 5).
				AddTile(TileID.DemonAltar).
				Register();
			/**
			ModRecipe recipe = new ModRecipe(Mod);
			recipe.AddRecipeGroup("ExUtil:AnyMirror");
			recipe.AddRecipeGroup("ExUtil:AnyRichTomb", 5);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();/**/
		}
        public override bool CanUseItem(Player player)
        {
            return player.lastDeathPostion != Vector2.Zero;
        }

        public override bool? UseItem(Player player)
        {
			player.UnityTeleport(player.lastDeathPostion);
            return true;
        }
    }
}
