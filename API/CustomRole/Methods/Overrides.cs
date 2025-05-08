using LabApi.Events.Arguments.PlayerEvents;

namespace LabAPI.CustomRoles.API.CustomRole
{
    public abstract partial class CustomRole
    {
        public override void OnPlayerLeft(PlayerLeftEventArgs ev)
        {
            if (Check(ev.Player))
            {
                _trackedPlayers.Remove(ev.Player);
            }
        }

        public override void OnPlayerDeath(PlayerDeathEventArgs ev)
        {
            if (Check(ev.Player))
            {
                RemoveRole(ev.Player);
            }
        }
    }
}
