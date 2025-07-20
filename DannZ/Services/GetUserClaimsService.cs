using DannZ.Context;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DannZ.Services
{
    public class GetUserClaimsService : IGetUserClaimsService
    {
        private readonly MyDbContext _context;
        public GetUserClaimsService(MyDbContext context)
        {
            _context = context;
        }
        public async Task<List<Claim>> GetClaims(int roleId)
        {
            var userClaims = await _context.RolePermissions
                   .Where(x => x.RoleId == roleId)
                   .Include(x => x.Permission)
                   .Select(x => x.Permission)
                   .ToListAsync();

            List<Claim> claimList = new List<Claim>();
            foreach (var permission in userClaims)
            {
                if (!string.IsNullOrEmpty(permission.PermissionName))
                    claimList.Add(new Claim("permission", permission.PermissionName.ToString()));
            }
            return claimList;
        }

    }
}
