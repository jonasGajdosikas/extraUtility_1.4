using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace extraUtility.StatusEffects
{
    class Withering : ModBuff
    {
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Withering");
            Description.SetDefault("All plants around you wither");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
			player.GetModPlayer<ExUtilPlayer>().WithersPlants = true;
		}
		
	}
	class WitherFXplayer : ModPlayer
    {
		public float witheRingScale;
		public float witheRingRot;
	}
	class WitherFXDrawLayer : PlayerDrawLayer
    {
		const float scaleDecrease = 1f;
		private Asset<Texture2D> witheRingTexture;
		public override Position GetDefaultPosition() => new Between(PlayerDrawLayers.ElectrifiedDebuffFront, PlayerDrawLayers.IceBarrier);
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
			return drawInfo.drawPlayer.GetModPlayer<ExUtilPlayer>().WithersPlants;
        }
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            if (witheRingTexture == null)
            {
				witheRingTexture = ModContent.Request<Texture2D>("extraUtility/StatusEffects/Withering_Spritesheet");
			}
			WitherFXplayer modPlayer = drawInfo.drawPlayer.GetModPlayer<WitherFXplayer>();
			float ringScale = 1f * scaleDecrease;
			float deltaScale = 0.1f * scaleDecrease;
			float avgScale = 0.9f * scaleDecrease;
			if (!Main.gamePaused && Main.instance.IsActive)
			{
				modPlayer.witheRingScale += 0.004f * scaleDecrease;
			}
			if (modPlayer.witheRingScale < 1f * scaleDecrease)
			{
				ringScale = modPlayer.witheRingScale;
			}
			else
			{
				modPlayer.witheRingScale = 0.8f * scaleDecrease;
				ringScale = modPlayer.witheRingScale;
			}
			if (!Main.gamePaused && Main.instance.IsActive)
			{
				modPlayer.witheRingRot += 0.05f;
			}
			if (modPlayer.witheRingRot > (float)Math.PI * 2f)
			{
				modPlayer.witheRingRot -= (float)Math.PI * 2f;
			}
			if (modPlayer.witheRingRot < (float)Math.PI * -2f)
			{
				modPlayer.witheRingRot += (float)Math.PI * 2f;
			}
			for (int j = 0; j < 3; j++)
			{
				float scale = ringScale + deltaScale * (float)j;
				if (scale > 1f * scaleDecrease)
				{
					scale -= deltaScale * 2f;
				}
				float grayScale = MathHelper.Lerp(0.8f, 0f, Math.Abs(scale - avgScale) * 10f);
				drawInfo.DrawDataCache.Add(new DrawData(
					witheRingTexture.Value,											//texture
					modPlayer.Player.Center - Main.screenPosition,					//position
					new Rectangle(0, 236 * j, 236, 236),							//source rectangle
					new Color(grayScale, grayScale, grayScale, grayScale / 2f),		//color
					modPlayer.witheRingRot + (float)Math.PI / 3f * (float)j,		//rotation
					new Vector2(236f, 236f) * 0.5f,									//origin from textures center
					scale,															//scale
					SpriteEffects.None,												//effects
					0																//layer
				));
			}
		}
    }
}
