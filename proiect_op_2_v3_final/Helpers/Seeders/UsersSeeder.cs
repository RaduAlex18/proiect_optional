using proiect_op_2_v3_final.Data;
using proiect_op_2_v3_final.Models;

namespace proiect_op_2_v3_final.Helpers.Seeders
{
    public class UsersSeeder
    {
        public readonly tableContext _tableContext;

        public UsersSeeder(tableContext tableContext)
        {
            _tableContext = tableContext;
        }

        public void SeedInitialUsers()
        {
            if (!_tableContext.Users.Any())
            {
                var user1 = new User
                {
                    FirstName = "Fist name User 1",
                    LastName = "Last name User 1",
                    Email = "user1@mail.com",
                    Nickname = "user1"
                };

                var user2 = new User
                {
                    FirstName = "Fist name User 2",
                    LastName = "Last name User 2",
                    Email = "user2@mail.com",
                    Nickname = "user2"
                };

                _tableContext.Users.Add(user1);
                _tableContext.Users.Add(user2);

                _tableContext.SaveChanges();
            }
        }
    }
}