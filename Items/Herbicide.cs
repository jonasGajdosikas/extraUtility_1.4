using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;

namespace extraUtility.Items
{
    class Herbicide : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Herbicide Potion"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Withers nearby plants\n" +
                "For better Korruptd traversal\n" +
                "Does not heal corona");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 32;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = 30;
            Item.consumable = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(silver: 10);
            Item.buffTime = 5 * 60 * 60;
            Item.buffType = ModContent.BuffType<StatusEffects.Withering>();
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BottledWater)
                .AddIngredient(ItemID.Deathweed)
                .AddRecipeGroup(exUtilSystem.AnyCopperOre)
                .AddTile(TileID.Bottles)
                .Register();
        }
    }
}
