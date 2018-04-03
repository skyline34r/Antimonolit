using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;

namespace Services
{
    public class AuthService : IAuthService
    {
        private const string secretKey = "A37F4B08-D855-4B8B-A9E8-E795869BCC5A";

        public string GetToken(string l, string p)
        {
            if(string.IsNullOrWhiteSpace(l) || string.IsNullOrWhiteSpace(p))
            {
                return null;
            }
            var user = getUserData(l);


            if(!isCorrectPassword(user, p))
            {
                return null;
            }
            return JsonWebToken.Encode(user, secretKey);
        }

        public object GetData(string token)
        {
            if(JsonWebToken.IsCorrectToken(token, secretKey))
            {
                return JsonWebToken.Decode(token, secretKey);
            }
            return null;
        }

        private User getUserData(string login)
        {
            var users = new List<User> {
                new User { Login = "test", Password = "qwerty", UserName = "John" }
            };
            return users.FirstOrDefault(x => x.Login == login);
        }

        private bool isCorrectPassword(User user, string password)
        {
            return user.Password == password;
        }
    }

    public interface IAuthService
    {
        string GetToken(string l, string p);
        object GetData(string token);
    }
}
