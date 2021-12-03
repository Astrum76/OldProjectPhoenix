

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.ModLoader;
using ProjectPhoenix;
using System;
using Terraria.Localization;
using Terraria.ID;
using System.Collections.Generic;

namespace ProjectPhoenix.UI
{
    //TODO (11.30)
    //This textbox system is held together by duct tape and dreams.
    //This shit is not gonna fly.
    //I'm gutting the internals and trying again.
    class Textbox : UIState
    {
        private bool iamhalfasleeppleasehlepme;
        //private bool playMenuClose;
        //private bool playMenuOpen;
        private Vector2 Modifier;
        private int textSpeed;
        public static bool instant;
        private bool hasRan = false;
        private bool idle;
        private int idlePose;
        private Color textColor1 = Color.Black;
        private Color textColor2 = Color.White;
        float left = 620f;
        float top = 110.5f;
        private bool blinkOn;
        private int cooldown;
        private int clickState;
        private int lastCount;
        private int clickTimeStamp = 0;
        private UIImage clicky;
        private bool blinking = false;
        private int blinkTimer = 0;
        private int blinkTimeStamp;
        private bool unload;
        private bool dontPlaySound;
        private bool unloadDone;
        private string menuSound = "Sounds/Custom/TalkSoundSmokey"; //lol ok
        private string textureRef = ("ProjectPhoenix/UI/blank");
        private UIImage port;
        private int target;
        private int timestamp = 0;
        private int timer = 0;
        private int count = 0;
        private UIText text = new UIText("ah! so sorry!");
        private UITextPanel<string> sus;
        private string inputText = "Dreamed";
        //private int lineLength = 128 - 15;
        private bool timerOn;
        private int waitTime;
        private List<Vector3> ModifierList = new List<Vector3>();

        public bool SkipText(bool playMenuClose)
        {
            if (cooldown < 3)
            {
                if (count == target)
                {
                    if (playMenuClose)Main.PlaySound(ProjectPhoenix.Instance.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/MenuClose"));

                    Unload();
                    return false;
                }
                cooldown = 9;
                ModifierList.Clear();
                if (timerOn)
                {
                    waitTime = 0;
                    timerOn = false;
                }
               
                count = target;
            }
            return true;
        }
        public void Load(bool openSfx)
        {
            if (openSfx)Main.PlaySound(ProjectPhoenix.Instance.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/MenuOpen"));

            unload = false;
            unloadDone = false;
            clicky.SetImage(ModContent.GetTexture("ProjectPhoenix/UI/blank"));

        }

        public void Unload()
        {
            clicky.SetImage(ModContent.GetTexture("ProjectPhoenix/UI/blank"));
            unload = true;
            timestamp = timer + 3;

        }




        protected override void DrawSelf(SpriteBatch spriteBatch)
        {

            base.DrawSelf(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            text.TextColor = textColor1;
            cooldown--;
            timer++;
            blinkTimer++;
            //ok, sick. the code is gonna take ModifierList
            //if the X is 1, add Z to timer at pos Y
            if (count == target)
            {
                clicky.SetImage(ModContent.GetTexture("ProjectPhoenix/UI/arrow"));
               
                clicky.Top = new StyleDimension(top + (float)Math.Sin(timer/7)*8, 0);

                
                
                clicky.Recalculate();
            }
            else
            {
                clicky.SetImage(ModContent.GetTexture("ProjectPhoenix/UI/blank"));
            }

            if (idle)
            {
                if (timer % 25 == 0)
                {
                    idlePose++;
                    if (idlePose == 2) idlePose = 0;
                }
            }



            if (blinkOn && idlePose == 0)
            {

                if (blinkTimer > blinkTimeStamp && !blinking)
                {
                    blinkTimer = 0;
                    blinking = true;
                    blinkTimeStamp = Main.rand.Next(5) + 3;

                }
                if (blinkTimer > blinkTimeStamp && blinking)
                {
                    blinkTimer = 0;
                    blinking = false;
                    blinkTimeStamp = Main.rand.Next(100) + 50;

                }


                if (blinking)
                {
                    port.SetImage(ModContent.GetTexture(textureRef + "Blink"));

                }
                else
                {
                    port.SetImage(ModContent.GetTexture(textureRef));

                }
            }
            else
            {
                if (idlePose == 1)
                {
                    port.SetImage(ModContent.GetTexture(textureRef + "Idle"));

                }
                else
                {
                    port.SetImage(ModContent.GetTexture(textureRef));

                }
            }


            if (unload)
            {
                text.SetText("");
                if (timestamp < timer) unloadDone = true;

            }

            if (unloadDone)
            {
                ProjectPhoenix.Instance.MyInterface?.SetState(null);

            }



            if (!unload)
            {
                if (timer > timestamp)
                {
                    if (instant)
                    {
                        if (!hasRan)
                        {
                            SkipText(false);
                            hasRan = true;
                            Main.PlaySound(ProjectPhoenix.Instance.GetLegacySoundSlot(SoundType.Custom, menuSound));

                        }
                    }
                    if (ModifierList.Count != 0)
                    {
                        for (int k = 0; k < ModifierList.Count; k++)
                        {
                            if(ModifierList[k].Y == count)
                            {
                                waitTime += (int)ModifierList[k].Z;
                                ModifierList.RemoveAt(k);
                                
                            }
                        }
                    }
                    if (waitTime != 0)
                    {
                        timerOn = true;
                        waitTime--;
                        if (waitTime == 0) timerOn = false;
                    }
                    if (!timerOn)
                    {
                        
                        text.SetText(inputText.Substring(0, count), 1.1f, false);
                        if (count < target)
                        {
                            if (timer % textSpeed == 0)
                            {
                                Main.PlaySound(ProjectPhoenix.Instance.GetLegacySoundSlot(SoundType.Custom, menuSound));
                                
                                count++;


                            }

                        }
                    }
                    if (false)//inputText.ToCharArray()[count] == '^' && timerOn==false)
                    {
                        timerOn = true;
                        dontPlaySound = true;
                        waitTime = 5;
                        inputText = TextHelper.RemoveFirst(inputText,'^');
                        target--;
                        
                    }
                    if (timerOn)
                    {

                        waitTime--;
                        if (waitTime == 0)
                        {
                            dontPlaySound = false;
                            timerOn = false;
                        }
                    }
                    
                    
                    base.Update(gameTime);
                }
                else
                {
                    text.SetText("");

                }
            }


        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            /*CalculatedStyle innerDimensions = GetInnerDimensions();
			//Vector2 drawPos = new Vector2(innerDimensions.X + 5f, innerDimensions.Y + 30f);

			float shopx = innerDimensions.X;
			float shopy = innerDimensions.Y;
			*/
            base.Draw(spriteBatch);
        }
        //Main.NewText(Main.UIScale);
        //this was a terrible idea
        //			Utils.DrawBorderStringFourWay(spriteBatch, ProjectPhoenix.Smokey ?? Main.fontItemStack, inputText, (Main.UIScale * 0.25f), (Main.UIScale * 0.6666667f), Color.Black, Color.White, new Vector2(0.3f), 1.2f);

        public override void OnInitialize()
        {


            unload = false;
            unloadDone = false;
            UIImage panel = new UIImage(ModContent.GetTexture("ProjectPhoenix/UI/port"));
            panel.Width.Set(736f, 0f);
            panel.Height.Set(154f, 0f);
            panel.HAlign = 0.1979167f;
            panel.VAlign = 0.77f;
            panel.ImageScale = 1f;
            Append(panel);


            clicky = new UIImage(ModContent.GetTexture("ProjectPhoenix/UI/arrow"));
            clicky.Width.Set(64f, 0);
            clicky.Height.Set(64f, 0);
            clicky.Left.Set(620f, 0f);
            clicky.Top.Set(99.5f, 0f);
            panel.Append(clicky);




            UIElement area = new UIElement();

            area.Left.Set(226, 0);
            area.Top.Set(112, 0f);
            area.Width.Set(700, 0f);
            area.Height.Set(300, 0);
            area.HAlign = 0.13f;
            area.VAlign = 0.77f;
          


            Append(area);

            text.Left.Set(0f, 0f);
            text.Top.Set(24f, 0f);
            text.Width.Set(0, 0);
            text.Height.Set(155, 0);
            text.SetText(" ");


            area.Append(text);

            /*UIPanel panel2 = new UIPanel();
			panel2.Width.Set(32, 0);
			panel2.Height.Set(32, 0);
			panel2.HAlign = 0.6604167f; // 1
			panel2.VAlign = 0.6979167f;
			*/


            port = new UIImage(ModContent.GetTexture("ProjectPhoenix/UI/blank"));
            port.Width.Set(128f, 0f);
            port.Height.Set(128f, 0f);
            port.HAlign = 0.01741667f;
            port.VAlign = 0.78125f;
            port.SetPadding(12f);
            panel.Append(port);

            UIImageButton fin = new UIImageButton(ModContent.GetTexture("ProjectPhoenix/UI/blank"));
            fin.Width.Set(760f, 0f);
            fin.Height.Set(170f, 0f);
            fin.HAlign = 0.1979167f;
            fin.VAlign = 0.77f;
            fin.OnClick += new MouseEvent(PlayButtonClicked);
            panel.Append(fin);
        }
        private void PlayButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            if (SkipText(false) == false)
            {
                ProjectPhoenix.Instance.playboy.ButtonBeenClicked = true;
            }

            //SkipText(true);
        }

        //run ALL THESE to set up box!!!
        internal void TextSet(string text)
        {
            inputText = text;
            inputText.TrimEnd('^');
            int lineLength = 40;
            string pre;
            string post;
            ModifierList.Clear();

            try
            {
                //NEEDS to accout for \n!!!
                //and also for ^ chars, try -- 2?? or don't at all?
                //fixed, see below
                for (int i = 0; i < inputText.Length - 1; i++)
                {
                    iamhalfasleeppleasehlepme = false;

                    if (inputText.Substring(i, 1).Equals("^"))
                    {
                        //mayb list em then removeall?
                        inputText = TextHelper.RemoveFirst(inputText, '^');
                      
                        ModifierList.Add(new Vector3(1,i+1,5));
                        iamhalfasleeppleasehlepme = true;
                        i--;
                    }

                    if (i % lineLength == 0&& !iamhalfasleeppleasehlepme) // so, i must be getting locked at linelength. have a bool to ignore this ig
                        //THAT WORKED???????????????????????????
                    {
                        for (int j = i; j > 0; j--)
                        {
                            /*if (inputText.Substring(j, 2).Equals("\\p"))
							{
								pre = inputText.Substring(0, j);
								post = inputText.Substring(2+j, (inputText.Length) - j);
								inputText = pre + "\n" + post.Substring(1, post.Length - 1);

							}*/ //will add pause mechanic soon, using special chars
                                //the char will be... ^. 
                                //if given, it does not print it, and waits 60 frames per ^.
                           
                            if (inputText.Substring(j, 1).Equals(" "))
                            {
                                pre = inputText.Substring(0, j);
                                post = inputText.Substring(j, (inputText.Length) - j);
                                inputText = pre + "\n" + post.Substring(1, post.Length - 1);
                                break;
                            }
                            if (j == 1)
                            {
                                //forces linebreak if needed
                                //not ideal but will cover the occasional edge case once or twice
                                pre = inputText.Substring(0, lineLength - 10);
                                post = inputText.Substring(lineLength - 10, (inputText.Length) - lineLength - 10);
                                inputText = pre + "\n" + post.Substring(1, post.Length - 1);
                                break;
                            }
                        }
                    }
                }
                //if (ModifierList.Count > 0)
                //{
                   // foreach(Vector3 s in ModifierList)
                    //{
                        //Main.NewText(s);
                 //   }
                //}
                target = inputText.Length;
            }
            catch
            {
                Main.NewText("Unexpected Error in textbox. This should not happen!", Color.Red);
                inputText = "I have no idea what just happened.";
            }

        }
        internal void TextureSet(string text, bool customAnim)
        {
            textureRef = text;
            blinkOn = customAnim;
        }
        internal void SoundSet(string sound)
        {
            menuSound = sound;
        }
        internal void SetColor(Color color1, Color color2)
        {
            textColor1 = color1;
            textColor2 = color2;
        }
        internal void SetBlinking(bool b)
        {
            blinkOn = !b;
        }
        internal void SetPriority(bool set)
        {
            //todo
        }
        internal void SetIdle(bool b)
        {
            idle = b;
            idlePose = 0;
        }
        internal void GetMod(Vector2 mod)
        {
            Modifier = mod;
            if (Modifier.X == 0)
            {
                textSpeed = 1;
            }
            if (Modifier.X == 1)
            {
                textSpeed = (int)Modifier.Y;

            }
        }
        internal void StartBox()
        {
            hasRan = false;
            blinking = !Main.rand.NextBool(100);
            blinkTimer = 0;
            blinkTimeStamp = Main.rand.Next(50);
            lastCount = 0;
            timestamp = timer + 10;
            count = 0;

        }
       
    }
}
