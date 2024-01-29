using proiect_op_2_v3_final.Models;
using proiect_op_2_v3_final.Repositories.GenericRepository;

namespace proiect_op_2_v3_final.Repositories.RoutesRepository
{
    public interface IRoutesRepository : IGenericRepository<Routes>
    {
        List<Routes> OrderByKm(int km);

        //List<Routes> GetAllWithInclude();
        List<dynamic> GetAllWithJoin();
        Routes Where(string cityStart);
        void GroupBy();
    }
}
