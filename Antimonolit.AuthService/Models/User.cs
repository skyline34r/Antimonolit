using System;

namespace Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }

        public string RefreshToken { get; set; }
    }
}
