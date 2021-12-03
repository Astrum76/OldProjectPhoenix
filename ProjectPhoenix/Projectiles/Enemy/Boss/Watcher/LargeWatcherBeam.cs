using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectPhoenix.Projectiles.Enemy.Boss.Watcher
{
	class LargeWatcherBeam : ModProjectile
	{
		public bool alive = true;
		int frame = 0;
		private int cool = 0;
		private float offset = 50; //cosmetic
		private float Distance = 0;
		private const float MOVEDEEZNUTS = 200f;
		

		// Note, this Texture is actually just a blank texture, FYI.
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			return false;
		}
		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			target.velocity *= -1;
			Main.NewText("fuck you *eats your balls*");
		}
		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			projectile.damage = (int)(projectile.damage * 0.8);
		}
		private void SetLaserPosition(NPC npc)
		{

			for (Distance = MOVEDEEZNUTS; Distance <= 2200f; Distance += 5f)
			{
				var start = npc.Center + projectile.velocity * Distance;
				if (!Collision.CanHitLine(npc.Center, 1, 1, start, 1, 1))
				{
					Distance -= 5f;
					break;
				}
			}
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Vector2 end1 = projectile.Center;
			Vector2 end2 = projectile.Center + new Vector2(0, Distance-offset);
			Texture2D texture;
			if (end1 != end2)
			{
				float length = Vector2.Distance(end1, end2);
				Vector2 direction = end2 - end1;
				direction.Normalize();
				float start = (float)projectile.frameCounter % 8f;
				start *= 2f;
				if (projectile.localAI[1] == 0f)
				{
					start *= 2f;
					start %= 16f;
				}
				texture = mod.GetTexture("Projectiles/Enemy/Boss/Watcher/LargeWatcherBeam");
				for (float k = start + 16; k <= length-16; k += 16f)
				{
					spriteBatch.Draw(texture, end1 + k * direction - Main.screenPosition, null, Color.White, 0f, new Vector2(16f, 16f), 1f, SpriteEffects.None, 0f);
				}
			}
			texture = Main.projectileTexture[projectile.type];
			Vector2 drawPos = projectile.Center - Main.screenPosition;
			Vector2 drawOrigin = new Vector2(texture.Width / 2, texture.Height / 2 / 1);
			//spriteBatch.Draw(texture, drawPos, new Rectangle(1, 1, 1, 1), Color.White * 255, 0f, drawOrigin, 1f, SpriteEffects.None, 0f);

			texture = mod.GetTexture("Projectiles/Enemy/Boss/Watcher/WatcherBeamTail");
			drawPos = end2 - Main.screenPosition;

			spriteBatch.Draw(texture, end2 - Main.screenPosition, null, Color.White, 0f, new Vector2(16f, 16f), 1f, SpriteEffects.None, 0f);

			drawPos = projectile.Center - Main.screenPosition;
			texture = mod.GetTexture("Projectiles/Enemy/Boss/Watcher/WatcherBeamHead");
			spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, null, Color.White, 0f, new Vector2(16f, 16f), 1f, SpriteEffects.None, 0f);

			return false;
		}

		public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
		{
			// Add this projectile to the list of projectiles that will be drawn BEFORE tiles and NPC are drawn. This makes the projectile appear to be BEHIND the tiles and NPC.
			drawCacheProjsBehindNPCsAndTiles.Add(index);
		}


		/*public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{

			NPC player = Main.npc[(int)projectile.ai[1]];
			Vector2 unit = projectile.velocity;
			float point = 0f;
			// Run an AABB versus Line check to look for collisions, look up AABB collision first to see how it works
			// It will look for collisions on the given line using AABB
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), player.Center,
				player.Center + unit * Distance, 22, ref point);
		}*/
		private void CastLights()
		{
			// Cast a light along the line of the laser
			DelegateMethods.v3_1 = new Vector3(0.8f, 0.8f, 1f);
			Utils.PlotTileLine(projectile.Center, projectile.Center + projectile.velocity * (Distance - MOVEDEEZNUTS), 26, DelegateMethods.CastLight);
		}

		public override void AI()
		{
			if (alive)
			{
				//Main.NewText("alive from black messa");
				frame++;
				projectile.velocity = new Vector2(0, 1f);
				SetLaserPosition(Main.npc[(int)projectile.ai[1]]);
				//Main.NewText(cool);
				if (cool > 0)
				{
					cool--;
				}
				projectile.timeLeft = 10;
				Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + Distance - offset), 16, 16, DustID.Electric);
				Rectangle rect = new Rectangle((int)projectile.Center.X - 32, (int)projectile.Center.Y, 64, (int)Distance);
				if (rect.Contains(Main.player[Main.myPlayer].getRect()))
				{
					if (cool == 0)
					{
						Player player = Main.player[Main.myPlayer];
						player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " ran out of time."), 250, 0);
						player.velocity.X *= -1.1f;
						cool = 10;
					}


				}
				Lighting.AddLight(projectile.Center, (Color.Aquamarine.ToVector3()) * 0.5f);
				CastLights();
				if (frame % 6 == 0)
				{
					//Main.PlaySound(15, (int)projectile.position.X, (int)projectile.position.Y + (int)Distance - (int)offset, 2, 5, 0);
					//WTF ITS SOMEONE SCREAMING???? SAVE THIS
					Main.PlaySound(SoundID.Item72, new Vector2(projectile.position.X, projectile.position.Y + Distance - offset));

				}
                //projectile.position = new Vector2(projectile.ai[0], projectile.ai[1]);

            }
            else
            {
				projectile.Kill();


				
            }
		}
	}
}



		/*
		public float Distance
		{
			get => projectile.ai[0];
			set => projectile.ai[0] = value;
		}
		private const float MOVE_DISTANCE = 60f;

		public override void SetDefaults()
		{
			//projectile.CloneDefaults(ProjectileID.LastPrism);
			projectile.width = 16;
			projectile.height = 16;
			// NO! projectile.aiStyle = 48;
			projectile.hide = true;
			projectile.friendly = false;
			projectile.hostile = true;
			projectile.magic = true;
			projectile.extraUpdates = 100;
			projectile.timeLeft = 8000; // lowered from 300
			projectile.penetrate = -1;
		}


		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			// We start drawing the laser if we have charged up
			
			DrawLaser(spriteBatch, Main.projectileTexture[projectile.type], Main.npc[(int)projectile.ai[1]].Center,
			projectile.velocity, 10, projectile.damage, -1.57f, 1f, 1000f, Color.White, (int)MOVE_DISTANCE);
			return false;
		}

		public void DrawLaser(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 unit, float step, int damage, float rotation = 0f, float scale = 1f, float maxDist = 2000f, Color color = default(Color), int transDist = 50)
		{
			float r = unit.ToRotation() + rotation;

			// Draws the laser 'body'
			for (float i = transDist; i <= Distance; i += step)
			{
				Color c = Color.White;
				var origin = start + i * unit;
				spriteBatch.Draw(texture, origin - Main.screenPosition,
					new Rectangle(0, 26, 28, 26), i < transDist ? Color.Transparent : c, r,
					new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
			}

			// Draws the laser 'tail'
			spriteBatch.Draw(texture, start + unit * (transDist - step) - Main.screenPosition,
				new Rectangle(0, 0, 28, 26), Color.White, r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);

			// Draws the laser 'head'
			spriteBatch.Draw(texture, start + (Distance + step) * unit - Main.screenPosition,
				new Rectangle(0, 52, 28, 26), Color.White, r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
		}
		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			// We can only collide if we are at max charge, which is when the laser is actually fired

			NPC npc = Main.npc[(int)projectile.ai[1]];
			Vector2 unit = projectile.velocity;
			float point = 0f;
			// Run an AABB versus Line check to look for collisions, look up AABB collision first to see how it works
			// It will look for collisions on the given line using AABB
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), npc.Center,
				npc.Center + unit * Distance, 22, ref point);
		}

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
			target.immuneTime = 5;
			target.velocity *= -1;
			base.OnHitPlayer(target, damage, crit);
        }
		public override void AI()
		{
			Main.NewText("alive");
			NPC npc = Main.npc[(int)projectile.ai[1]];
			projectile.position = npc.Center + projectile.velocity * MOVE_DISTANCE;
			projectile.timeLeft = 2;

			// By separating large AI into methods it becomes very easy to see the flow of the AI in a broader sense
			// First we update player variables that are needed to channel the laser
			// Then we run our charging laser logic
			// If we are fully charged, we proceed to update the laser's position
			// Finally we spawn some effects like dusts and light

			

			// If laser is not charged yet, stop the AI here.

			SetLaserPosition(npc);
			//SpawnDusts(npc);
			CastLights();
		}

		private void SetLaserPosition(NPC npc)
		{
			for (Distance = MOVE_DISTANCE; Distance <= 2200f; Distance += 5f)
			{
				var start = npc.Center + projectile.velocity * Distance;
				if (!Collision.CanHit(npc.Center, 1, 1, start, 1, 1))
				{
					Distance -= 5f;
					break;
				}
			}
		}
		private void SpawnDusts(NPC npc)
		{
			Vector2 unit = projectile.velocity * -1;
			Vector2 dustPos = npc.Center + projectile.velocity * Distance;

			for (int i = 0; i < 2; ++i)
			{
				float num1 = projectile.velocity.ToRotation() + (Main.rand.Next(2) == 1 ? -1.0f : 1.0f) * 1.57f;
				float num2 = (float)(Main.rand.NextDouble() * 0.8f + 1.0f);
				Vector2 dustVel = new Vector2((float)Math.Cos(num1) * num2, (float)Math.Sin(num1) * num2);
				Dust dust = Main.dust[Dust.NewDust(dustPos, 0, 0, 226, dustVel.X, dustVel.Y)];
				dust.noGravity = true;
				dust.scale = 1.2f;
				dust = Dust.NewDustDirect(Main.npc[(int)projectile.ai[1]].Center, 0, 0, 31,
					-unit.X * Distance, -unit.Y * Distance);
				dust.fadeIn = 0f;
				dust.noGravity = true;
				dust.scale = 0.88f;
				dust.color = Color.Cyan;
			}

			if (Main.rand.NextBool(5))
			{
				Vector2 offset = projectile.velocity.RotatedBy(1.57f) * ((float)Main.rand.NextDouble() - 0.5f) * projectile.width;
				Dust dust = Main.dust[Dust.NewDust(dustPos + offset - Vector2.One * 4f, 8, 8, 31, 0.0f, 0.0f, 100, new Color(), 1.5f)];
				dust.velocity *= 0.5f;
				dust.velocity.Y = -Math.Abs(dust.velocity.Y);
				unit = dustPos - Main.player[projectile.owner].Center;
				unit.Normalize();
				dust = Main.dust[Dust.NewDust(Main.player[projectile.owner].Center + 55 * unit, 8, 8, 31, 0.0f, 0.0f, 100, new Color(), 1.5f)];
				dust.velocity = dust.velocity * 0.5f;
				dust.velocity.Y = -Math.Abs(dust.velocity.Y);
			}
		}

		private void CastLights()
		{
			// Cast a light along the line of the laser
			DelegateMethods.v3_1 = new Vector3(0.8f, 0.8f, 1f);
			Utils.PlotTileLine(projectile.Center, projectile.Center + projectile.velocity * (Distance - MOVE_DISTANCE), 26, DelegateMethods.CastLight);
		}
		public override bool ShouldUpdatePosition() => false;

		public override void CutTiles()
		{
			DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
			Vector2 unit = projectile.velocity;
			Utils.PlotTileLine(projectile.Center, projectile.Center + unit * Distance, (projectile.width + 16) * projectile.scale, DelegateMethods.CutTiles);
		}

	}
}


		*/















/*
public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
{
	// Add this projectile to the list of projectiles that will be drawn BEFORE tiles and NPC are drawn. This makes the projectile appear to be BEHIND the tiles and NPC.
	drawCacheProjsBehindNPCsAndTiles.Add(index);
}

// Note, this Texture is actually just a blank texture, FYI.
public override bool OnTileCollide(Vector2 oldVelocity)
{
	return false; 
}
public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
{
	target.velocity *= -1;
	Main.NewText("fuck you *reverses your vel*");
}
public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
{
	projectile.damage = (int)(projectile.damage * 0.8);
}

public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
{
	Vector2 end1 = projectile.Center;
	Vector2 end2 = projectile.Center + new Vector2(0, 100000);
	Texture2D texture;
	if (end1 != end2)
	{
		float length = Vector2.Distance(end1, end2);
		Vector2 direction = end2 - end1;
		direction.Normalize();
		float start = (float)projectile.frameCounter % 8f;
		start *= 2f;
		if (projectile.localAI[1] == 0f)
		{
			start *= 2f;
			start %= 16f;
		}
		texture = mod.GetTexture("Projectiles/Enemy/Boss/Watcher/LargeWatcherBeam");
		for (float k = start; k <= length; k += 16f)
		{
			spriteBatch.Draw(texture, end1 + k * direction - Main.screenPosition, null, Color.White, 0f, new Vector2(16f, 16f), 1f, SpriteEffects.None, 0f);
		}
	}
	texture = Main.projectileTexture[projectile.type];
	Vector2 drawPos = projectile.Center - Main.screenPosition;
	Vector2 drawOrigin = new Vector2(texture.Width / 2, texture.Height / 2 / 1);
	spriteBatch.Draw(texture, drawPos, new Rectangle(1,1,1,1), Color.White*255, 0f, drawOrigin, 1f, SpriteEffects.None, 0f);



	return false;
}







public override void AI()
{
	//Collision.
	projectile.timeLeft = 10;
	//projectile.position = new Vector2(projectile.ai[0], projectile.ai[1]);
}
}
}*/