using proiect_op_2_v3_final.Models;
using proiect_op_2_v3_final.Repositories.GenericRepository;

namespace proiect_op_2_v3_final.Repositories.CargoTrailerRepository
{
    public interface ICargoTrailerRepository : IGenericRepository<CargoTrailer>
    {
        List<CargoTrailer> OrderByYear(int year);
        List<CargoTrailer> OrderByType(string type);
        List<CargoTrailer> GetAllWithInclude();
        List<dynamic> GetAllWithJoin();
        List<CargoTrailer> GetAllWithModelRelationTRCT();
        CargoTrailer Where(string type);
        void GroupBy();
    }
}
