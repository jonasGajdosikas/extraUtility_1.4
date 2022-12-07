using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ExtraUtility.ModProjectile
{
    public class ExUtilGlobalProjectile : GlobalProjectile
    {
        public static List<int> bombs = new List<int>(){ ProjectileID.Bomb, ProjectileID.BombFish, ProjectileID.StickyBomb, ProjectileID.DirtStickyBomb, ProjectileID.ScarabBomb};
        public static List<int> dynamites = new List<int>(){ProjectileID.StickyDynamite };
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            base.OnSpawn(projectile, source);
            if (projectile.owner == Main.myPlayer && bombs.Contains(projectile.type))
            {
                Main.NewText("cut fuse");
                //projectile.timeLeft /= 3;
            }
        }
        public override bool TileCollideStyle(Projectile projectile, ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            if (bombs.Contains(projectile.type))
            {
                Main.NewText("cut fuse");
                //SetMin(ref projectile.timeLeft, 3);
            }
            return base.TileCollideStyle(projectile, ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }
        public override bool OnTileCollide(Projectile projectile, Vector2 oldVelocity)
        {
            //Main.NewText("hit ground");
            if (bombs.Contains(projectile.type))
            {
                Main.NewText("cut fuse");
                //SetMin(ref projectile.timeLeft, 3);
            }
            return base.OnTileCollide(projectile, oldVelocity);
        }
        public static void SetMin<T>(ref T obj, T val) where T : IComparable<T>
        {
            if (obj.CompareTo(val) < 0) obj = val;
        }
    }
}

