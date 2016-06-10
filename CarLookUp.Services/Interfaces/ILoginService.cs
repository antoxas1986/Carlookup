using CarLookUp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLookUp.Services.Interfaces
{
    /// <summary>
    /// Api to work with login service
    /// </summary>
    public interface ILoginService
    {
        /// <summary>
        /// Logins the user into session.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="messages">The validation messages.</param>
        void LoginUser(UserDTO user, ValidationMessageList messages);

        /// <summary>
        /// Logoffs user form session.
        /// </summary>
        void Logoff();
    }
}
