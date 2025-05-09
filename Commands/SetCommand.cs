using CommandSystem;
using LabAPI.CustomRoles.API.CustomRole;
using LabAPI.CustomRoles.Interfaces;
using Utils;

namespace LabAPI.CustomRoles.Commands;

[CommandHandler(typeof(LCRParentCommand))]
public sealed class SetCommand : ICommand, IUsageProvider
{
    /// <inheritdoc/>
    public string Command => "set";

    /// <inheritdoc/>
    public string[] Aliases => [];

    /// <inheritdoc/>
    public string Description => "Set CustomRole to player(s)";

    /// <inheritdoc/>
    public string[] Usage => ["<Id/Name>", "%player% (Optional)"];

    public PlayerPermissions RequiredPerm = PlayerPermissions.PlayersManagement;

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (!sender.CheckPermission(RequiredPerm))
        {
            response = Plugin.Instance.Config.DontHaveAccess;
            return false;
        }

        var player = Player.Get(sender);

        if (arguments.Count == 1)
        {
            response = "To execute this command provide at least 2 arguments!" +
                $"\nUsage: {arguments.Array[0]} {this.DisplayCommandUsage()}";
            return false;
        }

        string choosenRole = arguments.At(0);
        bool isId = false;

        if (ulong.TryParse(choosenRole, out ulong id))
        {
            isId = true;
        }

        ICustomRole customRole = isId ? CustomRole.Get(id) : CustomRole.Get(choosenRole);

        if (customRole == null)
        {
            response = "Custom Role not found!";
            return false;
        }

        IEnumerable<Player> players;

        if (arguments.Count == 2)
        {
            players = [.. RAUtils.ProcessPlayerIdOrNamesList(arguments, 1, out _).Select(Player.Get)];
        }
        else
        {
            response = "No players!";
            return false;
        }

        foreach (Player pl in players)
        {
            customRole.AddRole(pl);
        }

        response = Plugin.Instance.Config.Successfully;
        return true;
    }
}
