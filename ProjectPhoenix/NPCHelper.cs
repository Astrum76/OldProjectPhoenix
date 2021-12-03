using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using ProjectPhoenix.Items.Crafting;
using Terraria.ID;
using Terraria.ModLoader;
using ProjectPhoenix.NPCs.Unique;

namespace ProjectPhoenix
{
    public class NPCHelper
    {
        ///<summary>doesnt set hitsound/death amongst other things</summary>

        public static void SDEnemy(NPC npc, int width, int height, int damage, int defense, int lifeMax, float price, float kbresis, int aiStyle, bool dontTakeDamage1, int alpha1, bool boss1, bool lavaImmuno, bool gravityOff, bool noTiles)
        {
            npc.width = width;
            npc.height = height;
            npc.damage = damage;
            npc.defense = defense;
            npc.lifeMax = lifeMax;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = price;
            npc.knockBackResist = kbresis;
            npc.aiStyle = aiStyle;
            npc.dontTakeDamage = dontTakeDamage1;
            npc.alpha = alpha1;
            npc.boss = boss1;
            npc.lavaImmune = lavaImmuno;
            npc.noGravity = gravityOff;
            npc.noTileCollide = noTiles;

        }


        ///<summary>Returns the index of an NPC in main. Returns -1 if not found.</summary>
        public static int IsEveryoneFuckingHomeless()
        {
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].type != NPCID.OldMan && Main.npc[i].friendly == true && Main.npc[i].active && Main.npc[i].townNPC == true && Main.npc[i].homeless == false) return i;
            }
            return -1;
        }
        public static int FindNPC(int npc,int whom)
        {
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].type == npc && Main.npc[i].active == true && i != whom) return i;
            }
            return -1;
        }
        public static int FindNPC(int npc)
        {
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].type == npc && Main.npc[i].active == true) return i;
            }
            return -1;
        }


        ///<summary>Give it an NPC you dumb fuck. Returns 1 for top, 2 for right, 3 for bottom, 4 for left.</summary>
        public static int GetSideOfTile(NPC npc)
        {
            Point TileNPC = npc.Center.ToTileCoordinates();
            Vector2 TileCenter = TileNPC.ToWorldCoordinates();
            if (npc.Center.Y > TileCenter.Y)
            {
                return 1; //top
            }
            if (npc.Center.Y < TileCenter.Y)
            {
                return 3;  //bottom
            }
            if (npc.Center.X > TileCenter.X)
            {
                return 2; //right
            }
            if (npc.Center.X < TileCenter.X)
            {
                return 4; //left
            }
           

            return 1;
        }


        ///<summary>A favorite of my AI components, this is ripped right from EM. Works like a charm.</summary>

        public static void MoveSlowNPC(NPC npc, float speed, Vector2 moveTo, float turnRes)
        {
            Player player = Main.player[npc.target];
            Vector2 move = moveTo - npc.Center;
            float magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            float turnResistance = turnRes; //the larger this is, the slower the npc will turn
            move = (npc.velocity * turnResistance + move) / (turnResistance + 1f);
            magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            npc.velocity = move;
        }
        ///<summary>How many of the given NPC exist in the world. Returns -1 if none exist.</summary>
        [Obsolete("This method is obsolete. Use the vanilla one instead.", true)]
        public static int NumberOfNpcs(NPC lookup)
        {
            int count = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].type == lookup.type)
                {
                    count++;
                }
            }
            return count;
        }
        ///<summary>NPC.wet replacement lol.</summary>

        public static bool IsInWater(NPC npc)
        {
            Tile tile = Framing.GetTileSafely((int)npc.Center.X / 16, (int)npc.Center.Y / 16);
            if (tile.liquid > 0)
            {
                return true;
            }

            return false;

        }
        ///<summary>A momentum affecting function for those that are affected by gravity. FP rip.</summary>

        public static Vector2 MomentumHelper(NPC npc, float XVel, float YVel, float gravity, float terminalVelocity, float XVelDecay, bool waterAffected, bool isWet)
        {
            if (waterAffected)
            {


                if (isWet)
                {
                    npc.position.Y += YVel / 2;
                    npc.position.X += XVel / 2;
                    XVel *= XVelDecay;
                    XVel *= XVelDecay;

                    if (YVel <= terminalVelocity / 2)
                    {
                        YVel += gravity;
                    }
                }
                else
                {
                    npc.position.Y += YVel;
                    npc.position.X += XVel;
                    XVel *= XVelDecay;
                    if (YVel <= terminalVelocity)
                    {
                        YVel += gravity;
                    }
                }
            }
            else
            {
                npc.position.Y += YVel;
                npc.position.X += XVel;
                XVel *= XVelDecay;
                if (YVel <= terminalVelocity)
                {
                    YVel += gravity;
                }

            }
            return new Vector2(XVel, YVel);

        }


    }
}
