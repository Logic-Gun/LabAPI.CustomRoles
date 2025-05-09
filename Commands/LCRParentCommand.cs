using CommandSystem;

namespace LabAPI.CustomRoles.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
internal sealed class LCRParentCommand : ParentCommand, IUsageProvider
{
    public override string Command => "lcr";

    public override string[] Aliases => [];

    public override string Description => "Handling LabAPI Custom Roles.";

    public string[] Usage => ["set", "reset"];


    public override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        response = $"Please specify a valid subcommand!\n- {Command} set\n- {Command} reset";
        return false;
    }

    public override void LoadGeneratedCommands()
    {
        RegisterCommand(new SetCommand());
    }
}
