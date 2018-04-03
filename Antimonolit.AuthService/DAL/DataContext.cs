using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }

    public static class DbContextExtention
    {
        public static void EnsureSeeded(this DataContext context)
        {
            var users = new List<User>
            {
                new User { UserName = "TestUser", Login = "Test", Password = "12345" },
            };

            var items = context.Users.AsEnumerable();
            foreach (var user in users)
            {
                var item = items.FirstOrDefault(x => x.Login == user.Login);
                if (item == null)
                {
                    context.Users.Add(user);
                }
                else
                {
                    item.UserName = user.UserName;
                    item.Login = user.Login;
                }
            }

            context.SaveChanges();
        }
    }
}
