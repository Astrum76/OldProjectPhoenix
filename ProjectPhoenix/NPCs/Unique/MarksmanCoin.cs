using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ProjectPhoenix;



namespace ProjectPhoenix.NPCs.Unique
{
    public class MarksmanCoin : ModNPC
    {
        private float deathGravity;
        float npccolor = 1;
        int deathTimer = 0;
        private bool deadCoin;
        private int drySpell;
        private bool isWet;
        private bool spawnedInWater = false;
        private int timer
        {
            get => (int)npc.ai[0];
            set => npc.ai[0] = value;
        }
        float gravity = 0.25f;
        int ignore = Main.projectile.Length - 1;
        int targetProjectileID;
        int targetIDNPC;

        float prevDis;
        bool setup = false;
        Vector2 Velocity = new Vector2(0, 0);
        public override void SetStaticDefaults()
        {

            DisplayName.SetDefault("Tossed Coin");
            Main.npcFrameCount[npc.type] = 8; //This is an animated projectile

        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            // npc.friendly = true;
            npc.lifeMax = 1;
            npc.damage = 0;
            npc.defense = 0;
            npc.knockBackResist = 99f;
            npc.dontTakeDamage = true;
            npc.alpha = 0;
            npc.width = 54;
            npc.height = 64;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 5f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;

        }

        public override void AI()
        {

            Player player1 = Main.player[Main.myPlayer];
            prevDis = 2000;
            NPC targetNPC;
            Projectile targetProjectile;
            timer++;

            //Main.NewText(npc.Center);

            if (!deadCoin)
            {



                targetProjectileID = ProjectileHelper.IsBulletProjectileOverlapping(npc, ignore, 1);
                targetIDNPC = ProjectileHelper.LockonNPCLineOfSight(prevDis, false, npc);

                //have we found a valid projectile?
                if (targetProjectileID != -1)
                {
                    //set its ID
                    targetProjectile = Main.projectile[targetProjectileID];
                    //other coin



                    //got an NPc target?
                    if (targetIDNPC != -1)
                    {
                        targetNPC = Main.npc[targetIDNPC];
                        //we know its bul or highvel. so, hitscan the enemy and shoot a dummy bullet

                        ignore = Projectile.NewProjectile(npc.Center, (npc.DirectionTo(targetNPC.Center - ((targetNPC.velocity) / 10))) * 30f, ProjectileID.BulletHighVelocity, targetProjectile.damage * 0, 0f, player1.whoAmI);
                        targetNPC.StrikeNPC(targetProjectile.damage, targetProjectile.knockBack, (int)targetProjectile.rotation, true);
                    }
                    //no valid npc?
                    else
                    {
                        //just shoot forwards
                        ignore = Projectile.NewProjectile(npc.Center, targetProjectile.velocity, targetProjectile.type, targetProjectile.damage, 0f, player1.whoAmI);
                        // ignore =  Projectile.NewProjectile(npc.Center, (npc.DirectionTo(targetNPC.Center - ((targetNPC.velocity) / 10))) * 30f, targetProjectile.type, targetProjectile.damage + targetProjectile.damage + 10, 0f, player1.whoAmI);
                    }
                    // Main.NewText("Target ID is valid", Color.Blue);
                    //slightly lead shot hopefully
                    //nah
                    //would use normal proj, but it FUCKING MISSES. could be useful, but this thing is designed to make events
                    //a lot less ass for gun-centric players. so...
                    //hitscan it is, then.
                    //ignore = Projectile.NewProjectile(npc.Center, (npc.DirectionTo(   targetNPC.Center - ((targetNPC.velocity)/10)   )) * 30f, targetProjectile.type, targetProjectile.damage+100, 0f, player1.whoAmI) ;
                    Random a = new Random();
                    //for (int k = 0; k < 1; k++)
                    //{
                    //centered!
                    //delete the right stuff
                    Dust.NewDust(new Vector2(npc.Center.X - npc.width / 2, npc.Center.Y - npc.height / 2), npc.width, npc.height, DustID.GoldCoin, npc.velocity.X * -0.1f, npc.velocity.Y * -0.1f, 0, default, 1.25f);   //spawns dust behind it, this is a spectral light blue dust lol
                    Main.PlaySound(SoundID.CoinPickup, npc.position);
                    Velocity.X += targetProjectile.velocity.X / 10;
                    Velocity.Y = (targetProjectile.knockBack * -1) / 1.5f;
                    //cooldown = timer + 1;
                    targetProjectile.Kill();
                }
                if (Collision.SolidCollision(npc.Center, 16, 16))
                {
                    npc.active = false;
                    deadCoin = true;
                    deathTimer = timer + 90;
                    Main.PlaySound(SoundID.CoinPickup, npc.position);
                    int balls = NPCHelper.GetSideOfTile(npc);
                    if (balls == 1)
                    {
                        Point coords = npc.Center.ToTileCoordinates();
                        Vector2 center = coords.ToWorldCoordinates();
                        npc.Center = new Vector2(npc.Center.X, npc.Center.Y);
                        //up
                    }
                    if (balls == 2)
                    {
                        Point coords = npc.Center.ToTileCoordinates();
                        Vector2 center = coords.ToWorldCoordinates();
                        npc.Center = new Vector2(center.X - 17, npc.Center.Y);
                        //right
                    }
                    if (balls == 3)
                    {
                        Point coords = npc.Center.ToTileCoordinates();
                        Vector2 center = coords.ToWorldCoordinates();
                        npc.Center = new Vector2(npc.Center.X, center.Y + 17);
                        //down
                    }
                    if (balls == 4)
                    {
                        Point coords = npc.Center.ToTileCoordinates();
                        Vector2 center = coords.ToWorldCoordinates();
                        npc.Center = new Vector2(center.X + 17, npc.Center.Y);
                        //right
                    }

                    for (int i = 0; i < 10; i++)
                    {
                        //centered!
                        Dust.NewDust(new Vector2(npc.Center.X - npc.width / 2, npc.Center.Y - npc.height / 2), npc.width, npc.height, DustID.GoldCoin, npc.velocity.X * -0.1f, npc.velocity.Y * -0.1f, 0, default, 1.25f);   //spawns dust behind it, this is a spectral light blue dust lol

                    }
                    /*
                    if (Collision.SolidCollision(new Vector2(npc.Center.X+0.1f,npc.Center.Y-Velocity.Y), 1, 1) || Collision.SolidCollision(new Vector2(npc.Center.X - 0.1f, npc.Center.Y-Velocity.Y), 1, 1))
                    {
                        npc.active = false;
                        for (int i = 0; i < 10; i++)
                        {
                            //centered!
                            Dust.NewDust(new Vector2(npc.Center.X - npc.width / 2, npc.Center.Y - npc.height / 2), npc.width, npc.height, DustID.GoldCoin, npc.velocity.X * -0.1f, npc.velocity.Y * -0.1f, 0, default, 1.25f);   //spawns dust behind it, this is a spectral light blue dust lol

                        }

                    }


                    Main.PlaySound(SoundID.CoinPickup, npc.position);

                    if (Velocity.Y > 2 || Velocity.Y < -2)
                    {
                        Velocity.Y = (Math.Abs(Velocity.Y)) * -1;
                        Velocity.Y /= 2f;
                    }
                    else
                    {
                        npc.active = false;

                        for (int i = 0; i < 10; i++)
                        {
                            //centered!
                            Dust.NewDust(new Vector2(npc.Center.X - npc.width / 2, npc.Center.Y - npc.height / 2), npc.width, npc.height, DustID.GoldCoin, npc.velocity.X * -0.1f, npc.velocity.Y * -0.1f, 0, default, 1.25f);   //spawns dust behind it, this is a spectral light blue dust lol

                        }
                    }
                    */

                }

                npc.rotation += (Velocity.X + Velocity.Y) / 60;
                npc.TargetClosest(true);
                Player player = Main.player[npc.target];
                if (!setup)
                {
                    if (NPCHelper.IsInWater(npc)) spawnedInWater = true;
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
                isWet = NPCHelper.IsInWater(npc);

                //Main.NewText(isWet);
                drySpell++;
                if (isWet) drySpell = 0;
                if (drySpell > 30)
                {
                    spawnedInWater = false;
                }

                if (!spawnedInWater)
                {
                    if (isWet)
                    {

                        if (Velocity.X < -6)
                        {
                            Velocity.Y = Velocity.X / 2;
                            // Velocity.X *= 0.99f;
                            for (int i = 0; i < 10; i++)
                            {
                                Dust.NewDust(npc.Center, 16, 16, Dust.dustWater(), Main.rand.Next(5) - 10, Main.rand.Next(1) - 5);

                            }
                        }

                        if (Velocity.X > 6)
                        {
                            Velocity.Y = -1 * Velocity.X / 2;
                            // Velocity.X *= 0.99f;
                            for (int i = 0; i < 10; i++)
                            {
                                Dust.NewDust(npc.Center, 16, 16, Dust.dustWater(), Main.rand.Next(5) - 10, Main.rand.Next(1) - 5);

                            }
                        }
                    }

                }
                //Main.NewText("air");


                if (!deadCoin)
                {
                    Velocity = NPCHelper.MomentumHelper(npc, Velocity.X, Velocity.Y, gravity, 10, 0.99f, true, NPCHelper.IsInWater(npc));

                }
                //Main.NewText((Velocity.X + Velocity.Y) / 60);
            }
            else
            {
                if (!Collision.SolidCollision(npc.Center, 1, 1))
                {
                    npc.active = false;
                    deathGravity += gravity;
                    npc.Center = new Vector2(npc.Center.X, npc.Center.Y + deathGravity);
                }
                if (timer % 2 == 0 && timer > deathTimer)
                {
                    npccolor -= 0.01f;
                    if (npccolor <= 0)
                    {
                        npc.active = false;
                        npccolor = 1;
                    }
                }
            }
        }
        private float sussy(float pos)
        {
            if (!Collision.SolidCollision(npc.Center, 1, 1)) return sussy(pos + 1);
            return pos;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Vector2 drawCenter = new Vector2(0, 0f);

            spriteBatch.Draw(Main.npcTexture[npc.type], npc.Center - Main.screenPosition, npc.frame, Color.White * npccolor, npc.rotation, npc.frame.Size() / 2f, npc.scale, SpriteEffects.None, 0f);

            return false;
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void FindFrame(int frameHeight)
        {
            if (!deadCoin)
            {
                if (timer % 3 == 0)
                {
                    npc.frame.Y += frameHeight;
                    if (npc.frame.Y == frameHeight * 8)
                    {
                        npc.frame.Y = 0;

                    }
                }
            }


        }

        public override bool PreNPCLoot()
        {
            return false;
        }
    }
}

/*

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ProjectPhoenix;



namespace ProjectPhoenix.NPCs.Unique
{
    public class MarksmanCoin : ModNPC
    {
        private bool drawBounce;
        public bool firstCoin = false;
        private float deathGravity;
        float npccolor = 1;
        int deathTimer = 0;
        private bool deadCoin;
        private int drySpell;
        private bool isWet;
        private bool spawnedInWater = false;
        private int timer
        {
            get => (int)npc.ai[0];
            set => npc.ai[0] = value;
        }
        float gravity = 0.25f;
        int ignore = Main.projectile.Length - 1;
        int targetProjectileID;
        int targetIDNPC;
        int otherCoinID;
        NPC otherCoin;
        float prevDis;
        bool setup = false;
        Vector2 Velocity = new Vector2(0, 0);
        public override void SetStaticDefaults()
        {

            DisplayName.SetDefault("Tossed Coin");
            Main.npcFrameCount[npc.type] = 8; //This is an animated projectile

        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            // npc.friendly = true;
            npc.lifeMax = 1;
            npc.damage = 0;
            npc.defense = 0;
            npc.knockBackResist = 99f;
            npc.dontTakeDamage = true;
            npc.alpha = 0;
            npc.width = 54;
            npc.height = 64;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 5f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;

        }

        public override void AI()
        {

            Player player1 = Main.LocalPlayer;
            prevDis = 2000;
            NPC targetNPC;
            Projectile targetProjectile;
            timer++;

            // Main.NewText(npc.Center);

            if (!deadCoin)
            {



                targetProjectileID = ProjectileHelper.IsBulletProjectileOverlapping(npc, ignore, 1);
                targetIDNPC = ProjectileHelper.LockonNPCLineOfSight(prevDis, false, npc);

                //have we found a valid projectile?
                if (targetProjectileID != -1)
                {
                    //set its ID
                    targetProjectile = Main.projectile[targetProjectileID];
                    //other coin?
                    otherCoinID = NPCHelper.FindNPC(ModContent.NPCType<MarksmanCoin>(), npc.whoAmI);

                    //got a coin target?
                    if (otherCoinID != -1)
                    {
                        drawBounce = true;
                        otherCoin = Main.npc[otherCoinID];


                    }
                    if (drawBounce)
                    {
                        //do the same thing but execute from the second coin
                        //got an NPc target?
                        if (targetIDNPC != -1)
                        {
                            targetNPC = Main.npc[targetIDNPC];
                            //we know its bul or highvel. so, hitscan the enemy and shoot a dummy bullet

                            ignore = Projectile.NewProjectile(otherCoin.Center, (otherCoin.DirectionTo(targetNPC.Center - ((targetNPC.velocity) / 10))) * 30f, ProjectileID.BulletHighVelocity, targetProjectile.damage * 0, 0f, player1.whoAmI);
                            targetNPC.StrikeNPC(targetProjectile.damage, targetProjectile.knockBack, (int)targetProjectile.rotation, true);
                        }
                        //no valid npc?
                        else
                        {
                            //just shoot forwards
                            ignore = Projectile.NewProjectile(otherCoin.Center, targetProjectile.velocity, targetProjectile.type, targetProjectile.damage, 0f, player1.whoAmI);
                            // ignore =  Projectile.NewProjectile(npc.Center, (npc.DirectionTo(targetNPC.Center - ((targetNPC.velocity) / 10))) * 30f, targetProjectile.type, targetProjectile.damage + targetProjectile.damage + 10, 0f, player1.whoAmI);
                        }
                        // Main.NewText("Target ID is valid", Color.Blue);
                        //slightly lead shot hopefully
                        //nah
                        //would use normal proj, but it FUCKING MISSES. could be useful, but this thing is designed to make events
                        //a lot less ass for gun-centric players. so...
                        //hitscan it is, then.
                        //ignore = Projectile.NewProjectile(npc.Center, (npc.DirectionTo(   targetNPC.Center - ((targetNPC.velocity)/10)   )) * 30f, targetProjectile.type, targetProjectile.damage+100, 0f, player1.whoAmI) ;
                        Random a = new Random();
                        //for (int k = 0; k < 1; k++)
                        //{
                        //centered!
                        //delete the right stuff
                        Dust.NewDust(new Vector2(otherCoin.Center.X - otherCoin.width / 2, otherCoin.Center.Y - otherCoin.height / 2), otherCoin.width, otherCoin.height, DustID.GoldCoin, otherCoin.velocity.X * -0.1f, otherCoin.velocity.Y * -0.1f, 0, default, 1.25f);   //spawns dust behind it, this is a spectral light blue dust lol
                        Main.PlaySound(SoundID.CoinPickup, otherCoin.position);
                        Velocity.X += targetProjectile.velocity.X / 50;
                        Velocity.Y = (targetProjectile.knockBack * -1) / 2;
                        //cooldown = timer + 1;
                        targetProjectile.Kill();
                        //and shatter the second coin after draw
                        for (int i = 0; i < 20; i++)
                        {
                            //centered!
                            Dust.NewDust(new Vector2(otherCoin.Center.X - otherCoin.width / 2, otherCoin.Center.Y - otherCoin.height / 2), otherCoin.width, otherCoin.height, DustID.GoldCoin, otherCoin.velocity.X * -0.1f, otherCoin.velocity.Y * -0.1f, 0, default, 1.25f);   //spawns dust behind it, this is a spectral light blue dust lol

                        }

                    }
                    if (!drawBounce)
                    {
                        //got an NPc target?
                        if (targetIDNPC != -1)
                        {
                            targetNPC = Main.npc[targetIDNPC];
                            //we know its bul or highvel. so, hitscan the enemy and shoot a dummy bullet

                            ignore = Projectile.NewProjectile(npc.Center, (npc.DirectionTo(targetNPC.Center - ((targetNPC.velocity) / 10))) * 30f, ProjectileID.BulletHighVelocity, targetProjectile.damage * 0, 0f, player1.whoAmI);
                            targetNPC.StrikeNPC(targetProjectile.damage, targetProjectile.knockBack, (int)targetProjectile.rotation, true);
                        }
                        //no valid npc?
                        else
                        {
                            //just shoot forwards
                            ignore = Projectile.NewProjectile(npc.Center, targetProjectile.velocity, targetProjectile.type, targetProjectile.damage, 0f, player1.whoAmI);
                            // ignore =  Projectile.NewProjectile(npc.Center, (npc.DirectionTo(targetNPC.Center - ((targetNPC.velocity) / 10))) * 30f, targetProjectile.type, targetProjectile.damage + targetProjectile.damage + 10, 0f, player1.whoAmI);
                        }
                        // Main.NewText("Target ID is valid", Color.Blue);
                        //slightly lead shot hopefully
                        //nah
                        //would use normal proj, but it FUCKING MISSES. could be useful, but this thing is designed to make events
                        //a lot less ass for gun-centric players. so...
                        //hitscan it is, then.
                        //ignore = Projectile.NewProjectile(npc.Center, (npc.DirectionTo(   targetNPC.Center - ((targetNPC.velocity)/10)   )) * 30f, targetProjectile.type, targetProjectile.damage+100, 0f, player1.whoAmI) ;
                        Random a = new Random();
                        //for (int k = 0; k < 1; k++)
                        //{
                        //centered!
                        //delete the right stuff
                        Dust.NewDust(new Vector2(npc.Center.X - npc.width / 2, npc.Center.Y - npc.height / 2), npc.width, npc.height, DustID.GoldCoin, npc.velocity.X * -0.1f, npc.velocity.Y * -0.1f, 0, default, 1.25f);   //spawns dust behind it, this is a spectral light blue dust lol
                        Main.PlaySound(SoundID.CoinPickup, npc.position);
                        Velocity.X += targetProjectile.velocity.X / 50;
                        Velocity.Y = (targetProjectile.knockBack * -1) / 2;
                        //cooldown = timer + 1;
                        targetProjectile.Kill();
                    }

                }
                if (Collision.SolidCollision(npc.Center, 16, 16))
                {
                    npc.active = false;
                    deadCoin = true;
                    deathTimer = timer + 90;
                    Main.PlaySound(SoundID.CoinPickup, npc.position);
                    int balls = NPCHelper.GetSideOfTile(npc);
                    if (balls == 1)
                    {
                        Point coords = npc.Center.ToTileCoordinates();
                        Vector2 center = coords.ToWorldCoordinates();
                        npc.Center = new Vector2(npc.Center.X, npc.Center.Y);
                        //up
                    }
                    if (balls == 2)
                    {
                        Point coords = npc.Center.ToTileCoordinates();
                        Vector2 center = coords.ToWorldCoordinates();
                        npc.Center = new Vector2(center.X - 17, npc.Center.Y);
                        //right
                    }
                    if (balls == 3)
                    {
                        Point coords = npc.Center.ToTileCoordinates();
                        Vector2 center = coords.ToWorldCoordinates();
                        npc.Center = new Vector2(npc.Center.X, center.Y + 17);
                        //down
                    }
                    if (balls == 4)
                    {
                        Point coords = npc.Center.ToTileCoordinates();
                        Vector2 center = coords.ToWorldCoordinates();
                        npc.Center = new Vector2(center.X + 17, npc.Center.Y);
                        //right
                    }

                    for (int i = 0; i < 10; i++)
                    {
                        //centered!
                        Dust.NewDust(new Vector2(npc.Center.X - npc.width / 2, npc.Center.Y - npc.height / 2), npc.width, npc.height, DustID.GoldCoin, npc.velocity.X * -0.1f, npc.velocity.Y * -0.1f, 0, default, 1.25f);   //spawns dust behind it, this is a spectral light blue dust lol

                    }*/
                    /*
                    if (Collision.SolidCollision(new Vector2(npc.Center.X+0.1f,npc.Center.Y-Velocity.Y), 1, 1) || Collision.SolidCollision(new Vector2(npc.Center.X - 0.1f, npc.Center.Y-Velocity.Y), 1, 1))
                    {
                        npc.active = false;
                        for (int i = 0; i < 10; i++)
                        {
                            //centered!
                            Dust.NewDust(new Vector2(npc.Center.X - npc.width / 2, npc.Center.Y - npc.height / 2), npc.width, npc.height, DustID.GoldCoin, npc.velocity.X * -0.1f, npc.velocity.Y * -0.1f, 0, default, 1.25f);   //spawns dust behind it, this is a spectral light blue dust lol

                        }

                    }


                    Main.PlaySound(SoundID.CoinPickup, npc.position);

                    if (Velocity.Y > 2 || Velocity.Y < -2)
                    {
                        Velocity.Y = (Math.Abs(Velocity.Y)) * -1;
                        Velocity.Y /= 2f;
                    }
                    else
                    {
                        npc.active = false;

                        for (int i = 0; i < 10; i++)
                        {
                            //centered!
                            Dust.NewDust(new Vector2(npc.Center.X - npc.width / 2, npc.Center.Y - npc.height / 2), npc.width, npc.height, DustID.GoldCoin, npc.velocity.X * -0.1f, npc.velocity.Y * -0.1f, 0, default, 1.25f);   //spawns dust behind it, this is a spectral light blue dust lol

                        }
                    }
                    */

                /*}

                npc.rotation += (Velocity.X + Velocity.Y) / 60;
                npc.TargetClosest(true);
                Player player = Main.player[npc.target];
                if (!setup)
                {
                    if (NPCHelper.FindNPC(ModContent.NPCType<MarksmanCoin>(), npc.whoAmI) == -1) firstCoin = true;

                    if (NPCHelper.IsInWater(npc)) spawnedInWater = true;
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
                isWet = NPCHelper.IsInWater(npc);

                //Main.NewText(isWet);
                drySpell++;
                if (isWet) drySpell = 0;
                if (drySpell > 30)
                {
                    spawnedInWater = false;
                }

                if (!spawnedInWater)
                {
                    if (isWet)
                    {

                        if (Velocity.X < -6)
                        {
                            Velocity.Y = Velocity.X / 2;
                            // Velocity.X *= 0.99f;
                            for (int i = 0; i < 64; i++)
                            {
                                Dust.NewDust(npc.Center, 16, 16, Dust.dustWater(), Main.rand.Next(5) - 10, Main.rand.Next(1) - 5);

                            }
                        }

                        if (Velocity.X > 6)
                        {
                            Velocity.Y = -1 * Velocity.X / 2;
                            // Velocity.X *= 0.99f;
                            for (int i = 0; i < 64; i++)
                            {
                                Dust.NewDust(npc.Center, 16, 16, Dust.dustWater(), Main.rand.Next(5) - 10, Main.rand.Next(1) - 5);

                            }
                        }
                    }

                }
                //Main.NewText("air");


                if (!deadCoin)
                {
                    Velocity = NPCHelper.MomentumHelper(npc, Velocity.X, Velocity.Y, gravity, 10, 0.99f, true, NPCHelper.IsInWater(npc));

                }
                //Main.NewText((Velocity.X + Velocity.Y) / 60);
            }
            else
            {
                if (!Collision.SolidCollision(npc.Center, 1, 1))
                {
                    npc.active = false;
                    deathGravity += gravity;
                    npc.Center = new Vector2(npc.Center.X, npc.Center.Y + deathGravity);
                }
                if (timer % 2 == 0 && timer > deathTimer)
                {
                    npccolor -= 0.01f;
                    if (npccolor <= 0)
                    {
                        npc.active = false;
                        npccolor = 1;
                    }
                }
            }
        }
        private float sussy(float pos)
        {
            if (!Collision.SolidCollision(npc.Center, 1, 1)) return sussy(pos + 1);
            return pos;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (drawBounce)
            {
                drawBounce = false;
                Vector2 end1 = npc.Center;
                Vector2 end2 = otherCoin.Center;
                Texture2D texture;
                if (end1 != end2)
                {
                    float length = Vector2.Distance(end1, end2);
                    Vector2 direction = end2 - end1;
                    direction.Normalize();
                    float start = (float)npc.frameCounter % 8f;
                    start *= 2f;
                    if (npc.localAI[1] == 0f)
                    {
                        start *= 2f;
                        start %= 16f;
                    }
                    texture = mod.GetTexture("NPCs/Unique/Coin");
                    for (float k = start; k <= length; k += 16f)
                    {
                        spriteBatch.Draw(texture, end1 + k * direction - Main.screenPosition, null, Color.White, new Vector2(npc.Center.X - otherCoin.Center.X, npc.Center.Y - otherCoin.Center.Y).ToRotation(), new Vector2(16f, 16f), 1f, SpriteEffects.None, 0f);
                    }

                }
            }


            Vector2 drawCenter = new Vector2(0, 0f);

            spriteBatch.Draw(Main.npcTexture[npc.type], npc.Center - Main.screenPosition, npc.frame, Color.White * npccolor, npc.rotation, npc.frame.Size() / 2f, npc.scale, SpriteEffects.None, 0f);

            return false;
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void FindFrame(int frameHeight)
        {
            if (!deadCoin)
            {
                if (timer % 3 == 0)
                {
                    npc.frame.Y += frameHeight;
                    if (npc.frame.Y == frameHeight * 8)
                    {
                        npc.frame.Y = 0;

                    }
                }
            }


        }

        public override bool PreNPCLoot()
        {
            return false;
        }
    }
}
*/



/*
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ProjectPhoenix;



namespace ProjectPhoenix.NPCs.Unique
{
    public class MarksmanCoin : ModNPC
    {
        private float deathGravity;
        float npccolor = 1;
        int deathTimer = 0;
        private bool deadCoin;
        private int drySpell;
        private bool isWet;
        private bool spawnedInWater = false;
        private int timer
        {
            get => (int)npc.ai[0];
            set => npc.ai[0] = value;
        }
        float gravity = 0.25f;
        int ignore = Main.projectile.Length - 1;
        int targetProjectileID;
        int targetIDNPC;

        float prevDis;
        bool setup = false;
        Vector2 Velocity = new Vector2(0, 0);
        public override void SetStaticDefaults()
        {

            DisplayName.SetDefault("Tossed Coin");
            Main.npcFrameCount[npc.type] = 8; //This is an animated projectile

        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            // npc.friendly = true;
            npc.lifeMax = 1;
            npc.damage = 0;
            npc.defense = 0;
            npc.knockBackResist = 99f;
            npc.dontTakeDamage = true;
            npc.alpha = 0;
            npc.width = 54;
            npc.height = 64;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 5f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;

        }

        public override void AI()
        {

            Player player1 = Main.player[Main.myPlayer];
            prevDis = 2000;
            NPC targetNPC;
            Projectile targetProjectile;
            timer++;

            Main.NewText(npc.Center);

            if (!deadCoin)
            {



                targetProjectileID = ProjectileHelper.IsBulletProjectileOverlapping(npc, ignore, 1);
                targetIDNPC = ProjectileHelper.LockonNPCLineOfSight(prevDis, false, npc);

                //have we found a valid projectile?
                if (targetProjectileID != -1)
                {
                    //set its ID
                    targetProjectile = Main.projectile[targetProjectileID];

                    //got an NPc target?
                    if (targetIDNPC != -1)
                    {
                        targetNPC = Main.npc[targetIDNPC];
                        //we know its bul or highvel. so, hitscan the enemy and shoot a dummy bullet

                        ignore = Projectile.NewProjectile(npc.Center, (npc.DirectionTo(targetNPC.Center - ((targetNPC.velocity) / 10))) * 30f, ProjectileID.BulletHighVelocity, targetProjectile.damage * 0, 0f, player1.whoAmI);
                        targetNPC.StrikeNPC(targetProjectile.damage, targetProjectile.knockBack, (int)targetProjectile.rotation, true);
                    }
                    //no valid npc?
                    else
                    {
                        //just shoot forwards
                        ignore = Projectile.NewProjectile(npc.Center, targetProjectile.velocity, targetProjectile.type, targetProjectile.damage, 0f, player1.whoAmI);
                        // ignore =  Projectile.NewProjectile(npc.Center, (npc.DirectionTo(targetNPC.Center - ((targetNPC.velocity) / 10))) * 30f, targetProjectile.type, targetProjectile.damage + targetProjectile.damage + 10, 0f, player1.whoAmI);
                    }
                    // Main.NewText("Target ID is valid", Color.Blue);
                    //slightly lead shot hopefully
                    //nah
                    //would use normal proj, but it FUCKING MISSES. could be useful, but this thing is designed to make events
                    //a lot less ass for gun-centric players. so...
                    //hitscan it is, then.
                    //ignore = Projectile.NewProjectile(npc.Center, (npc.DirectionTo(   targetNPC.Center - ((targetNPC.velocity)/10)   )) * 30f, targetProjectile.type, targetProjectile.damage+100, 0f, player1.whoAmI) ;
                    Random a = new Random();
                    //for (int k = 0; k < 1; k++)
                    //{
                    //centered!
                    //delete the right stuff
                    Dust.NewDust(new Vector2(npc.Center.X - npc.width / 2, npc.Center.Y - npc.height / 2), npc.width, npc.height, DustID.GoldCoin, npc.velocity.X * -0.1f, npc.velocity.Y * -0.1f, 0, default, 1.25f);   //spawns dust behind it, this is a spectral light blue dust lol
                    Main.PlaySound(SoundID.CoinPickup, npc.position);
                    Velocity.X += targetProjectile.velocity.X / 50;
                    Velocity.Y = (targetProjectile.knockBack * -1) / 2;
                    //cooldown = timer + 1;
                    targetProjectile.Kill();
                }
                if (Collision.SolidCollision(npc.Center, 16, 16))
                {

                    deadCoin = true;
                    deathTimer = timer + 90;
                    Main.PlaySound(SoundID.CoinPickup, npc.position);
                    int balls = NPCHelper.GetSideOfTile(npc);
                    if (balls == 1)
                    {
                        Point coords = npc.Center.ToTileCoordinates();
                        Vector2 center = coords.ToWorldCoordinates();
                        npc.Center = new Vector2(npc.Center.X, npc.Center.Y);
                        //up
                    }
                    if (balls == 2)
                    {
                        Point coords = npc.Center.ToTileCoordinates();
                        Vector2 center = coords.ToWorldCoordinates();
                        npc.Center = new Vector2(center.X - 17,npc.Center.Y);
                        //right
                    }
                    if (balls == 3)
                    {
                        Point coords = npc.Center.ToTileCoordinates();
                        Vector2 center = coords.ToWorldCoordinates();
                        npc.Center = new Vector2(npc.Center.X, center.Y + 17);
                        //down
                    }
                    if (balls == 4)
                    {
                        Point coords = npc.Center.ToTileCoordinates();
                        Vector2 center = coords.ToWorldCoordinates();
                        npc.Center = new Vector2(center.X + 17, npc.Center.Y);
                        //right
                    }

                    for (int i = 0; i < 10; i++)
                    {
                        //centered!
                        Dust.NewDust(new Vector2(npc.Center.X - npc.width / 2, npc.Center.Y - npc.height / 2), npc.width, npc.height, DustID.GoldCoin, npc.velocity.X * -0.1f, npc.velocity.Y * -0.1f, 0, default, 1.25f);   //spawns dust behind it, this is a spectral light blue dust lol

                    }*/
/*
if (Collision.SolidCollision(new Vector2(npc.Center.X+0.1f,npc.Center.Y-Velocity.Y), 1, 1) || Collision.SolidCollision(new Vector2(npc.Center.X - 0.1f, npc.Center.Y-Velocity.Y), 1, 1))
{
    npc.active = false;
    for (int i = 0; i < 10; i++)
    {
        //centered!
        Dust.NewDust(new Vector2(npc.Center.X - npc.width / 2, npc.Center.Y - npc.height / 2), npc.width, npc.height, DustID.GoldCoin, npc.velocity.X * -0.1f, npc.velocity.Y * -0.1f, 0, default, 1.25f);   //spawns dust behind it, this is a spectral light blue dust lol

    }

}


Main.PlaySound(SoundID.CoinPickup, npc.position);

if (Velocity.Y > 2 || Velocity.Y < -2)
{
    Velocity.Y = (Math.Abs(Velocity.Y)) * -1;
    Velocity.Y /= 2f;
}
else
{
    npc.active = false;

    for (int i = 0; i < 10; i++)
    {
        //centered!
        Dust.NewDust(new Vector2(npc.Center.X - npc.width / 2, npc.Center.Y - npc.height / 2), npc.width, npc.height, DustID.GoldCoin, npc.velocity.X * -0.1f, npc.velocity.Y * -0.1f, 0, default, 1.25f);   //spawns dust behind it, this is a spectral light blue dust lol

    }
}
*/
/*
}

npc.rotation += (Velocity.X + Velocity.Y) / 60;
npc.TargetClosest(true);
Player player = Main.player[npc.target];
if (!setup)
{
if (NPCHelper.IsInWater(npc)) spawnedInWater = true;
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
isWet = NPCHelper.IsInWater(npc);

//Main.NewText(isWet);
drySpell++;
if (isWet) drySpell = 0;
if (drySpell > 30)
{
spawnedInWater = false;
}

if (!spawnedInWater)
{
if (isWet)
{

    if (Velocity.X < -6)
    {
        Velocity.Y = Velocity.X / 2;
        // Velocity.X *= 0.99f;
        for (int i = 0; i < 64; i++)
        {
            Dust.NewDust(npc.Center, 16, 16, Dust.dustWater(), Main.rand.Next(5) - 10, Main.rand.Next(1) - 5);

        }
    }

    if (Velocity.X > 6)
    {
        Velocity.Y = -1 * Velocity.X / 2;
        // Velocity.X *= 0.99f;
        for (int i = 0; i < 64; i++)
        {
            Dust.NewDust(npc.Center, 16, 16, Dust.dustWater(), Main.rand.Next(5) - 10, Main.rand.Next(1) - 5);

        }
    }
}

}
//Main.NewText("air");


if (!deadCoin)
{
Velocity = NPCHelper.MomentumHelper(npc, Velocity.X, Velocity.Y, gravity, 10, 0.99f, true, NPCHelper.IsInWater(npc));

}
//Main.NewText((Velocity.X + Velocity.Y) / 60);
}
else
{
if (!Collision.SolidCollision(npc.Center, 1, 1))
{
deathGravity+=gravity;
npc.Center = new Vector2(npc.Center.X,npc.Center.Y+deathGravity);
}
if (timer % 2 == 0 && timer > deathTimer)
{
npccolor -= 0.01f;
if (npccolor <= 0)
{
    npc.active = false;
    npccolor = 1;
}
}
}
}
private float sussy(float pos)
{
if (!Collision.SolidCollision(npc.Center, 1, 1)) return sussy(pos+1);
return pos;
}
public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
{
Vector2 drawCenter = new Vector2(0, 0f);

spriteBatch.Draw(Main.npcTexture[npc.type], npc.Center - Main.screenPosition, npc.frame, Color.White * npccolor, npc.rotation, npc.frame.Size() / 2f, npc.scale, SpriteEffects.None, 0f);

return false;
}
public override bool CheckActive()
{
return false;
}
public override void FindFrame(int frameHeight)
{
if (!deadCoin)
{
if (timer % 3 == 0)
{
npc.frame.Y += frameHeight;
if (npc.frame.Y == frameHeight * 8)
{
    npc.frame.Y = 0;

}
}
}


}

public override bool PreNPCLoot()
{
return false;
}
}
}



*/