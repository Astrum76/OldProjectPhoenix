/*using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ReplaceThisName
{
	[AutoloadBossHead]

	public class Vinesoos : ModNPC
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
			DisplayName.SetDefault("Smiley");
			Main.npcFrameCount[npc.type] = 6;
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
			npc.value = Item.buyPrice(0, 20, 0, 0);
			npc.npcSlots = 5f;
			npc.boss = true;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.NPCHit5;
			npc.DeathSound = SoundID.NPCDeath7;
			music = MusicID.Boss2;
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

				}

				if (bossMode == 1)
				{
					
				}

				if (bossMode == 2)
				{
					
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

		public override bool CheckDead()
		{
			
			return true;

		}
		public override bool PreNPCLoot()
		{
			return true;
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
		public override void FindFrame(int frameHeight)
		{
			if (bossTime %5 == 0)
			{
				npc.frame.Y += frameHeight;
				if(npc.frame.Y == frameHeight * 6)
                {
					npc.frame.Y = 0;

				}
			}
			
		}
		public int GetDebuff()
		{

			return BuffID.Ichor;

		}

		public int GetDebuffTime()
		{
			int time;
			time = 120;
			return time;
		}

	}
}*/