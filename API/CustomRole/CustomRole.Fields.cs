namespace LabAPI.CustomRoles.API.CustomRole;

public abstract partial class CustomRole
{
    // ──────────────── PRIVATE STATIC FIELDS ────────────────
    private static readonly List<CustomRole> _customRoles;

    // ──────────────── PRIVATE INSTANCE FIELDS ────────────────
    private List<Player> _trackedPlayers;
}
