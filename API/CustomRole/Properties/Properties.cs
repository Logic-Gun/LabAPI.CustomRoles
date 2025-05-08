using LabApi.Features.Wrappers;
using System.Collections.Generic;

namespace LabAPI.CustomRoles.API.CustomRole
{
    public abstract partial class CustomRole
    {
        // ──────────────── PUBLIC STATIC PROPERTIES ────────────────

        public static IReadOnlyList<CustomRole> CustomRoles { get; private set; } = _customRoles;


        // ──────────────── PUBLIC INSTANCE PROPERTIES ────────────────

        public IReadOnlyList<Player> TrackedPlayers => _trackedPlayers;
        public bool IsTrackedPlayers => _trackedPlayers.Count != 0;


        // ──────────────── PROTECTED INSTANCE PROPERTIES ────────────────

        protected bool IsMustBeResetInventory => Inventory.Count != 0;
    }
}