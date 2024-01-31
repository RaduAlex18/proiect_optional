using proiect_op_2_v3_final.Data;
using proiect_op_2_v3_final.Models;
using proiect_op_2_v3_final.Repositories.GenericRepository;
using proiect_op_2_v3_final.Helpers.Extensions;

namespace proiect_op_2_v3_final.Repositories.UserRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(tableContext tableContext) : base(tableContext) { }

        public List<User> FindAllActive()
        {
            return _table.GetActiveUsers().ToList();
        }

        public User FindByNickname(string nickname)
        {
            return _table.FirstOrDefault(u => u.Nickname.Equals(nickname));
        }
    }
}
