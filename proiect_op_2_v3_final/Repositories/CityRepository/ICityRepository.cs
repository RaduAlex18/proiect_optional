using proiect_op_2_v3_final.Models;
using proiect_op_2_v3_final.Repositories.GenericRepository;

namespace proiect_op_2_v3_final.Repositories.CityRepository
{
    public interface ICityRepository : IGenericRepository<City>
    {
        List<City> OrderByCountry(string name);
        List<City> OrderByZipCode(int zipCode);
        List<City> GetAllWithInclude();
        List<dynamic> GetAllWithJoin();
        City Where(string name);
        void GroupBy();
    }
}
