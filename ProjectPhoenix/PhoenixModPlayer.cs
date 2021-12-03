using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.Collections;
using ProjectPhoenix.Items;
using ProjectPhoenix.UI;
using ProjectPhoenix.Items.Unique.Player;
using Terraria.IO;
using Terraria.Graphics.Effects;
using static ProjectPhoenix.Datatypes;

namespace ProjectPhoenix
{

    /*you can check Main.chest[i].x/y
and see if it compares to, say Main.spawnTileX/Y
but uh
sec
player.chest is the index of the player's currently opened chest
you could store 4 unique IDs in a list or w/e and on the 5th ID
add whatever to the chest ig
you'll have to hope there's a frame or two before the UI comes up properly or you'll have the items phase in a bit late*/





    public class PhoenixModPlayer : Terraria.ModLoader.ModPlayer
    {
        //DONE: Switch this to a dictonary or some other datatype. An array is so solid, it's hard to read
        //the code a lot of the time. It would suck, yes, but oh well. It'd still be stored as an int[], but the extra
        //step in conversion could save me hours down the line if flags keep growing like
        //this. Just a thought


        //11.27 - Fucking lists man
        //11.28 think i got it
        private int deathCount;
        private int lastInteraction = 0;
        private bool firstRemind;
        public int daysPassed;
        private string[] allTags = { "DiedALot",
        "queenbee",
        "SeenHell",
        "AmogusEye",
        "HomelessIdiot",
        "NurseArrived",
        "IdiotGuide",
        "ALittleTrolling",
        "ComplimentNotDying",
         "WeaponComment",
         "CorruptionSmell",
        "CrimsonSmell",
          "ShroomJoke",
         "PostShroomJoke",
         "EatEther",
         "BindKey",
         "MerchantArrived",
         "BloodyFace",
         "SnowSmell",
         "RandomDrop1",
         "TheEdge",
         "coky",
         "HearBeach",
         "space1",
         "Loot1",
         "Ritual",
         "InUndergroundJungle"
        };
        //list:
        /* [0]: First core timer
         * [1]: First text stamp
         *  [2]: Crimson timer
         * [corrput timer 3]
         *  [4]: Amogus eye
         *  [5] Mushroom biome (shrooms joke)
         *  [6] Post shroom joke
         *  [7] First see hell
         *  
         *  
         *  
         *  
         *  
         */
        //MATCH TIMESTAMPS TO FLAGS!
        //init list here. init it to length of tags, make it 0, it'll override if needed below :)
        private int OpenedChests;
        private List<Vector2> OpenedChestPositions = new List<Vector2>();
        private bool sex = false;
        public List<List<Vector3>> Chest = new List<List<Vector3>>();
        private bool ChestCanBeLoaded = false;
        private List<string> tags;
        private List<int> values = new List<int>();
        private bool heldKeyP;
        private List<int> WorldNames = new List<int>();
        public Dictionary<string, int> timeStamps;
        //goto load to find what timestamps are used so far
        public Dictionary<string, int> flags;

        //list:
        /*
         * [0] = Has seen the "bind key" dialogue, 1 if seen
         * [1] = Has seen the first core scene, 2 if finished, 1 if waiting to open 
         * [2]: "Stop dying so much!" part 1
         * [3] "Have you considered not using a copper sword?" 1 if triggered, 2 if wooden sword is made
         * [4] "Good job on not dying!" Only plays if deathcount is 0. 1 if played positive, 2 if deathcount was not 0.
         *  [5] "I am outside your house"
         * [6] Guide is idiot
         * [7[ Nurse is greedy
         * [8] Homeless bitch. If 0 by day 6, player is not a little bitch
         * [9] Open wound. 1 if waiting, 2 if done
         * [10] .Butcher shop 1 if waiting, 2 if done
         *[11] eyeS
         * [12] Glowshroom
         * [13]Sober after
         *[14]Hell
         *
         *
         *
         *
         */
        float progress;
        private int timeHelper = 0;
        public bool MenuState = false;
        public int timer = 0;
        public bool ButtonBeenClicked = false;
        public int timePlayed; //in seconds, how long the player has existed with this mod on
        public bool firstEther = false;
        int currentText = 0;
        public List<NewTextBox> textBoxes = new List<NewTextBox>();
        public int etheralDamage;
        public int totalHearts;
        public bool etherHeart;
        public int bonusHearts;
        public int bonusDamage;
        private int TBC;
        private bool TBCBool;


        

        public static bool GetCurrentAmmoRed(int ammo)
        //thanks SO MUCH to GabeHasWon#0038. trans rights, baby.
        {
            Player p = Main.LocalPlayer;
            float chance = 0;

            float CombineChances(float p1, float p2) => p1 + p2 - (p1 * p2);
            
            if (p.ammoBox) //1/5 chance to reduce
                chance = 0.2f;

            if (p.ammoCost75) //1/4 chance to reduce
                chance = CombineChances(chance, 0.25f);

            if (p.ammoCost80) //1/5 chance to reduce
                chance = CombineChances(chance, 0.2f);

            if (p.ammoPotion) //1/5 chance to reduce
                chance = CombineChances(chance, 0.2f);

            if (ammo == AmmoID.Arrow && p.magicQuiver) //1/5 chance to reduce for arrows only
                chance = CombineChances(chance, 0.2f);

            if (p.armor[0].type == ItemID.ChlorophyteHelmet) //1/5 chance to reduce -- seems unique??
                chance = CombineChances(chance, 0.2f);

            return Main.rand.NextFloat(1f) > chance;

        }

        public override bool CloneNewInstances => base.CloneNewInstances;
        /*public void OnEnterWorld(Player player)
		{
			// We can refresh UI using OnEnterWorld. OnEnterWorld happens after Load, so nonStopParty is the correct value.
			
		}*/

        // In MP, other clients need accurate information about your player or else bugs happen.
        // clientClone, SyncPlayer, and SendClientChanges, ensure that information is correct.
        // We only need to do this for data that is changed by code not executed by all clients, 
        // or data that needs to be shared while joining a world.

        public void SetFlag(string tag, int value) //this is retarded, setters and getters are java. fucking fix this
        {
            flags[tag] = value;
        }

        public void OpenedMenu()
        {
            MenuState = true;
        }



        public override void OnRespawn(Player player)
        {
            deathCount++;
            if (Main.rand.NextDouble() > 0.90 && deathCount > 4) //stop dyin
            {
                if (flags["DiedALot"] == 0)
                {
                    NewTextBox dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("death1"),
              Text.SmokeyDialogue1.GetTexture("Default"),
              Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                    dialogue.AddBox();

                    dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("death2"),
                       Text.SmokeyDialogue1.GetTexture("Bruh"),
                       Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                    dialogue.AddBox();

                    dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("death3"),
                       Text.SmokeyDialogue1.GetTexture("Angry"),
                       Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                    dialogue.AddBox();
                    flags["DiedALot"] = 1;
                }

            }


        }




        private void FlagLogicSmokeyDefault()
        {

            /*Remember: Senses are split by boss defeats!
             * Boss1 and slime king are treated as equally strong.
             * Preboss comments should be very generic/judgemental
             * Sense by sense she acts a little more human, still cold
             * Some comments about places can be triggered even without circumstances
             * Others can only be seen in certain game states
             */
            try
            {
                if(OpenedChests >= 2 && flags["Loot1"] == 0)
                {
                    NewTextBox dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("Loot"), //guide idiot
                     Text.SmokeyDialogue1.GetTexture("Default"),
                     Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                    dialogue.AddBox();
                    flags["Loot1"] = 1;
                }

                if (OpenedChests >= 4 && flags["Loot1"] == 1)
                {
                    NewTextBox dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("Loot2"), //guide idiot
                     Text.SmokeyDialogue1.GetTexture("Bruh"),
                     Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                    dialogue.AddBox();
                    flags["Loot1"] = 2;
                }
                if (OpenedChests >= 7 && flags["Loot1"] == 2)
                {
                    NewTextBox dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("Loot3"), //guide idiot
                    Text.SmokeyDialogue1.GetTexture("Bruh"),
                    Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                    dialogue.AddBox();

                     dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("Loot3.1"), //guide idiot
                    Text.SmokeyDialogue1.GetTexture("Bruh"),
                    Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                    dialogue.AddBox();
                    dialogue.Pause();
                    dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("Loot3.2"), //guide idiot
                    Text.SmokeyDialogue1.GetTexture("Bruh"),
                    Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                    dialogue.AddBox();

                    flags["Loot1"] = 3;
                }


                    if (daysPassed == 6 || daysPassed == 9 )
                {
                    if(Main.time == 42069)
                    {
                        if (Main.rand.NextBool(1000))
                        {
                            NewTextBox dialogue = new NewTextBox("Booba", //guide idiot
                        "ProjectPhoenix/Gores/image17",
                        Text.SmokeyDialogue1.GetVoice(), true, Color.White, true, false);
                            dialogue.AddBox();
                            dialogue.Pause();
                        }
                       

                    }
                }


                //generic replies

                if (flags["Ritual"] == 0 && Main.tile[(int)player.Center.X / 16, (int)player.Center.Y / 16].wall == WallID.LihzahrdBrickUnsafe)
                {
                    NewTextBox dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("ritual1"), //guide idiot
                    Text.SmokeyDialogue1.GetTexture("Angry"),
                    Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                    dialogue.AddBox();
                    dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("ritual1.1"), //guide idiot
                    Text.SmokeyDialogue1.GetTexture("Bruh"),
                    Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                    dialogue.AddBox();
                    dialogue.Pause();
                    flags["Ritual"] = 1;
                }

                if (player.ZoneJungle && player.ZoneRockLayerHeight && flags["InUndergroundJungle"]==0)
                {

                    flags["InUndergroundJungle"] = 1;
                    NewTextBox dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("preritual1"), //guide idiot
                     Text.SmokeyDialogue1.GetTexture("Bruh"),
                     Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                    dialogue.AddBox();
                    dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("preritual2"), //guide idiot
                     Text.SmokeyDialogue1.GetTexture("Angry"),
                     Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                    dialogue.AddBox();
                    dialogue.Pause();


                }
                if (flags["queenbee"] == 1 && timeStamps["queenbee"] < timer)
                {
                    NewTextBox dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("Queenbee"), //guide idiot
                     Text.SmokeyDialogue1.GetTexture("Default"),
                     Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                    dialogue.AddBox();
                    dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("Queenbeefollow"), //guide idiot
                     Text.SmokeyDialogue1.GetTexture("Bruh"),
                     Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                    dialogue.AddBox();
                    dialogue.Pause();
                    flags["queenbee"] = 2;

                }
                if (player.ZoneUnderworldHeight && flags["SeenHell"] == 0)
                {
                    flags["SeenHell"] = 1;
                    timeStamps["FirstSeeHell"] = timer + 700;

                }
                if (player.ZoneUnderworldHeight && flags["SeenHell"] == 1)
                {
                    NewTextBox dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("hell1"), //guide idiot
                     Text.SmokeyDialogue1.GetTexture("Thinking"),
                     Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                    dialogue.AddBox();
                    dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("hell2"), //guide idiot
                    Text.SmokeyDialogue1.GetTexture("Thinking"),
                    Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                    dialogue.AddBox();
                    flags["SeenHell"] = 2;
                }
                if (player.Center.Y/16 < Main.worldSurface*0.25 && flags["space1"] == 0)
                {
                    NewTextBox dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("Air"), //guide idiot
                     Text.SmokeyDialogue1.GetTexture("Thinking"),
                     Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                    dialogue.AddBox();
                    flags["space1"] = 1;

                }
                if (NPCHelper.FindNPC(NPCID.DungeonGuardian) != -1)
                {
                    timeStamps["dg"] = timer + 60;

                }
                if (timeStamps["dg"] == timer)
                {
                    NewTextBox dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("idiot1"), //guide idiot
                      Text.SmokeyDialogue1.GetTexture("Bruh"),
                      Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                    dialogue.AddBox();

                    dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("idiot2"), //guide idiot
                      Text.SmokeyDialogue1.GetTexture("Bruh"),
                      Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                    dialogue.AddBox(); dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("idiot3"), //guide idiot
                       Text.SmokeyDialogue1.GetTexture("Bruh"),
                       Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                    dialogue.AddBox();
                }



                //so it should be that preboss comments are mostly relagated to more general
                //npc comments should exist outside of these bc funny lol
                if (NPCHelper.FindNPC(NPCID.Merchant) != -1)
                {
                    if (Main.npc[NPCHelper.FindNPC(NPCID.Merchant)].homeless != true)
                    {
                        if (flags["MerchantArrived"] == 0 && !Main.dayTime && Main.npc[NPCHelper.FindNPC(NPCID.Merchant)].Distance(Main.LocalPlayer.Center) < 500)
                        {
                            NewTextBox dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("Merchant1"), //guide idiot
                        Text.SmokeyDialogue1.GetTexture("Bruh"),
                        Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                            dialogue.AddBox();
                            dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("Coingunpre"), //guide idiot
                       Text.SmokeyDialogue1.GetTexture("Thinking"),
                       Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                            dialogue.AddBox();
                            flags["MerchantArrived"] = 1;
                        }
                    }
                }

                if (NPCHelper.FindNPC(NPCID.Nurse) != -1)
                {
                    if (Main.npc[NPCHelper.FindNPC(NPCID.Nurse)].homeless != true)
                    {
                        if (flags["NurseArrived"] == 0 && Main.dayTime && Main.time > 9000 && Main.npc[NPCHelper.FindNPC(NPCID.Nurse)].Distance(Main.LocalPlayer.Center) < 500)
                        {
                            NewTextBox dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("Nurse1"), //guide idiot
                        Text.SmokeyDialogue1.GetTexture("Thinking"),
                        Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                            dialogue.AddBox();
                            dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("Nurse2"), //guide idiot
                       Text.SmokeyDialogue1.GetTexture("Bruh"),
                       Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                            dialogue.AddBox();
                            flags["NurseArrived"] = 1;
                        }
                    }
                }

                if (NPCHelper.FindNPC(NPCID.Guide) != -1) //todo: debug
                {
                    if (Main.npc[NPCHelper.FindNPC(NPCID.Guide)].homeless != true)
                    {
                        if ((daysPassed == 2 || daysPassed == 3) && flags["IdiotGuide"] == 0 && !Main.dayTime && Main.time > 1000 && Main.npc[NPCHelper.FindNPC(NPCID.Guide)].Distance(Main.LocalPlayer.Center) < 500 && Main.npc[NPCHelper.FindNPC(NPCID.Zombie)].Distance(Main.LocalPlayer.Center) < 750)
                        {
                            NewTextBox dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("IdiotGuide1"), //guide idiot
                        Text.SmokeyDialogue1.GetTexture("Angry"),
                        Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                            dialogue.AddBox();
                            flags["IdiotGuide"] = 1;
                        }
                    }
                }
                //preboss dialogue
                if (!NPC.downedSlimeKing && !NPC.downedBoss1)
                {

                    if (Main.bloodMoon && Main.time > 5000 && player.ZoneDirtLayerHeight && flags["BloodyFace"] == 0)
                    {
                        NewTextBox dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("BloodyFace"), //guide idiot
                           Text.SmokeyDialogue1.GetTexture("Default"),
                           Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                        dialogue.AddBox();
                        flags["BloodyFace"] = 1;
                    }
                    if (flags["TheEdge"] == 0 && (Main.LocalPlayer.Center.X/16 <= 100|| Main.LocalPlayer.Center.X/16 >= Main.maxTilesX-70))
                    {
                        NewTextBox dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("edge1"), //guide idiot
                         Text.SmokeyDialogue1.GetTexture("Thinking"),
                         Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                        dialogue.AddBox();
                        dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("edge2"), //guide idiot
                         Text.SmokeyDialogue1.GetTexture("Default"),
                         Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                        dialogue.AddBox();
                        dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("edge3"), //guide idiot
                         Text.SmokeyDialogue1.GetTexture("Bruh"),
                         Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                        dialogue.AddBox();
                        flags["TheEdge"] = 1;
                    }
                    if ((daysPassed < 6 && daysPassed > 1) && NPCHelper.IsEveryoneFuckingHomeless() == -1 && Main.dayTime && Main.time > 20000)
                    {
                        if (flags["HomelessIdiot"] == 0)
                        {
                            NewTextBox dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("homelessidiot1"), //guide idiot
                          Text.SmokeyDialogue1.GetTexture("Bruh"),
                          Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                            dialogue.AddBox();
                            dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("homelessidiot2"), //guide idiot
                            Text.SmokeyDialogue1.GetTexture("Bruh"),
                            Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                            dialogue.AddBox();
                            dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("homelessidiot3"), //guide idiot
                           Text.SmokeyDialogue1.GetTexture("Angry"),
                           Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                            dialogue.AddBox();
                            flags["HomelessIdiot"] = 1;
                        }
                    }






                    if (daysPassed >= 5 && !Main.dayTime && Main.time > 20000 && flags["ALittleTrolling"] == 0 && Main.rand.NextBool(10000))
                    {
                        flags["ALittleTrolling"] = 1;
                        NewTextBox dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("trolling1"), //i am outside
                            Text.SmokeyDialogue1.GetTexture("Smile"),
                            Text.SmokeyDialogue1.GetVoice(), true, Color.White, true, false);
                        dialogue.AddBox();

                        dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("trolling2"),
                   Text.SmokeyDialogue1.GetTexture("Default"),
                   Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                        dialogue.AddBox();

                        dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("trolling3"),
                   Text.SmokeyDialogue1.GetTexture("Bruh"),
                   Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                        dialogue.AddBox();
                    }
                    if (daysPassed >= 7 && Main.time > 500)
                    {
                        if (deathCount == 0)
                        {
                            if (flags["ComplimentNotDying"] == 0)
                            {
                                flags["ComplimentNotDying"] = 1;

                                NewTextBox dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("goodjob1"), //not dead!
                            Text.SmokeyDialogue1.GetTexture("Thinking"),
                            Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                                dialogue.AddBox();

                                dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("goodjob2"),
                           Text.SmokeyDialogue1.GetTexture("Thinking"),
                           Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                                dialogue.AddBox();

                                dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("goodjob3"),
                           Text.SmokeyDialogue1.GetTexture("Default"),
                           Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                                dialogue.AddBox();
                            }

                        }
                        else
                        {
                            flags["ComplimentNotDying"] = 2;
                        }
                    }



                    if (flags["WeaponComment"] == 1)
                    {
                        //Main.NewText("roast");
                        if (timer % 60 == 0)
                        {
                            if (PlayerHelper.CheckForItem(ItemID.WoodenSword, Main.LocalPlayer) != -1)
                            {
                                NewTextBox dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("copper3"), //nice sword bro
                            Text.SmokeyDialogue1.GetTexture("Bruh"),
                            Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                                dialogue.AddBox();

                                flags["WeaponComment"] = 2;
                            }
                        }
                    }
                    if (daysPassed == 2 && Main.time > 10000 && flags["WeaponComment"] == 0)
                    {
                        if (PlayerHelper.CheckForItem(ItemID.WoodenSword, Main.LocalPlayer) != -1) flags["WeaponComment"] = 2;

                        if (PlayerHelper.CheckForItem(ItemID.CopperShortsword, Main.LocalPlayer) != -1 && flags["WeaponComment"] != -2) //nice shite sword
                        {


                            NewTextBox dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("copper1"),
                             Text.SmokeyDialogue1.GetTexture("Default"),
                             Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                            dialogue.AddBox();
                            dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("copper2"),
                             Text.SmokeyDialogue1.GetTexture("Suprise"),
                             Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                            dialogue.AddBox();
                            flags["WeaponComment"] = 1;

                        }
                    }

                    /*
                     * Main.time - A value between 0 (4:30 AM) and 54000 (7:30 PM) during the day and between 0 (7:30 PM) and 32400 (4:30 AM) during the night. Use with Main.dayTime. Main.time usually increments by 1 each tick. Each in-game hour is 3600 ticks.

                         Example: Main.dayTime && Main.time < 18000.0 - Morning between 4:30 AM and 9:30 AM (because 18000/3600 == 5)

                     * 
                     * 
                     */


                }
                if (NPC.downedBoss1 || NPC.downedSlimeKing)
                {
                    if ((!Main.LocalPlayer.ZoneCorrupt || !Main.LocalPlayer.ZoneCrimson)&& Main.LocalPlayer.ZoneSnow && flags["SnowSmell"] == 0 && player.ZoneOverworldHeight)
                    {
                        flags["SnowSmell"] = 1;
                        timeStamps["SnowTimer"] = timer + 200;

                    }
                    if (timeStamps["SnowTimer"] < timer && flags["SnowSmell"] == 1)
                    {
                        NewTextBox dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("SmellSnow"), //guide idiot
                          Text.SmokeyDialogue1.GetTexture("Thinking"),
                          Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                        dialogue.AddBox();
                        dialogue.Pause();
                        flags["SnowSmell"] = 2;
                        flags["SnowSmell"] = 2;
                    }
                    if (Main.LocalPlayer.ZoneCorrupt && flags["CorruptionSmell"] == 0 && player.ZoneOverworldHeight)
                    {
                        flags["CorruptionSmell"] = 1;
                        timeStamps["CorruptionTimer"] = timer + 400;
                    }
                    if (timeStamps["CorruptionTimer"] < timer && flags["CorruptionSmell"] == 1)
                    {
                        NewTextBox dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("corruption1"), //guide idiot
                          Text.SmokeyDialogue1.GetTexture("Default"),
                          Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                        dialogue.AddBox();
                        flags["CorruptionSmell"] = 2;
                    }





                    if (Main.LocalPlayer.ZoneCrimson && flags["CrimsonSmell"] == 0 && player.ZoneOverworldHeight)
                    {
                        flags["CrimsonSmell"] = 1;
                        timeStamps["CrimsonTimer"] = timer + 400;
                    }
                    if (timeStamps["CrimsonTimer"] < timer && flags["CrimsonSmell"] == 1)
                    {
                        NewTextBox dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("crimson1"), //guide idiot
                          Text.SmokeyDialogue1.GetTexture("Default"),
                          Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                        dialogue.AddBox();
                        flags["CrimsonSmell"] = 2;
                    }
                    if (player.ZoneGlowshroom && flags["ShroomJoke"] == 0)
                    {
                        flags["ShroomJoke"] = 1;
                        timeStamps["MushroomEnter"] = timer + 180;
                    }
                    if (timeStamps["MushroomEnter"] < timer && flags["ShroomJoke"] == 1)
                    {
                        NewTextBox dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("glowshroom1"), //guide idiot
                          Text.SmokeyDialogue1.GetTexture("Smile"),
                          "Sounds/Custom/TalkSoundSmokeyHigh", true, Color.White, true, false,new Vector2(1,4));
                        dialogue.AddBox();
                        flags["ShroomJoke"] = 2;
                    }
                    if (!player.ZoneGlowshroom && flags["ShroomJoke"] == 2 && flags["PostShroomJoke"] == 0)
                    {
                        flags["PostShroomJoke"] = 1;
                        timeStamps["PostMushroomEnter"] = timer + 420;
                    }
                    if (timeStamps["PostMushroomEnter"] < timer && flags["PostShroomJoke"] == 1)
                    {
                        if (player.ZoneGlowshroom)
                        {
                            timeStamps["PostMushroomEnter"] = timer + 420;

                        }
                        NewTextBox dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("postglowshroom1"), //guide idiot
                          Text.SmokeyDialogue1.GetTexture("Bruh"),
                          Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true,new Vector2(1, 2));
                        dialogue.AddBox();
                        flags["PostShroomJoke"] = 2;
                    }

                }

                
                if (NPC.downedBoss1 && flags["AmogusEye"] != 2)
                {
                    if (flags["AmogusEye"] == 0)
                    {
                        timeStamps["AmogusEye"] = timer + 1000;
                        flags["AmogusEye"] = 1;
                    }
                    if (timeStamps["AmogusEye"] < timer)
                    {
                        NewTextBox dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("eyedown1"),
                Text.SmokeyDialogue1.GetTexture("Thinking"),
                Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                        dialogue.AddBox();
                        dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("eyedown2"),
                Text.SmokeyDialogue1.GetTexture("Bruh"),
                Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                        dialogue.AddBox();
                        dialogue.Pause();

                        flags["AmogusEye"] = 2;
                    }
                }
                if (NPC.downedBoss2)
                {
                    if (deathCount < 3 && flags["coky"] == 0)
                    {
                        NewTextBox dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("cocky"), //guide idiot
                     Text.SmokeyDialogue1.GetTexture("FuckOff"),
                     Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                        dialogue.AddBox();
                        dialogue.Pause();

                        flags["coky"] = 1;
                    }

                    if (player.ZoneBeach && flags["HearBeach"] == 0)
                    {
                        NewTextBox dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("OceanHear1"), //guide idiot
                                            Text.SmokeyDialogue1.GetTexture("Thinking"),
                                            Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                        dialogue.AddBox();
                        dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("OceanHear2"), //guide idiot
                    Text.SmokeyDialogue1.GetTexture("Smile"),
                    Text.SmokeyDialogue1.GetVoice(), true, Color.White, true, false);
                        dialogue.AddBox();
                        dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("OceanHear3"), //guide idiot
                     Text.SmokeyDialogue1.GetTexture("Thinking"),
                     Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                        dialogue.AddBox();
                        dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("OceanHear4"), //guide idiot
                     Text.SmokeyDialogue1.GetTexture("Default"),
                     Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                        dialogue.AddBox();
                        flags["HearBeach"] = 1;
                    }

                    if (WorldGen.crimson)
                    {

                    }
                    else
                    {

                    }
                }
                if (etherHeart == true)
                {
                    if (flags["EatEther"] == 0)
                    {
                        timeStamps["CoreTimer"] = timer + 180;
                        flags["EatEther"] = 1;


                    }
                    if (flags["EatEther"] == 1)
                    {
                        if (timer > timeStamps["CoreTimer"]) //you ate the core? wtf?
                        {


                            NewTextBox dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("core1"),
                            Text.SmokeyDialogue1.GetTexture("Suprise"),
                            Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                            dialogue.AddBox();

                            dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("core2"),
                               Text.SmokeyDialogue1.GetTexture("Suprise"),
                               Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                            dialogue.AddBox();

                            dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText("core3"),
                               Text.SmokeyDialogue1.GetTexture("Bruh"),
                               Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                            dialogue.AddBox();


                            flags["EatEther"] = 2;
                        }
                    }
                }



                if (timeStamps["FirstTextStamp"] == timer)
                {
                    if (flags["BindKey"] == 0)
                    {
                        flags["BindKey"] = 1;
                        Main.NewText("Use Controls to bind the Dialogue Key. Use it to interact with text boxes.");
                    }
                }
            }
            catch
            {
                ProjectPhoenix.Instance.Logger.Debug("Critical error in ModPlayer block. Likely due to a typo! Disable the mod and report this!");

                WorldGen.SaveAndQuit();
            }
        }
        private void ChestLootActive()
        {
            //Main.NewText(OpenedChests);
            int worldID = 0;
            if (ChestCanBeLoaded)

            {
                if (player.chest >= 0)
                {


                    for (int i = 0; i < WorldNames.Count; i++)
                    {
                        if (WorldNames[i] == Main.worldID) worldID = i;
                    }

                    for (int j = 0; j < Chest[worldID].Count; j++)
                    {
                        /*if (Vector2.Distance(new Vector2(player.Center.X/16f,player.Center.Y/16f),new Vector2(Chest[worldID][j].X, Chest[worldID][j].Y)) < 1)
                        {
                            Main.NewText("mm ches");
                        }*/
                        if (player.chest >= 0 && player?.chest != null)
                        {
                            if ((Main.chest[player.chest].GetType() == new Terraria.Chest().GetType()) && !OpenedChestPositions.Contains(new Vector2(Chest[worldID][j].X, Chest[worldID][j].Y)) && (Main.chest[player.chest].x == Chest[worldID][j].X) && (Main.chest[player.chest].y == Chest[worldID][j].Y) && (Chest[worldID][j].Z == 1))
                            {
                                OpenedChestPositions.Add(new Vector2(Chest[worldID][j].X, Chest[worldID][j].Y));
                                OpenedChests++;
                                //  Main.NewText(OpenedChests);

                                if (((Main.rand.NextBool(5)) || (OpenedChests == 5)) && (flags["RandomDrop1"] == 0))
                                {
                                    for (int g = 0; g < Main.chest[player.chest].item.Length; g++)
                                    {
                                        if (Main.chest[player.chest].item[g].stack == 0)
                                        {
                                            flags["RandomDrop1"] = 1;
                                            Item item = new Item();
                                            item.SetDefaults(ModContent.ItemType<CorruptedCore>());
                                            item.stack = 1;
                                            Main.chest[player.chest].item[g] = item;
                                            g = Main.chest[player.chest].item.Length;
                                        }
                                    }
                                    if (flags["RandomDrop1"] == 0) ModContent.GetInstance<ProjectPhoenix>().Logger.Debug("Sosmehow, couldn't fit an item into a loot table. If you're reading this, report it ASAP!");

                                }
                            }
                        }

                    }


                }
            }
            bool k;
            List<Vector3> Temp = new List<Vector3>();


            if (WorldNames.Count > 0)
            {
                for (int i = 0; i < WorldNames.Count; i++)
                {
                    if (WorldNames[i] == Main.worldID)
                    {
                        ChestCanBeLoaded = true;
                    }
                    else
                    {
                        ChestCanBeLoaded = false;
                    }
                }
            }

            if (!ChestCanBeLoaded)
            {
                timeStamps["ChestTimer"]++;
                if (timeStamps["ChestTimer"] > 2)
                {
                    WorldNames.Add(Main.worldID);
                    for (int i = 0; i < Main.chest.Length; i++)
                    {
                        if (Main.chest[i] == null)
                        {

                        }
                        else
                        {
                            Temp.Add(new Vector3(Main.chest[i].x, Main.chest[i].y, 0));
                            if ((Main.chest[i].y > Main.rockLayer) && (Main.chest[i].y < Main.maxTilesY - 200))
                            {
                                Temp[i] = new Vector3(Temp[i].X, Temp[i].Y, 1);
                            }
                        }


                    }
                    ChestCanBeLoaded = true;
                    Chest.Add(Temp);
                }

            }

            /*{
                if(WorldNames.Count > 0)
                {
                    foreach(int i in WorldNames)
                    {
                        Main.NewText(i);
                    }
                }
            }*/
        }
        private void GenericUpdate()
        {



            /*R - 20 - 255
G - 14 - 182
B - 4 - 66
             * 
             * 
             * 
             */
            if (Main.netMode != NetmodeID.Server && !Filters.Scene["Tint"].IsActive())
            {
                //Filters.Scene.Activate("Tint", player.Center).GetShader().UseTargetPosition(player.Center).UseColor(new Color(255,255,255,0));
                // Main.NewText(Color.Orange);
            }



            if (Main.netMode != NetmodeID.Server && Filters.Scene["Tint"].IsActive())
            {
                // Main.NewText("tinting");
            }




            //troll
            //Main.LocalPlayer.maxRunSpeed = 999;
            //Main.LocalPlayer.maxFallSpeed = 999;
            //

            // Main.NewText(NPCHelper.IsEveryoneFuckingHomeless());

            if (timeHelper == 0) if (!Main.dayTime) timeHelper = 1;

            if (timeHelper == 1)
            {
                if (Main.dayTime)
                {
                    timeHelper = 0;
                    daysPassed++;
                }
            }

            timer++;
            if (timer % 60 == 0)
            {
                timePlayed++;
            }
        }
        public void HandleTextBoxes()
        {
            //todo: gaps between boxes added so she doesn't spam
            //not sure how to do that considering the absolute state of this system
            //simply enough, we're gonna manually add a stop character
            //newtextbox.Pause()
            if (textBoxes.Count > 0)
            {
                for (int i = 0; i < textBoxes.Count; i++)
                {
                    if (!MenuState)
                    {
                        if (textBoxes[i].GetText().Equals("PauseCharacter")&&!TBCBool)
                        {
                            Main.PlaySound(ProjectPhoenix.Instance.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/MenuClose"));
                            TBCBool = true;
                            TBC = 180;
                            i = textBoxes.Count;
                        }
                        else if(textBoxes[i].GetText().Equals("PauseCharacter")&&TBCBool)
                        {
                            TBC--;
                            if(TBC == 0)
                            {
                                TBCBool = false;
                                textBoxes.RemoveAt(i);
                                lastInteraction = timer;

                            }
                            i = textBoxes.Count;

                        }
                        else
                        {
                            ProjectPhoenix.Instance.ShowMyUI(textBoxes[i]);
                            MenuState = true;
                        }
                       

                    }
                }
            }
            if (heldKeyP || ButtonBeenClicked)
            {
                ButtonBeenClicked = false;
                if (MenuState)
                {

                    if (textBoxes.Count > 1)
                    {
                        MenuState = ProjectPhoenix.Instance.SkipText(false);

                    }
                    else
                    {
                        MenuState = ProjectPhoenix.Instance.SkipText(true);

                    }
                    if (MenuState == false)
                    {
                        textBoxes.RemoveAt(0);

                    }
                }

            }

        }
        public override void PreUpdate()
        {
            GenericUpdate();
            ChestLootActive();
            if (true)
            {
                FlagLogicSmokeyDefault();
            }
            //this handles all regular interaction, should not be ran during the death or betrayal routes
            HandleTextBoxes();

            base.PreUpdate();
        }
        public override void ProcessTriggers(TriggersSet triggersSet)
        {





            if (ProjectPhoenix.TextBoxTest.JustPressed)
            {
                heldKeyP = true;
            }
            else
            {
                heldKeyP = false;
            }

            /*
            if (ProjectPhoenix.TextBoxTest2.JustPressed)
            {
                MenuState = !MenuState;
                if (MenuState)
                {

                    // ProjectPhoenix.Instance.ShowMyUI(@"According to all known laws of aviation, there is no way a bee should be able to fly. Its wings are too small to get its fat little body off the ground. The bee, of course, flies anyway because bees don't care what humans think is impossible.");
                    ProjectPhoenix.Instance.ShowMyUI(Text.SmokeyDialogue1.DialogueBox(Main.rand.Next(3)), Text.SmokeyDialogue1.FaceRef(Main.rand.Next(2)), Text.SmokeyDialogue1.VoiceRef(),true,Color.Black);
                }
                else
                {

                   // ProjectPhoenix.Instance.HideMyUI();

                }
            }*/ //debug code

        }
        public override TagCompound Save()
        {
            List<int> values = new List<int>();
            if (flags?.Values != null)
            {
                values.AddRange(flags.Values.ToList());
            }
            return new TagCompound {
				// {"somethingelse", somethingelse}, // To save more data, add additional lines
				{"Bonus Damage 1", etheralDamage},
                {"Ether", etherHeart},
                {"Non-Ether", totalHearts},
                {"Life Fruits", bonusHearts},
                {"Life Fruit Dam", bonusDamage},
                {"Player Flags",values},
                {"Player Time Played",timePlayed},
                {"Days Passed",daysPassed},
                {"Death Count",deathCount },
                {"Chest Array",Chest },
                {"Safe to load Chest?",ChestCanBeLoaded },
                {"World Names",WorldNames},
                {"Opened Chests",OpenedChestPositions },
                {"OC",OpenedChests }



            };
            // Read https://github.com/tModLoader/tModLoader/wiki/Saving-and-loading-using-TagCompound to better understand Saving and Loading data.
        }
        public override void Load(TagCompound tag)
        {
            OpenedChests = tag.GetInt("OC");
            timer = 0;
            flags = new Dictionary<string, int>();
            timeStamps = new Dictionary<string, int>();

            tags = allTags.ToList();
            values = new List<int>();

            List<int> test = (List<int>)tag.GetList<int>("Player Flags");
            while (test.Count < tags.Count)
                test.Add(0);
            values = new List<int>(test);
            WorldNames = (List<int>)tag.GetList<int>("World Names");
            for (int i = 0; i < tags.Count; i++)
            {
                flags.Add(tags[i], values[i]);
            }
            if (tag.GetBool("Safe to load Chest?"))
            {
                Chest = (List<List<Vector3>>)tag.GetList<List<Vector3>>("Chest Array");
            }
            ChestCanBeLoaded = tag.GetBool("Safe to load Chest?");
            OpenedChestPositions = (List<Vector2>)tag.GetList<Vector2>("Opened Chests");
            if (timeStamps.Count == 0)
            {
                timeStamps.Add("FirstTextStamp", 300);
                timeStamps.Add("CrimsonTimer", 0);
                timeStamps.Add("CorruptionTimer", 0);
                timeStamps.Add("CoreTimer", 0);
                timeStamps.Add("AmogusEye", 0);
                timeStamps.Add("MushroomEnter", 0);
                timeStamps.Add("PostMushroomEnter", 0);
                timeStamps.Add("FirstSeeHell", 0);
                timeStamps.Add("SnowTimer", 0);
                timeStamps.Add("ChestTimer", 0);
                timeStamps.Add("dg", 0);
                timeStamps.Add("queenbee", 0);


            }
            etheralDamage = tag.GetInt("Bonus Damage 1");
            etherHeart = tag.GetBool("Ether");
            totalHearts = tag.GetInt("Non-Ether");
            bonusHearts = tag.GetInt("Life Fruits");
            bonusDamage = tag.GetInt("Life Fruit Dam");
            timePlayed = tag.GetInt("Player Time Played");
            daysPassed = tag.GetInt("Days Passed");
            deathCount = tag.GetInt("Death Count");

            // ModContent.GetInstance<ProjectPhoenix>().Logger.Debug(string.Join("What I can see: ", flags));
            /*
            */
        }
        public override void SetupStartInventory(IList<Item> items, bool mediumcoreDeath)
        {
            Item item = new Item();
            item.SetDefaults(ModContent.ItemType<CorruptedCore>());
            item.stack = 1;
            items.Add(item);
            //poopman be praised
        }
        public override void ModifyWeaponDamage(Item item, ref float add, ref float mult, ref float flat)
        {
            //bonus damage is cut in half, or it'd be fucking busted
            //20 life fruits + 15 crystals = +35 flat damage!
            //maybe that's kinda cool ngl.
            //we'll leave it cut in half, but if people say the damage bonus isn't worth the price
            //just cut it out

            //okay now its flat as of 11.29
            flat += (bonusDamage + etheralDamage);
            if (etherHeart) flat += 5;
        }

    }
}