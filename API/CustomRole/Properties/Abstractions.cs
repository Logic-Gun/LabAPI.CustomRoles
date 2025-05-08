using PlayerRoles;

namespace LabAPI.CustomRoles.API.CustomRole
{
    public abstract partial class CustomRole
    {
        public abstract ulong Id { get; set; }
        public abstract string Name { get; set; }
        public abstract RoleTypeId Role { get; set; }
    }
}