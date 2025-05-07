using LabApi.Events.CustomHandlers;
using LabApi.Loader.Features.Plugins;
using LabAPI.CustomRoles.Handlers;

namespace LabAPI.CustomRoles;

public sealed class Plugin : Plugin<Config>
{
    private List<CustomEventsHandler> _handlers = [];

    public override string Name => "LabAPI.CustomRoles";
    public override string Description => "Implementing the creation of Custom Roles for LabAPI";
    public override string Author => "Logic_Gun";
    public override Version Version => new(1, 0, 1);
    public override Version RequiredApiVersion => LabApi.Features.LabApiProperties.CurrentVersion;

    public static Plugin Instance;

    public override void Disable()
    {
        Instance = null;

        UnregisterHandlers();
    }

    public override void Enable()
    {
        Instance = this;

        Instance.Config.Test.Register();

        RegisterHandlers();
    }

    private void RegisterHandlers()
    {
        PlayerHandler playerHandler = new();

        CustomHandlersManager.RegisterEventsHandler(playerHandler);
        _handlers.Add(playerHandler);
    }

    private void UnregisterHandlers()
    {
        foreach (var handler in _handlers)
        {
            CustomHandlersManager.UnregisterEventsHandler(handler);
        }

        _handlers = null;
    }
}
