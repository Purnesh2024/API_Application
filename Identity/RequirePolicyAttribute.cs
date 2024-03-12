using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;

namespace API_Application.Identity
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequirePolicyAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly ClaimType _claimType;

        public RequirePolicyAttribute(ClaimType claimType)
        {
            _claimType = claimType;
            Policy = GetPolicyNameFromClaimType(claimType);
        }

        public void OnAuthorization(AuthorizationFilterContext context) { }

        private static string GetPolicyNameFromClaimType(ClaimType claimType)
        {
            return claimType switch
            {
                ClaimType.Employee => "EmployeePolicy",
                ClaimType.Admin => "AdminPolicy",
                ClaimType.Manager => "ManagerPolicy",
                _ => string.Empty
            };
        }
    }

    public enum ClaimType
    {
        Employee,
        Admin,
        Manager,
    }
}
