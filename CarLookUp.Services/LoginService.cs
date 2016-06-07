using CarLookUp.Core.Constants;
using CarLookUp.Core.Enum;
using CarLookUp.Core.Models;
using CarLookUp.Core.Utilities;
using CarLookUp.Services.Interfaces;

namespace CarLookUp.Services
{
    public class LoginService : ILoginService
    {
        private IRoleService _roleService;

        public LoginService(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Logins the user into session.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="messages">The validation messages.</param>
        public void LoginUser(UserDTO user, ValidationMassageList messages)
        {
            RoleDTO role = _roleService.GetById(user.Role.Id);

            if (role == null)
            {
                messages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_ROLE));
                return;
            }
            user.Role = role;

            SessionManager.User = user;
        }

        /// <summary>
        /// Logoffs user form session.
        /// </summary>
        public void Logoff()
        {
            if (SessionManager.User != null)
            {
                SessionManager.Abandon();
            }
        }
    }
}
