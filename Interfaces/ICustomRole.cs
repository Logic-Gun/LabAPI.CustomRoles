using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Features.Wrappers;
using PlayerRoles;
using System.Collections.Generic;
using UnityEngine;

namespace LabAPI.CustomRoles.Interfaces
{
    public interface ICustomRole
    {
        IReadOnlyList<Player> TrackedPlayers { get; }

        bool IsTrackedPlayers { get; }
        bool IsMustBeResetInventory { get; }

        float MaxHealth { get; set; }
        float MaxHumeShield { get; set; }
        float MaxArtificialHealth { get; set; }

        Vector3? Gravity { get; set; }
        Vector3 Scale { get; set; }

        ulong Id { get; set; }
        string Name { get; set; }
        RoleTypeId Role { get; set; }
        string Description { get; set; }
        string CustomInfo { get; set; }

        Dictionary<ItemType, ushort> Inventory { get; set; }
        Dictionary<ItemType, ushort> AdditionalInventory { get; set; }

        void Register();
        void Unregister();

        bool Check(Player pl);

        void SubscribeEvents();
        void UnsubscribeEvents();

        void AddRole(Player pl, RoleChangeReason roleChangeReason = RoleChangeReason.RoundStart);
        void RemoveRole(Player pl);

        void OnRoleAdded(Player pl);
        void OnRoleRemoved(Player pl);

        void OnLeft(PlayerLeftEventArgs ev);
        void OnDied(PlayerDeathEventArgs ev);

        void ClearTrackedPlayers();

        void BeforeClearTrackedPlayers();
    }
}
