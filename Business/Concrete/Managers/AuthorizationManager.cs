using Business.Abstract.Services;
using Business.Utilities.Messages.TurkishMessages;
using Core.Entity.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entity.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete.Managers
{
    public class AuthorizationManager : IAuthorizationService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthorizationManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims.Data);
            return new SuccessDataResult<AccessToken>(accessToken,Messages.TokenCreated);
        }

        public IDataResult<User> Login(UserForLogin userForLogin)
        {
            var userToLogin = _userService.GetByEmail(userForLogin.Email);
            if(userToLogin == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if(!HashingHelper.VerifyPasswordHash(userForLogin.Password,userToLogin.Data.PasswordHash,userToLogin.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.WrongData);
            }

            return new SuccessDataResult<User>(userToLogin.Data,Messages.SuccessMessage);
        }

        public IDataResult<User> Register(UserForRegister userForRegister)
        {
            if(UserExists(userForRegister.Email).Success)
            {
                return new ErrorDataResult<User>(Messages.UserAlreadyExist);
            }
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegister.Password,out passwordHash,out passwordSalt);
            var user = new User
            {
                FirstName = userForRegister.FirstName,
                LastName = userForRegister.LastName,
                Email = userForRegister.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                IsActive = true
            };
            if(!_userService.Add(user).Success)
            {
                return new ErrorDataResult<User>(Messages.ErrorMessage);
            }
            return new SuccessDataResult<User>(user, Messages.SuccessMessage);
        }

        public IResult UserExists(string email)
        {
            var user = _userService.GetByEmail(email);
            if(user.Data == null)
            {
                return new ErrorResult(Messages.EmailAvailable);
            }
            return new SuccessResult(Messages.UserAlreadyExist);
        }
    }
}
