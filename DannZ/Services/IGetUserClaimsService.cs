using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DannZ.Services
{
    public interface IGetUserClaimsService
    {
        Task<List<Claim>> GetClaims(int roleId);
    }
}
