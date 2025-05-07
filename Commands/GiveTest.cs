using CommandSystem;
using LabApi.Features.Wrappers;
using System;

namespace LabAPI.CustomRoles.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal sealed class GiveTest : ICommand
    {
        public string Command => "givetest";

        public string[] Aliases => ["gt"];

        public string Description => string.Empty;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player pl = Player.Get(sender);

            Plugin.Instance.Config.Test.AddRole(pl);

            response = "Successfully!";
            return true;
        }
    }
}
