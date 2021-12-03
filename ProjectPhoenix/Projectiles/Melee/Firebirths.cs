using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectPhoenix.Projectiles.Melee
{
    public class Firebirths : ModProjectile
    {
        private bool shocked;

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Firebirths");

		}
        public override void SetDefaults()
        {
            projectile.width = 24;       //projectile width
            projectile.height = 20;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.melee = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 2;      //how many npc will penetrate
            projectile.timeLeft = 200;   //how many time this projectile has before disepire
            projectile.light = 0.5f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true; 
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.DD2_LightningAuraZap, projectile.position);
            Random a = new Random();

            for (int k = 0; k < 5; k++)
            {


                Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Enchanted_Gold, (float)((a.Next() % 100) / 100), (float)((a.Next() % 100) / 100), 0, default, 1.5f);   //spawns dust behind it, this is a spectral light blue dust. 15 is the dust, change that to what you want.


            }

        }
        public override void AI()           //this make that the projectile will face the corect way
        {


           





            // |
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            projectile.ai[0]++;
            if (projectile.ai[0] % 5 == 0)
            {
                int DustID2 = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, DustID.Firework_Yellow, projectile.velocity.X * -2.1f, projectile.velocity.Y * -2.1f, 0, default, 0.5f);   //spawns dust behind it, this is a spectral light blue dust lol
                Main.dust[DustID2].noGravity = true;
            }
               
            
        }
    }
}