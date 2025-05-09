using CommandSystem;
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
    public string Description => "Set customRole to player(s)";

    /// <inheritdoc/>
    public string[] Usage => ["CustomRoleId/CustomRoleName", "%player% (Optional)"];

    public PlayerPermissions RequiredPerm = PlayerPermissions.PlayersManagement;

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (!sender.CheckPermission(RequiredPerm))
        {
            response = "You do not have permission!";
            return false;
        }
        List<Player> players = [];
        var player = Player.Get(sender);
        if (arguments.Count == 1 && player == null)
        {
            response = "To execute this command provide at least 2 arguments!\nUsage: " + arguments.Array[0] + " " + this.DisplayCommandUsage();
            return false;
        }
        players.Add(player);
        bool IsID = false;
        string customroleArg = arguments.At(0);
        ulong id = 0;
        if (ulong.TryParse(customroleArg, out id))
            IsID = true;
        ICustomRole customRole = Main.Instance.Config.CustomRoles.FirstOrDefault(x=> IsID ? x.Id == id : x.Name.Equals(customroleArg, StringComparison.InvariantCultureIgnoreCase));
        if (customRole == null)
        {
            response = "Custom Role not found!";
            return false;
        }
        if (arguments.Count == 2)
        {
            players = [.. RAUtils.ProcessPlayerIdOrNamesList(arguments, 1, out _).Select(Player.Get)];
        }
        if (players.Count == 0)
        {
            response = "No players!";
            return false;
        }
        foreach (Player p in players)
        {
            customRole.AddRole(p);
        }
        response = "OK!";
        return true;
    }
}
