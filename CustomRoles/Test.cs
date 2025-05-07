using LabAPI.CustomRoles.API;
using PlayerRoles;
using System.Collections.Generic;

namespace LabAPI.CustomRoles.CustomRoles
{
    public class Test : CustomRole
    {
        public override float MaxHumeShield { get; set; } = 100;
        public override float MaxArtificialHealth { get; set; } = 30;

        public override ulong Id { get; set; } = 1;
        public override string Name { get; set; } = "Test";
        public override RoleTypeId Role { get; set; } = RoleTypeId.ClassD;
        public override string Description { get; set; } = "You are a test role!";
        public override string CustomInfo { get; set; } = "Tester";

        public override Dictionary<ItemType, ushort> Inventory { get; set; } = new()
        {
            { ItemType.MicroHID, 3 },
            { ItemType.SCP018, 2 },
            { ItemType.SCP268, 2 }
        };
    }
}
