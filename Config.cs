using LabAPI.CustomRoles.CustomRoles;
using LabAPI.CustomRoles.Interfaces;

namespace LabAPI.CustomRoles;

public sealed class Config
{
    public string Successfully { get; set; } = "Successfully!";
    public string DontHaveAccess { get; set; } = "You do not have permission for this command!";
    public string RoleAdded { get; set; } = "<b><color=yellow>You are <color=blue>%name%</color><color=#3a3a3a>!</color></b>";
    public string RoleRemoved { get; set; } = "<b><color=yellow>You are <color=red>no longer</color> <color=blue>%name%</color><color=#3a3a3a>!</color></b>";

    public List<ICustomRole> CustomRoles { get; set; } = [new Test()];
}
