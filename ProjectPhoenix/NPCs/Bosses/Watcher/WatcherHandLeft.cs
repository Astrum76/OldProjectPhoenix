using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectPhoenix.NPCs.Bosses.Watcher
{
    public class WatcherHandLeft : ModNPC
    {
        float speed = 30;
        float turnRes = 15f;
        public bool linkPhase = true;

        public bool getLinkPhase() { return linkPhase; }
        public void setLinkPhase(bool valu) { linkPhase = valu; }

        public bool colOn = true;

        public bool getCol() { return linkPhase; }
        public void setCol(bool valu) { linkPhase = valu; }



        public override void SetStaticDefaults() //name and animation (probably unused unless GLori gives me an animated sprite or I choose to make the hand spin)
        {
            DisplayName.SetDefault("Watcher Choke");
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults() //for global NPC stats, will be possibly overriden later! 
        {

            npc.aiStyle = -1;
            //deathTimer1 = 0; //what the fuck is this doing here
            npc.lifeMax = 700;
            npc.damage = 20;
            npc.defense = 15;
            npc.knockBackResist = 0f;
            npc.dontTakeDamage = true;
            npc.alpha = 0;
            npc.width = 120;
            npc.height = 140;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 5f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
        }
        public override bool CheckActive()
        {
            return false;
        }
        public void moveSlownpc(float speed, Vector2 moveTo, float turnRes)
        {

            NPCHelper.MoveSlowNPC(npc, speed, moveTo, turnRes);
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
           return colOn;
        }
        public override bool CheckDead()
        {
            for (int k = 0; k < 12; k++)
            {
                Random a = new Random();

                Main.PlaySound(SoundID.Item93, npc.position);

                Dust.NewDust(npc.position, npc.width, npc.height, DustID.Clentaminator_Red, (float)((a.Next() % 100) / 100), (float)((a.Next() % 100) / 100), 0, default, 1.5f);   //spawns dust behind it, this is a spectral light blue dust. 15 is the dust, change that to what you want.


            }
            return true;

        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (colOn)
            {
                target.AddBuff(BuffID.OnFire, 150);
                base.OnHitPlayer(target, damage, crit);
            }
            else
            {
            }
        }
        private void Col()
        {

            for (int i = 0; i < Main.npc.Length - 1; i++)
            {

                if (Main.npc[i].type != ModContent.NPCType<WatcherHandLeft>() && (Main.npc[i].Distance(npc.Center) <= 128))
                {
                    //Main.NewText("loop lol");

                    Main.npc[i].StrikeNPC(7, 3, (int)npc.rotation);
                }
            }

        }
        public override void AI()
        {
            //this is npc dust
            for (int i = 0; i < 6; i++)
            {
                // int DustID2 = Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, DustID.IchorTorch, npc.velocity.X * -0.1f, npc.velocity.Y * -0.1f, 0, default, 1.25f);   //spawns dust behind it, this is a spectral light blue dust lol
                // Main.dust[DustID2].noGravity = true;
            }
            if (npc.ai[0] == 0 && npc.ai[1] == 0)
            {
                npc.frameCounter++;

            }
            if (npc.frameCounter > 120)
            {
                npc.ai[0] = Main.player[0].position.X - 2*75;
                npc.ai[1] = Main.player[0].position.Y - 10;
                npc.friendly = true;
                npc.dontTakeDamage = false;
                Col();
            }

            if (linkPhase) moveSlownpc(speed, new Vector2(npc.ai[0], npc.ai[1]), turnRes);
            if (!colOn && npc.alpha < 255) npc.alpha += 3;
            if (colOn) npc.alpha = 0;

        }

    }
}



