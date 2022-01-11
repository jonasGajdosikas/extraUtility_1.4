using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using extraUtility.Items;

namespace extraUtility
{
    public class ExUtilPlayer : ModPlayer
    {

        public ExUtilPlayer()
        {
        }
        /**
        public override void PreUpdate()
        {
            base.PreUpdate();

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
            }
        }/**/
    }
}
