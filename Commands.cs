using System;
using Terraria.ModLoader;

namespace extraUtility
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
}
