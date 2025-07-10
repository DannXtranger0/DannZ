using Microsoft.AspNetCore.Authorization;

namespace DannZ.Policies
{
    public class OwnsResourceRequirement : IAuthorizationRequirement
    {
        public string ResourceType {  get;}

        public OwnsResourceRequirement(string resourceType)
        {
            ResourceType = resourceType;
        }
    }
}
