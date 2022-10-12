using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using extraUtility.Items;

namespace extraUtility
{
    public class ExUtilPlayer : ModPlayer
    {
        static List<int> nonPlantCuttables = new List<int>(new int[] { 28, 444, 231 });
        public bool WithersPlants;
        public bool hasPocketPylon;

        public void ReturnToDeathPoint()
        {
            Player.RemoveAllGrapplingHooks();
            Player.PotionOfReturnOriginalUsePosition = new Vector2?(Player.Bottom);
            bool flag = Player.immune;
            int num = Player.immuneTime;
            Player.StopVanityActions(false);
            Player.Teleport(Player.lastDeathPostion + Player.Size * new Vector2(-0.5f, -1f), 1, 0);
            Player.velocity = Vector2.Zero;
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, (float)Player.whoAmI, Player.lastDeathPostion.X - Player.Size.X * 0.5f, Player.lastDeathPostion.Y - Player.Size.Y, 1, 0, 0);
            }
            Player.PotionOfReturnHomePosition = new Vector2?(Player.Bottom);
            NetMessage.SendData(MessageID.PlayerControls, -1, Player.whoAmI, null, Player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
            Player.immune = flag;
            Player.immuneTime = num;
        }

        public ExUtilPlayer()
        {
        }
        public override void PreUpdate()
        {
            base.PreUpdate();
            if (WithersPlants)
            {
                if (this.WithersPlants)
                {
                    Rectangle hitbox = Player.Hitbox;
                    int x0 = (hitbox.X + hitbox.Width / 2) / 16;
                    int y0 = (hitbox.Y + hitbox.Height / 2) / 16;
                    for (int dx = -5; dx < 6; dx++)
                    {
                        for (int dy = -5; dy < 6; dy++)
                        {
                            if (dx * dx + dy * dy < 36)
                            {
                                if (Main.tile[x0 + dx, y0 + dy] != null &&
                                    Main.tileCut[Main.tile[x0 + dx, y0 + dy].TileType] &&
                                    !nonPlantCuttables.Contains(Main.tile[x0 + dx, y0 + dy].TileType) &&
                                    WorldGen.CanCutTile(x0 + dx, y0 + dy, Terraria.Enums.TileCuttingContext.AttackMelee))
                                {
                                    WorldGen.KillTile(x0 + dx, y0 + dy);
                                    if (Main.netMode == NetmodeID.MultiplayerClient) NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, x0 + dx, y0 + dy);
                                }
                            }
                        }
                    }
                }
            }
            /**
                Item item = Player.inventory[Player.selectedItem];
                if (item.type == ModContent.ItemType<DeathMirror>() && Player.itemAnimation > 0)
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        Dust.NewDust(Player.position, Player.width, Player.height, DustID.LifeDrain, 0f, 0f, 150, new Color(), 1.1f);
                    }
                    if (Player.itemTime == 0) Player.itemTime = 90;
                    else if (Player.itemTime == 45)
                    {
                        for (int index = 0; index < 70; ++index)
                        {
                            int d = Dust.NewDust(Player.position, Player.width, Player.height, DustID.LifeDrain, Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 150, new Color(), 1.5f);
                            Main.dust[d].velocity *= 4f;
                            Main.dust[d].noGravity = true;
                        }
                        Player.grappling[0] = -1;
                        Player.grapCount = 0;
                        for (int index = 0; index < 1000; ++index)
                        {
                            if (Main.projectile[index].active && Main.projectile[index].owner == Player.whoAmI && Main.projectile[index].aiStyle == 7)
                                Main.projectile[index].Kill();
                        }

                        if (Player.whoAmI == Main.myPlayer)
                        {
                            Player.Teleport(Player.lastDeathPostion, 1);
                            Player.velocity = Vector2.Zero;
                            if (Main.netMode == NetmodeID.MultiplayerClient)
                                NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, Player.whoAmI, Player.lastDeathPostion.X, Player.lastDeathPostion.Y - 16, 1);
                        }

                        for (int index = 0; index < 70; ++index)
                        {
                            int d = Dust.NewDust(Player.position, Player.width, Player.height, DustID.LifeDrain, 0.0f, 0.0f, 150, new Color(), 1.5f);
                            Main.dust[d].velocity *= 4f;
                            Main.dust[d].noGravity = true;
                        }
                    }
                }
                if (item.type == ModContent.ItemType<DeathRecallPot>() && Player.itemAnimation > 0)
                {
                    if (Player.itemTime == 0)
                    {
                        Player.itemTime = 15;
                    }
                    else if (Player.itemTime == 2)
                    {
                        for (int index = 0; index < 70; ++index)
                        {
                            int d = Dust.NewDust(Player.position, Player.width, Player.height, DustID.GemAmethyst, Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 150, new Color(), 1.5f);
                            Main.dust[d].velocity *= 4f;
                            Main.dust[d].noGravity = true;
                        }

                        Player.grappling[0] = -1;
                        Player.grapCount = 0;
                        for (int index = 0; index < 1000; ++index)
                        {
                            if (Main.projectile[index].active && Main.projectile[index].owner == Player.whoAmI && Main.projectile[index].aiStyle == 7)
                                Main.projectile[index].Kill();
                        }

                        if (Player.whoAmI == Main.myPlayer)
                        {
                            Player.Teleport(Player.lastDeathPostion, 1);
                            Player.velocity = Vector2.Zero;
                            if (Main.netMode == NetmodeID.MultiplayerClient)
                                NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, Player.whoAmI, Player.lastDeathPostion.X, Player.lastDeathPostion.Y - 16, 1);
                        }

                        for (int index = 0; index < 70; ++index)
                        {
                            int d = Dust.NewDust(Player.position, Player.width, Player.height, DustID.GemAmethyst, 0.0f, 0.0f, 150, new Color(), 1.5f);
                            Main.dust[d].velocity *= 4f;
                            Main.dust[d].noGravity = true;
                        }
                        if (ItemLoader.ConsumeItem(item, Player) && item.stack > 0)
                        {
                            item.stack--;
                        }
                    }
                }/**/
        }
        
        public override void ResetEffects()
        {
            WithersPlants = false;
            PocketPylon = false;
        }
        
    }
}
