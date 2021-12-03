using Microsoft.Xna.Framework;
using ProjectPhoenix.UI;
using ReLogic.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

using ProjectPhoenix.Dusts;
using System;
using Terraria.ID;
using ProjectPhoenix.Projectiles.Magic;
using ProjectPhoenix;
using ProjectPhoenix.Projectiles.Enemy.Boss.Watcher;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.Graphics.Effects;

namespace ProjectPhoenix
{
    class ProjectPhoenix : Mod
    {
        public static string blankTexture;

        public PhoenixModPlayer playboy => Main.LocalPlayer.GetModPlayer<PhoenixModPlayer>();
        public static DynamicSpriteFont Smokey;
        internal UserInterface MyInterface;
        internal Textbox Texting;
        //internal TextboxInternal TextboxI;
        public ProjectPhoenix()
        {
        }
        public static ProjectPhoenix Instance;
        public static ModHotKey TextBoxTest;
        public static ModHotKey TextBoxTest2;

        private void LoadShaders()
        {
            // First, you load in your shader file.
            // You'll have to do this regardless of what kind of shader it is,
            // and you'll have to do it for every shader file.
            // This example assumes you have both armour and screen shaders.

            Ref<Effect> tint = new Ref<Effect>(GetEffect("Effects/MyBalls/Tint")); // The path to the compiled shader file.
            Filters.Scene["Tint"] = new Filter(new ScreenShaderData(tint, "passy"), EffectPriority.VeryHigh);
            Filters.Scene["Tint"].Load();

            GameShaders.Armor.BindShader(ModContent.ItemType<Items.Dyes.TestDye>(), new ArmorShaderData(new Ref<Effect>(GetEffect("Effects/MyBalls/Experimental")), "ok")).UseColor(Color.Yellow);
            Ref<Effect> screenRef = new Ref<Effect>(GetEffect("Effects/MyBalls/Shockwave")); // The path to the compiled shader file.
            Filters.Scene["Shockwave"] = new Filter(new ScreenShaderData(screenRef, "Shockwave"), EffectPriority.VeryHigh);
            Filters.Scene["Shockwave"].Load();


            // To add a dye, simply add this for every dye you want to add.
            // "PassName" should correspond to the name of your pass within the *technique*,
            // so if you get an error here, make sure you've spelled it right across your effect file.


            // If your dye takes specific parameters such as colour, you can append them after binding the shader.
            // IntelliSense should be able to help you out here.  

            // To bind a miscellaneous, non-filter effect, use this.
            // If you're actually using this, you probably already know what you're doing anyway.

            //GameShaders.Misc["EffectName"] = new MiscShaderData(specialref, "PassName");

            // To bind a screen shader, use this.
            // EffectPriority should be set to whatever you think is reasonable.  

            //Filters.Scene["FilterName"] = new Filter(new ScreenShaderData(filterRef, "PassName"), EffectPriority.Medium);
        }
        public override void Load()
        {
            LoadShaders();

            blankTexture = ("ProjectPhoenix/UI/blank");
            Instance = ModContent.GetInstance<ProjectPhoenix>();




            //playboy = Main.LocalPlayer.GetModPlayer<PlayerMod>();	

            Text.SmokeyDialogue1.LoadText();
            TextBoxTest = RegisterHotKey("Dialogue Key", "P"); // See https://docs.microsoft.com/en-us/previous-versions/windows/xna/bb197781(v=xnagamestudio.41) for special keys
                                                               //TextBoxTest2 = RegisterHotKey("I am ses)", "L"); // See https://docs.microsoft.com/en-us/previous-versions/windows/xna/bb197781(v=xnagamestudio.41) for special keys

            ModContent.GetInstance<ProjectPhoenix>().Logger.Debug("ProjectPhoenix loaded.");
            if (!Main.dedServ)
            {

                if (FontExists("Fonts/Smokey"))
                    Smokey = GetFont("Fonts/Smokey");
                MyInterface = new UserInterface();

                Texting = new Textbox();
                //TextboxI = new TextboxInternal();
                Texting.Activate(); // Activate calls Initialize() on the UIState if not initialized, then calls OnActivate and then calls Activate on every child element
                                    //	TextboxI.Activate();
            }

            base.Load();
        }
        public override void Unload()
        {
            Instance = null;
            blankTexture = null;
            TextBoxTest = null;
            TextBoxTest2 = null;

            ModContent.GetInstance<ProjectPhoenix>().Logger.Debug("WAAA IM BEING UNLOADED.");
            //Texting?.SomeKindOfUnload(); // If you hold data that needs to be unloaded, call it in OO-fashion
            Texting = null;
            //	TextboxI = null;
            base.Unload();
        }
        private GameTime _lastUpdateUiGameTime;

        internal void ShowMyUI(NewTextBox textsex)
        {
            Texting.Load(true);
            MyInterface?.SetState(Texting);
            Texting.TextSet(textsex.GetText());
            Texting.TextureSet(textsex.GetTexture(), textsex.GetCustom());
            Texting.SoundSet(textsex.GetSound());
            Texting.SetColor(textsex.GetColor(), textsex.GetColor());
            Texting.SetBlinking(textsex.GetCustom());
            Texting.SetPriority(textsex.GetPriority());
            Texting.SetIdle(textsex.GetUseIdle());
            Texting.GetMod(textsex.GetModifier());
            Texting.StartBox();

        }
        internal void HideMyUI()
        {
            Texting.Unload();
        }
        internal bool SkipText(bool skip)
        {
            return Texting.SkipText(skip);
        }

        public override void UpdateUI(GameTime gameTime)
        {
            _lastUpdateUiGameTime = gameTime;
            if (MyInterface?.CurrentState != null)
            {
                MyInterface.Update(gameTime);
            }
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "MyMod: MyInterface",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && MyInterface?.CurrentState != null)
                        {
                            MyInterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                       InterfaceScaleType.UI));
            }
        }
    }
}
