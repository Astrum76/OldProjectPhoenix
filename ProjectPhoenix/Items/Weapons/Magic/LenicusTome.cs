using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ProjectPhoenix;
using ProjectPhoenix.Projectiles.Magic;

namespace ProjectPhoenix.Items.Weapons.Magic
{
    public class LenicusTome : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Armstrong's Tome");
			Tooltip.SetDefault("");
		}
        public override void SetDefaults()
        {
                    
            item.damage = 50;                        
            item.magic = true;                     //this make the item do magic damage
            item.width = 32;
            item.height = 32;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.HoldingOut;        //this is how the item is held
            item.noMelee = true;
            item.knockBack = 1;
            item.value = Item.buyPrice(0, 7, 50, 0); //7 gold
            item.rare = 6;
            item.mana = 6;             //mana use
                                       // item.UseSound = SoundID.Item21;  

            item.UseSound = SoundID.Item92;            //this is the sound when you use the item, fwsh afaik
            item.autoReuse = true;
            //item.shoot = mod.ProjectileType ("Lenicus");       // this shit is redunatant and only comes with older weps.
            item.shoot = ModContent.ProjectileType<Lenicus>(); //modern
            item.shootSpeed = 9f;    //projectile speed when shoot
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SpellTome, 1);
            recipe.AddIngredient(ItemID.SoulofMight, 30);
			recipe.AddTile(TileID.Bookcases);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}