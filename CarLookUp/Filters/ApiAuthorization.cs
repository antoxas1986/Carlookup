using CarLookUp.Core.Models;
using CarLookUp.Core.Utilities;
using CarLookUp.Services.Interfaces;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace CarLookUp.Web.Filters
{
    public class ApiAuthorization : AuthorizeAttribute
    {
        private IRoleService RoleService { get { return Ioc.AutofacConfig.Resolve<IRoleService>(); } }
        private UserDTO User { get { return SessionManager.User; } }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var isAuthenticated = AuthorizeCore();

            if (!isAuthenticated)
            {
                if (!string.IsNullOrEmpty(Roles))
                {
                    if (!CheckRoles(User))
                    {
                        HandleUnauthorizedRequest(actionContext);
                    }
                }
            }
            else
            {
                HandleUnauthorizedRequest(actionContext);
            }
        }

        private bool AuthorizeCore()
        {
            bool isAuthenticated = User != null;
            if (isAuthenticated)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(User.UserName), new string[] { User.Role.Name });
            }
            return isAuthenticated;
        }

        private bool CheckRoles(UserDTO user)
        {
            string[] roles = Roles.Split(',');
            if (roles.Length == 0)
            {
                return true;
            }
            if (user.Role == null)
            {
                return false;
            }
            return roles.Contains(user.Role.Name);
        }
    }
}
