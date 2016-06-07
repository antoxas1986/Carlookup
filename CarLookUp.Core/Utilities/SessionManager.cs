using CarLookUp.Core.Models;
using System.Web;
using System.Web.SessionState;

namespace CarLookUp.Core.Utilities
{
    public class SessionManager
    {
        public static UserDTO User
        {
            get { return ((Session["User"] is UserDTO) ? (UserDTO)Session["User"] : null); }
            set { Session["User"] = value; }
        }

        private static HttpSessionState Session
        {
            get
            {
                return (HttpContext.Current == null) ? null : HttpContext.Current.Session;
            }
        }

        public static void Abandon()
        {
            Session.Abandon();
        }
    }
}
