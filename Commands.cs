using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using ReLogic.OS;

namespace ExtraUtility
{
    public class ChangeNameCommand : ModCommand
    {
        public override CommandType Type => CommandType.Chat;
        public override string Command => "setName";
        public override string Usage => "/setName newName";
        public override string Description => "Change the name of your player";
        public override void Action(CommandCaller caller, string input, string[] args)
        {
            string name = "";
            if (args.Length != 0 && !string.IsNullOrEmpty(args[0]))
            {
                name = args[0];
            }
            caller.Player.name = name;
        }
    }
    public class SetDeathCommand : ModCommand
    {
        public override CommandType Type => CommandType.Chat;
        public override string Command => "setDeathPos";
        public override string Usage => "/setDeathPos";
        public override string Description => "Set the last death point to your current position";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            caller.Player.lastDeathPostion = caller.Player.Bottom;
        }
    }
    public class CopyCharacterTemplateCommand : ModCommand
    {
        public override CommandType Type => CommandType.Chat;
        public override string Command => "copyCharacter";
        public override string Usage => "/copyCharacter";
        public override string Description => "Copies your character template to your clipboard";
        public override void Action(CommandCaller caller, string input, string[] args)
        {
            Player player = caller.Player; 
            string text = JsonConvert.SerializeObject(new Dictionary<string, object> {
                { "version", 1 },
                { "hairStyle", player.hair },
                { "clothingStyle", player.skinVariant },
                { "hairColor", GetHexText(player.hairColor) },
                { "eyeColor", GetHexText(player.eyeColor) },
                { "skinColor", GetHexText(player.skinColor) },
                { "shirtColor", GetHexText(player.shirtColor) },
                { "underShirtColor", GetHexText(player.underShirtColor) },
                { "pantsColor", GetHexText(player.pantsColor) },
                { "shoeColor", GetHexText(player.shoeColor) }          }, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead,
                    Formatting = Formatting.Indented
                });

            Terraria.GameInput.PlayerInput.PrettyPrintProfiles(ref text);
            Platform.Get<IClipboard>().Value = text;
            Main.NewText("Copied character look to clipboard");
        }
        static string GetHexText(Color pendingColor) => "#" + pendingColor.Hex3().ToUpper();
    }
}
