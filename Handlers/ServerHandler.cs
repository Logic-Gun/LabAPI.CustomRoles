using LabApi.Events.CustomHandlers;
using LabAPI.CustomRoles.API.CustomRole;

namespace LabAPI.CustomRoles.Handlers;

internal sealed class ServerHandler : CustomEventsHandler
{
    public override void OnServerWaitingForPlayers()
    {
        CustomRole.ClearAllTrackedPlayers();
    }
}
