using System;
using System.IO;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using ProjectPhoenix.UI;
using ProjectPhoenix;


namespace ProjectPhoenix
{
    public class Configuration : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;
        [Label("Acceptance, Denial")]
        public bool embrace;
        [Label("Instant text in textboxes (for the impatient)")]
        public bool instantText;
        public override void OnChanged()
        {
            // Here we use the OnChanged hook to initialize ExampleUI.visible with the new values.
            // We maintain both ExampleUI.visible and ShowCoinUI as separate values so ShowCoinUI can act as a default while ExampleUI.visible can change within a play session.
            UI.Textbox.instant= instantText;
        }


    }
}