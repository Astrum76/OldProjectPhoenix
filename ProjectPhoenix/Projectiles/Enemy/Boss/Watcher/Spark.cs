using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectPhoenix.Projectiles.Enemy.Boss.Watcher
{
    public class Spark : ModProjectile
    {


        int speed = 5;

        public override void SetStaticDefaults()
        {
            projectile.timeLeft = 1000; //NEVER DESPAWN

            DisplayName.SetDefault("Spark");
            Main.projFrames[projectile.type] = 1; //This is an animated projectile

        }

        public override void SetDefaults()
        {
            projectile.scale = 1f;
            speed = 8;
            projectile.width = 256;
            projectile.height = 256;
            projectile.penetrate = 1;                       //this is the projectile penetration
            projectile.hostile = false;
            projectile.tileCollide = false;                 //this make that the projectile does not go thru walls
            projectile.ignoreWater = false;
            projectile.light = 1f;    // projectile light

        }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
        
            Texture2D texture = mod.GetTexture("Projectiles/Enemy/Boss/Watcher/Spark");
            if (projectile.ai[0] == 1)
            {
                spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    projectile.position.X - Main.screenPosition.X + projectile.width * 0.5f,
                    projectile.position.Y - Main.screenPosition.Y + projectile.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                projectile.rotation,
                texture.Size() * 0.5f,
                projectile.scale*0.4f,
                SpriteEffects.None,
                0f
            );
            }
            else
            {
                spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    projectile.position.X - Main.screenPosition.X + projectile.width * 0.5f,
                    projectile.position.Y - Main.screenPosition.Y + projectile.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                projectile.rotation,
                texture.Size() * 0.5f,
                projectile.scale,
                SpriteEffects.None,
                0f
            );
            }
            
        
         }
        public override void AI()
        {
            projectile.frame++;
            if (projectile.frame % 2 == 0)
            {
                projectile.rotation += MathHelper.ToRadians(speed);
                projectile.alpha += 10;
                projectile.scale -= 0.04f;

            }
            projectile.velocity = Vector2.Zero;
            if (projectile.scale < 0.05f)
            {
                projectile.Kill();
            }
            else
            {
            }


        }

    }
}



