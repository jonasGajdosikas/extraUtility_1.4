using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace extraUtility.Items
{
	public class DeathRecallPot : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Grave Recall Potion"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Drink the potion to return to your last death point");
		}

		public override void SetDefaults() 
		{
			Item.CloneDefaults(ItemID.RecallPotion);
			Item.useTime = 30;
			Item.maxStack = 30;
			Item.consumable = true;
			return;
		}
		
		public override void AddRecipes() 
		{
			CreateRecipe().
				AddIngredient(ItemID.BottledWater).
				AddIngredient(ItemID.SpecularFish).
				AddRecipeGroup("ExUtil:AnyPoorTomb", 5).
				AddTile(TileID.DemonAltar).
				Register();
			/**
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.BottledWater);
			recipe.AddIngredient(ItemID.SpecularFish);
			recipe.AddRecipeGroup("ExUtil:AnyPoorTomb");
			recipe.AddTile(TileID.Bottles);
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