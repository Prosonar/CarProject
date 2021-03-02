using Core.Entity.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Security.JWT;
using Entity.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract.Services
{
    public interface IAuthorizationService
    {
        IDataResult<User> Register(UserForRegister userForRegister);
        IDataResult<User> Login(UserForLogin userForLogin);
        IDataResult<AccessToken> CreateAccessToken(User user);
        IResult UserExists(string email);
    }
}
