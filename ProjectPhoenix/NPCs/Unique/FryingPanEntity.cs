using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ProjectPhoenix;

//diabeetus on fridge
//diabeetus email
//diabeetus on da news

namespace ProjectPhoenix.NPCs.Unique
{
    public class FryingPanEntity : ModNPC
    {
        private bool sighted = false;
        private int timer
        {
            get => (int)npc.ai[0];
            set => npc.ai[0] = value;
        }
        float gravity = 0.25f;
        int ignore = Main.projectile.Length - 1;
        int targetProjectileID;
        int targetIDNPC;
        float rotationVel;

        float prevDis;
        private int cooldown = 0;
        bool setup = false;
        Vector2 Velocity = new Vector2(0, 0);
        public bool linkPhase = true;
      
        public override void SetStaticDefaults()
        {

            DisplayName.SetDefault("Comically Large Pan");
            Main.npcFrameCount[npc.type] = 1; //This is an animated projectile

        }

        public override void SetDefaults()
        {
            linkPhase = true;
            npc.aiStyle = -1;
            // npc.friendly = true;
            npc.lifeMax = 1;
            npc.damage = 0;
            npc.defense = 0;
            npc.knockBackResist = 99f;
            npc.dontTakeDamage = true;
            npc.alpha = 0;
            npc.width = 52*2;
            npc.height = 52*2;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 5f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;


        }

        public override void AI()
        {
            Player player1 = Main.player[Main.myPlayer];
            Projectile targetProjectile;
            targetProjectileID = ProjectileHelper.IsBulletProjectileOverlapping(npc, ignore, 1);
            //have we found a valid projectile?
            if (targetProjectileID != -1)
            {
                //set its ID
                targetProjectile = Main.projectile[targetProjectileID];
                //alt targeting
                /*if(player1.Center.X > npc.Center.X)
                {
                    ignore = Projectile.NewProjectile(npc.Center, targetProjectile.velocity.RotatedBy(MathHelper.ToRadians(90*3)), targetProjectile.type, targetProjectile.damage, 0f, player1.whoAmI);

                }
                else
                {
                    ignore = Projectile.NewProjectile(npc.Center, targetProjectile.velocity.RotatedBy(MathHelper.ToRadians(90)), targetProjectile.type, targetProjectile.damage, 0f, player1.whoAmI);

                }*/
                if (true) //if ur lucky itll just lockon but this doesnt seem to work and the code is ugly so eh
                {
                    int targetIDNPC = ProjectileHelper.LockonNPCLineOfSight(prevDis, false, npc);
                    if (targetIDNPC != -1)
                    {
                        NPC targetNPC = Main.npc[targetIDNPC];
                        ignore = Projectile.NewProjectile(npc.Center, (npc.DirectionTo(targetNPC.Center - ((targetNPC.velocity) / 10))) * 30f, targetProjectile.type, targetProjectile.damage * 2, 0f, player1.whoAmI);

                    }
                    else
                    {
                        ignore = Projectile.NewProjectile(npc.Center, targetProjectile.velocity.RotatedBy((npc.rotation)) * (0.55f + Main.rand.NextFloat()*1.2f), targetProjectile.type, targetProjectile.damage * 2, 0f, player1.whoAmI);

                    }

                }
                else if (Main.rand.NextDouble() <= 0.5f)//or try
                {
                    int targetIDNPC = ProjectileHelper.LockonNPCLineOfSight(prevDis, false, npc);
                    if (targetIDNPC != -1)
                    {
                        NPC targetNPC = Main.npc[targetIDNPC];
                        ignore = Projectile.NewProjectile(npc.Center, npc.DirectionTo(targetNPC.Center) * 10f, targetProjectile.type, targetProjectile.damage * 2, 0f, player1.whoAmI);

                    }
                    else
                    {
                        ignore = Projectile.NewProjectile(npc.Center, targetProjectile.velocity.RotatedBy((npc.rotation))*(0.55f +Main.rand.NextFloat()*1.2f), targetProjectile.type, targetProjectile.damage * 2, 0f, player1.whoAmI);

                    }


                }
                else
                {
                    //or just random shot
                    ignore = Projectile.NewProjectile(npc.Center, targetProjectile.velocity.RotatedBy((npc.rotation)) * (0.55f + Main.rand.NextFloat()*1.2f ), targetProjectile.type, targetProjectile.damage *2, 0f, player1.whoAmI);

                }
                Random a = new Random();
                //for (int k = 0; k < 1; k++)
                //{
                //centered!
                //delete the right stuff
                Dust.NewDust(new Vector2(npc.Center.X - npc.width / 2, npc.Center.Y - npc.height / 2), npc.width, npc.height, DustID.Ambient_DarkBrown, npc.velocity.X * -0.1f, npc.velocity.Y * -0.1f, 0, default, 0.5f);   //spawns dust behind it, this is a spectral light blue dust lol
                Main.PlaySound(SoundID.Item37, npc.position);
                Velocity.X += targetProjectile.velocity.X / 100;
                Velocity.Y = (targetProjectile.knockBack * -1) / 2.5f;
                // cooldown = timer + 1;
                npc.rotation += Main.rand.NextFloat()/2;
                targetProjectile.Kill();
            }
            if (Collision.SolidCollision(npc.Center, 1, 1))
            {
                npc.active = false;

                for (int i = 0; i < 10; i++)
                {
                    //centered!
                    Dust.NewDust(new Vector2(npc.Center.X - npc.width / 2, npc.Center.Y - npc.height / 2), npc.width, npc.height, DustID.Ambient_DarkBrown, npc.velocity.X * -0.1f, npc.velocity.Y * -0.1f, 0, default, 1.25f);   //spawns dust behind it, this is a spectral light blue dust lol

                }

            }
            // rotationVel += (Velocity.X + Velocity.Y) /100f;
            npc.rotation = npc.rotation + (gravity - (Velocity.X + Velocity.Y) / 60);

            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            if (!setup)
            {
                timer = 0;
                Velocity.Y = -6.5f + player.velocity.Y;
                Velocity.X = player.velocity.X;

                if (player.direction == 1)
                {
                    Velocity.X += 3;
                }
                else
                {
                    Velocity.X -= 3;
                }
                setup = true;
            }
            timer++;
            Velocity = NPCHelper.MomentumHelper(npc,Velocity.X,Velocity.Y,gravity,15,0.98f,true,NPCHelper.IsInWater(npc));
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Vector2 drawCenter = new Vector2(0, 0f);

            spriteBatch.Draw(Main.npcTexture[npc.type], npc.Center - Main.screenPosition, npc.frame, Color.White, npc.rotation, npc.frame.Size() / 2f, npc.scale, SpriteEffects.None, 0f);

            return false;
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void FindFrame(int frameHeight)
        {
           /* if (timer % 3 == 0)
            {
                npc.frame.Y += frameHeight;
                if (npc.frame.Y == frameHeight * 8)
                {
                    npc.frame.Y = 0;

                }
            }*/

        }

        public override bool PreNPCLoot()
        {
            return false;
        }
    }
}



