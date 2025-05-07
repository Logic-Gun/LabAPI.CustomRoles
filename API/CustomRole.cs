using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Features.Wrappers;
using LabAPI.CustomRoles.Interfaces;
using MEC;
using PlayerRoles;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace LabAPI.CustomRoles.API
{
    public abstract class CustomRole : ICustomRole
    {
        private static List<CustomRole> _customRoles;

        private List<Player> _trackedPlayers;

        public static IReadOnlyList<CustomRole> CustomRoles { get; private set; } = _customRoles;

        [XmlIgnore]
        public IReadOnlyList<Player> TrackedPlayers => _trackedPlayers;
        [XmlIgnore]
        public bool IsTrackedPlayers => _trackedPlayers.Count != 0;
        [XmlIgnore]
        public bool IsMustBeResetInventory => Inventory.Count != 0;

        static CustomRole()
        {
            _customRoles = [];
        }

        public CustomRole()
        {
            _trackedPlayers = [];
        }

        public virtual float MaxHealth { get; set; }
        public virtual float MaxHumeShield { get; set; }
        public virtual float MaxArtificialHealth { get; set; }

        public virtual Vector3? Gravity { get; set; }
        public virtual Vector3 Scale { get; set; } = Vector3.zero;

        public abstract ulong Id { get; set; }
        public abstract string Name { get; set; }
        public abstract RoleTypeId Role { get; set; }
        public abstract string Description { get; set; }
        public abstract string CustomInfo { get; set; }

        public virtual Dictionary<ItemType, ushort> Inventory { get; set; } = new();
        public virtual Dictionary<ItemType, ushort> AdditionalInventory { get; set; } = new();

        public static void ClearAllTrackedPlayers()
        {
            BeforeClearAllTrackedPlayers();

            foreach (var customRole in _customRoles)
            {
                customRole.BeforeClearTrackedPlayers();
                customRole.ClearTrackedPlayers();
            }
        }

        public void Register()
        {
            _customRoles.Add(this);
            SubscribeEvents();
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

        public bool Check(Player pl) => _trackedPlayers.Contains(pl);

        public virtual void SubscribeEvents()
        {
            LabApi.Events.Handlers.PlayerEvents.Death += OnDied;
            LabApi.Events.Handlers.PlayerEvents.Left += OnLeft;
        }

        public virtual void UnsubscribeEvents()
        {
            LabApi.Events.Handlers.PlayerEvents.Death -= OnDied;
            LabApi.Events.Handlers.PlayerEvents.Left -= OnLeft;
        }

        public virtual void AddRole(Player pl, RoleChangeReason roleChangeReason = RoleChangeReason.RoundStart)
        {
            if (!_trackedPlayers.Contains(pl))
            {
                _trackedPlayers.Add(pl);
            }

            pl.SetRole(Role, roleChangeReason, IsMustBeResetInventory ? RoleSpawnFlags.All : RoleSpawnFlags.UseSpawnpoint);

            Timing.CallDelayed(0.35f, () =>
            {
                pl.CustomInfo = CustomInfo;

                if (MaxHumeShield != 0)
                {
                    pl.MaxHumeShield = MaxHumeShield;
                    pl.HumeShield = pl.MaxHumeShield;
                }

                if (MaxHealth != 0)
                {
                    pl.MaxHealth = MaxHealth;
                    pl.Health = pl.MaxHealth;
                }

                if (MaxArtificialHealth != 0)
                {
                    pl.ArtificialHealth = MaxArtificialHealth;
                    pl.ArtificialHealth = pl.MaxArtificialHealth;
                }

                if (Gravity != null)
                {
                    pl.Gravity = (Vector3)Gravity;
                }

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
            });

            OnRoleAdded(pl);
        }

        public virtual void RemoveRole(Player pl)
        {
            if (_trackedPlayers.Contains(pl))
            {
                _trackedPlayers.Remove(pl);
            }

            pl.SetRole(RoleTypeId.Spectator, RoleChangeReason.Died);
            OnRoleRemoved(pl);
        }

        public virtual void OnRoleAdded(Player pl)
        {
            pl.SendBroadcast(Plugin.Instance.Config.RoleAdded.Replace("%name%", Name), 10, shouldClearPrevious: true);
        }

        public virtual void OnRoleRemoved(Player pl)
        {
            pl.SendBroadcast(Plugin.Instance.Config.RoleRemoved.Replace("%name%", Name), 10, shouldClearPrevious: true);
        }

        public virtual void OnLeft(PlayerLeftEventArgs ev)
        {
            if (Check(ev.Player)) _trackedPlayers.Remove(ev.Player);
        }

        public virtual void OnDied(PlayerDeathEventArgs ev)
        {
            if (Check(ev.Player)) RemoveRole(ev.Player);
        }

        public virtual void ClearTrackedPlayers()
        {
            _trackedPlayers = [];
        }

        public static void BeforeClearAllTrackedPlayers() { }
        public virtual void BeforeClearTrackedPlayers() { }
    }
}
