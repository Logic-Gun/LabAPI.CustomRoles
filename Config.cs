using LabAPI.CustomRoles.CustomRoles;

namespace LabAPI.CustomRoles
{
    public sealed class Config
    {
        public string RoleAdded { get; set; } = "<b><color=yellow>You are <color=blue>%name%</color><color=#3a3a3a>!</color></b>";
        public string RoleRemoved { get; set; } = "<b><color=yellow>You are <color=red>no longer</color> <color=blue>%name%</color><color=#3a3a3a>!</color></b>";

        public Test Test { get; set; } = new();
    }
}
