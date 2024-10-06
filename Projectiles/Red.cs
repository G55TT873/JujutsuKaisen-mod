using System.Data;
using Microsoft.Xna.Framework;
using ReLogic.Reflection;
using JujutsuKaisen.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JujutsuKaisen.Projectiles
{
    public class Red : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 27;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            Projectile.ai[0]++;
            if (Projectile.ai[0] < 60f)
            {
                Projectile.velocity *= 1.01f;
            }
            else
            {
                Projectile.velocity *= 1.05f;
                if (Projectile.ai[0] >= 180)
                {
                    Projectile.Kill();
                }
            }

            float rotateSpeed = 0.1f * (float)Projectile.direction;
            Projectile.rotation += rotateSpeed;

            Lighting.AddLight(Projectile.Center, 0.75f, 0.75f, 0.75f);

            if (Main.rand.NextBool(2))
            {
                int numToSpawn = Main.rand.Next(3);
                for (int i = 0; i < numToSpawn; i++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<RedDust>(), Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f, 0, default(Color), 1f);
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            int radius = 15; 
            int centerX = (int)(Projectile.Center.X / 16f);
            int centerY = (int)(Projectile.Center.Y / 16f);

            for (int x = centerX - radius; x <= centerX + radius; x++)
            {
                for (int y = centerY - radius; y <= centerY + radius; y++)
                {
                    if (Vector2.Distance(new Vector2(x * 16 + 8, y * 16 + 8), Projectile.Center) <= radius * 16)
                    {
                        WorldGen.KillTile(x, y, false, false, false); 
                    }
                }
            }

            
            return true;
        }
    }
}

