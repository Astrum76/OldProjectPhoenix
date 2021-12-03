using ProjectPhoenix.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectPhoenix.NPCs.Standard
{
	//[AutoloadBossHead]

	public class Testing_Boss : ModNPC
	{
		private int bossMode
		{
			get => (int)npc.ai[0];
			set => npc.ai[0] = value;
		}
		private int bossTime
        {
			get => (int)npc.ai[1];
			set => npc.ai[1] = value;
		}
		


		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("???");
			Main.npcFrameCount[npc.type] = 1;
		}

		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.lifeMax = 10000;
			npc.damage = 40;
			npc.defense = 0;
			npc.knockBackResist = 0f;
			npc.dontTakeDamage = false;
			npc.alpha = 0;
			npc.width = 50;
			npc.height = 50;
			npc.value = Item.buyPrice(0, 60, 0, 0);
			npc.npcSlots = 5f;
			//npc.boss = true;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.NPCHit5;
			npc.DeathSound = SoundID.NPCDeath7;
			//music = MusicID.Boss2;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (Main.hardMode) return SpawnCondition.Cavern.Chance * 0.001f;
			return 0;
		}
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);
			npc.damage = (int)(npc.damage * 0.6f);
        }

        public override void AI()
		{
			bossTime++;
			Vector2 desVel = new Vector2(0f, -0.05f);

			Player player = Main.player[npc.target];

			//Main.NewText(bossTime,default,true);
			if (player.active && player.dead != true)
			{
				if (bossMode == 0)
				{
					npc.TargetClosest();

					npc.rotation = npc.AngleTo(player.Center);
					Vector2 moveTo = player.Center + new Vector2(0f, -150); //This is 200 pixels above the center of the player.


					//npc.position = moveTo; //direct mvoe

					float speed = 5f;
					Vector2 move = moveTo - npc.Center;
					float magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
					if (magnitude > speed)
					{
						move *= speed / magnitude;
					}
					float turnResistance = 30f; //the larger this is, the slower the npc will turn
					move = (npc.velocity * turnResistance + move) / (turnResistance + 1f);
					magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
					if (magnitude > speed)
					{
						move *= speed / magnitude;
					}
					npc.velocity = move;

					if (bossTime == 60 * 5)
					{
						bossMode = 1;
					}
				}

				if (bossMode == 1)
				{
					npc.rotation = npc.AngleTo(player.Center);
					npc.TargetClosest();
					npc.velocity *= 0.9f;
					if (bossTime % 10 == 0)
					{

						Vector2 position = npc.Center;
						Vector2 targetPosition = Main.player[npc.target].Center;
						Vector2 direction = targetPosition - position;
						direction.Normalize();
						float speed = 10f;
						int type = ProjectileID.PinkLaser;
						int damage = npc.damage; //If the projectile is hostile, the damage passed into NewProjectile will be applied doubled, and quadrupled if expert mode, so keep that in mind when balancing projectiles
						Projectile.NewProjectile(position, direction * speed, type, damage, 0f, Main.myPlayer);

					}
					if (bossTime == 60 * 10)
					{
						bossMode = 2;
					}
				}

				if(bossMode == 2)
                {
					npc.TargetClosest();
					npc.velocity *= 1.001f;
					npc.rotation+=0.03f;
					if (bossTime % 4 == 0)
					{

						Projectile.NewProjectile(npc.Center, (new Vector2(1,1).RotatedBy(npc.rotation))*5f, ProjectileID.PinkLaser, npc.damage, 0f, Main.myPlayer);

					}
					if (bossTime >= 60 * 15)
					{
						bossMode = 0;
						bossTime = 0;
					}
				}
			}
            else
            {
				if (!player.active || player.dead)
				{
					npc.TargetClosest(false);
					npc.velocity = npc.velocity + desVel;
					
					
						if (npc.timeLeft > 360)
						{
							npc.timeLeft = 360;
						}
					
				}

			}

		}

		private void CreateDust()
		{
			Color? color = GetColor();
			if (color.HasValue)
			{
				for (int k = 0; k < 5; k++)
				{
					int dust = Dust.NewDust(npc.position, npc.width, npc.height, ModContent.DustType<Blood>(), 0f, 0f, 0, color.Value);
					double angle = Main.rand.NextDouble() * 2.0 * Math.PI;
					Main.dust[dust].velocity = 3f * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
				}
			}
		}
        public override bool CheckDead()
        {
			for (int i = 0; i < 10; i++)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, DustID.PurpleCrystalShard, (float)((Main.rand.Next(101) % 100) / 100), (float)((Main.rand.Next(101) % 100) / 100), 0, default, 1.5f);   //spawns dust behind it, this is a spectral light blue dust. 15 is the dust, change that to what you want.

			}
			Main.NewText("A Fractal has been shattered!" , Color.Purple);
			return true;

		}
        public override bool PreNPCLoot()
		{
			return false;
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{

			return true;
		}

		public override void OnHitPlayer(Player player, int dmgDealt, bool crit)
		{
			if (Main.expertMode || Main.rand.NextBool())
			{
				int debuff = GetDebuff();
				npc.velocity *= 0;
				if (debuff >= 0)
				{
					player.AddBuff(debuff, GetDebuffTime(), true);
				}
			}
		}

		public int GetDebuff()
		{

			return BuffID.ChaosState;

		}

		public int GetDebuffTime()
		{
			int time;
			time = 60;
			return time;
		}

		public Color? GetColor()
		{

			return new Color(0, 230, 230);
		}
	}
}