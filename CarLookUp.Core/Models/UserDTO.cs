namespace CarLookUp.Core.Models
{
    /// <summary>
    /// Base UserDTO model
    /// </summary>
    public class UserDTO
    {
        public RoleDTO Role { get; set; }
        public string UserName { get; set; }
    }
}
