using Microsoft.Xna.Framework;
using JujutsuKaisen.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JujutsuKaisen.Items
{
	public class PurpleStaff : ModItem
	{

        public override void SetStaticDefaults()
        {
			Item.staff[Item.type]= true;
        }
        public override void SetDefaults()
		{
			Item.damage = 10000;
			Item.mana = 200;
			Item.DamageType = DamageClass.Magic;
			Item.width = 20;
			Item.height = 20;
			Item.useTime = 300;
			Item.useAnimation = 300;
			Item.useStyle = 5;
			Item.knockBack = 0;
			Item.value = 1000000000;
			Item.rare = 9;
			Item.UseSound = SoundID.Item9;
			Item.autoReuse = false;
			Item.shoot = ModContent.ProjectileType<Purple>();
			Item.shootSpeed = 2f;
			Item.noMelee = true;
			
		}
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			Vector2 offset = new Vector2(velocity.X * 3, velocity.Y * 3);
			position += offset;
            return true;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "BlueStaff", 1);
			recipe.AddIngredient(null, "RedStaff", 1);

			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}