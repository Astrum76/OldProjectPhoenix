using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectPhoenix.Projectiles.Enemy
{
	public class LenicusEnemy : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shockwave");
			Main.projFrames[projectile.type] = 20; //This is an animated projectile

		}
		public override void SetDefaults()
		{
			projectile.width = 26;
			projectile.height = 26;
            projectile.friendly = false;
            projectile.penetrate = 5;                       //this is the projectile penetration
            projectile.hostile = true;
			projectile.magic = true;                        //this make the projectile do magic damage
            projectile.tileCollide = false;                 //this make that the projectile does not go thru walls
			projectile.ignoreWater = false;
			projectile.light = 0.4f;    // projectile light

		}
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 12; k++)
            {
                Random a = new Random();

                Main.PlaySound(SoundID.Item93, projectile.position);
             
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Clentaminator_Red, (float)((a.Next()%100)/100), (float)((a.Next() % 100) / 100),0,default,1.5f);   //spawns dust behind it, this is a spectral light blue dust. 15 is the dust, change that to what you want.

                
            }

        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 150);
            base.OnHitPlayer(target, damage, crit);
        }

        public override void AI()
        {
            //this is projectile dust
            for (int i = 0; i < 6; i++) 
            { 
                int DustID2 = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, DustID.IchorTorch, projectile.velocity.X * -0.1f, projectile.velocity.Y * -0.1f,0,default,1.25f);   //spawns dust behind it, this is a spectral light blue dust lol
                Main.dust[DustID2].noGravity = true;
             }

            //this make that the projectile faces the right way
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            
            projectile.localAI[0] += 1f;
            //projectile.alpha = (int)projectile.localAI[0] * 2;
          
            if (projectile.localAI[0] > 1600f) //projectile time left before disappears
            {

                projectile.Kill();
            }
                       // Loop through the 20 animation frames, spending 15 ticks on each.
            if (++projectile.frameCounter >= 30)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 20)
                {
                    projectile.frame = 0;
                }
            }
        }
		
	}
}



