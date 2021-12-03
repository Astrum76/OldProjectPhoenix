using ProjectPhoenix.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ProjectPhoenix.Projectiles.Magic;
using ProjectPhoenix;
using ProjectPhoenix.Dusts;
using ProjectPhoenix.Projectiles.Enemy.Boss.Watcher;
using Microsoft.Xna.Framework.Graphics;
using ProjectPhoenix.UI;
using Terraria.Graphics.Effects;

namespace ProjectPhoenix.NPCs.Bosses.Watcher

	
{
	[AutoloadBossHead]

	public class Watcher : ModNPC
	{
		//watcher is a multiattack boss that hovers above the player.
		//when it dies, it just fucks off. 
		//todo: change dialogue / find a suitable fix
		//code by Megs. sprite by Glorifier. 
		//music if done will be done by Cecelune
		//fully commented to the best of my ability for future readability :) 




		//code provided by SuperAndyHero#1185, will be used to fix this later
		//todo: make multiplayer implementation better
		/*
		 * public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(npc.localAI[0]);
            writer.Write(npc.localAI[1]);
            writer.Write(npc.localAI[2]);
            writer.Write(npc.localAI[3]);
            //writer.Write(otherVar);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            npc.localAI[0] = reader.ReadSingle();
            npc.localAI[1] = reader.ReadSingle();
            npc.localAI[2] = reader.ReadSingle();
            npc.localAI[3] = reader.ReadSingle();
            //otherVar = reader.ReadInt32();
        }
		 * 
		 * 
		 * 
		 */
		//dumbest fix of my life.
		private int finaltimer = 180;
		private int bulletHellColldown = 0;
		private int handLBeam;
		private int handRBeam;
		private bool isDeathPosStored = false;
		private bool dustActive;
		Vector2 deathPos = new Vector2(0, 0);
		private bool colPlayer = true;
		private int bulletHellSec = 10;
		private int bulletCooldownDefault = 120;
		private int cooldownTimer = 0;
		private float alphaSet = 100;
		private float setupLaser;
		private int tpSet;
		private int bossTimestamp;
		private bool teleportSet;
		private int deathTimer1 = 0;
		private bool handL = false; //
		private bool handR = false; //
		public Vector2 teleportCoords = new Vector2(0, 0);
		//cut these down.
		
		//synced by default, most important (EXPECT HANDREF. FUCK THAT. TODO: ASS WHY IS IT SYNCE?D????)
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
		private int handLRef
		{
			get => (int)npc.ai[2];
			set => npc.ai[2] = value;
		}
		private int handRRef
		{
			get => (int)npc.ai[3];
			set => npc.ai[3] = value;
		}

		public void setBossMode3(int boss)
        {
			bossMode = boss;
        }


		public override void SetStaticDefaults() //name and animation (probably unused unless GLori gives me an animated sprite or I choose to make the hand spin)
		{
			DisplayName.SetDefault("The Watcher");
			Main.npcFrameCount[npc.type] = 1;
		}
		public override void SetDefaults() //for global NPC stats, will be possibly overriden later! 
		{
			npc.aiStyle = -1; 
			//deathTimer1 = 0; //what the fuck is this doing here
			npc.lifeMax = 7000;
			npc.damage = 20;
			npc.defense = 15;
			npc.knockBackResist = 0f;
			npc.dontTakeDamage = false;
			npc.alpha = 0;
			npc.width = 120;
			npc.height = 140;
			npc.value = Item.buyPrice(0, 0, 0, 0);
			npc.npcSlots = 5f;
			npc.boss = true;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.NPCHit5;
			npc.DeathSound = SoundID.NPCDeath7;
			music = MusicID.Boss2;
		}
		//obvious
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);
			npc.damage = (int)(npc.damage * 0.6f);
		}
		//agony
		public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
		{

			Texture2D texture = mod.GetTexture("NPCs/Bosses/Watcher/Watcher_Glowmask");
			spriteBatch.Draw
			(
				texture,
				new Vector2
				(
					npc.position.X - Main.screenPosition.X + npc.width * 0.5f,
					npc.position.Y - Main.screenPosition.Y + npc.height - texture.Height * 0.5f + 2f
				),
				new Rectangle(0, 0, texture.Width, texture.Height),
				Color.White * (alphaSet / 100),
				npc.rotation,
				texture.Size() * 0.5f,
				npc.scale,
				SpriteEffects.None,
				1f
			);
			/*public override Color? GetAlpha(Color lightColor) => Color.White;
			Put this outside of any other hooks in your projectile code, but still inside the projectile class.
			 * 
			 * 
			 * 
			 * 
			 * 
			 */

		}
		private void Phase1AI()
        {

			Player player = Main.player[npc.target];
			colPlayer = true;
			(Main.npc[(int)handLRef].modNPC as WatcherHandLeft).colOn = true;
			(Main.npc[(int)handRRef].modNPC as WatcherHandRight).colOn = true;

			NPCHelper.MoveSlowNPC(npc, 17f, player.Center + new Vector2(0f, -500)+ (player.velocity * 25f), 40f);
            try
            {
				(Main.npc[(int)handRRef].modNPC as WatcherHandRight).setCol(true);
				(Main.npc[(int)handLRef].modNPC as WatcherHandLeft).setCol(true);
				npc.dontTakeDamage = false;
				setupLaser = 0;

				npc.TargetClosest();
				npc.rotation = 0;

				//Main.npc[(int)handRRef].ai[0] = 15f;
				Main.npc[(int)handRRef].ai[0] = npc.Center.X + 150;
				Main.npc[(int)handRRef].ai[1] = npc.Center.Y + 75;

				Main.npc[(int)handLRef].ai[0] = npc.Center.X - 150;
				Main.npc[(int)handLRef].ai[1] = npc.Center.Y + 75;
            }
            catch
            {
				handL = false;
				handR = false;
				CheckHands();
				
			}

			//Main.npc[(int)handRRef].ai[3] = 60f;


			//Main.npc[(int)handRRef].modProjectile as WatcherHandRight).linkPhase;
			//this is how you get it. idk why. i dont want to know why

			if (bulletHellColldown < bossTime)
			{

				if (bossTime % 420 == 0 && Main.rand.Next(10) <= 5)
				{

					Main.PlaySound(SoundID.Item8, Main.npc[(int)handLRef].Center);
					bossMode = 1;
				}
				if (bossTime % 1100 == 0 && Main.rand.Next(9) <= 4)
				{

					Main.PlaySound(SoundID.Item11, Main.npc[(int)handLRef].Center);
					bossMode = 2;
				}
			}

			//bullet passes
			if(bulletHellColldown < bossTime)
            {
				if (cooldownTimer <= bossTime)
				{
					if (bossTime % 55 == 0 && Main.rand.Next(14) <= 5)
					{
						ProjectileHelper.Shoot3TrackedProjAtPlayer(npc, ModContent.ProjectileType<WatcherBolt>(), 20, 7f, npc.damage, Main.npc[(int)handRRef].Center, Main.player[npc.target].Center, player);
						Main.PlaySound(SoundID.Item46, Main.npc[(int)handRRef].Center); // fwsh
					}
					if (bossTime % 66 == 0 && Main.rand.Next(15) <= 5)
					{
						ProjectileHelper.Shoot3TrackedProjAtPlayer(npc, ModContent.ProjectileType<WatcherBolt>(), 20, 7f, npc.damage, Main.npc[(int)handLRef].Center, Main.player[npc.target].Center, player);
						Main.PlaySound(SoundID.Item46, Main.npc[(int)handLRef].Center); // fwsh					
					}
				}

			}


			/*if (bossTime == 60 * 5)
			{
				bossMode = 1;
			}*/
		}
		private void Phase2AI()
        {
			Player player = Main.player[npc.target];
			npc.dontTakeDamage = true;
			npc.velocity *= 0.99f;

			(Main.npc[(int)handRRef].modNPC as WatcherHandRight).setLinkPhase(true);
			(Main.npc[(int)handLRef].modNPC as WatcherHandLeft).setLinkPhase(true);



			GetAlpha(Color.White);
			
			Main.npc[(int)handRRef].ai[0] = npc.Center.X + 150;
			Main.npc[(int)handRRef].ai[1] = npc.Center.Y + 75;

			Main.npc[(int)handLRef].ai[0] = npc.Center.X - 150;
			Main.npc[(int)handLRef].ai[1] = npc.Center.Y + 75;

			(Main.npc[(int)handLRef].modNPC as WatcherHandLeft).colOn = false;
			(Main.npc[(int)handRRef].modNPC as WatcherHandRight).colOn = false;


			if (alphaSet == 0 && tpSet == 1)
			{

				for (int k = 0; k <1; k++)
				{
					Random a = new Random();
					if (!dustActive)
					{
						Main.PlaySound(SoundID.MaxMana);
						Projectile.NewProjectile((teleportCoords), new Vector2(0,0), ModContent.ProjectileType<Spark>(),0,0,0);
						

						dustActive = true;
					}
				}
				if (bossTimestamp <= bossTime)
				{

					//re-enabling col here should allow for tp jump attack to work, but not before. hopefully.
					colPlayer = true;

					(Main.npc[(int)handRRef].modNPC as WatcherHandRight).setCol(true);
					(Main.npc[(int)handLRef].modNPC as WatcherHandLeft).setCol(true);
					if (npc.Center.Y - 1 > Main.maxTilesY)
					{
						npc.Center = teleportCoords;

					}
					else
					{
						npc.Center = new Vector2(npc.Center.X, npc.Center.Y + 3);
					}
					npc.Center = teleportCoords;



					Main.npc[(int)handRRef].Center = npc.Center + new Vector2(150, 75);
					Main.npc[(int)handLRef].Center = npc.Center + new Vector2(-150, 75);
					alphaSet = 100;
					GetAlpha(Color.White);
					for (int k = 0; k < 20; k++)
					{
						Random a = new Random();

						Main.PlaySound(SoundID.Item93, npc.position);

						Dust.NewDust(npc.Center, npc.width, npc.height, DustID.DungeonSpirit, (float)((a.Next() % 100) / 100), (float)((a.Next() % 100) / 100), 0, default, 1.5f);   //spawns dust behind it, this is a spectral light blue dust. 15 is the dust, change that to what you want.




					}


					for (int k = 0; k < 6; k++)
					{
						Random a = new Random();

						Main.PlaySound(SoundID.Item93, Main.npc[(int)handLRef].Center);

						Dust.NewDust(Main.npc[(int)handLRef].Center, Main.npc[(int)handLRef].width, Main.npc[(int)handLRef].height, DustID.Clentaminator_Red, (float)((a.Next() % 100) / 100), (float)((a.Next() % 100) / 100), 0, default, 1.5f);   //spawns dust behind it, this is a spectral light blue dust. 15 is the dust, change that to what you want.

					}

					for (int k = 0; k < 6; k++)
					{
						Random a = new Random();

						Main.PlaySound(SoundID.Item93, Main.npc[(int)handRRef].Center);

						Dust.NewDust(Main.npc[(int)handRRef].Center, Main.npc[(int)handRRef].width, Main.npc[(int)handLRef].height, DustID.Clentaminator_Red, (float)((a.Next() % 100) / 100), (float)((a.Next() % 100) / 100), 0, default, 1.5f);   //spawns dust behind it, this is a spectral light blue dust. 15 is the dust, change that to what you want.


					}
					ProjectileHelper.Shoot3TrackedProjAtPlayer(npc, ModContent.ProjectileType<WatcherBolt>(), 40, 7f, npc.damage, npc.Center, Main.player[npc.target].Center, player);
					ProjectileHelper.Shoot3TrackedProjAtPlayer(npc, ModContent.ProjectileType<WatcherBolt>(), 80, 9f, npc.damage, npc.Center, Main.player[npc.target].Center, player);
					ProjectileHelper.Shoot3TrackedProjAtPlayer(npc, ModContent.ProjectileType<WatcherBolt>(), 120, 11f, npc.damage, npc.Center, Main.player[npc.target].Center, player);
					ProjectileHelper.Shoot3TrackedProjAtPlayer(npc, ModContent.ProjectileType<WatcherBolt>(), 160, 13f, npc.damage, npc.Center, Main.player[npc.target].Center, player);

					tpSet = 0;
					bossMode = 0;
					dustActive = false;
					cooldownTimer = bossTime + bulletCooldownDefault;

				}

			}

			if (alphaSet > 0)
			{
				alphaSet--;
				colPlayer = false;

				(Main.npc[(int)handRRef].modNPC as WatcherHandRight).setCol(false);
				(Main.npc[(int)handLRef].modNPC as WatcherHandLeft).setCol(false);
			}

			if (alphaSet == 0 && tpSet == 0)
			{
				teleportCoords = player.Center;
				tpSet = 1;
				bossTimestamp = bossTime + (55);
				Main.PlaySound(SoundID.Item93, npc.position);

			}
		}
		private void Phase3AI()
        {
			Player player = Main.player[npc.target];
			(Main.npc[(int)handRRef].modNPC as WatcherHandRight).setLinkPhase(false);
			(Main.npc[(int)handLRef].modNPC as WatcherHandLeft).setLinkPhase(false);
			
			if (setupLaser == 0)
			{


				//player.AddBuff(BuffID.Slow, 60 * bulletHellSec, true);
				npc.Center = player.Center + player.velocity*30f + new Vector2(0, -400);
				Main.npc[(int)handRRef].Center = npc.Center + new Vector2(550, 75);
				Main.npc[(int)handLRef].Center = npc.Center + new Vector2(-550, 75);
				Main.npc[(int)handLRef].velocity *= 0;

				Main.npc[(int)handRRef].velocity *= 0;
				handRBeam = Projectile.NewProjectile(Main.npc[(int)handRRef].Center, new Vector2(0, 0f), ModContent.ProjectileType<LargeWatcherBeam>(), npc.damage, 10f, player.whoAmI);
				handLBeam = Projectile.NewProjectile(Main.npc[(int)handLRef].Center, new Vector2(0, 0f), ModContent.ProjectileType<LargeWatcherBeam>(), npc.damage, 10f, player.whoAmI);
				(Main.projectile[(int)handLBeam]).ai[1] = handLRef;
				(Main.projectile[(int)handRBeam]).ai[1] = handRRef;



				npc.velocity *= 0;
				bossTimestamp = bossTime + 60 * bulletHellSec;//multiply by phase length in seconds (1 is 1 sec)
				setupLaser = 1;



			}

			Main.projectile[handLBeam].Center = Main.npc[(int)handLRef].Center + new Vector2(5, 50);
			Main.projectile[handRBeam].Center = Main.npc[(int)handRRef].Center + new Vector2(-5, 50);
		


			if (bossTimestamp <= bossTime && setupLaser == 1)
			{
				Main.projectile[handRBeam].Kill();
				Main.projectile[handLBeam].Kill();
				bossMode = 0;
				bulletHellColldown = bossTime + 180;
				setupLaser = 0;
				(Main.npc[(int)handRRef].modNPC as WatcherHandRight).setLinkPhase(true);
				(Main.npc[(int)handLRef].modNPC as WatcherHandLeft).setLinkPhase(true);

				Main.npc[(int)handRRef].velocity = new Vector2(3f, 3f);
				Main.npc[(int)handLRef].velocity = new Vector2(3f, 3f);
			}

			if (setupLaser == 1 && bossTime % 2 == 0)
			{
				Main.npc[(int)handRRef].Center = Main.npc[(int)handRRef].Center + new Vector2(-1, 0);
				Main.npc[(int)handLRef].Center = Main.npc[(int)handLRef].Center + new Vector2(1, 0);
			}

			if (setupLaser == 1 && bossTime % 33 == 0)
			{
				ProjectileHelper.Shoot3TrackedProjAtPlayer(npc, ModContent.ProjectileType<WatcherBolt>(), 25, 5f, npc.damage, npc.Center, Main.player[npc.target].Center, player);
				ProjectileHelper.Shoot3TrackedProjAtPlayer(npc, ModContent.ProjectileType<WatcherBolt>(), 45, 7f, npc.damage, npc.Center, Main.player[npc.target].Center, player);

			}
			


		}
		private void PhaseDeathAI()
        {

			if (Main.netMode != NetmodeID.Server && !Filters.Scene["Shockwave"].IsActive())
			{
				Filters.Scene.Activate("Shockwave", npc.Center).GetShader().UseColor(1, 3, 10).UseTargetPosition(npc.Center);
			}



			if (Main.netMode != NetmodeID.Server && Filters.Scene["Shockwave"].IsActive())
			{
				//Main.NewText(finaltimer);
				float progress = (180f - finaltimer) / 60f; // Will range from -3 to 3, 0 being the point where the bomb explodes.
				Filters.Scene["Shockwave"].GetShader().UseProgress(progress).UseOpacity(300f * (1 - progress / 3f));
			}
			finaltimer--;
			if (finaltimer <= 0)
			{
				Filters.Scene["Shockwave"].Deactivate();
			}


			//this is the only way i could stop it from warping to 0,0
			//todo: fuck me
			if (!isDeathPosStored && deathTimer1 >= 90)
			{
				//fucking get this shit outta here
				//for unknown reasons, killing the projectile DOES NOT WORK?
				/*	for(int i = 0; i < Main.projectile.Length - 1; i++)
					{
						if(Main.projectile[i].type == ModContent.ProjectileType<LargeWatcherBeam>())
						{
							Main.projectile[i].Kill();
							(Main.projectile[i].modProjectile as LargeWatcherBeam).alive = false;
							Main.NewText("minos prime says: die");
						}
					}*/
				//i'm retarded


				npc.Center = Main.player[0].Center + new Vector2(100, -250);

				Projectile.NewProjectile((npc.Center), new Vector2(0, 0), ModContent.ProjectileType<Spark>(), 0, 0, 0);
				

				

				


				for (int k = 0; k < 25; k++)
				{
					Random a = new Random();


					Dust.NewDust(npc.position, npc.width, npc.height, DustID.DungeonSpirit, (float)((a.Next() % 100) / 100), (float)((a.Next() % 100) / 100), 0, default, 1.5f);   //spawns dust behind it, this is a spectral light blue dust. 15 is the dust, change that to what you want.


				}

				deathPos = npc.position;
				Main.PlaySound(SoundID.NPCHit5, npc.position);

				isDeathPosStored = true;
				
			}
			if (deathTimer1 % 10 == 0) Dust.NewDust(npc.position, npc.width, npc.height, DustID.DungeonSpirit, (float)((Main.rand.Next(101) % 100) / 100), (float)((Main.rand.Next(101) % 100) / 100), 0, default, 1.5f);   //spawns dust behind it, this is a spectral light blue dust. 15 is the dust, change that to what you want.
            try
            {
				if (deathTimer1 == 0 && setupLaser == 1)
				{
					(Main.projectile[(int)handLBeam].modProjectile as LargeWatcherBeam).alive = false;
					(Main.projectile[(int)handRBeam].modProjectile as LargeWatcherBeam).alive = false;
				}
            }
            catch
            {
				ModContent.GetInstance<ProjectPhoenix>().Logger.Debug("Silently caught exception: beamRef not set. This should not happen.");

			}


			deathTimer1++;
			if (deathTimer1 >= 95)
			{

				//start shaking
				if (deathTimer1 % 3 == 0)
					npc.Center = npc.Center + new Vector2(4f, 0);

				if (deathTimer1 % 5 == 0)
					npc.Center = npc.Center + new Vector2(-4f, 0);
				if (deathTimer1 % 7 == 0)
				{
					npc.position = deathPos;

				}



			}

			npc.dontTakeDamage = true;
			npc.damage = 0;
			npc.life = 1;
			if (deathTimer1 == 90)
			{
				for (int i = 0; i < 10; i++)
				{
					Dust.NewDust(npc.position, npc.width, npc.height, DustID.DungeonSpirit, (float)((Main.rand.Next(101) % 100) / 100), (float)((Main.rand.Next(101) % 100) / 100), 0, default, 1.5f);   //spawns dust behind it, this is a spectral light blue dust. 15 is the dust, change that to what you want.

				}
			}
			if (deathTimer1 == 180)
			{
				//Main.NewText("An unjust lust for violence.", Color.Goldenrod);
			}
			if (deathTimer1 == 300)
			{
				//CombatText.NewText(npc.getRect(), Color.Purple, "...");
				//PlayerMod player = (PlayerMod)mod.GetPlayer("MyPlayer");

				NewTextBox dialogue = new NewTextBox("Your time's ticking.", "ProjectPhoenix/UI/Port/Watcher/WatcherDefault", "Sounds/Custom/TalkSoundWatcher", true,Color.Red,true,false);
				dialogue.AddBox();
				



			}



			/*if (deathTimer1 == 340)
			{
				int text = CombatText.NewText(npc.getRect(), Color.Purple, "Your.", true, false);

			}
			if (deathTimer1 == 380)
			{
				int text = CombatText.NewText(npc.getRect(), Color.Purple, "time's.", true, false);

			}
			if (deathTimer1 == 420)
			{
				int text = CombatText.NewText(npc.getRect(), Color.Purple, "ticking.", true, false);


			}*/
			if(deathTimer1 >= 420)
            {
				
				
			}
			if (deathTimer1 == 420)
			{
				for (int k = 0; k < 82; k++)
				{
					Random a = new Random();


					Dust.NewDust(npc.position, npc.width, npc.height, DustID.DungeonSpirit, (float)((a.Next() % 100) / 100), (float)((a.Next() % 100) / 100), 0, default, 1.5f);   //spawns dust behind it, this is a spectral light blue dust. 15 is the dust, change that to what you want.


				}
				//CombatText.NewText(npc.getRect(), Color.Purple, "Your time's ticking.");
				Main.PlaySound(SoundID.NPCDeath7, npc.position);

				Item.NewItem(npc.Center, ItemID.PlatinumCoin, 1);

				npc.active = false;

				
			}
		}
		private void StartDespawn()
        {
			Vector2 desVel = new Vector2(0f, -0.05f);

			Player player = Main.player[npc.target];
			if (!player.active || player.dead)
			{
				Main.npc[(int)handRRef].Center = npc.Center + new Vector2(150, 75);

				Main.npc[(int)handLRef].Center = npc.Center + new Vector2(-150, 75);

				Main.npc[(int)handLRef].timeLeft = 5;

				Main.npc[(int)handRRef].timeLeft = 5;

				Main.projectile[handRBeam].Kill();
				Main.projectile[handLBeam].Kill();


				npc.TargetClosest(false);
				npc.velocity = npc.velocity + desVel;
				if (npc.timeLeft == 1)
				{
					Main.npc[(int)handLRef].active = false;
					Main.npc[(int)handRRef].active = false;

				}

				if (npc.timeLeft > 180)
				{
					npc.timeLeft = 180;

				}

			}
		}
		private void CheckHands()
        {
			if (handL == false)
			{
				handLRef = NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, ModContent.NPCType<WatcherHandLeft>());
				handL = true;
			}
			if (handR == false)
			{
				handRRef = NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, ModContent.NPCType<WatcherHandRight>());
				handR = true;
			}
		}
		public override void AI()
		{
			try
			{
				

				if (npc.Center.Y + 5 < Main.maxTilesY)
				{
				}
				npc.TargetClosest();
				Player player = Main.player[npc.target];
				bossTime++;
				//Main.NewText(bossTime,default,true);
				//ai loop if players exist
				if (player.active && player.dead != true)
				{
					CheckHands();
					
					npc.timeLeft = 1000;
					//main loop
					if (bossMode == 0)//main loop
					{
						
						Phase1AI();
					}

					//teleport attack
					if (bossMode == 1) //teleport attack
					{ 
						Phase2AI();
					}

					//BULLET HELL
					if (bossMode == 2)
					{
						Phase3AI();
					}
					//death animation
					if (bossMode == 3)
					{
						
						PhaseDeathAI();
					}
				}
				else //ai loop for despawn with redundant check ig
				{
					StartDespawn();
				}
			}
			catch
			{
				handL = false;
				handR = false;
				CheckHands();
				ModContent.GetInstance<ProjectPhoenix>().Logger.Debug("Handcheck failed, respawning! This should not happen in normal gameplay!");


			}
		}
		public override bool CheckDead() //override death and start animation
		{
			npc.life = 1;
			npc.velocity *= 0;
			for (int k = 0; k < 42; k++)
			{
				Random a = new Random();

				Main.PlaySound(SoundID.Item93, npc.position);

				Dust.NewDust(npc.position, npc.width, npc.height, DustID.DungeonSpirit, (float)((a.Next() % 100) / 100), (float)((a.Next() % 100) / 100), 0, default, 1.5f);   //spawns dust behind it, this is a spectral light blue dust. 15 is the dust, change that to what you want.


			}
			Main.npc[(int)handLRef].active =false;

			Main.npc[(int)handRRef].active = false;
			bossMode = 3;
			return false;

		}
		public override bool CanHitPlayer(Player target, ref int cooldownSlot) //override col for tp 

		{

			return colPlayer;
		}
		public override void OnHitPlayer(Player player, int dmgDealt, bool crit) // not sure why this still exists but ok. functional
		{
			if (Main.expertMode || Main.rand.NextBool())
			{
				int debuff = GetDebuff();
				//npc.velocity *= 0; //this was kept over from Doobie Ralsei's ai. why the fuck was it enabled 
				//over **(*9 versions*** after the port???? what the fuck????? keeping this bc wtf
				if (debuff >= 0)
				{
					player.AddBuff(debuff, GetDebuffTime(), true);
				}
			}
		}
		public int GetDebuff() //could be cut
		{

			return BuffID.OnFire;

		}
		public int GetDebuffTime() //see above
		{
			int time;
			time = 300;
			return time;
		}
		public override Color? GetAlpha(Color drawColor) //for transparency
		{
			return drawColor * (alphaSet / 100);

		}
	}
}