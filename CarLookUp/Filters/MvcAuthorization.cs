using CarLookUp.Core.Models;
using CarLookUp.Core.Utilities;
using System;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace CarLookUp.Web.Filters
{
    /// <summary>
    /// Custom filter for authorize mvc controllers
    /// </summary>
    /// <seealso cref="System.Web.Mvc.AuthorizeAttribute" />
    public class MvcAuthorization : AuthorizeAttribute
    {
        private UserDTO User { get { return SessionManager.User; } }

        public override void OnAuthorization(AuthorizationContext context)
        {
            var isAuthenticated = AuthorizeCore(context.HttpContext);

            if (isAuthenticated)
            {
                if (!string.IsNullOrEmpty(Roles))
                {
                    if (!CheckRoles(User))
                    {
                        context.Result = new RedirectResult("/Error/Forbidden");
                    }
                }
            }
            else
            {
                context.Result = new RedirectResult("/Login/Index");
            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool isAuthenticarad = User != null;
            if (isAuthenticarad)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(User.UserName), new string[] { User.Role.Name });
            }
            return isAuthenticarad;
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
