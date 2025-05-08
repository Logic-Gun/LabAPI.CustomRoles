using LabApi.Events.CustomHandlers;
using LabApi.Features.Wrappers;
using MEC;
using PlayerRoles;

namespace LabAPI.CustomRoles.API.CustomRole
{
    public abstract partial class CustomRole
    {
        // ──────────────── PUBLIC INSTANCE METHODS ─────────────────

        public virtual void AddRole(Player pl, RoleChangeReason roleChangeReason = RoleChangeReason.RoundStart)
        {
            AddPlayerToTracking(pl);
            AssignRole(pl);
            ApplyDelayedChanges(pl);
            OnRoleAdded(pl);
        }

        public virtual void RemoveRole(Player pl)
        {
            RemovePlayerFromTracking(pl);
            pl.SetRole(RoleTypeId.Spectator, RoleChangeReason.Died);
            OnRoleRemoved(pl);
        }

        // ──────────────── PROTECTED INSTANCE METHODS ─────────────────

        protected virtual void BeforeClearAllTrackedPlayers() { }
        /// <summary>
        /// The default implementation does nothing.
        /// When player is added to the tracking list this method is called.
        /// </summary>
        protected virtual void BeforeClearTrackedPlayers() { }

        /// <summary>
        /// The default implementation does nothing.
        /// When Custom Info is applied to the player this method is called.
        /// </summary>
        /// <param name="pl">Player</param>
        protected virtual void AfterApplyCustomInfo(Player pl) { }


        /// <summary>
        /// You can override base class methods to handle LabAPI events directly if needed.
        /// </summary>
        protected virtual void SubscribeEvents()
        {
            CustomHandlersManager.RegisterEventsHandler(this);
        }

        /// <summary>
        /// You can override base class methods to handle LabAPI events directly if needed.
        /// </summary>
        protected virtual void UnsubscribeEvents()
        {
            CustomHandlersManager.UnregisterEventsHandler(this);
        }

        protected virtual void AddPlayerToTracking(Player pl)
        {
            if (pl == null) return;

            if (!_trackedPlayers.Contains(pl))
            {
                _trackedPlayers.Add(pl);
            }
        }

        protected virtual void RemovePlayerFromTracking(Player pl)
        {
            if (pl == null) return;
            if (_trackedPlayers.Contains(pl))
            {
                _trackedPlayers.Remove(pl);
            }
        }

        protected virtual void AssignRole(Player pl)
        {
            pl?.SetRole(
                Role,
                RoleChangeReason.Respawn,
                IsMustBeResetInventory
                ? RoleSpawnFlags.All
                : RoleSpawnFlags.UseSpawnpoint
            );
        }

        protected virtual void ApplyDelayedChanges(Player pl)
        {
            Timing.CallDelayed(0.35f, () =>
            {
                ApplyCustomInfo(pl);
                ApplyMaxHealth(pl);
                ApplyArtificialHealth(pl);
                ApplyHumeShield(pl);
                ApplyGravity(pl);
                AddItems(pl);
            });
        }

        protected virtual void OnRoleAdded(Player pl)
        {
            pl?.SendBroadcast(Plugin.Instance.Config.RoleAdded.Replace("%name%", Name), 10, shouldClearPrevious: true);
        }

        protected virtual void OnRoleRemoved(Player pl)
        {
            pl?.SendBroadcast(Plugin.Instance.Config.RoleRemoved.Replace("%name%", Name), 10, shouldClearPrevious: true);
        }
    }
}
