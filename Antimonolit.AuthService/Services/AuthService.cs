using System;
using System.Collections.Generic;
using System.Linq;
using Antimonolith.Services.Models;
using DAL;
using Models;

namespace Services
{
    public class AuthService : IAuthService
    {
        private const string secretKey = "A37F4B08-D855-4B8B-A9E8-E795869BCC5A";
        private const int defaultExpirationTimeout = 15;

        private DataContext context;

        public AuthService(DataContext context)
        {
            this.context = context;
        }

        public TokenModel GetToken(string login, string password)
        {
            if(string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            var user = getUserData(login);

            if(!isCorrectPassword(user, password))
            {
                return null;
            }

            return getToken(user);
        }

        public TokenModel RenewToken(string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                return null;
            }
            var user = getUserDataByRefreshToken(refreshToken);
            return getToken(user);
        }

        public object GetData(string token)
        {
            if(JsonWebToken.IsCorrectToken(token, secretKey))
            {
                return JsonWebToken.Decode(token, secretKey);
            }
            return null;
        }

        private TokenModel getToken(User user)
        {
            user.RefreshToken = Guid.NewGuid().ToString();
            updateRefreshToken(user);
            var payload = new PayloadModel
            {
                UserName = user.UserName,
                ExpirationDate = DateTimeOffset.Now.AddMinutes(defaultExpirationTimeout).ToString()
            };
            return new TokenModel
            {
                AccessToken = JsonWebToken.Encode(payload, secretKey),
                RefreshToken = user.RefreshToken
            };
        }

        private User getUserData(string login)
        {
            return context.Users.FirstOrDefault(x => x.Login == login);
        }

        private User getUserDataByRefreshToken(string refreshToken)
        {
            return context.Users.FirstOrDefault(x => x.RefreshToken == refreshToken);
        }

        private void updateRefreshToken(User user)
        {
            context.Update(user);
            context.SaveChanges();
        }

        private bool isCorrectPassword(User user, string password)
        {
            return user.Password == password;
        }
    }

    public interface IAuthService
    {
        TokenModel GetToken(string login, string password);

        object GetData(string token);
    }
}
