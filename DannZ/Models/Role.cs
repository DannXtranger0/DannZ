namespace DannZ.Models
{ 
    public enum RoleEnum
    {
        Administrator = 1,
        User = 2,
        Guest = 3
    }
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public virtual ICollection<RolePermission>? RolePermissions { get; set; }
    }
}
