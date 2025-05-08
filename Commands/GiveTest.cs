using CommandSystem;
using LabAPI.CustomRoles.CustomRoles;

namespace LabAPI.CustomRoles.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
internal sealed class GiveTest : ICommand
{
    public string Command => "givetest";

    public string[] Aliases => ["gt"];

    public string Description => string.Empty;

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        Player pl = Player.Get(sender);

        // Temp set custom role
        Plugin.Instance.Config.CustomRoles.First(x=>x.GetType() == typeof(Test)).AddRole(pl);

        response = "Successfully!";
        return true;
    }
}
