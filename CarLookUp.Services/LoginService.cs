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

        public void Logoff()
        {
            if (SessionManager.User != null)
            {
                SessionManager.Abandon();
            }
        }
    }
}
