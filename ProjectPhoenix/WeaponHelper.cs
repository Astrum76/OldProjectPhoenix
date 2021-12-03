using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;


namespace ProjectPhoenix
{
    class WeaponHelper
    {
        ///<summary>Sets useAmmo to bullet, ranged, some other gun defaults. Override if needed. Does not set UseSound to anything but generic.</summary>
        public static void GunDefaults(Item item,int damage, int knockback, int width, int height, int useTime, int useStyle, int value1, int rarity, bool autoUse, int shoot, int shootspeed)
        {
            item.UseSound = SoundID.Item1;
            item.damage = damage;  //gun damage
            item.ranged = true;   //its a gun so set this to true
            item.width = width;     //gun image width
            item.height = height;   //gun image  height
            item.useTime = useTime;  //how fast 
            item.useAnimation = useTime;
            item.useAmmo = AmmoID.Bullet;
            item.useStyle = useStyle;    //
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = knockback;
            item.value = value1;
            item.rare = rarity;
            item.autoReuse = autoUse;
            item.shoot = shoot; //idk why but all the guns in the vanilla source have this
            item.shootSpeed = shootspeed;


        }
        ///<summary>Sets melee, some other sword defaults. Override if needed. Does not set UseSound to anything but generic. If shoot, fires a generic Terra Beam.</summary>

        public static void SwordDefaults(Item item, int damage, int knockback, int width, int height, int useTime, int useStyle, int value1, int rarity, bool autoUse, int shoot, int shootspeed, bool shoots)
        {
            item.UseSound = SoundID.Item1;
            item.damage = damage;  //gun damage
            item.melee = true;   //its a gun so set this to true
            item.width = width;     //gun image width
            item.height = height;   //gun image  height
            item.useTime = useTime;  //how fast 
            item.useAnimation = useTime;
            item.useStyle = useStyle;    //
            item.noMelee = false; //so the item's animation doesn't do damage
            item.knockBack = knockback;
            item.value = value1;
            item.rare = rarity;
            item.autoReuse = autoUse;
            if (shoots)
            {
                item.shoot = ProjectileID.TerraBeam;
                item.shootSpeed = shootspeed;
            }
            



        }


    }
}
