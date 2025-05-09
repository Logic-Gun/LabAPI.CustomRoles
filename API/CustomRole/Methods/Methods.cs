namespace LabAPI.CustomRoles.API.CustomRole;

public abstract partial class CustomRole
{
    // ──────────────── PUBLIC INSTANCE METHODS ────────────────

    public bool Check(Player pl) => _trackedPlayers.Contains(pl);

    public void Register()
    {
        SubscribeEvents();
        _customRoles.Add(this);
    }

    public void Unregister()
    {
        foreach (var pl in _trackedPlayers) RemoveRole(pl);

        if (_customRoles.Contains(this))
        {
            UnsubscribeEvents();
            _customRoles.Remove(this);
        }
    }

    // ──────────────── INTERNAL STATIC METHODS ────────────────

    internal static void ClearAllTrackedPlayers()
    {
        foreach (var customRole in _customRoles)
        {
            customRole.BeforeClearTrackedPlayers();
            customRole.ClearTrackedPlayers();
        }
    }
    // ──────────────── PRIVATE INSTANCE METHODS ────────────────

    private void ClearTrackedPlayers()
    {
        _trackedPlayers = [];
    }

    // ──────────────── APPLY CUSTOM DETAILS METHODS ────────────────

    private void ApplyCustomInfo(Player pl)
    {
        if (pl != null) return;

        pl.CustomInfo = CustomInfo;
        AfterApplyCustomInfo(pl);
    }

    private void ApplyMaxHealth(Player pl)
    {
        if (pl == null || MaxHealth == 0) return;

        pl.MaxHealth = MaxHealth;
        pl.Health = MaxHealth;
    }

    private void ApplyArtificialHealth(Player pl)
    {
        if (pl == null || MaxArtificialHealth == 0) return;

        pl.ArtificialHealth = MaxArtificialHealth;
        pl.MaxArtificialHealth = MaxArtificialHealth;
    }

    private void ApplyHumeShield(Player pl)
    {
        if (pl == null || MaxHumeShield == 0) return;

        pl.MaxHumeShield = MaxHumeShield;
        pl.HumeShield = MaxHumeShield;
    }

    private void ApplyGravity(Player pl)
    {
        if (pl == null || !Gravity.HasValue) return;
        pl.Gravity = Gravity.Value;
    }

    private void AddItems(Player pl)
    {
        if (pl == null) return;

        foreach (var item in Inventory)
        {
            for (int i = 0; i < item.Value; i++)
            {
                pl.AddItem(item.Key);
            }
        }

        foreach (var item in AdditionalInventory)
        {
            for (int i = 0; i < item.Value; i++)
            {
                pl.AddItem(item.Key);
            }
        }
    }

    // TODO: Reset stuff for player (ie: CustomInfo, Scale, etc)
}