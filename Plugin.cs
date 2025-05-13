using LabApi.Events.CustomHandlers;
using LabApi.Loader.Features.Plugins;
using LabAPI.CustomRoles.Handlers;

namespace LabAPI.CustomRoles;

public sealed class Plugin : Plugin<Config>
{
    private readonly List<CustomEventsHandler> _handlers = [];

    public override string Name => "LabAPI.CustomRoles";
    public override string Description => "Implementing the creation of Custom Roles for LabAPI";
    public override string Author => "Logic_Gun | SlejmUr";
    public override Version Version => new(1, 0, 2);
    public override Version RequiredApiVersion => LabApi.Features.LabApiProperties.CurrentVersion;

    public static Plugin Instance;

    public override void Disable()
    {
        Instance = null;

        foreach (var customRole in Config.CustomRoles)
        {
            customRole.Unregister();
        }

        UnregisterHandlers();
    }

    public override void Enable()
    {
        Instance = this;

        foreach (var customRole in Config.CustomRoles)
        {
            customRole.Register();
        }

        RegisterHandlers();
    }

    private void RegisterHandlers()
    {
        PlayerHandler playerHandler = new();
        CustomHandlersManager.RegisterEventsHandler(playerHandler);
        _handlers.Add(playerHandler);

        ServerHandler serverHandler = new();
        CustomHandlersManager.RegisterEventsHandler(serverHandler);
        _handlers.Add(serverHandler);
    }

    private void UnregisterHandlers()
    {
        foreach (var handler in _handlers)
        {
            CustomHandlersManager.UnregisterEventsHandler(handler);
        }

        _handlers.Clear();
    }
}
