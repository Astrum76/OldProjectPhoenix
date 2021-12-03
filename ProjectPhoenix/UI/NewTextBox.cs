using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPhoenix.UI
{
    public class NewTextBox
    {
        private string text;
        private string portTexture;
        private string sound;
        private bool custom;
        private Color color;
        private bool priority;
        private bool sequental;
        private bool idleAnimation;
        private Vector2 Modifier;
        public NewTextBox(string text1)
        {
            this.text = text1;
            this.portTexture = ProjectPhoenix.blankTexture;
            this.sound = "Sounds/Custom/TalkSoundSmokey"; //lol ok
            this.custom = false;
            this.color = Color.White;
            this.priority = false;
            this.Modifier = Vector2.Zero;

        }
        public NewTextBox(string text1,string portT)
        {
            this.text = text1;
            this.portTexture = portT;
            this.sound = "Sounds/Custom/TalkSoundSmokey"; //lol ok
            this.custom = false;
            this.color = Color.White;
            this.priority = false;
            this.Modifier = Vector2.Zero;

        }
        public NewTextBox(string text1, string portT,string sound1)
        {
            this.text = text1;
            this.portTexture = portT;
            this.sound = sound1;
            this.custom = false;
            this.color = Color.White;
            this.priority = false;
            this.Modifier = Vector2.Zero;

        }
        public NewTextBox(string text1, string portT, string sound1,Color color1, bool idleish)
        {
            this.text = text1;
            this.portTexture = portT;
            this.sound = sound1;
            this.custom = false;
            this.color = color1;
            this.priority = false;
            this.idleAnimation = idleish;
            this.Modifier = Vector2.Zero;

        }

        public NewTextBox(string text1, string portTexture1, string sound1, bool custom1, Color color1, bool priority1)
        {
            this.text = text1;
            this.portTexture = portTexture1;
            this.sound = sound1;
            this.custom = custom1;
            this.color = color1;
            this.priority = priority1;
            this.Modifier = Vector2.Zero;

        }
        public NewTextBox(string text1, string portTexture1, string sound1, bool custom1, Color color1)
        {
            this.text = text1;
            this.portTexture = portTexture1;
            this.sound = sound1;
            this.custom = custom1;
            this.color = color1;
            this.Modifier = Vector2.Zero;

        }
        ///<summary>Takes: Text for box, texture as a string (eg "ProjectPhoenix/UI/Port/Smokey/SmokeySus", a vocal sound string, bool if should use a custom blink - use false to use normal blink logic. Text color, another bool - true for normal, false for override priority, and bool (true for idle animation, false for none.)</summary>

        public NewTextBox(string text1, string portTexture1, string sound1, bool custom1, Color color1, bool priority1, bool idleAnim)
        {
            this.text = text1;
            this.portTexture = portTexture1;
            this.sound = sound1;
            this.custom = custom1;
            this.color = color1;
            this.priority = priority1;
            this.idleAnimation = idleAnim;
            this.Modifier = Vector2.Zero;
        }
        public NewTextBox(string text1, string portTexture1, string sound1, bool custom1, Color color1, bool priority1, bool idleAnim, Vector2 ModifierKey)
        {
            this.text = text1;
            this.portTexture = portTexture1;
            this.sound = sound1;
            this.custom = custom1;
            this.color = color1;
            this.priority = priority1;
            this.idleAnimation = idleAnim;
            this.Modifier = ModifierKey;
        }
        public void AddBox()
        {
            ProjectPhoenix.Instance.playboy.textBoxes.Add(this);
        }
        
        public string GetText()
        {
            return this.text;
        }
        public string GetTexture()
        {
            return this.portTexture;

        }
        public string GetSound()
        {
            return this.sound;

        }
        public bool GetCustom()
        {
            return this.custom;
        }
        public Color GetColor()
        {
            return this.color;
        }
        public bool GetPriority()
        {
            return this.priority;
        }
        public bool GetUseIdle()
        {
            return this.idleAnimation;
        }
        public Vector2 GetModifier()
        {
            return this.Modifier;
        }
        public void Pause()
        {
            ProjectPhoenix.Instance.playboy.textBoxes.Add(new NewTextBox("PauseCharacter"));
        }

    }
}
