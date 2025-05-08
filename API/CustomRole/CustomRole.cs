using LabApi.Events.CustomHandlers;
using LabAPI.CustomRoles.Interfaces;

namespace LabAPI.CustomRoles.API.CustomRole;

public abstract partial class CustomRole : CustomEventsHandler, ICustomRole
{
    static CustomRole()
    {
        _customRoles = [];
    }

    public CustomRole()
    {
        _trackedPlayers = [];
    }
}
