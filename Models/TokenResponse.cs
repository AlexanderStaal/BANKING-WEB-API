using System;
namespace BankingWebAPI.Models
{
    public class TokenResponse
    {
        public string JWTToken { get; set; }
        public string RefreshToken { get; set; }
    }

    public class JWTSetting
    {
        public string securitykey { get; set; }
    }

    public class APIResponse
    {
        public string keycode { get; set; }
        public string result { get; set; }
    }

    public class UserCred
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public partial class Role
    {
        public string Roleid { get; set; }
        public string Name { get; set; }
    }

    public partial class Refreshtoken
    {
        public string UserId { get; set; }
        public string TokenId { get; set; }
        public string RefreshToken { get; set; }
        public bool? IsActive { get; set; }
    }

    public partial class Permission
    {
        public string RoleId { get; set; }
        public string MenuId { get; set; }
    }

    public partial class Menu
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LinkName { get; set; }
    }
}


