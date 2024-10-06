using Microsoft.Xna.Framework;
using JujutsuKaisen.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JujutsuKaisen.Projectiles
{
    public class Blue : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
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
            
            Vector2 cursorPosition = Main.MouseWorld;
            Vector2 direction = cursorPosition - Projectile.Center;
            
            if (direction.Length() > 0)
            {
                direction.Normalize();
                Projectile.velocity = direction * 10f; 
            }

            float pullRadius = 100f;
            float pullStrength = 2f;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && !npc.friendly && !npc.dontTakeDamage)
                {
                    Vector2 npcDirection = Projectile.Center - npc.Center;
                    float distance = npcDirection.Length();

                    if (distance < pullRadius)
                    {
                        npcDirection.Normalize();
                        npc.velocity += npcDirection * pullStrength;
                    }
                }
            }

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
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<BlueDust>(), Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f, 0, default(Color), 1f);
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            int radius = 3; 
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

            return false;
        }
    }
}
