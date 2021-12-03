using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectPhoenix.Items;
using ProjectPhoenix.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ProjectPhoenix.Buffs;
using ProjectPhoenix.Items.Weapons.Melee;
using ProjectPhoenix.UI;
using Terraria.GameContent.Events;

namespace ProjectPhoenix.NPCs
{
    class PhoenixGlobalNPC : GlobalNPC
    {

        public bool stun;
        private int aiold;
        private bool rotLock = false;
        private int frameCounter;
        private int spriteDir;
        private float rotLockPos;
        bool setAI;



        public override void NPCLoot(NPC npc)
        {
            if (npc.type == NPCID.Skeleton && Main.hardMode == false)
            {
                if (Main.expertMode) if (Main.rand.Next(200) == 2) Item.NewItem(npc.getRect(), ModContent.ItemType<Dawnbroken>(), 1);
                    else if (Main.rand.Next(100) == 2 && Main.expertMode) Item.NewItem(npc.getRect(), ModContent.ItemType<Dawnbroken>(), 1);

            }
            // Addtional if statements here if you would like to add drops to other vanilla npc.
        }
        public override bool InstancePerEntity => true;
        public override void SetDefaults(NPC npc)
        {
           /* if (npc.townNPC)
            {
                npc.defense *= 10;
                npc.damage *= 4;
                npc.lifeRegen *= 2;
                npc.lifeMax *= 2;
                if (npc.lifeMax > 400)
                {
                    npc.lifeMax = 400;
                }
            }*/
            if (npc.type == NPCID.WyvernHead || npc.type == NPCID.MoonLordCore || npc.type == NPCID.MoonLordHand || npc.type == NPCID.MoonLordHead || npc.type == NPCID.MoonLordFreeEye || npc.type == NPCID.Golem || npc.type == NPCID.GolemFistLeft || npc.type == NPCID.GolemFistRight || npc.type == NPCID.GolemHead || npc.type == NPCID.GolemHeadFree || npc.type == NPCID.WallofFleshEye)
            {
                npc.buffImmune[ModContent.BuffType<Buffs.Stunned>()] = true;

            }

        }
        public override bool PreAI(NPC npc)
        {
				
            if (npc.townNPC && npc.aiStyle == 7)
            {


				/*Behavior Documentation:
				 *By default runs back and forth, counting to 0 and randomly resetting. 
				 *Fun fact: to make them move, talk to them, sets timer to 300, wait for move and talk again
				 * ai[0]: State of NPC. 0 for stand, 1 for walk. Set to 0 when talked to. State of '8' spotted near enemies. Also '12' (attack?)
				 * ai[1]: Decrementing timer. When hits 0, change state. Set to 300 when talked to.
				 * ai[2]: ??? Changes when an enemy is near, remains for some frames, returns to 0. Values seen to be -0.1rep or 0.1 ish. Possibly rotation?
				 * ai[3]:
				 * 
				 * 
				 */


				/*LocalAI seems to be same length
				 * [0]:
				 * [1]:
				 * [2]: Has been seen to switch to '8' when enemy nearby
				 * [3]: Has been seen set up to 3k? Set to 99 on talk. Has been seen to switch to '7' when enemy nearby, -1 by default, flickered to 0 on being hit? Stayed at 0 while shooting. Seen to count up after shooting?
				 */
				//Chekc LocalAI now
				//Time to test this shit 
				//Before return false, set npc.ai[0] to 12
				/* 12: Locked in attack pose
				 * 8: just kinda runs
				 * 
				 */
				//Next, try and cut attack delay for the guide in state 12
				//Watching phase 12 to see if timer is re-used
				//Code is shockingly similar to watcher AI

				//Results inconclusive


				for(int i = 0; i < npc.ai.Length; i++)
                {
					//Main.NewText("Ai Array " + i+ " is currently:"+npc.ai[i]);

				}
				//end user init
				int maxValue;
				maxValue = 300;
				bool safeToWalkOut;
				safeToWalkOut = Main.raining;
				if (!Main.dayTime)
				{
					safeToWalkOut = true;
				}
				if (Main.eclipse)
				{
					safeToWalkOut = true;
				}
				if (Main.slimeRain)
				{
					safeToWalkOut = true;
				}
				float npcPower;
				npcPower = 1f;
				if (Main.expertMode)
				{
					npc.defense = (npc.dryadWard ? (npc.defDefense + 10) : npc.defDefense);
				}
				else
				{
					npc.defense = (npc.dryadWard ? (npc.defDefense + 6) : npc.defDefense);
				}
				if (npc.townNPC || npc.type == NPCID.SkeletonMerchant)
				{
					if (NPC.downedBoss1)
					{
						npcPower += 0.1f;
						npc.defense += 3;
					}
					if (NPC.downedBoss2)
					{
						npcPower += 0.1f;
						npc.defense += 3;
					}
					if (NPC.downedBoss3)
					{
						npcPower += 0.1f;
						npc.defense += 3;
					}
					if (NPC.downedQueenBee)
					{
						npcPower += 0.1f;
						npc.defense += 3;
					}
					if (Main.hardMode)
					{
						npcPower += 0.4f;
						npc.defense += 12;
					}
					if (NPC.downedMechBoss1)
					{
						npcPower += 0.15f;
						npc.defense += 6;
					}
					if (NPC.downedMechBoss2)
					{
						npcPower += 0.15f;
						npc.defense += 6;
					}
					if (NPC.downedMechBoss3)
					{
						npcPower += 0.15f;
						npc.defense += 6;
					}
					if (NPC.downedPlantBoss)
					{
						npcPower += 0.15f;
						npc.defense += 8;
					}
					if (NPC.downedGolemBoss)
					{
						npcPower += 0.15f;
						npc.defense += 8;
					}
					if (NPC.downedAncientCultist)
					{
						npcPower += 0.15f;
						npc.defense += 8;
					}
					NPCLoader.BuffTownNPC(ref npcPower, ref npc.defense);
				}
				if (npc.type == NPCID.SantaClaus && Main.netMode != NetmodeID.MultiplayerClient && !Main.xMas)
				{
					npc.StrikeNPCNoInteraction(9999, 0f, 0);
					if (Main.netMode == NetmodeID.Server)
					{
						NetMessage.SendData(MessageID.StrikeNPC, -1, -1, null, npc.whoAmI, 9999f);
					}
				}
				
				if (npc.type == NPCID.Mechanic)
				{
					bool thrownWrench;
					thrownWrench = false;
					for (int i = 0; i < 1000; i++)
					{
						if (Main.projectile[i].active && Main.projectile[i].type == 582 && Main.projectile[i].ai[1] == (float)npc.whoAmI)
						{
							thrownWrench = true;
							break;
						}
					}
					npc.localAI[0] = thrownWrench.ToInt();
				}
				
				switch (npc.type)
				{
					case NPCID.TaxCollector:
						NPC.savedTaxCollector = true;
						break;
					case NPCID.GoblinTinkerer:
						NPC.savedGoblin = true;
						break;
					case NPCID.Wizard:
						NPC.savedWizard = true;
						break;
					case NPCID.Mechanic:
						NPC.savedMech = true;
						break;
					case NPCID.Stylist:
						NPC.savedStylist = true;
						break;
					case NPCID.Angler:
						NPC.savedAngler = true;
						break;
					case NPCID.DD2Bartender:
						NPC.savedBartender = true;
						break;
				}
				if (npc.type >= NPCID.None && NPCID.Sets.TownCritter[npc.type] && npc.target == 255)
				{
					npc.TargetClosest();
					if (npc.position.X < Main.player[npc.target].position.X)
					{
						npc.direction = 1;
						npc.spriteDirection = npc.direction;
					}
					if (npc.position.X > Main.player[npc.target].position.X)
					{
						npc.direction = -1;
						npc.spriteDirection = npc.direction;
					}
					if (npc.homeTileX == -1)
					{
						npc.homeTileX = (int)((npc.position.X + (float)(npc.width / 2)) / 16f);
					}
				}
				else if (npc.homeTileX == -1 && npc.homeTileY == -1 && npc.velocity.Y == 0f)
				{
					npc.homeTileX = (int)npc.Center.X / 16;
					npc.homeTileY = (int)(npc.position.Y + (float)npc.height + 4f) / 16;
				}
				bool talkFlag;
				talkFlag = false;
				int homeTileY; //WHY J WHYYYYYYYYYYYYYYY wait its ok
				homeTileY = npc.homeTileY;
				if (npc.type == NPCID.TaxCollector)
				{
					NPC.taxCollector = true;
				}
				npc.directionY = -1;
				if (npc.direction == 0)
				{
					npc.direction = 1;
				}
				for (int k = 0; k < 255; k++)
				{
					if (Main.player[k].active && Main.player[k].talkNPC == npc.whoAmI)
					{
						talkFlag = true;
						if (npc.ai[0] != 0f)
						{
							npc.netUpdate = true;
						}
						npc.ai[0] = 0f;
						npc.ai[1] = 300f;
						npc.localAI[3] = 100f;
						//confirmed
						if (Main.player[k].position.X + (float)(Main.player[k].width / 2) < npc.position.X + (float)(npc.width / 2))
						{
							npc.direction = -1;
						}
						else
						{
							npc.direction = 1;
						}
						//face player when talking
					}
				}
				if (npc.ai[3] == 1f)
				{
					//if ai[3] is set to 1, they just fucking die?
					//i guess to get rid of 'em like during christmas
					//same with oldman
					npc.life = -1;
					npc.HitEffect();
					npc.active = false;
					npc.netUpdate = true;
					if (npc.type == NPCID.OldMan)
					{
						Main.PlaySound(SoundID.Roar, (int)npc.position.X, (int)npc.position.Y, 0);
					}
					//return;
				}
				if (npc.type == NPCID.OldMan && Main.netMode != NetmodeID.MultiplayerClient)
				{
					npc.homeless = false;
					npc.homeTileX = Main.dungeonX;
					npc.homeTileY = Main.dungeonY;
					if (NPC.downedBoss3)
					{
						npc.ai[3] = 1f;
						npc.netUpdate = true;
					}
				}
				if (Main.netMode != NetmodeID.MultiplayerClient && npc.homeTileY > 0)
				{
					for (; !WorldGen.SolidTile(npc.homeTileX, homeTileY) && homeTileY < Main.maxTilesY - 20; homeTileY++)
					{
					}
				}
				if (npc.type == NPCID.TravellingMerchant)
				{
					npc.homeless = true;
					if (!Main.dayTime)
					{
						npc.homeTileX = (int)(npc.Center.X / 16f);
						npc.homeTileY = (int)(npc.position.Y + (float)npc.height + 2f) / 16;
						if (!talkFlag && npc.ai[0] == 0f)
						{
							npc.ai[0] = 1f;
							npc.ai[1] = 200f;
						}
						safeToWalkOut = false;
					}
				}
				if (npc.type == NPCID.Angler && npc.homeless && npc.wet)
				{
					if (npc.Center.X / 16f < 380f || npc.Center.X / 16f > (float)(Main.maxTilesX - 380))
					{
						npc.homeTileX = Main.spawnTileX;
						npc.homeTileY = Main.spawnTileY;
						npc.ai[0] = 1f;
						npc.ai[1] = 200f;
					}
					if (npc.position.X / 16f < 200f)
					{
						npc.direction = 1;
					}
					else if (npc.position.X / 16f > (float)(Main.maxTilesX - 200))
					{
						npc.direction = -1;
					}
				}
				//above is some geenral NPC logic for not-moving-in types, and some general busywork
				int XPosAndWidth;
				XPosAndWidth = (int)(npc.position.X + (float)(npc.width / 2)) / 16;
				int XYPosAndHeight;
				XYPosAndHeight = (int)(npc.position.Y + (float)npc.height + 1f) / 16;
				if (!WorldGen.InWorld(XPosAndWidth, XYPosAndHeight) || Main.tile[XPosAndWidth, XYPosAndHeight] == null)
				{
					ProjectPhoenix.Instance.Logger.Info("Couldn't find a valid spawn location and this adapted code couldn't return properly. Shouldn't cause any issues... in thoery.");
					//return;
				}
				if (!npc.homeless && Main.netMode != NetmodeID.MultiplayerClient && npc.townNPC && (safeToWalkOut || Main.tileDungeon[Main.tile[XPosAndWidth, XYPosAndHeight].type]) && (XPosAndWidth != npc.homeTileX || XYPosAndHeight != homeTileY))
				{
					bool homelessCheck;
					homelessCheck = true;
					for (int l = 0; l < 2; l++)
					{
						Rectangle rectangle;
						rectangle = new Rectangle((int)(npc.position.X + (float)(npc.width / 2) - (float)(NPC.sWidth / 2) - (float)NPC.safeRangeX), (int)(npc.position.Y + (float)(npc.height / 2) - (float)(NPC.sHeight / 2) - (float)NPC.safeRangeY), NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
						if (l == 1)
						{
							rectangle = new Rectangle(npc.homeTileX * 16 + 8 - NPC.sWidth / 2 - NPC.safeRangeX, homeTileY * 16 + 8 - NPC.sHeight / 2 - NPC.safeRangeY, NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
						}
						for (int m = 0; m < 255; m++)
						{
							if (Main.player[m].active && new Rectangle((int)Main.player[m].position.X, (int)Main.player[m].position.Y, Main.player[m].width, Main.player[m].height).Intersects(rectangle))
							{
								homelessCheck = false;
								break;
							}
							if (!homelessCheck)
							{
								break;
							}
						}
					}
					if (homelessCheck)
					{
						if (npc.type == NPCID.OldMan || !Collision.SolidTiles(npc.homeTileX - 1, npc.homeTileX + 1, homeTileY - 3, homeTileY - 1))
						{
							npc.velocity.X = 0f;
							npc.velocity.Y = 0f;
							npc.position.X = npc.homeTileX * 16 + 8 - npc.width / 2;
							npc.position.Y = (float)(homeTileY * 16 - npc.height) - 0.1f;
							npc.netUpdate = true;
						}
						else
						{
							npc.homeless = true;
							WorldGen.QuickFindHome(npc.whoAmI);
						}
					}
				}
				bool flag17;
				flag17 = false;// npc.type == NPCID.Mouse || npc.type == NPCID.GoldMouse;
				//not needed but i am scared to delete it as it broke shit last time
				//classic terraria code 
				float dangerRanger;
				dangerRanger = 200f;
				if (NPCID.Sets.DangerDetectRange[npc.type] != -1)
				{
					dangerRanger = NPCID.Sets.DangerDetectRange[npc.type];
				}
				dangerRanger *= 3;
				bool fleeState;
				fleeState = false;
				bool altFlee;
				altFlee = false;
				float npcID;
				npcID = -1f;
				float compareID;
				compareID = -1f;
				int direction;
				direction = 0;
				int yetAnotherNpcId;
				yetAnotherNpcId = -1;
				int moreNpcID;
				moreNpcID = -1;
				if (Main.netMode != NetmodeID.MultiplayerClient && !talkFlag)
				{
					for (int n = 0; n < 200; n++)
					{
						if (!Main.npc[n].active)
						{
							continue;
						}
						bool? modCanHit;
						modCanHit = NPCLoader.CanHitNPC(Main.npc[n], npc);
						if (modCanHit.HasValue && !modCanHit.Value)
						{
							continue;
						}
						bool canHitVal;
						canHitVal = modCanHit.HasValue && modCanHit.Value;
						if (Main.npc[n].active && !Main.npc[n].friendly && Main.npc[n].damage > 0 && Main.npc[n].Distance(npc.Center) < dangerRanger && (npc.type != NPCID.SkeletonMerchant || !NPCID.Sets.Skeletons.Contains(Main.npc[n].netID) || canHitVal))
						{
							//start running
							fleeState = true;
							float num2;
							num2 = Main.npc[n].Center.X - npc.Center.X;
							if (num2 < 0f && (npcID == -1f || num2 > npcID))
							{
								npcID = num2;
								yetAnotherNpcId = n;
							}
							if (num2 > 0f && (compareID == -1f || num2 < compareID))
							{
								compareID = num2;
								moreNpcID = n;
							}
						}
					}
					if (fleeState)
					{
						direction = ((npcID == -1f) ? 1 : ((compareID != -1f) ? (compareID < 0f - npcID).ToDirectionInt() : (-1)));
						float num6;
						num6 = 0f;
						if (npcID != -1f)
						{
							num6 = 0f - npcID;
						}
						if (num6 == 0f || (compareID < num6 && compareID > 0f))
						{
							num6 = compareID;
						}
						if (npc.ai[0] == 8f)
						{
							if (npc.direction == -direction)
							{
								npc.ai[0] = 1f;
								npc.ai[1] = 300 + Main.rand.Next(300);
								npc.ai[2] = 0f;
								npc.localAI[3] = 0f;
								npc.netUpdate = true;
							}
						}
						else if (npc.ai[0] != 10f && npc.ai[0] != 12f && npc.ai[0] != 13f && npc.ai[0] != 14f && npc.ai[0] != 15f)
						{
							//huh?
							//i guess ai[0]...? not sure.
							if (NPCID.Sets.PrettySafe[npc.type] != -1 && (float)NPCID.Sets.PrettySafe[npc.type] < num6)
							{
								fleeState = false;
								altFlee = true;
							}
							else if (npc.ai[0] != 1f)
							{
								if (npc.ai[0] == 3f || npc.ai[0] == 4f || npc.ai[0] == 16f || npc.ai[0] == 17f)
								{
									NPC nPC;
									nPC = Main.npc[(int)npc.ai[2]];
									if (nPC.active)
									{
										nPC.ai[0] = 1f;
										nPC.ai[1] = 120 + Main.rand.Next(120);
										nPC.ai[2] = 0f;
										nPC.localAI[3] = 0f;
										nPC.direction = -direction;
										nPC.netUpdate = true;
									}
								}
								npc.ai[0] = 1f;
								npc.ai[1] = 120 + Main.rand.Next(120);
								npc.ai[2] = 0f;
								npc.localAI[3] = 0f;
								npc.direction = -direction;
								npc.netUpdate = true;
							}
							else if (npc.ai[0] == 1f && npc.direction != -direction)
							{
								npc.direction = -direction;
								npc.netUpdate = true;
							}
						}
					}
				}
				//walk logic - only during safe ig
				//state 0 = stand still
				if (npc.ai[0] == 0f)
				{
					if (npc.localAI[3] > 0f)
					{
						npc.localAI[3] -= 1f;
					}
					if (safeToWalkOut && !talkFlag && !NPCID.Sets.TownCritter[npc.type])
					{
						if (Main.netMode != NetmodeID.MultiplayerClient)
						{
							if (XPosAndWidth == npc.homeTileX && XYPosAndHeight == homeTileY)
							{
								if (npc.velocity.X != 0f)
								{
									npc.netUpdate = true;
								}
								if (npc.velocity.X > 0.1f)
								{
									npc.velocity.X -= 0.1f;
								}
								else if (npc.velocity.X < -0.1f)
								{
									npc.velocity.X += 0.1f;
								}
								else
								{
									npc.velocity.X = 0f;
								}
							}
							else
							{
								if (XPosAndWidth > npc.homeTileX)
								{
									npc.direction = -1;
								}
								else
								{
									npc.direction = 1;
								}
								npc.ai[0] = 1f;
								npc.ai[1] = 200 + Main.rand.Next(200);
								npc.ai[2] = 0f;
								npc.localAI[3] = 0f;
								npc.netUpdate = true;
							}
						}
					}
					else
					{
						if (flag17)
						{
							npc.velocity.X *= 0.5f;
						}
						if (npc.velocity.X > 0.1f)
						{
							npc.velocity.X -= 0.1f;
						}
						else if (npc.velocity.X < -0.1f)
						{
							npc.velocity.X += 0.1f;
						}
						else
						{
							npc.velocity.X = 0f;
						}
						if (Main.netMode != NetmodeID.MultiplayerClient)
						{
							if (npc.ai[1] > 0f)
							{
								npc.ai[1] -= 1f;
							}
							if (npc.ai[1] <= 0f)
							{
								npc.ai[0] = 1f;
								npc.ai[1] = 200 + Main.rand.Next(300);
								npc.ai[2] = 0f;
								if (NPCID.Sets.TownCritter[npc.type])
								{
									npc.ai[1] += Main.rand.Next(200, 400);
								}
								npc.localAI[3] = 0f;
								npc.netUpdate = true;
							}
						}
					}
					if (Main.netMode != NetmodeID.MultiplayerClient && (!safeToWalkOut || (XPosAndWidth == npc.homeTileX && XYPosAndHeight == homeTileY)))
					{
						if (XPosAndWidth < npc.homeTileX - 25 || XPosAndWidth > npc.homeTileX + 25)
						{
							if (npc.localAI[3] == 0f)
							{
								if (XPosAndWidth < npc.homeTileX - 50 && npc.direction == -1)
								{
									npc.direction = 1;
									npc.netUpdate = true;
								}
								else if (XPosAndWidth > npc.homeTileX + 50 && npc.direction == 1)
								{
									npc.direction = -1;
									npc.netUpdate = true;
								}
							}
						}
						else if (Main.rand.Next(80) == 0 && npc.localAI[3] == 0f)
						{
							npc.localAI[3] = 200f;
							npc.direction *= -1;
							npc.netUpdate = true;
						}
					}
				}
				else if (npc.ai[0] == 1f)
					
				{
					if (Main.netMode != NetmodeID.MultiplayerClient && safeToWalkOut && XPosAndWidth == npc.homeTileX && XYPosAndHeight == npc.homeTileY && !NPCID.Sets.TownCritter[npc.type])
					{
						npc.ai[0] = 0f;
						npc.ai[1] = 200 + Main.rand.Next(200);
						npc.localAI[3] = 60f;
						npc.netUpdate = true;
					}
					else
					{
						bool flag20;
						flag20 = Collision.DrownCollision(npc.position, npc.width, npc.height, 1f);
						if (!flag20)
						{
							if (Main.netMode != NetmodeID.MultiplayerClient && !npc.homeless && !Main.tileDungeon[Main.tile[XPosAndWidth, XYPosAndHeight].type] && (XPosAndWidth < npc.homeTileX - 35 || XPosAndWidth > npc.homeTileX + 35))
							{
								if (npc.position.X < (float)(npc.homeTileX * 16) && npc.direction == -1)
								{
									npc.ai[1] -= 5f;
								}
								else if (npc.position.X > (float)(npc.homeTileX * 16) && npc.direction == 1)
								{
									npc.ai[1] -= 5f;
								}
							}
							npc.ai[1] -= 1f;
						}
						if (npc.ai[1] <= 0f)
						{
							npc.ai[0] = 0f;
							npc.ai[1] = 300 + Main.rand.Next(300);
							npc.ai[2] = 0f;
							if (NPCID.Sets.TownCritter[npc.type])
							{
								npc.ai[1] -= Main.rand.Next(100);
							}
							else
							{
								npc.ai[1] += Main.rand.Next(900);
							}
							npc.localAI[3] = 60f;
							npc.netUpdate = true;
						}
						if (npc.closeDoor && ((npc.position.X + (float)(npc.width / 2)) / 16f > (float)(npc.doorX + 2) || (npc.position.X + (float)(npc.width / 2)) / 16f < (float)(npc.doorX - 2)))
						{
							Tile tileSafely;
							tileSafely = Framing.GetTileSafely(npc.doorX, npc.doorY);
							if (TileLoader.CloseDoorID(tileSafely) >= 0)
							{
								if (WorldGen.CloseDoor(npc.doorX, npc.doorY))
								{
									npc.closeDoor = false;
									NetMessage.SendData(MessageID.ChangeDoor, -1, -1, null, 1, npc.doorX, npc.doorY, npc.direction);
								}
								if ((npc.position.X + (float)(npc.width / 2)) / 16f > (float)(npc.doorX + 4) || (npc.position.X + (float)(npc.width / 2)) / 16f < (float)(npc.doorX - 4) || (npc.position.Y + (float)(npc.height / 2)) / 16f > (float)(npc.doorY + 4) || (npc.position.Y + (float)(npc.height / 2)) / 16f < (float)(npc.doorY - 4))
								{
									npc.closeDoor = false;
								}
							}
							else if (tileSafely.type == 389)
							{
								if (WorldGen.ShiftTallGate(npc.doorX, npc.doorY, closing: true))
								{
									npc.closeDoor = false;
									NetMessage.SendData(MessageID.ChangeDoor, -1, -1, null, 5, npc.doorX, npc.doorY);
								}
								if ((npc.position.X + (float)(npc.width / 2)) / 16f > (float)(npc.doorX + 4) || (npc.position.X + (float)(npc.width / 2)) / 16f < (float)(npc.doorX - 4) || (npc.position.Y + (float)(npc.height / 2)) / 16f > (float)(npc.doorY + 4) || (npc.position.Y + (float)(npc.height / 2)) / 16f < (float)(npc.doorY - 4))
								{
									npc.closeDoor = false;
								}
							}
							else
							{
								npc.closeDoor = false;
							}
						}
						float num7;
						num7 = 1f;
						float num8;
						num8 = 0.07f;
						
						if (flag17)
						{
							num7 = 2f;
							num8 = 1f;
						}
						if (npc.friendly && (fleeState || flag20))
						{
							num7 = 1.5f;
							float num9;
							num9 = 1f - (float)npc.life / (float)npc.lifeMax;
							num7 += num9 * 0.9f;
							num8 = 0.1f;
						}
						if (npc.velocity.X < 0f - num7 || npc.velocity.X > num7)
						{
							if (npc.velocity.Y == 0f)
							{
								npc.velocity *= 0.8f;
							}
						}
						else if (npc.velocity.X < num7 && npc.direction == 1)
						{
							npc.velocity.X += num8;
							if (npc.velocity.X > num7)
							{
								npc.velocity.X = num7;
							}
						}
						else if (npc.velocity.X > 0f - num7 && npc.direction == -1)
						{
							npc.velocity.X -= num8;
							if (npc.velocity.X > num7)
							{
								npc.velocity.X = num7;
							}
						}
						bool holdsMatching;
						holdsMatching = true;
						if ((float)(npc.homeTileY * 16 - 32) > npc.position.Y)
						{
							holdsMatching = false;
						}
						if ((npc.direction == 1 && npc.position.Y + (float)(npc.width / 2) > (float)(npc.homeTileX * 16)) || (npc.direction == -1 && npc.position.Y + (float)(npc.width / 2) < (float)(npc.homeTileX * 16)))
						{
							holdsMatching = true;
						}
						if (npc.velocity.Y == 0f)
						{
							Collision.StepDown(ref npc.position, ref npc.velocity, npc.width, npc.height, ref npc.stepSpeed, ref npc.gfxOffY);
						}
						if (npc.velocity.Y >= 0f)
						{
							Collision.StepUp(ref npc.position, ref npc.velocity, npc.width, npc.height, ref npc.stepSpeed, ref npc.gfxOffY, 1, holdsMatching, 1);
						}
						if (npc.velocity.Y == 0f)
						{
							int num10;
							num10 = (int)((npc.position.X + (float)(npc.width / 2) + (float)(15 * npc.direction)) / 16f);
							int num11;
							num11 = (int)((npc.position.Y + (float)npc.height - 16f) / 16f);
							bool flag21;
							flag21 = false;
							bool flag2;
							flag2 = true;
							bool flag3;
							flag3 = XPosAndWidth >= npc.homeTileX - 35 && XPosAndWidth <= npc.homeTileX + 35;
							if (npc.townNPC && npc.ai[1] < 30f)
							{
								flag21 = !Utils.PlotTileLine(npc.Top, npc.Bottom, npc.width, DelegateMethods.SearchAvoidedByNPCs);
								if (!flag21)
								{
									Rectangle hitbox;
									hitbox = npc.Hitbox;
									hitbox.X -= 20;
									hitbox.Width += 40;
									for (int num12 = 0; num12 < 200; num12++)
									{
										if (Main.npc[num12].active && Main.npc[num12].friendly && num12 != npc.whoAmI && Main.npc[num12].velocity.X == 0f && hitbox.Intersects(Main.npc[num12].Hitbox))
										{
											flag21 = true;
											break;
										}
									}
								}
							}
							if (!flag21 && flag20)
							{
								flag21 = true;
							}
							if (flag2 && (NPCID.Sets.TownCritter[npc.type] || (!flag3 && npc.direction == Math.Sign(npc.homeTileX - XPosAndWidth))))
							{
								flag2 = false;
							}
							if (flag2)
							{
								int num13;
								num13 = 0;
								for (int num14 = -1; num14 <= 4; num14++)
								{
									Tile tileSafely2;
									tileSafely2 = Framing.GetTileSafely(num10 - npc.direction * num13, num11 + num14);
									if (tileSafely2.lava() && tileSafely2.liquid > 0)
									{
										flag2 = true;
										break;
									}
									if (tileSafely2.nactive() && Main.tileSolid[tileSafely2.type])
									{
										flag2 = false;
										break;
									}
								}
							}
							if (!flag2 && npc.wet)
							{
								bool flag4;
								flag4 = flag20;
								bool flag5;
								flag5 = false;
								if (!flag4)
								{
									flag5 = Collision.DrownCollision(npc.position + new Vector2(npc.width * npc.direction, 0f), npc.width, npc.height, 1f);
								}
								if ((flag5 || Collision.DrownCollision(npc.position + new Vector2(npc.width * npc.direction, npc.height * 2 - 16 - (flag4 ? 16 : 0)), npc.width, 16 + (flag4 ? 16 : 0), 1f)) && npc.localAI[3] <= 0f)
								{
									flag2 = true;
									npc.localAI[3] = 600f;
								}
							}
							if (npc.position.X == npc.localAI[3])
							{
								npc.direction *= -1;
								npc.netUpdate = true;
								npc.localAI[3] = 600f;
							}
							if (flag20)
							{
								if (npc.localAI[3] > 0f)
								{
									npc.localAI[3] -= 1f;
								}
							}
							else
							{
								npc.localAI[3] = -1f;
							}
							Tile tileSafely3;
							tileSafely3 = Framing.GetTileSafely(num10, num11);
							Tile tileSafely4;
							tileSafely4 = Framing.GetTileSafely(num10, num11 - 1);
							Tile tileSafely5;
							tileSafely5 = Framing.GetTileSafely(num10, num11 - 2);
							if (npc.townNPC && tileSafely5.nactive() && (TileLoader.OpenDoorID(tileSafely5) >= 0 || tileSafely5.type == 388) && (Main.rand.Next(10) == 0 || safeToWalkOut))
							{
								if (Main.netMode != NetmodeID.MultiplayerClient)
								{
									if (WorldGen.OpenDoor(num10, num11 - 2, npc.direction))
									{
										npc.closeDoor = true;
										npc.doorX = num10;
										npc.doorY = num11 - 2;
										NetMessage.SendData(MessageID.ChangeDoor, -1, -1, null, 0, num10, num11 - 2, npc.direction);
										npc.netUpdate = true;
										npc.ai[1] += 80f;
									}
									else if (WorldGen.OpenDoor(num10, num11 - 2, -npc.direction))
									{
										npc.closeDoor = true;
										npc.doorX = num10;
										npc.doorY = num11 - 2;
										NetMessage.SendData(MessageID.ChangeDoor, -1, -1, null, 0, num10, num11 - 2, -npc.direction);
										npc.netUpdate = true;
										npc.ai[1] += 80f;
									}
									else if (WorldGen.ShiftTallGate(num10, num11 - 2, closing: false))
									{
										npc.closeDoor = true;
										npc.doorX = num10;
										npc.doorY = num11 - 2;
										NetMessage.SendData(MessageID.ChangeDoor, -1, -1, null, 4, num10, num11 - 2);
										npc.netUpdate = true;
										npc.ai[1] += 80f;
									}
									else
									{
										npc.direction *= -1;
										npc.netUpdate = true;
									}
								}
							}
							else
							{
								if ((npc.velocity.X < 0f && npc.spriteDirection == -1) || (npc.velocity.X > 0f && npc.spriteDirection == 1))
								{
									if (tileSafely5.nactive() && Main.tileSolid[tileSafely5.type] && !Main.tileSolidTop[tileSafely5.type])
									{
										if (!Collision.SolidTilesVersatile(num10 - npc.direction * 2, num10 - npc.direction, num11 - 5, num11 - 1) && !Collision.SolidTiles(num10, num10, num11 - 5, num11 - 3))
										{
											npc.velocity.Y = -6f;
											npc.netUpdate = true;
										}
										else if (flag17)
										{
											if (WorldGen.SolidTile((int)(npc.Center.X / 16f) + npc.direction, (int)(npc.Center.Y / 16f)))
											{
												npc.direction *= -1;
												npc.velocity.X *= 0f;
												npc.netUpdate = true;
											}
										}
										else if (fleeState)
										{
											flag21 = false;
											npc.velocity.X = 0f;
											npc.direction *= -1;
											npc.netUpdate = true;
											npc.ai[0] = 8f;
											npc.ai[1] = 240f;
										}
										else
										{
											npc.direction *= -1;
											npc.netUpdate = true;
										}
									}
									else if (tileSafely4.nactive() && Main.tileSolid[tileSafely4.type] && !Main.tileSolidTop[tileSafely4.type])
									{
										if (!Collision.SolidTilesVersatile(num10 - npc.direction * 2, num10 - npc.direction, num11 - 4, num11 - 1) && !Collision.SolidTiles(num10, num10, num11 - 4, num11 - 2))
										{
											npc.velocity.Y = -5f;
											npc.netUpdate = true;
										}
										else if (fleeState)
										{
											flag21 = false;
											npc.velocity.X = 0f;
											npc.direction *= -1;
											npc.netUpdate = true;
											npc.ai[0] = 8f;
											npc.ai[1] = 240f;
										}
										else
										{
											npc.direction *= -1;
											npc.netUpdate = true;
										}
									}
									else if (npc.position.Y + (float)npc.height - (float)(num11 * 16) > 20f && tileSafely3.nactive() && Main.tileSolid[tileSafely3.type] && !tileSafely3.topSlope())
									{
										if (!Collision.SolidTilesVersatile(num10 - npc.direction * 2, num10, num11 - 3, num11 - 1))
										{
											npc.velocity.Y = -4.4f;
											npc.netUpdate = true;
										}
										else if (fleeState)
										{
											flag21 = false;
											npc.velocity.X = 0f;
											npc.direction *= -1;
											npc.netUpdate = true;
											npc.ai[0] = 8f;
											npc.ai[1] = 240f;
										}
										else
										{
											npc.direction *= -1;
											npc.netUpdate = true;
										}
									}
									else if (flag2)
									{
										npc.direction *= -1;
										npc.velocity.X *= -1f;
										npc.netUpdate = true;
										if (fleeState)
										{
											flag21 = false;
											npc.velocity.X = 0f;
											npc.ai[0] = 8f;
											npc.ai[1] = 240f;
										}
									}
									if (flag21)
									{
										npc.ai[1] = 90f;
										npc.netUpdate = true;
									}
									if (npc.velocity.Y < 0f)
									{
										npc.localAI[3] = npc.position.X;
									}
								}
								if (npc.velocity.Y < 0f && npc.wet)
								{
									npc.velocity.Y *= 1.2f;
								}
								if (npc.velocity.Y < 0f && NPCID.Sets.TownCritter[npc.type] && !flag17)
								{
									npc.velocity.Y *= 1.2f;
								}
							}
						}
					}
				}
				else if (npc.ai[0] == 2f || npc.ai[0] == 11f)
				{
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						npc.localAI[3] -= 1f;
						if (Main.rand.Next(60) == 0 && npc.localAI[3] == 0f)
						{
							npc.localAI[3] = 60f;
							npc.direction *= -1;
							npc.netUpdate = true;
						}
					}
					npc.ai[1] -= 1f;
					npc.velocity.X *= 0.8f;
					if (npc.ai[1] <= 0f)
					{
						npc.localAI[3] = 40f;
						npc.ai[0] = 0f;
						npc.ai[1] = 60 + Main.rand.Next(60);
						npc.netUpdate = true;
					}
				}
				else if (npc.ai[0] == 3f || npc.ai[0] == 4f || npc.ai[0] == 5f || npc.ai[0] == 8f || npc.ai[0] == 9f || npc.ai[0] == 16f || npc.ai[0] == 17f)
				{
					npc.velocity.X *= 0.8f;
					npc.ai[1] -= 1f;
					if (npc.ai[0] == 8f && npc.ai[1] < 60f && fleeState)
					{
						npc.ai[1] = 180f;
						npc.netUpdate = true;
					}
					if (npc.ai[0] == 5f)
					{
						Point point;
						point = npc.Center.ToTileCoordinates();
						if (Main.tile[point.X, point.Y].type != 15)
						{
							npc.ai[1] = 0f;
						}
					}
					if (npc.ai[1] <= 0f)
					{
						npc.ai[0] = 0f;
						npc.ai[1] = 60 + Main.rand.Next(60);
						npc.ai[2] = 0f;
						npc.localAI[3] = 30 + Main.rand.Next(60);
						npc.netUpdate = true;
					}
				}
				else if (npc.ai[0] == 6f || npc.ai[0] == 7f || npc.ai[0] == 18f)
				{
					if (npc.ai[0] == 18f && (npc.localAI[3] < 1f || npc.localAI[3] > 2f))
					{
						npc.localAI[3] = 2f;
					}
					npc.velocity.X *= 0.8f;
					npc.ai[1] -= 1f;
					int num16;
					num16 = (int)npc.ai[2];
					if (num16 < 0 || num16 > 255 || !Main.player[num16].active || Main.player[num16].dead || Main.player[num16].Distance(npc.Center) > 200f || !Collision.CanHitLine(npc.Top, 0, 0, Main.player[num16].Top, 0, 0))
					{
						npc.ai[1] = 0f;
					}
					if (npc.ai[1] > 0f)
					{
						int num17;
						num17 = ((npc.Center.X < Main.player[num16].Center.X) ? 1 : (-1));
						if (num17 != npc.direction)
						{
							npc.netUpdate = true;
						}
						npc.direction = num17;
					}
					else
					{
						npc.ai[0] = 0f;
						npc.ai[1] = 60 + Main.rand.Next(60);
						npc.ai[2] = 0f;
						npc.localAI[3] = 30 + Main.rand.Next(60);
						npc.netUpdate = true;
					}
				}
				else if (npc.ai[0] == 10f)
				{
					int npcProjectileID;
					npcProjectileID = 0;
					int npcDamage;
					npcDamage = 0;
					float knockBack;
					knockBack = 0f;
					float npcProjSpeedMultiplier;
					npcProjSpeedMultiplier = 0f;
					int npcAttackDelay;
					npcAttackDelay = 0;
					int npcCooldown;
					npcCooldown = 0;
					int maxValue2;
					maxValue2 = 0;
					float gravityCorrection;
					gravityCorrection = 0f;
					float num24;
					num24 = NPCID.Sets.DangerDetectRange[npc.type];
					float randomOffset;
					randomOffset = 0f;
					if ((float)NPCID.Sets.AttackTime[npc.type] == npc.ai[1])
					{
						npc.frameCounter = 0.0;
						npc.localAI[3] = 0f;
					}
					if (npc.type == NPCID.Demolitionist)
					{
						npcProjectileID = 30;
						npcProjSpeedMultiplier = 6f;
						npcDamage = 20;
						npcAttackDelay = 10;
						npcCooldown = 180;
						maxValue2 = 120;
						gravityCorrection = 16f;
						knockBack = 7f;
					}
					else if (npc.type == NPCID.DD2Bartender)
					{
						npcProjectileID = 669;
						npcProjSpeedMultiplier = 6f;
						npcDamage = 24;
						npcAttackDelay = 10;
						npcCooldown = 120;
						maxValue2 = 60;
						gravityCorrection = 16f;
						knockBack = 9f;
					}
					else if (npc.type == NPCID.PartyGirl)
					{
						npcProjectileID = 588;
						npcProjSpeedMultiplier = 6f;
						npcDamage = 30;
						npcAttackDelay = 10;
						npcCooldown = 60;
						maxValue2 = 120;
						gravityCorrection = 16f;
						knockBack = 6f;
					}
					else if (npc.type == NPCID.Merchant)
					{
						npcProjectileID = 48;
						npcProjSpeedMultiplier = 9f;
						npcDamage = 12;
						npcAttackDelay = 10;
						npcCooldown = 60;
						maxValue2 = 60;
						gravityCorrection = 16f;
						knockBack = 1.5f;
					}
					else if (npc.type == NPCID.Angler)
					{
						npcProjectileID = 520;
						npcProjSpeedMultiplier = 12f;
						npcDamage = 10;
						npcAttackDelay = 10;
						npcCooldown = 0;
						maxValue2 = 1;
						gravityCorrection = 16f;
						knockBack = 3f;
					}
					else if (npc.type == NPCID.SkeletonMerchant)
					{
						npcProjectileID = 21;
						npcProjSpeedMultiplier = 14f;
						npcDamage = 14;
						npcAttackDelay = 10;
						npcCooldown = 0;
						maxValue2 = 1;
						gravityCorrection = 16f;
						knockBack = 3f;
					}
					else if (npc.type == NPCID.GoblinTinkerer)
					{
						npcProjectileID = 24;
						npcProjSpeedMultiplier = 5f;
						npcDamage = 15;
						npcAttackDelay = 10;
						npcCooldown = 60;
						maxValue2 = 60;
						gravityCorrection = 16f;
						knockBack = 1f;
					}
					else if (npc.type == NPCID.Mechanic)
					{
						npcProjectileID = 582;
						npcProjSpeedMultiplier = 10f;
						npcDamage = 11;
						npcAttackDelay = 1;
						npcCooldown = 30;
						maxValue2 = 30;
						knockBack = 3.5f;
					}
					else if (npc.type == NPCID.Nurse)
					{
						npcProjectileID = 583;
						npcProjSpeedMultiplier = 8f;
						npcDamage = 8;
						npcAttackDelay = 1;
						npcCooldown = 15;
						maxValue2 = 10;
						knockBack = 2f;
						gravityCorrection = 10f;
					}
					else if (npc.type == NPCID.SantaClaus)
					{
						npcProjectileID = 589;
						npcProjSpeedMultiplier = 7f;
						npcDamage = 22;
						npcAttackDelay = 1;
						npcCooldown = 10;
						maxValue2 = 1;
						knockBack = 2f;
						gravityCorrection = 10f;
					}
					NPCLoader.TownNPCAttackStrength(npc, ref npcDamage, ref knockBack);
					NPCLoader.TownNPCAttackCooldown(npc, ref npcCooldown, ref maxValue2);
					NPCLoader.TownNPCAttackProj(npc, ref npcProjectileID, ref npcAttackDelay);
					NPCLoader.TownNPCAttackProjSpeed(npc, ref npcProjSpeedMultiplier, ref gravityCorrection, ref randomOffset);
					if (Main.expertMode)
					{
						npcDamage = (int)((float)npcDamage * Main.expertNPCDamage);
					}
					npcDamage = (int)((float)npcDamage * npcPower);
					npc.velocity.X *= 0.8f;
					npc.ai[1] -= 1f;
					npc.localAI[3] += 1f;
					if (npc.localAI[3] == (float)npcAttackDelay && Main.netMode != NetmodeID.MultiplayerClient)
					{
						Vector2 vec;
						vec = -Vector2.UnitY;
						if (direction == 1 && npc.spriteDirection == 1 && moreNpcID != -1)
						{
							vec = npc.DirectionTo(Main.npc[moreNpcID].Center + new Vector2(0f, (0f - gravityCorrection) * MathHelper.Clamp(npc.Distance(Main.npc[moreNpcID].Center) / num24, 0f, 1f)));
						}
						if (direction == -1 && npc.spriteDirection == -1 && yetAnotherNpcId != -1)
						{
							vec = npc.DirectionTo(Main.npc[yetAnotherNpcId].Center + new Vector2(0f, (0f - gravityCorrection) * MathHelper.Clamp(npc.Distance(Main.npc[yetAnotherNpcId].Center) / num24, 0f, 1f)));
						}
						if (vec.HasNaNs() || Math.Sign(vec.X) != npc.spriteDirection)
						{
							vec = new Vector2(npc.spriteDirection, -1f);
						}
						vec *= npcProjSpeedMultiplier;
						vec += Utils.RandomVector2(Main.rand, 0f - randomOffset, randomOffset);
						int num27;
						num27 = 1000;
						num27 = ((npc.type == NPCID.Mechanic) ? Projectile.NewProjectile(npc.Center.X + (float)(npc.spriteDirection * 16), npc.Center.Y - 2f, vec.X, vec.Y, npcProjectileID, npcDamage, knockBack, Main.myPlayer, 0f, npc.whoAmI) : ((npc.type != NPCID.SantaClaus) ? Projectile.NewProjectile(npc.Center.X + (float)(npc.spriteDirection * 16), npc.Center.Y - 2f, vec.X, vec.Y, npcProjectileID, npcDamage, knockBack, Main.myPlayer) : Projectile.NewProjectile(npc.Center.X + (float)(npc.spriteDirection * 16), npc.Center.Y - 2f, vec.X, vec.Y, npcProjectileID, npcDamage, knockBack, Main.myPlayer, 0f, Main.rand.Next(5))));
						Main.projectile[num27].npcProj = true;
						Main.projectile[num27].noDropItem = true;
					}
					if (npc.ai[1] <= 0f)
					{
						npc.ai[0] = ((npc.localAI[2] == 8f && fleeState) ? 8 : 0);
						npc.ai[1] = npcCooldown + Main.rand.Next(maxValue2);
						npc.ai[2] = 0f;
						npc.localAI[1] = (npc.localAI[3] = npcCooldown / 2 + Main.rand.Next(maxValue2));
						npc.netUpdate = true;
					}
				}
				else if (npc.ai[0] == 12f)
				{
					int num28;
					num28 = 0;
					int num29;
					num29 = 0;
					float num30;
					num30 = 0f;
					int num31;
					num31 = 0;
					int num32;
					num32 = 0;
					int maxValue3;
					maxValue3 = 0;
					float knockBack2;
					knockBack2 = 0f;
					float num33;
					num33 = 0f;
					bool flag6;
					flag6 = false;
					float num34;
					num34 = 0f;
					if ((float)NPCID.Sets.AttackTime[npc.type] == npc.ai[1])
					{
						npc.frameCounter = 0.0;
						npc.localAI[3] = 0f;
					}
					int num35;
					num35 = -1;
					if (direction == 1 && npc.spriteDirection == 1)
					{
						num35 = moreNpcID;
					}
					if (direction == -1 && npc.spriteDirection == -1)
					{
						num35 = yetAnotherNpcId;
					}
					if (npc.type == NPCID.ArmsDealer)
					{
						num28 = 14;
						num30 = 13f;
						num29 = 24;
						num32 = 14;
						maxValue3 = 4;
						knockBack2 = 3f;
						num31 = 1;
						num34 = 0.5f;
						if ((float)NPCID.Sets.AttackTime[npc.type] == npc.ai[1])
						{
							npc.frameCounter = 0.0;
							npc.localAI[3] = 0f;
						}
						if (Main.hardMode)
						{
							num29 = 15;
							if (npc.localAI[3] > (float)num31)
							{
								num31 = 10;
								flag6 = true;
							}
							if (npc.localAI[3] > (float)num31)
							{
								num31 = 20;
								flag6 = true;
							}
							if (npc.localAI[3] > (float)num31)
							{
								num31 = 30;
								flag6 = true;
							}
						}
					}
					else if (npc.type == NPCID.Painter)
					{
						num28 = 587;
						num30 = 10f;
						num29 = 8;
						num32 = 10;
						maxValue3 = 1;
						knockBack2 = 1.75f;
						num31 = 1;
						num34 = 0.5f;
						if (npc.localAI[3] > (float)num31)
						{
							num31 = 12;
							flag6 = true;
						}
						if (npc.localAI[3] > (float)num31)
						{
							num31 = 24;
							flag6 = true;
						}
						if (Main.hardMode)
						{
							num29 += 2;
						}
					}
					else if (npc.type == NPCID.TravellingMerchant)
					{
						num28 = 14;
						num30 = 13f;
						num29 = 24;
						num32 = 12;
						maxValue3 = 5;
						knockBack2 = 2f;
						num31 = 1;
						num34 = 0.2f;
						if (Main.hardMode)
						{
							num29 = 30;
							num28 = 357;
						}
					}
					else if (npc.type == NPCID.Guide)
					{
						num30 = 10f;
						num29 = 8;
						num31 = 1;
						if (Main.hardMode)
						{
							num28 = 2;
							num32 = 15;
							maxValue3 = 10;
							num29 += 6;
						}
						else
						{
							num28 = 1;
							num32 = 30;
							maxValue3 = 20;
						}
						knockBack2 = 2.75f;
						num33 = 4f;
						num34 = 0.7f;
					}
					else if (npc.type == NPCID.WitchDoctor)
					{
						num28 = 267;
						num30 = 14f;
						num29 = 20;
						num31 = 1;
						num32 = 10;
						maxValue3 = 1;
						knockBack2 = 3f;
						num33 = 6f;
						num34 = 0.4f;
					}
					else if (npc.type == NPCID.Steampunker)
					{
						num28 = 242;
						num30 = 13f;
						num29 = 15;
						num32 = 10;
						maxValue3 = 1;
						knockBack2 = 2f;
						num31 = 1;
						if (npc.localAI[3] > (float)num31)
						{
							num31 = 8;
							flag6 = true;
						}
						if (npc.localAI[3] > (float)num31)
						{
							num31 = 16;
							flag6 = true;
						}
						num34 = 0.3f;
					}
					else if (npc.type == NPCID.Pirate)
					{
						num28 = 14;
						num30 = 14f;
						num29 = 24;
						num32 = 10;
						maxValue3 = 1;
						knockBack2 = 2f;
						num31 = 1;
						num34 = 0.7f;
						if (npc.localAI[3] > (float)num31)
						{
							num31 = 16;
							flag6 = true;
						}
						if (npc.localAI[3] > (float)num31)
						{
							num31 = 24;
							flag6 = true;
						}
						if (npc.localAI[3] > (float)num31)
						{
							num31 = 32;
							flag6 = true;
						}
						if (npc.localAI[3] > (float)num31)
						{
							num31 = 40;
							flag6 = true;
						}
						if (npc.localAI[3] > (float)num31)
						{
							num31 = 48;
							flag6 = true;
						}
						if (npc.localAI[3] == 0f && num35 != -1 && npc.Distance(Main.npc[num35].Center) < (float)NPCID.Sets.PrettySafe[npc.type])
						{
							num34 = 0.1f;
							num28 = 162;
							num29 = 50;
							knockBack2 = 10f;
							num30 = 24f;
						}
					}
					else if (npc.type == NPCID.Cyborg)
					{
						num28 = Utils.SelectRandom<int>(Main.rand, 134, 133, 135);
						num31 = 1;
						switch (num28)
						{
							case 135:
								num30 = 12f;
								num29 = 30;
								num32 = 30;
								maxValue3 = 10;
								knockBack2 = 7f;
								num34 = 0.2f;
								break;
							case 133:
								num30 = 10f;
								num29 = 25;
								num32 = 10;
								maxValue3 = 1;
								knockBack2 = 6f;
								num34 = 0.2f;
								break;
							case 134:
								num30 = 13f;
								num29 = 20;
								num32 = 20;
								maxValue3 = 10;
								knockBack2 = 4f;
								num34 = 0.1f;
								break;
						}
					}
					NPCLoader.TownNPCAttackStrength(npc, ref num29, ref knockBack2);
					NPCLoader.TownNPCAttackCooldown(npc, ref num32, ref maxValue3);
					NPCLoader.TownNPCAttackProj(npc, ref num28, ref num31);
					NPCLoader.TownNPCAttackProjSpeed(npc, ref num30, ref num33, ref num34);
					NPCLoader.TownNPCAttackShoot(npc, ref flag6);
					if (Main.expertMode)
					{
						num29 = (int)((float)num29 * Main.expertNPCDamage);
					}
					num29 = (int)((float)num29 * npcPower);
					npc.velocity.X *= 0.8f;
					npc.ai[1] -= 1f;
					npc.localAI[3] += 1f;
					if (npc.localAI[3] == (float)num31 && Main.netMode != NetmodeID.MultiplayerClient)
					{
						Vector2 vec2;
						vec2 = Vector2.Zero;
						if (num35 != -1)
						{
							vec2 = npc.DirectionTo(Main.npc[num35].Center + new Vector2(0f, 0f - num33));
						}
						if (vec2.HasNaNs() || Math.Sign(vec2.X) != npc.spriteDirection)
						{
							vec2 = new Vector2(npc.spriteDirection, 0f);
						}
						vec2 *= num30;
						vec2 += Utils.RandomVector2(Main.rand, 0f - num34, num34);
						int num36;
						num36 = 1000;
						num36 = ((npc.type != NPCID.Painter) ? Projectile.NewProjectile(npc.Center.X + (float)(npc.spriteDirection * 16), npc.Center.Y - 2f, vec2.X, vec2.Y, num28, num29, knockBack2, Main.myPlayer) : Projectile.NewProjectile(npc.Center.X + (float)(npc.spriteDirection * 16), npc.Center.Y - 2f, vec2.X, vec2.Y, num28, num29, knockBack2, Main.myPlayer, 0f, (float)Main.rand.Next(12) / 6f));
						Main.projectile[num36].npcProj = true;
						Main.projectile[num36].noDropItem = true;
					}
					if (npc.localAI[3] == (float)num31 && flag6 && num35 != -1)
					{
						Vector2 vector;
						vector = npc.DirectionTo(Main.npc[num35].Center);
						if (vector.Y <= 0.5f && vector.Y >= -0.5f)
						{
							npc.ai[2] = vector.Y;
						}
					}
					if (npc.ai[1] <= 0f)
					{
						npc.ai[0] = ((npc.localAI[2] == 8f && fleeState) ? 8 : 0);
						npc.ai[1] = num32 + Main.rand.Next(maxValue3);
						npc.ai[2] = 0f;
						npc.localAI[1] = (npc.localAI[3] = num32 / 2 + Main.rand.Next(maxValue3));
						npc.netUpdate = true;
					}
				}
				else if (npc.ai[0] == 13f)
				{
					npc.velocity.X *= 0.8f;
					if ((float)NPCID.Sets.AttackTime[npc.type] == npc.ai[1])
					{
						npc.frameCounter = 0.0;
					}
					npc.ai[1] -= 1f;
					npc.localAI[3] += 1f;
					if (npc.localAI[3] == 1f && Main.netMode != NetmodeID.MultiplayerClient)
					{
						Vector2 vec3;
						vec3 = npc.DirectionTo(Main.npc[(int)npc.ai[2]].Center + new Vector2(0f, -20f));
						if (vec3.HasNaNs() || Math.Sign(vec3.X) == -npc.spriteDirection)
						{
							vec3 = new Vector2(npc.spriteDirection, -1f);
						}
						vec3 *= 8f;
						int num38;
						num38 = Projectile.NewProjectile(npc.Center.X + (float)(npc.spriteDirection * 16), npc.Center.Y - 2f, vec3.X, vec3.Y, ProjectileID.NurseSyringeHeal, 0, 0f, Main.myPlayer, npc.ai[2]);
						Main.projectile[num38].npcProj = true;
						Main.projectile[num38].noDropItem = true;
					}
					if (npc.ai[1] <= 0f)
					{
						npc.ai[0] = 0f;
						npc.ai[1] = 10 + Main.rand.Next(10);
						npc.ai[2] = 0f;
						npc.localAI[3] = 5 + Main.rand.Next(10);
						npc.netUpdate = true;
					}
				}
				else if (npc.ai[0] == 14f)
				{
					int num39;
					num39 = 0;
					int num40;
					num40 = 0;
					float num41;
					num41 = 0f;
					int num42;
					num42 = 0;
					int num43;
					num43 = 0;
					int maxValue4;
					maxValue4 = 0;
					float knockBack3;
					knockBack3 = 0f;
					float num44;
					num44 = 0f;
					float num45;
					num45 = NPCID.Sets.DangerDetectRange[npc.type];
					float num46;
					num46 = 1f;
					float num47;
					num47 = 0f;
					if ((float)NPCID.Sets.AttackTime[npc.type] == npc.ai[1])
					{
						npc.frameCounter = 0.0;
						npc.localAI[3] = 0f;
					}
					int num49;
					num49 = -1;
					if (direction == 1 && npc.spriteDirection == 1)
					{
						num49 = moreNpcID;
					}
					if (direction == -1 && npc.spriteDirection == -1)
					{
						num49 = yetAnotherNpcId;
					}
					if (npc.type == NPCID.Clothier)
					{
						num39 = 585;
						num41 = 10f;
						num40 = 16;
						num42 = 30;
						num43 = 20;
						maxValue4 = 15;
						knockBack3 = 2f;
						num47 = 1f;
					}
					else if (npc.type == NPCID.Wizard)
					{
						num39 = 15;
						num41 = 6f;
						num40 = 18;
						num42 = 15;
						num43 = 15;
						maxValue4 = 5;
						knockBack3 = 3f;
						num44 = 20f;
					}
					else if (npc.type == NPCID.Truffle)
					{
						num39 = 590;
						num40 = 40;
						num42 = 15;
						num43 = 10;
						maxValue4 = 1;
						knockBack3 = 3f;
						for (; npc.localAI[3] > (float)num42; num42 += 15)
						{
						}
					}
					else if (npc.type == NPCID.Dryad)
					{
						num39 = 586;
						num42 = 24;
						num43 = 10;
						maxValue4 = 1;
						knockBack3 = 3f;
					}
					NPCLoader.TownNPCAttackStrength(npc, ref num40, ref knockBack3);
					NPCLoader.TownNPCAttackCooldown(npc, ref num43, ref maxValue4);
					NPCLoader.TownNPCAttackProj(npc, ref num39, ref num42);
					NPCLoader.TownNPCAttackProjSpeed(npc, ref num41, ref num44, ref num47);
					NPCLoader.TownNPCAttackMagic(npc, ref num46);
					if (Main.expertMode)
					{
						num40 = (int)((float)num40 * Main.expertNPCDamage);
					}
					num40 = (int)((float)num40 * npcPower);
					npc.velocity.X *= 0.8f;
					npc.ai[1] -= 1f;
					npc.localAI[3] += 1f;
					if (npc.localAI[3] == (float)num42 && Main.netMode != NetmodeID.MultiplayerClient)
					{
						Vector2 vec4;
						vec4 = Vector2.Zero;
						if (num49 != -1)
						{
							vec4 = npc.DirectionTo(Main.npc[num49].Center + new Vector2(0f, (0f - num44) * MathHelper.Clamp(npc.Distance(Main.npc[num49].Center) / num45, 0f, 1f)));
						}
						if (vec4.HasNaNs() || Math.Sign(vec4.X) != npc.spriteDirection)
						{
							vec4 = new Vector2(npc.spriteDirection, 0f);
						}
						vec4 *= num41;
						vec4 += Utils.RandomVector2(Main.rand, 0f - num47, num47);
						if (npc.type == NPCID.Wizard)
						{
							int num50;
							num50 = Utils.SelectRandom<int>(Main.rand, 1, 1, 1, 1, 2, 2, 3);
							for (int num51 = 0; num51 < num50; num51++)
							{
								Vector2 vector2;
								vector2 = Utils.RandomVector2(Main.rand, -3.4f, 3.4f);
								int num52;
								num52 = Projectile.NewProjectile(npc.Center.X + (float)(npc.spriteDirection * 16), npc.Center.Y - 2f, vec4.X + vector2.X, vec4.Y + vector2.Y, num39, num40, knockBack3, Main.myPlayer);
								Main.projectile[num52].npcProj = true;
								Main.projectile[num52].noDropItem = true;
							}
						}
						else if (npc.type == NPCID.Truffle)
						{
							if (num49 != -1)
							{
								Vector2 vector3;
								vector3 = Main.npc[num49].position - Main.npc[num49].Size * 2f + Main.npc[num49].Size * Utils.RandomVector2(Main.rand, 0f, 1f) * 5f;
								int num53;
								num53 = 10;
								while (num53 > 0 && WorldGen.SolidTile(Framing.GetTileSafely((int)vector3.X / 16, (int)vector3.Y / 16)))
								{
									num53--;
									vector3 = Main.npc[num49].position - Main.npc[num49].Size * 2f + Main.npc[num49].Size * Utils.RandomVector2(Main.rand, 0f, 1f) * 5f;
								}
								int num54;
								num54 = Projectile.NewProjectile(vector3.X, vector3.Y, 0f, 0f, num39, num40, knockBack3, Main.myPlayer);
								Main.projectile[num54].npcProj = true;
								Main.projectile[num54].noDropItem = true;
							}
						}
						else if (npc.type == NPCID.Dryad)
						{
							int num55;
							num55 = Projectile.NewProjectile(npc.Center.X + (float)(npc.spriteDirection * 16), npc.Center.Y - 2f, vec4.X, vec4.Y, num39, num40, knockBack3, Main.myPlayer, 0f, npc.whoAmI);
							Main.projectile[num55].npcProj = true;
							Main.projectile[num55].noDropItem = true;
						}
						else
						{
							int num56;
							num56 = Projectile.NewProjectile(npc.Center.X + (float)(npc.spriteDirection * 16), npc.Center.Y - 2f, vec4.X, vec4.Y, num39, num40, knockBack3, Main.myPlayer);
							Main.projectile[num56].npcProj = true;
							Main.projectile[num56].noDropItem = true;
						}
					}
					if (num46 > 0f)
					{
						Vector3 vector4;
						vector4 = NPCID.Sets.MagicAuraColor[npc.type].ToVector3() * num46;
						Lighting.AddLight(npc.Center, vector4.X, vector4.Y, vector4.Z);
					}
					if (npc.ai[1] <= 0f)
					{
						npc.ai[0] = ((npc.localAI[2] == 8f && fleeState) ? 8 : 0);
						npc.ai[1] = num43 + Main.rand.Next(maxValue4);
						npc.ai[2] = 0f;
						npc.localAI[1] = (npc.localAI[3] = num43 / 2 + Main.rand.Next(maxValue4));
						npc.netUpdate = true;
					}
				}
				else if (npc.ai[0] == 15f)
				{
					int num57;
					num57 = 0;
					int maxValue5;
					maxValue5 = 0;
					if ((float)NPCID.Sets.AttackTime[npc.type] == npc.ai[1])
					{
						npc.frameCounter = 0.0;
						npc.localAI[3] = 0f;
					}
					int num58;
					num58 = 0;
					float num60;
					num60 = 0f;
					int num61;
					num61 = 0;
					int num62;
					num62 = 0;
					if (direction == 1)
					{
						_ = npc.spriteDirection;
					}
					if (direction == -1)
					{
						_ = npc.spriteDirection;
					}
					if (npc.type == NPCID.DyeTrader)
					{
						num58 = 11;
						num61 = (num62 = 32);
						num57 = 12;
						maxValue5 = 6;
						num60 = 4.25f;
					}
					else if (npc.type == NPCID.TaxCollector)
					{
						num58 = 9;
						num61 = (num62 = 28);
						num57 = 9;
						maxValue5 = 3;
						num60 = 3.5f;
					}
					else if (npc.type == NPCID.Stylist)
					{
						num58 = 10;
						num61 = (num62 = 32);
						num57 = 15;
						maxValue5 = 8;
						num60 = 5f;
					}
					NPCLoader.TownNPCAttackStrength(npc, ref num58, ref num60);
					NPCLoader.TownNPCAttackCooldown(npc, ref num57, ref maxValue5);
					NPCLoader.TownNPCAttackSwing(npc, ref num62, ref num61);
					if (Main.expertMode)
					{
						num58 = (int)((float)num58 * Main.expertNPCDamage);
					}
					num58 = (int)((float)num58 * npcPower);
					npc.velocity.X *= 0.8f;
					npc.ai[1] -= 1f;
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						Tuple<Vector2, float> swingStats;
						swingStats = npc.GetSwingStats(NPCID.Sets.AttackTime[npc.type] * 2, (int)npc.ai[1], npc.spriteDirection, num61, num62);
						Rectangle itemRectangle;
						itemRectangle = new Rectangle((int)swingStats.Item1.X, (int)swingStats.Item1.Y, num61, num62);
						if (npc.spriteDirection == -1)
						{
							itemRectangle.X -= num61;
						}
						itemRectangle.Y -= num62;
						npc.TweakSwingStats(NPCID.Sets.AttackTime[npc.type] * 2, (int)npc.ai[1], npc.spriteDirection, ref itemRectangle);
						int myPlayer;
						myPlayer = Main.myPlayer;
						for (int num63 = 0; num63 < 200; num63++)
						{
							NPC nPC2;
							nPC2 = Main.npc[num63];
							if (nPC2.active && nPC2.immune[myPlayer] == 0 && !nPC2.dontTakeDamage && !nPC2.friendly && nPC2.damage > 0 && itemRectangle.Intersects(nPC2.Hitbox) && (nPC2.noTileCollide || Collision.CanHit(npc.position, npc.width, npc.height, nPC2.position, nPC2.width, nPC2.height)))
							{
								nPC2.StrikeNPCNoInteraction(num58, num60, npc.spriteDirection);
								if (Main.netMode != NetmodeID.SinglePlayer)
								{
									NetMessage.SendData(MessageID.StrikeNPC, -1, -1, null, num63, num58, num60, npc.spriteDirection);
								}
								nPC2.netUpdate = true;
								nPC2.immune[myPlayer] = (int)npc.ai[1] + 2;
							}
						}
					}
					if (npc.ai[1] <= 0f)
					{
						bool flag7;
						flag7 = false;
						if (fleeState)
						{
							int num64;
							num64 = -direction;
							if (!Collision.CanHit(npc.Center, 0, 0, npc.Center + Vector2.UnitX * num64 * 32f, 0, 0) || npc.localAI[2] == 8f)
							{
								flag7 = true;
							}
							if (flag7)
							{
								int num65;
								num65 = NPCID.Sets.AttackTime[npc.type];
								int num66;
								num66 = ((direction == 1) ? moreNpcID : yetAnotherNpcId);
								int num67;
								num67 = ((direction == 1) ? yetAnotherNpcId : moreNpcID);
								if (num66 != -1 && !Collision.CanHit(npc.Center, 0, 0, Main.npc[num66].Center, 0, 0))
								{
									num66 = ((num67 == -1 || !Collision.CanHit(npc.Center, 0, 0, Main.npc[num67].Center, 0, 0)) ? (-1) : num67);
								}
								if (num66 != -1)
								{
									npc.ai[0] = 15f;
									npc.ai[1] = num65;
									npc.ai[2] = 0f;
									npc.localAI[3] = 0f;
									npc.direction = ((npc.position.X < Main.npc[num66].position.X) ? 1 : (-1));
									npc.netUpdate = true;
								}
								else
								{
									flag7 = false;
								}
							}
						}
						if (!flag7)
						{
							npc.ai[0] = ((npc.localAI[2] == 8f && fleeState) ? 8 : 0);
							npc.ai[1] = num57 + Main.rand.Next(maxValue5);
							npc.ai[2] = 0f;
							npc.localAI[1] = (npc.localAI[3] = num57 / 2 + Main.rand.Next(maxValue5));
							npc.netUpdate = true;
						}
					}
				}
				if (Main.netMode == NetmodeID.MultiplayerClient || (!npc.townNPC && npc.type != NPCID.SkeletonMerchant) || talkFlag)
				{
					//return;
				}
				bool flag8;
				flag8 = npc.ai[0] < 2f && !fleeState;
				bool flag9;
				flag9 = (npc.ai[0] < 2f || npc.ai[0] == 8f) && (fleeState || altFlee);
				if (npc.localAI[1] > 0f)
				{
					npc.localAI[1] -= 1f;
				}
				if (npc.localAI[1] > 0f)
				{
					flag9 = false;
				}
				if (flag9 && npc.type == NPCID.Mechanic && npc.localAI[0] == 1f)
				{
					flag9 = false;
				}
				if (flag9 && npc.type == NPCID.Dryad)
				{
					flag9 = false;
					for (int num68 = 0; num68 < 200; num68++)
					{
						NPC nPC3;
						nPC3 = Main.npc[num68];
						if (nPC3.active && nPC3.townNPC && !(npc.Distance(nPC3.Center) > 1200f) && nPC3.FindBuffIndex(165) == -1)
						{
							flag9 = true;
							break;
						}
					}
				}
				if (flag8 && npc.ai[0] == 0f && npc.velocity.Y == 0f && Main.rand.Next(300) == 0)
				{
					int num69;
					num69 = 420;
					num69 = ((Main.rand.Next(2) != 0) ? (num69 * Main.rand.Next(1, 3)) : (num69 * Main.rand.Next(1, 4)));
					int num71;
					num71 = 100;
					int num72;
					num72 = 20;
					for (int num73 = 0; num73 < 200; num73++)
					{
						NPC nPC4;
						nPC4 = Main.npc[num73];
						bool flag10;
						flag10 = (nPC4.ai[0] == 1f && nPC4.closeDoor) || (nPC4.ai[0] == 1f && nPC4.ai[1] > 200f) || nPC4.ai[0] > 1f;
						if (nPC4 != npc && nPC4.active && nPC4.CanTalk && !flag10 && nPC4.Distance(npc.Center) < (float)num71 && nPC4.Distance(npc.Center) > (float)num72 && Collision.CanHit(npc.Center, 0, 0, nPC4.Center, 0, 0))
						{
							int num74;
							num74 = (npc.position.X < nPC4.position.X).ToDirectionInt();
							npc.ai[0] = 3f;
							npc.ai[1] = num69;
							npc.ai[2] = num73;
							npc.direction = num74;
							npc.netUpdate = true;
							nPC4.ai[0] = 4f;
							nPC4.ai[1] = num69;
							nPC4.ai[2] = npc.whoAmI;
							nPC4.direction = -num74;
							nPC4.netUpdate = true;
							break;
						}
					}
				}
				else if (flag8 && npc.ai[0] == 0f && npc.velocity.Y == 0f && Main.rand.Next(1800) == 0)
				{
					int num75;
					num75 = 420;
					num75 = ((Main.rand.Next(2) != 0) ? (num75 * Main.rand.Next(1, 3)) : (num75 * Main.rand.Next(1, 4)));
					int num76;
					num76 = 100;
					int num77;
					num77 = 20;
					for (int num78 = 0; num78 < 200; num78++)
					{
						NPC nPC5;
						nPC5 = Main.npc[num78];
						bool flag11;
						flag11 = (nPC5.ai[0] == 1f && nPC5.closeDoor) || (nPC5.ai[0] == 1f && nPC5.ai[1] > 200f) || nPC5.ai[0] > 1f;
						if (nPC5 != npc && nPC5.active && nPC5.CanTalk && !flag11 && nPC5.Distance(npc.Center) < (float)num76 && nPC5.Distance(npc.Center) > (float)num77 && Collision.CanHit(npc.Center, 0, 0, nPC5.Center, 0, 0))
						{
							int num79;
							num79 = (npc.position.X < nPC5.position.X).ToDirectionInt();
							npc.ai[0] = 16f;
							npc.ai[1] = num75;
							npc.ai[2] = num78;
							npc.localAI[2] = Main.rand.Next(4);
							npc.localAI[3] = Main.rand.Next(3 - (int)npc.localAI[2]);
							npc.direction = num79;
							npc.netUpdate = true;
							nPC5.ai[0] = 17f;
							nPC5.ai[1] = num75;
							nPC5.ai[2] = npc.whoAmI;
							nPC5.localAI[2] = 0f;
							nPC5.localAI[3] = 0f;
							nPC5.direction = -num79;
							nPC5.netUpdate = true;
							break;
						}
					}
				}
				else if (flag8 && npc.ai[0] == 0f && npc.velocity.Y == 0f && Main.rand.Next(1200) == 0 && (npc.type == NPCID.PartyGirl || (BirthdayParty.PartyIsUp && NPCID.Sets.AttackType[npc.type] == NPCID.Sets.AttackType[208])))
				{
					int num80;
					num80 = 300;
					int num82;
					num82 = 150;
					for (int num83 = 0; num83 < 255; num83++)
					{
						Player player;
						player = Main.player[num83];
						if (player.active && !player.dead && player.Distance(npc.Center) < (float)num82 && Collision.CanHitLine(npc.Top, 0, 0, player.Top, 0, 0))
						{
							int direction2;
							direction2 = (npc.position.X < player.position.X).ToDirectionInt();
							npc.ai[0] = 6f;
							npc.ai[1] = num80;
							npc.ai[2] = num83;
							npc.direction = direction2;
							npc.netUpdate = true;
							break;
						}
					}
				}
				else if (flag8 && npc.ai[0] == 0f && npc.velocity.Y == 0f && Main.rand.Next(600) == 0 && npc.type == NPCID.DD2Bartender)
				{
					int num84;
					num84 = 300;
					int num85;
					num85 = 150;
					for (int num86 = 0; num86 < 255; num86++)
					{
						Player player2;
						player2 = Main.player[num86];
						if (player2.active && !player2.dead && player2.Distance(npc.Center) < (float)num85 && Collision.CanHitLine(npc.Top, 0, 0, player2.Top, 0, 0))
						{
							int direction3;
							direction3 = (npc.position.X < player2.position.X).ToDirectionInt();
							npc.ai[0] = 18f;
							npc.ai[1] = num84;
							npc.ai[2] = num86;
							npc.direction = direction3;
							npc.netUpdate = true;
							break;
						}
					}
				}
				else if (flag8 && npc.ai[0] == 0f && npc.velocity.Y == 0f && Main.rand.Next(1800) == 0)
				{
					npc.ai[0] = 2f;
					npc.ai[1] = 45 * Main.rand.Next(1, 2);
					npc.netUpdate = true;
				}
				else if (flag8 && npc.ai[0] == 0f && npc.velocity.Y == 0f && Main.rand.Next(600) == 0 && npc.type == NPCID.Pirate && !altFlee)
				{
					npc.ai[0] = 11f;
					npc.ai[1] = 30 * Main.rand.Next(1, 4);
					npc.netUpdate = true;
				}
				else if (flag8 && npc.ai[0] == 0f && npc.velocity.Y == 0f && Main.rand.Next(1200) == 0)
				{
					int num87;
					num87 = 220;
					int num88;
					num88 = 150;
					for (int num89 = 0; num89 < 255; num89++)
					{
						Player player3;
						player3 = Main.player[num89];
						if (player3.active && !player3.dead && player3.Distance(npc.Center) < (float)num88 && Collision.CanHitLine(npc.Top, 0, 0, player3.Top, 0, 0))
						{
							int direction4;
							direction4 = (npc.position.X < player3.position.X).ToDirectionInt();
							npc.ai[0] = 7f;
							npc.ai[1] = num87;
							npc.ai[2] = num89;
							npc.direction = direction4;
							npc.netUpdate = true;
							break;
						}
					}
				}
				else if (flag8 && npc.ai[0] == 1f && npc.velocity.Y == 0f && Main.rand.Next(maxValue) == 0)
				{
					Point b;
					b = npc.Center.ToTileCoordinates();
					bool flag13;
					flag13 = WorldGen.InWorld(b.X, b.Y, 1);
					if (flag13)
					{
						for (int num90 = 0; num90 < 200; num90++)
						{
							if (Main.npc[num90].active && Main.npc[num90].aiStyle == 7 && Main.npc[num90].townNPC && Main.npc[num90].ai[0] == 5f && Main.npc[num90].Center.ToTileCoordinates() == b)
							{
								flag13 = false;
								break;
							}
						}
					}
					if (flag13)
					{
						Tile tile;
						tile = Main.tile[b.X, b.Y];
						flag13 = tile.type == 15;
						if (flag13 && tile.frameY == 1080)
						{
							flag13 = false;
						}
						if (flag13)
						{
							npc.ai[0] = 5f;
							npc.ai[1] = 900 + Main.rand.Next(10800);
							npc.direction = ((tile.frameX != 0) ? 1 : (-1));
							npc.Bottom = new Vector2(b.X * 16 + 8 + 2 * npc.direction, b.Y * 16 + 32);
							npc.velocity = Vector2.Zero;
							npc.localAI[3] = 0f;
							npc.netUpdate = true;
						}
					}
				}
				else if (flag8 && npc.ai[0] == 1f && npc.velocity.Y == 0f && Main.rand.Next(600) == 0 && Utils.PlotTileLine(npc.Top, npc.Bottom, npc.width, DelegateMethods.SearchAvoidedByNPCs))
				{
					Point point2;
					point2 = (npc.Center + new Vector2(npc.direction * 10, 0f)).ToTileCoordinates();
					bool flag14;
					flag14 = WorldGen.InWorld(point2.X, point2.Y, 1);
					if (flag14)
					{
						Tile tileSafely6;
						tileSafely6 = Framing.GetTileSafely(point2.X, point2.Y);
						if (!tileSafely6.nactive() || !TileID.Sets.InteractibleByNPCs[tileSafely6.type])
						{
							flag14 = false;
						}
					}
					if (flag14)
					{
						npc.ai[0] = 9f;
						npc.ai[1] = 40 + Main.rand.Next(90);
						npc.velocity = Vector2.Zero;
						npc.localAI[3] = 0f;
						npc.netUpdate = true;
					}
				}
				if (npc.ai[0] < 2f && npc.velocity.Y == 0f && npc.type == NPCID.Nurse)
				{
					int num91;
					num91 = -1;
					for (int num93 = 0; num93 < 200; num93++)
					{
						NPC nPC6;
						nPC6 = Main.npc[num93];
						if (nPC6.active && nPC6.townNPC && nPC6.life != nPC6.lifeMax && (num91 == -1 || nPC6.lifeMax - nPC6.life > Main.npc[num91].lifeMax - Main.npc[num91].life) && Collision.CanHitLine(npc.position, npc.width, npc.height, nPC6.position, nPC6.width, nPC6.height) && npc.Distance(nPC6.Center) < 500f)
						{
							num91 = num93;
						}
					}
					if (num91 != -1)
					{
						npc.ai[0] = 13f;
						npc.ai[1] = 34f;
						npc.ai[2] = num91;
						npc.localAI[3] = 0f;
						npc.direction = ((npc.position.X < Main.npc[num91].position.X) ? 1 : (-1));
						npc.netUpdate = true;
					}
				}
				if (flag9 && npc.velocity.Y == 0f && NPCID.Sets.AttackType[npc.type] == 0 && NPCID.Sets.AttackAverageChance[npc.type] > 0 && Main.rand.Next(NPCID.Sets.AttackAverageChance[npc.type] * 2) == 0)
				{
					int num94;
					num94 = NPCID.Sets.AttackTime[npc.type];
					int num95;
					num95 = ((direction == 1) ? moreNpcID : yetAnotherNpcId);
					int num96;
					num96 = ((direction == 1) ? yetAnotherNpcId : moreNpcID);
					if (num95 != -1 && !Collision.CanHit(npc.Center, 0, 0, Main.npc[num95].Center, 0, 0))
					{
						num95 = ((num96 == -1 || !Collision.CanHit(npc.Center, 0, 0, Main.npc[num96].Center, 0, 0)) ? (-1) : num96);
					}
					if (num95 != -1)
					{
						npc.localAI[2] = npc.ai[0];
						npc.ai[0] = 10f;
						npc.ai[1] = num94;
						npc.ai[2] = 0f;
						npc.localAI[3] = 0f;
						npc.direction = ((npc.position.X < Main.npc[num95].position.X) ? 1 : (-1));
						npc.netUpdate = true;
					}
				}
				else if (flag9 && npc.velocity.Y == 0f && NPCID.Sets.AttackType[npc.type] == 1 && NPCID.Sets.AttackAverageChance[npc.type] > 0 && Main.rand.Next(NPCID.Sets.AttackAverageChance[npc.type] * 2) == 0)
				{
					int num97;
					num97 = NPCID.Sets.AttackTime[npc.type];
					int num98;
					num98 = ((direction == 1) ? moreNpcID : yetAnotherNpcId);
					int num99;
					num99 = ((direction == 1) ? yetAnotherNpcId : moreNpcID);
					if (num98 != -1 && !Collision.CanHitLine(npc.Center, 0, 0, Main.npc[num98].Center, 0, 0))
					{
						num98 = ((num99 == -1 || !Collision.CanHitLine(npc.Center, 0, 0, Main.npc[num99].Center, 0, 0)) ? (-1) : num99);
					}
					if (num98 != -1)
					{
						Vector2 vector5;
						vector5 = npc.DirectionTo(Main.npc[num98].Center);
						if (vector5.Y <= 0.5f && vector5.Y >= -0.5f)
						{
							npc.localAI[2] = npc.ai[0];
							npc.ai[0] = 12f;
							npc.ai[1] = num97;
							npc.ai[2] = vector5.Y;
							npc.localAI[3] = 0f;
							npc.direction = ((npc.position.X < Main.npc[num98].position.X) ? 1 : (-1));
							npc.netUpdate = true;
						}
					}
				}
				if (flag9 && npc.velocity.Y == 0f && NPCID.Sets.AttackType[npc.type] == 2 && NPCID.Sets.AttackAverageChance[npc.type] > 0 && Main.rand.Next(NPCID.Sets.AttackAverageChance[npc.type] * 2) == 0)
				{
					int num100;
					num100 = NPCID.Sets.AttackTime[npc.type];
					int num101;
					num101 = ((direction == 1) ? moreNpcID : yetAnotherNpcId);
					int num102;
					num102 = ((direction == 1) ? yetAnotherNpcId : moreNpcID);
					if (num101 != -1 && !Collision.CanHitLine(npc.Center, 0, 0, Main.npc[num101].Center, 0, 0))
					{
						num101 = ((num102 == -1 || !Collision.CanHitLine(npc.Center, 0, 0, Main.npc[num102].Center, 0, 0)) ? (-1) : num102);
					}
					if (num101 != -1)
					{
						npc.localAI[2] = npc.ai[0];
						npc.ai[0] = 14f;
						npc.ai[1] = num100;
						npc.ai[2] = 0f;
						npc.localAI[3] = 0f;
						npc.direction = ((npc.position.X < Main.npc[num101].position.X) ? 1 : (-1));
						npc.netUpdate = true;
					}
					else if (npc.type == NPCID.Dryad)
					{
						npc.localAI[2] = npc.ai[0];
						npc.ai[0] = 14f;
						npc.ai[1] = num100;
						npc.ai[2] = 0f;
						npc.localAI[3] = 0f;
						npc.netUpdate = true;
					}
				}
				if (flag9 && npc.velocity.Y == 0f && NPCID.Sets.AttackType[npc.type] == 3 && NPCID.Sets.AttackAverageChance[npc.type] > 0 && Main.rand.Next(NPCID.Sets.AttackAverageChance[npc.type] * 2) == 0)
				{
					int num3;
					num3 = NPCID.Sets.AttackTime[npc.type];
					int num4;
					num4 = ((direction == 1) ? moreNpcID : yetAnotherNpcId);
					int num5;
					num5 = ((direction == 1) ? yetAnotherNpcId : moreNpcID);
					if (num4 != -1 && !Collision.CanHit(npc.Center, 0, 0, Main.npc[num4].Center, 0, 0))
					{
						num4 = ((num5 == -1 || !Collision.CanHit(npc.Center, 0, 0, Main.npc[num5].Center, 0, 0)) ? (-1) : num5);
					}
					if (num4 != -1)
					{
						npc.localAI[2] = npc.ai[0];
						npc.ai[0] = 15f;
						npc.ai[1] = num3;
						npc.ai[2] = 0f;
						npc.localAI[3] = 0f;
						npc.direction = ((npc.position.X < Main.npc[num4].position.X) ? 1 : (-1));
						npc.netUpdate = true;
					}
				}
				//npc.ai[0] =12;
				return false;
			}
            

           /* if (npc.townNPC)
            {
                npc.aiStyle = -1;
                return false;
            }*/
            return base.PreAI(npc);
        }
        public override void ResetEffects(NPC npc)
        {
            stun = false;
        }
        public override void AI(NPC npc)
        {
            if (rotLock && !stun)
            {
                npc.aiStyle = aiold;
                rotLock = false;
            }
            if (stun)
            {
                //looking at stoned, the Player has their control removed and velocity added.
                //not entirely sure how it stops when it hits the ground. some collision bs
                //so, i need to remove NPC control and then add negative velocity, stopping when touching a tile below it.
                //can't be THAT hard.

                //npcs don't have a canControl value, so i'll have to do it manually.
                //no f****** clue how to stop them from shooting tbh.
                //ah well. 

                //11.1 = should now lock rotation as well, properly fixing shit like wyverns and fixing the annoying flip bug.
                //using the tried-and-true flag system (afaik terraria does npc too!)

                //so npc didnt fucking work
                //shit.
                //god FUCKING DAMN IT.......... ok so like. i need to lock sprite direction too i guess.
                //rotlock seems to work however, so there's something - locking sprite direction should work too
                //...unless it updates after npc. fuuuuuuuuuuuuuuuuuuuu
                //npc may or may not be possible and idk if i'm willing to patch every. single. ai to work

                //npc DIDN FNT IFUckING WORK WHY DID IT NOT WORKM<>?????????????/
                //TODO: make worm segments not rotlock

                //fixed 12.1.21 by breaking ai lol
                if (!rotLock)
                {
                    aiold = npc.aiStyle;
                    npc.aiStyle = -1;
                    rotLock = true;

                }
                else
                {

                    if (npc.noTileCollide)
                    {
                        npc.velocity *= 0;
                    }
                    else
                    {
                        npc.velocity.X *= 0;
                        npc.velocity.Y -= -0.3f;
                        if (npc.collideY) npc.velocity.Y = 0;
                    }
                }

                //npc fucking breaks so many NPCs but it is pretty funny

                //disable it on worm bosses
                //in theory, sets X velocity to 0, makes them fall and cancels momentum when hit ground

                //also LOL

            }

        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {


        }

        public override bool PreNPCLoot(NPC npc)
        {
            if (Main.rand.NextBool(10000))
            {
                NewTextBox dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("RareKillLine" + (Main.rand.Next(4) + 1)), //guide idiot
                   Text.SmokeyDialogue1.GetTexture("Sus"),
                   Text.SmokeyDialogue1.GetVoice(), true, Color.White, true, false);
                dialogue.AddBox();
            }
            return base.PreNPCLoot(npc);
        }
        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (stun)
            {
                if (Main.rand.Next(0, 10) < 4)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustID.Electric, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, (Color.Black), 0.8f);
                    if (Main.rand.NextBool(4))
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.2f;
                    }
                }
                Lighting.AddLight(npc.position, 0.1f, 0.2f, 0.7f);
            }
        }





    }
}
