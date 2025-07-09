namespace DannZ.Models
{
    public class RolePermission
    {
        public int RoleId { get; set; }
        public Role? Roles { get; set; }
        public int PermissionId { get; set; }
        public  Permission? Permissions { get; set; }
    }
}
