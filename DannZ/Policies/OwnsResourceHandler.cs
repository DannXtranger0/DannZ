using DannZ.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DannZ.Policies
{
    public class OwnsResourceHandler : AuthorizationHandler<OwnsResourceRequirement>
    {
        private readonly MyDbContext _context;
        private readonly IHttpContextAccessor _http;

        public OwnsResourceHandler(MyDbContext context, IHttpContextAccessor http)
        {
            _context = context;
            _http = http;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context, //Información del usuaio actual
            OwnsResourceRequirement requirement)
        {
            //obtengo el id dle usuiario por medio de los claims
            var userIdClaim = context.User.FindFirstValue("userId");
            
            if (userIdClaim == null)
                return;

            int userId = int.Parse(userIdClaim);
            
            //Obtenemos el id de lo que estamos intentando acceder (post,perfil,etc)
            var routeValues = _http.HttpContext.Request.RouteValues;
            //Si no hay un "id" no damos acceso
            if (!routeValues.TryGetValue("id", out var routeIdVal))
                return;


            int resourceId = int.Parse(routeIdVal!.ToString()!);

            //Verificamos si el usuario es dueño del recurso, según el tipo de recurso

            bool isOwner = requirement.ResourceType switch
            {
                "User" => userId == resourceId,
                _ => false
            };

            if (isOwner)
                context.Succeed(requirement);

        }
    }
}
