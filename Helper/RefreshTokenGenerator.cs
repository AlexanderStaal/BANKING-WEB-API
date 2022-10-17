using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BankingWebAPI.Models;
using BankingWebAPI.Context;


namespace BankingWebAPI.Helper
{

    public class RefreshTokenGenerator : IRefreshTokenGenerator
    {
        private readonly UsersDBContext _userContext;

        public RefreshTokenGenerator(UsersDBContext userContext)
        {
            _userContext = userContext;
        }
        public string GenerateToken(string username)
        {
            var randomnumber = new byte[32];
            using (var randomnumbergenerator = RandomNumberGenerator.Create())
            {
                randomnumbergenerator.GetBytes(randomnumber);
                string RefreshToken = Convert.ToBase64String(randomnumber);

                var _user = _userContext.Refreshtoken.FirstOrDefault(o => o.UserId == username);
                if (_user != null)
                {
                    _user.RefreshToken = RefreshToken;
                    _userContext.SaveChanges();
                }
                else
                {
                    Refreshtoken tblRefreshtoken = new Refreshtoken()
                    {
                        UserId = username,
                        TokenId = new Random().Next().ToString(),
                        RefreshToken = RefreshToken,
                        IsActive = true
                    };
                }

                return RefreshToken;
            }
        }
    }
}


