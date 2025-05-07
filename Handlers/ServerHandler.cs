using LabApi.Events.CustomHandlers;
using LabAPI.CustomRoles.API;

namespace LabAPI.CustomRoles.Handlers
{
    internal sealed class ServerHandler : CustomEventsHandler
    {
        public override void OnServerWaitingForPlayers()
        {
            CustomRole.ClearAllTrackedPlayers();
        }
    }
}
