using proiect_op_2_v3_final.Models;
using proiect_op_2_v3_final.Repositories.GenericRepository;

namespace proiect_op_2_v3_final.Repositories.UserRepository
{
    public interface IUserRepository: IGenericRepository<User>
    {
        User FindByNickname(string nickname);

        List<User> FindAllActive();
    }
}
