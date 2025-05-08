using System.Collections.Generic;
using UnityEngine;

namespace LabAPI.CustomRoles.API.CustomRole
{
    public abstract partial class CustomRole
    {
        public virtual string Description { get; set; }
        public virtual string CustomInfo { get; set; }

        public virtual float MaxHealth { get; set; }
        public virtual float MaxHumeShield { get; set; }
        public virtual float MaxArtificialHealth { get; set; }

        public virtual Vector3? Gravity { get; set; }
        public virtual Vector3 Scale { get; set; } = Vector3.one;

        public virtual Dictionary<ItemType, ushort> Inventory { get; set; } = [];
        public virtual Dictionary<ItemType, ushort> AdditionalInventory { get; set; } = [];
    }
}