using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using ProjectPhoenix.NPCs.Unique;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectPhoenix
{
    public class ProjectileHelper
    {

        ///<summary>Method takes a target NPC to check, an ID of a single projectile to ignore, and an AI style. 1 follows the marksman coin AI the method was based off, 0 follows full bounces (buggy)</summary>
        public static int IsBulletProjectileOverlapping(NPC npc, int ignore, int aiStyle)
        {
            Projectile targetProjectile;
            for (int i = 0; i < Main.projectile.Length - 1; i++)
            {
                if (aiStyle == 1)
                {
                    targetProjectile = Main.projectile[i];
                    if (targetProjectile.damage >= 0 && targetProjectile != Main.projectile[ignore] && (targetProjectile.type == ProjectileID.Bullet || targetProjectile.type == ProjectileID.BulletHighVelocity) && targetProjectile.Colliding(targetProjectile.Hitbox, npc.Hitbox) && (targetProjectile.active == true) && targetProjectile.hostile == false)
                    {
                        return i;
                    }
                }
                if(aiStyle == 0)
                {
                    targetProjectile = Main.projectile[i];
                    if (targetProjectile != Main.projectile[ignore] && targetProjectile.Colliding(targetProjectile.Hitbox, npc.Hitbox) && (targetProjectile.active == true) && targetProjectile.hostile == false)
                    {
                        return i;
                    }
                }
              
            }

            return -1;
        }

        ///<summary>Takes an NPC, and checks for line of sight with nearby NPCs. MaxRange determines how far the targetting goes, ignoreBlocks is obvious and NPC is the entity this runs from. Ignores Marksman coins.</summary>

        public static int LockonNPCLineOfSight(float maxRange,bool ignoreBlocks, NPC npc) {
            NPC targetNPC;
            float prevDis = maxRange;
            int targetID = -1;
            for (int j = 0; j < Main.npc.Length - 1; j++)
            {
                targetNPC = Main.npc[j];
                // Main.NewText("lol");
                if ((targetNPC.CanBeChasedBy() || targetNPC.type == NPCID.WaterSphere  || targetNPC.type == NPCID.ChaosBall || targetNPC.type == NPCID.BurningSphere) && !targetNPC.townNPC && !targetNPC.friendly && (targetNPC.Distance(npc.Center) < prevDis) && npc.active && targetNPC.type != ModContent.NPCType<MarksmanCoin>() )
                {
                    if (ignoreBlocks)
                    {
                        prevDis = targetNPC.Distance(npc.Center);
                        targetID = j;
                    }
                    else
                    {
                        if(Collision.CanHitLine(npc.Center, 1, 1, targetNPC.Center, 1, 1))
                        {
                            prevDis = targetNPC.Distance(npc.Center);
                            targetID = j;
                        }
                    }
                    // Main.NewText("Valid Target Sighted", Color.Blue);
                   


                }


            }
            return targetID;

        }
        ///<summary>Takes an NPC, shoots 3 tracked proj</summary>

        public static void Shoot3TrackedProjAtPlayer(NPC npc,int type, int projAngle, float projSpeed, int damage, Vector2 position, Vector2 targetPosition, Player player)
        {

            //found somewhere and could be useful, not applicable here

            /*Vector2 direction = targetPosition - position;
			Vector2 normal = direction;
			normal.Normalize();
			Vector2 pos1 = normal.RotatedBy(MathHelper.ToRadians(projAngle));
			Vector2 pos2 = normal.RotatedBy(MathHelper.ToRadians(-projAngle));*/
            //Main.NewText(npc.DirectionTo(targetPosition + (player.velocity * 50)) * projSpeed);

            //this saves me a lot of time lol. actually
            //as of writing this, it defaults to 3
            //gonna make a for loop and 1 more var to shoot a selected amount, thus giving the bossfight WAY more variety
            //but it'd need a number of projs, and offset, and even / odd checks...
            //todo: re-use for other bosses/enemies

            //todo complete. copied from the WATCHER ai

            Projectile.NewProjectile(position, (npc.DirectionTo(targetPosition + (player.velocity * 25)) * projSpeed), type, damage, 0f, Main.myPlayer);
            Projectile.NewProjectile(position, (npc.DirectionTo(targetPosition + (player.velocity * 25)).RotatedBy(MathHelper.ToRadians(projAngle)) * projSpeed), type, damage, 0f, Main.myPlayer);
            Projectile.NewProjectile(position, (npc.DirectionTo(targetPosition + (player.velocity * 25)).RotatedBy(MathHelper.ToRadians(-projAngle))) * projSpeed, type, damage, 0f, Main.myPlayer);
        }
        public static void ShootTrackedProjAtPlayer(NPC npc, int type, int projAngle, float projSpeed, int damage, Vector2 position, Vector2 targetPosition, Player player)
        {

            //found somewhere and could be useful, not applicable here

            /*Vector2 direction = targetPosition - position;
			Vector2 normal = direction;
			normal.Normalize();
			Vector2 pos1 = normal.RotatedBy(MathHelper.ToRadians(projAngle));
			Vector2 pos2 = normal.RotatedBy(MathHelper.ToRadians(-projAngle));*/
            //Main.NewText(npc.DirectionTo(targetPosition + (player.velocity * 50)) * projSpeed);

            //this is a modified version of Watcher's ai targeting
            //sick that i get to reuse this

            Projectile.NewProjectile(position, (npc.DirectionTo(targetPosition + (player.velocity * 25)).RotatedBy(MathHelper.ToRadians(projAngle))) * projSpeed, type, damage, 0f, Main.myPlayer);
        }

    }
}
