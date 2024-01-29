using proiect_op_2_v3_final.Models;
using proiect_op_2_v3_final.Repositories.GenericRepository;

namespace proiect_op_2_v3_final.Repositories.GoodsRepository
{
    public interface IGoodsRepository: IGenericRepository<Goods>
    {
        List<Goods> OrderByPrice(int price);
        List<Goods> OrderByQuantity(int quantity);

        //List<Goods> GetAllWithInclude();
        List<dynamic> GetAllWithJoin();
        Goods Where(int price);
        void GroupBy();
    }
}
