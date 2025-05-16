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

    public static Plugin Instance { get; private set; }

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
        _handlers.Clear();
        _handlers.AddRange(CreateHandlers());

        foreach (var handler in _handlers)
        {
            CustomHandlersManager.RegisterEventsHandler(handler);
        }
    }

    private void UnregisterHandlers()
    {
        foreach (var handler in _handlers)
        {
            CustomHandlersManager.UnregisterEventsHandler(handler);
        }

        _handlers.Clear();
    }

    private IEnumerable<CustomEventsHandler> CreateHandlers()
    {
        yield return new ServerHandler();
        yield return new PlayerHandler();
    }
}
