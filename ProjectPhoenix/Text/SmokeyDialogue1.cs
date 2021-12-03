using ProjectPhoenix.Buffs;
using ProjectPhoenix.Dusts;
using ProjectPhoenix.Items;
using ProjectPhoenix.Items.Weapons;
using ProjectPhoenix.NPCs;
using ProjectPhoenix.NPCs.Bosses.Watcher;
using ProjectPhoenix.Projectiles;
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

namespace ProjectPhoenix.Text
{
    class SmokeyDialogue1
    {

        public static string Voice;
        public static Dictionary<string, string> Sus;
        public static Dictionary<string, string> Textures;
        public static void UnloadText()
        {
            Sus?.Clear();
            Textures?.Clear();
            Sus = null;
            Textures = null;
            Voice = null;
        }
        public static void LoadText()
        {

            Sus = new Dictionary<string, string>();
            Voice = "Sounds/Custom/TalkSoundSmokey";
            Textures = new Dictionary<string, string>();

            //at 59
            //one sense activates per boss. smell happens after boss1, so retcon those smells.
            Textures.Add("Angry", "ProjectPhoenix/UI/Port/Smokey/SmokeyAngry");
            Textures.Add("Blank", "ProjectPhoenix/UI/Port/Smokey/SmokeyBlankStare");
            Textures.Add("Bruh", "ProjectPhoenix/UI/Port/Smokey/SmokeyBruh");
            Textures.Add("Default", "ProjectPhoenix/UI/Port/Smokey/SmokeyDefault");
            Textures.Add("FuckOff", "ProjectPhoenix/UI/Port/Smokey/SmokeyFuckOff");
            Textures.Add("Smile", "ProjectPhoenix/UI/Port/Smokey/SmokeySmile");
            Textures.Add("Suprise", "ProjectPhoenix/UI/Port/Smokey/SmokeySuprise");
            Textures.Add("Sus", "ProjectPhoenix/UI/Port/Smokey/SmokeySus");
            Textures.Add("Thinking", "ProjectPhoenix/UI/Port/Smokey/SmokeyThinking");


            //draft 1
            Sus.Add("preritual1", "There's a... temple in this jungle. My advice? ^^^^Stay far, far away from it. Especially right now.");
            Sus.Add("preritual2","Don't you even think about it.");
            Sus.Add("ritual1", "Get out of there, it's too dangerous. Under no circumstances should you ^^EVER^^ need to continue through here.");
            Sus.Add("ritual1.1", "It's locked, anyway. \nJust leave.^^^^^^^^^ \nNow.");
            Sus.Add("ritual2", "What part of 'get out' did you not understand?! This place is a giant trap, you limp, arrogant blockhead!");
            Sus.Add("ritual3", "There is NOTHING here you need. Get out.");
            Sus.Add("ritual4", "You wouldn't. No. No way. You can't do this to me.");
        
            Sus.Add("Queenbee", "Killing her only daughter to bait her out of hiding. You're cunning.");
            Sus.Add("Queenbeefollow", "Or she's just a terrible, t^^e^^r^^r^^i^^b^^l^^e^^ parent.");
            Sus.Add("Loot", "I admit I'm not one for lusting after shiny things. ^^B^^u^^u^^u^^u^^u^^u^^t^^ I see the appeal.");
            Sus.Add("Loot2", "Got enough?");
            Sus.Add("Loot3", "Seems like a lot of danger for some^^^^.^^^^.^^^^.^^^^ junk. But you know what they say.");
            Sus.Add("Loot3.1", "'A Phoenix's beak shines best at its peak.'");
            Sus.Add("Loot3.2", "...You wouldn't get it. Too^^^ young.");

            Sus.Add("Multiplayer1", "More Riftwalkers. Don't trust them.");
            Sus.Add("PVP", "Told you.");

            Sus.Add("RareKillLine1", "Back to sleep.");
            Sus.Add("RareKillLine2", "Disappointing.");
            Sus.Add("RareKillLine3", "Hide your coins better next time.");
            Sus.Add("RareKillLine4", "Goodnight.");


            Sus.Add("DONOTUSE","^ is a 5 frame interval! They get changed to nothing so feel free to spam them for delays between letters, or words.");
            Sus.Add("UndergroundLong", "You planning on getting fresh air anytime soon? Or are you just a tad too...^^^^^^^ focused, let's say?");
            Sus.Add("FishingLong", "Tell me when you've caught anything more interesting than some old shoes and a soaked wooden box.");
            Sus.Add("Horny", "Fanservice!");
            Sus.Add("DesertHouse", "I'm sure it'll make for an amazing tourist destination. Heatstroke is a wonderful selling point.");
            Sus.Add("SmellSnow", "Smells like...^^^^^^^^^^ Christmas. It's fascinating. Despite everything, it hasn't been forgotten.");
            Sus.Add("notalk", "You're quite quiet. A good listener, ^^^or everything I say goes in one ear and right out the other.");
            Sus.Add("ranger", "You'd be quite the sharpshooter if your small fortune's worth of bullets actually hit their targets.");
            Sus.Add("Melee", "You're brave. Or very, very stupid. I hope it's the former for both of our sakes.");
            Sus.Add("Summoner", "The commander type, I see. Let your minions do the work.");
            Sus.Add("Magic", "You make it look so easy. Wou;d you believe me if I told you that when I was.^^^.^^^.^^^ alive, magic didn't exist?");
            Sus.Add("Magic2", "But for your kind it's as natural as breathing. ");
            Sus.Add("MagicDrown", "Which you aren't very good at, mind you.");
            Sus.Add("ModdedGeneric", "This place is so utterly unnatural...");
            Sus.Add("glowshroom1", "Ooooh.^^^^ Ehe^^^^^heh. Smel^^^^ls^ weird... eh^e^heh...^ heh....");
            Sus.Add("postglowshroom1", "Ngggh.^^^.^^^.^^^ my head hurts. What ^^^^W^^^^E^^^^R^^^^E^^^^ you doing?!");
            Sus.Add("townbig1", "Quite the wretched group of freeloaders you have.");
            Sus.Add("townbig2", "Ever consider taxing them? It's the only constant of life.");
            Sus.Add("sleep", "Are you not tired?");
            Sus.Add("Marble", "Oddly^^^^^^^ familiar place.");
            Sus.Add("Granite", "Something powerful lurks here.");
            Sus.Add("UndergroundDialogue1", "Finally.^^^^^ Some real exploration.");
            Sus.Add("UndergroundDialogue2", "Dark, cold, alone underground. The scent of death in the air.");
            Sus.Add("UndergroundDialogue3", ".^^^.^^^.^^^what?^^^^^^ You don't like it?");
            Sus.Add("BloodyFace", "You've got blood on your face.");
            Sus.Add("DontDare", "Don't you dare.");
            Sus.Add("Dontyoudare1", "Don't.");
            Sus.Add("Dontyoudare2", "You.");
            Sus.Add("Dontyoudare3", "Dare.");
            Sus.Add("IdiotGuide1", "He's going to open the door, isn't he?^^^^^^^^^ Right as the zombies come?^^^^^^^ Idiot.");
            Sus.Add("IdiotGuide2", "A sacrifical tool and nothing more.");
            Sus.Add("Merchant1", "Money grubbing bastard. Why would you let him move in?");
            Sus.Add("Coingunpre", "Coins are better spent being embedding in people's skulls at high speed.");
            Sus.Add("Nurse1", "She seems^^^ nice.");
            Sus.Add("Nurse2", "I'm joking. Those prices are absurd.");
            Sus.Add("trolling1", "I'm standing outside your house.");
            Sus.Add("trolling2", "I'm joking.");
            Sus.Add("trolling3", "If only.");
            Sus.Add("core1", "You^^^^^ consumed the core?");
            Sus.Add("core2", "What is WR^^^^O^^^^N^^^G^^^^ with you!?");
            Sus.Add("core3", "Utterly repulsive. By the Torivori...");
            Sus.Add("postcore1", "That core, the.^^^.^^^.^^^ ^^^^^^fleshy thing.");
            Sus.Add("postcore2", "You must be desperate.");
            Sus.Add("postcore3", "To think that you'd be willing to let that fester inside you.");
            Sus.Add("copper1", "Have you considered using something more effective yet?");
            Sus.Add("copper2", "I'm not-^^^^^ I'm not saying I doubt your competence,^^^^^ but that's nothing but^ a piece of copper on a stick. ");
            Sus.Add("copper3", "That's better. A piece of wood on a stick. R^^e^^a^^l^^l^^y^^ gonna show those slimes. ");
            Sus.Add("idiot1", ".^^^^^^^^^.^^^^^^^^^.");
            Sus.Add("idiot2", ".^^^^^^.^^^^^^.^^^^^^.^^^^^^.^^^^^^.^^^^^^.^^^^^^.^^^^^^.^^^^^^.");
            Sus.Add("idiot3", "Just.^^^^^ Bask^^^ in your own stupidity^^^ for a moment.");
            Sus.Add("death1", "That looked like it hurt.");
            Sus.Add("death2", "As I now have an ache in my skull from when your head hit the ground.");
            Sus.Add("death3", "Stop being s^^o^^ damn clumsy.");
            Sus.Add("goodjob1", "Nice job on surviving so well this far.");
            Sus.Add("goodjob2", "I had low expectations, and you're a^^^l^^^m^^^o^^^s^^^t^^^ meeting them.");
            Sus.Add("goodjob3", "Almost^^.");
            Sus.Add("ambient1", "I sense an^^^ evil presence watching you.");
            Sus.Add("wormpanic1", "EW! WORM!"); 
            Sus.Add("wormpanic2", "WORM! WORM WORM WORM!");
            Sus.Add("wormpanic3", "KILL IT KILL IT KILL IT NOW!");
            Sus.Add("wormpanic4", "WORM!!");
            Sus.Add("wormpanic5", "We did it-^^^ I mean, you.^^^ You-^^^^^^ you did it. Nice work.");
            Sus.Add("boc1", "Giant brain. Stay focused.");
            Sus.Add("boc2", "Stay focused.");
            Sus.Add("boc3", "PlayerName.");
            Sus.Add("boc4", "PlayerNamePlayerNamePlayerNamePlayerNamePlayerNamePlayerNamePlayerNamePlayerNamePlayerNamePlayerNamePlayerNamePlayerNamePlayerNamePlayerNamePlayerNamePlayerNamePlayerNamePlayerNamePlayerNamePlayerNamePlayerNamePlayerNamePlayerNamePlayerNamePlayerNamePlayerNamePlayerName.");
            Sus.Add("boc5", "PlayerName had their entrails smeared across the wall by .");
            Sus.Add("boc6", "I could have sworn I heard someone.");
            Sus.Add("Air", "Believe it or not, people used to be unable to breathe up here.");
            Sus.Add("cocky", "What a pushover.");
            Sus.Add("edge1", "What's wrong with this edge?");
            Sus.Add("edge2", "You should be able to...");
            Sus.Add("edge3", "Hm.");
            Sus.Add("hell1", "Now that's a familiar sight.");
            Sus.Add("hell2", "You recognize it too, I can tell.");
            Sus.Add("corruption1", "Smells like an open wound.");
            Sus.Add("crimson1", "Smells like a butcher shop.");
            Sus.Add("???1", "Smells like...^^^^^ ");
            Sus.Add("???2", "Smells like pain.");
            Sus.Add("???3", "Quiet.");
            Sus.Add("homelessidiot1", "You have the power to create anything you want.");
            Sus.Add("homelessidiot2", "And you choose to be homeless.");
            Sus.Add("homelessidiot3", "I'm bound to a m^^^o^^^r^^^o^^^n^^^.");
            Sus.Add("eyedown1", "'The Big Eye.'^^^ The townsfolk will rest easier now, which means they'll be easy prey for the nightmare demons.");
            Sus.Add("eyedown2", "I'm joking.^^^^^^^^^^^^^ \nMostly.");
            Sus.Add("corehold", "Still holding onto that^^^ disgusting core?");
            Sus.Add("ip", "192.81.99.24");
            Sus.Add("Dungeon1", "Smells like calcium.");
            Sus.Add("KingSlime1", "Looks tasty.^^^^ But don't be tempted.");
            Sus.Add("WallOfFlesh", "You're insane.");
            Sus.Add("OceanHear1", "There used to be waves here.");
            Sus.Add("OceanHear2", "You'd have loved the sound.");
            Sus.Add("OceanHear3", "It was beautiful. A raw display of power. Massive amounts of water crashing against the sand.^^^^^^.^^^^^^.");
            Sus.Add("OceanHear4", "But^^^^^^^^^ this ocean hasn't moved in^^^^ decades. Something's^^^^^^^ off.");
            Sus.Add("Painter1", "He just sprays colors! He doesn't even paint the pictures he sells! He's a ripoff! A sham! You let the WORST people into this town!");
            Sus.Add("Voodoo1", "A doll?^^^^ Of GuideName?");
            Sus.Add("Voodoo2", "Stab it.^^^ I want to see if he squirms.");
            Sus.Add("Voodoo3", "Or.^^^^^.^^^^^.^^^^^ a lava bath would be lovely, don't you think?");

            //Sus.Add("Temple1", "An old rital site. Would you kindly stay away from it?");
          
           
            Sus.Add("f1", "...");
            Sus.Add("f2", "Stay dead.");





            //exploration

            //HerText[46] = "These dark caves, I'm not a fan.";

            //npc comments

            //general

            //items

            //deat

            //boss responses

            //  HerText[58] = "So much gel. Make a smoothie already.";

            //new location

            //housing

        }
        public static string GetVoice(bool high)
        {
            return Voice;
        }
        public static string GetVoice()
        {
            return Voice;
        }
        public static string GetTexture(string input)
        {
            try
            {
                return Textures[input];

            }
            catch
            {
                return "Failure to access texture " + input + "!";
            }

        }
        public static string GetText(string input)
        {
            try
            {
                return Sus[input];

            }
            catch
            {
                return "Failure to access line " + input + "!";
            }
        }
    }
}
