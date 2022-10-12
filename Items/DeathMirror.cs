using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using System;

namespace extraUtility.Items
{
    public class DeathMirror : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Grave Mirror"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Gaze in the mirror to return to your last death point");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
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
                AddRecipeGroup(exUtilSystem.AnyMirror).
                AddRecipeGroup(exUtilSystem.AnyRichTomb, 5).
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
        public static int DustIDSelector(int index, int[] dusts) => dusts[index % dusts.Length];
        public static void Use(Player player)
        {
            if (player.itemTime == player.itemTimeMax / 2 && player.lastDeathPostion != Vector2.Zero)
            {
                for (int index = 0; index < 70; ++index)
                {
                    int d = Dust.NewDust(player.position, player.width, player.height, DustIDSelector(index, new int[] { DustID.GoldCoin, DustID.Honey, DustID.Ichor, DustID.DesertWater2 }), player.velocity.X * 0.5f, player.velocity.Y * 0.5f, 150, new Color(), 1.5f);
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
                    player.Teleport(player.lastDeathPostion, 1);
                    player.velocity = Vector2.Zero;
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                        NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, player.whoAmI, player.lastDeathPostion.X, player.lastDeathPostion.Y, 1);
                }
                for (int index = 0; index < 70; ++index)
                {
                    int d = Dust.NewDust(player.position, player.width, player.height, DustIDSelector(index, new int[] { DustID.GoldCoin, DustID.Honey, DustID.Ichor, DustID.DesertWater2 }), 0.0f, 0.0f, 150, new Color(), 1.5f);
                    Main.dust[d].velocity *= 4f;
                    Main.dust[d].noGravity = true;
                }
            }
        }
    }
}
