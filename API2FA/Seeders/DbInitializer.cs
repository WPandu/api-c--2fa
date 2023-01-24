using API2FA.Helpers;
using API2FA.Models;

namespace API2FA.Seeders
{
    internal class DbInitializer
    {
        internal static void Initialize(DataContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
            dbContext.Database.EnsureCreated();
            if (dbContext.Users.Any()) return;

            var users = new User[]
            {
                new User{ Email = "test@mailinator.com", Password = BCrypt.Net.BCrypt.HashPassword("password"), Name = "Lorem Ipsum" }
            };

            foreach (var user in users)
            {
                dbContext.Users.Add(user);
            }

            dbContext.SaveChanges();
        }
    }
}
