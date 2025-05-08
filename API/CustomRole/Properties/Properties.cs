using YamlDotNet.Serialization;

namespace LabAPI.CustomRoles.API.CustomRole;

public abstract partial class CustomRole
{
    // ──────────────── PUBLIC STATIC PROPERTIES ────────────────
    [YamlIgnore]
    public static IReadOnlyList<CustomRole> CustomRoles { get; private set; } = _customRoles;


    // ──────────────── PUBLIC INSTANCE PROPERTIES ────────────────
    [YamlIgnore]
    public IReadOnlyList<Player> TrackedPlayers => _trackedPlayers;
    [YamlIgnore]
    public bool HasTrackedPlayers => _trackedPlayers.Count != 0;


    // ──────────────── PROTECTED INSTANCE PROPERTIES ────────────────
    [YamlIgnore]
    protected bool IsMustBeResetInventory => Inventory.Count != 0;
}