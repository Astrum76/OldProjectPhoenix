using System; //what sources the code uses, these sources allow for calling of terraria functions, existing system functions and microsoft vector functions (probably more)
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectPhoenix.Projectiles.Magic //where it's stored. Replace Mod with the name of your mod. This file is stored in the folder \Mod Sources\(mod name, folder can't have spaces)\Projectiles.
{
    public class StaticBlast : ModProjectile //the class of the projectile. Change EtherealBullet to the ID of your projectile. The ID has to match the name of the sprite for that item in your folder and can have no spaces.
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 4; //The number of frames the sprite sheet has
        }
        public override void SetDefaults()
        {
            projectile.velocity = new Vector2(0.1f, 0.1f);
            projectile.stepSpeed = 0.1f;
            projectile.Name = "Static Blast"; //Name of the projectile, only shows this if you get killed by it
            projectile.light = 1f;
            projectile.width = 28; //sprite is 2 pixels wide
            projectile.height = 28;
            projectile.ignoreWater = true;
            projectile.magic = true;
            projectile.penetrate = 30;
            projectile.height = 32; 
            projectile.aiStyle = 0;
            projectile.friendly = true; //player projectile
      
            projectile.timeLeft = 600; //lasts for 600 frames/ticks. Terraria runs at 60FPS, so it lasts 10 seconds.
            aiType = ProjectileID.MagnetSphereBall; //This clones the exact AI of the vanilla projectile Bullet.
        }
        public override void AI()

        {
            Random a = new Random();
            if(a.Next()%20 == 1)
            {
                projectile.position.X += (float)(a.NextDouble() - 0.5f)*55f;
                projectile.position.Y += (float)(a.NextDouble() - 0.5f)*55f;

            }

            projectile.knockBack = (float)a.NextDouble() % 10;
            projectile.velocity *= 1.01f + (float)(a.NextDouble() - 0.5f)/10; 
            int frameSpeed = 4; //How fast you want it to animate
            projectile.frameCounter++;
            if (projectile.frameCounter >= frameSpeed)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
            
            }
            projectile.ai[0] += 0.1f;
            projectile.rotation += (float)projectile.direction * 0.05f; //Spins in a good speed
            //if(projectile.frameCounter % 4 == 0)Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, DustID.Electric, projectile.velocity.X * -1.5f, projectile.velocity.Y * -1.5f);   //spawns dust behind it, this is a spectral light blue dust. 15 is the dust, change that to what you want.
        }
        public override void Kill(int timeLeft)
        {
            Random a = new Random();
            Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
            for (int k = 0; k < 25; k++)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.t_Granite, (float)a.NextDouble() , (float)a.NextDouble(),0,default,0.7f);   //spawns dust behind it, this is a spectral light blue dust. 15 is the dust, change that to what you want.
            }
            Main.PlaySound(SoundID.Item10, projectile.position);

        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.velocity *= 0;
            Console.Write("Contant with enemy. Applying Stun.");
            //target.AddBuff(69, 1200, false); // useful ones are 20 for poison, 24 for On Fire!, 39 for Cursed Flames, 69 for Ichor, and 70 for Venom.
           // target.AddBuff(189, 1200, false); // useful ones are 20 for poison, 24 for On Fire!, 39 for Cursed Flames, 69 for Ichor, and 70 for Veno
            target.AddBuff(ModContent.BuffType<Buffs.Stunned>(), 300, false);
            


        }

    }
}