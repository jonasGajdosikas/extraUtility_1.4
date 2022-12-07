using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;

namespace ExtraUtility.Items
{
    public class DeathRecallPot : ModItem
    {
        public override void SetStaticDefaults() 
        {
            DisplayName.SetDefault($"{{$Mods.ExUtil.DeathRecallPot.Name}}");
            Tooltip.SetDefault($"{{$Mods.ExUtil.DeathRecallPot.Tooltip}}");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
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
                AddIngredient(ItemID.RecallPotion).
                AddRecipeGroup(ExUtilSystem.AnyPoorTomb).
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

        public static void Use(Player player)
        {
            if (player.itemTime == player.itemTimeMax / 2 && player.lastDeathPostion != Vector2.Zero)
            {
                for (int index = 0; index < 70; ++index)
                {
                    int d = Dust.NewDust(player.position, player.width, player.height, DustID.GemRuby, player.velocity.X * 0.5f, player.velocity.Y * 0.5f, 150, new Color(), 1.5f);
                    Main.dust[d].velocity *= 4f;
                    Main.dust[d].noGravity = true;
                }

                player.grappling[0] = -1;
                player.grapCount = 0;
                for (int index = 0; index < Main.maxProjectiles; ++index)
                {
                    if (Main.projectile[index].active && Main.projectile[index].owner == player.whoAmI && Main.projectile[index].aiStyle == 7)
                        Main.projectile[index].Kill();
                }
                if (player.whoAmI == Main.myPlayer)

                {
                    if (player.whoAmI == Main.myPlayer)
                    {
                        player.Teleport(player.lastDeathPostion, 1);
                        player.velocity = Vector2.Zero;
                        if (Main.netMode == NetmodeID.MultiplayerClient)
                            NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, player.whoAmI, player.lastDeathPostion.X, player.lastDeathPostion.Y, 1);
                    }
                }
                for (int index = 0; index < 70; ++index)
                {
                    int d = Dust.NewDust(player.position, player.width, player.height, DustID.GemRuby, 0.0f, 0.0f, 150, new Color(), 1.5f);
                    Main.dust[d].velocity *= 4f;
                    Main.dust[d].noGravity = true;
                }
            }
        }
    }
}