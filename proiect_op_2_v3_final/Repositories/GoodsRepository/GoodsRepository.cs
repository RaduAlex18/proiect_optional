using Microsoft.EntityFrameworkCore;
using proiect_op_2_v3_final.Data;
using proiect_op_2_v3_final.Models;
using proiect_op_2_v3_final.Repositories.GenericRepository;

namespace proiect_op_2_v3_final.Repositories.GoodsRepository
{
    public class GoodsRepository : GenericRepository<Goods>, IGoodsRepository
    {
        public GoodsRepository(tableContext tableContext) : base(tableContext) { }

        public List<Goods> OrderByPrice(int price) 
        {
            var goodsOrderedAsc = from s in _table
                                  orderby s.Price
                                  select s;

            return goodsOrderedAsc.ToList();
        }

        public List<Goods> OrderByQuantity(int quantity)
        {
            var goodsOrderedAscq = from s in _table
                                   orderby s.Quantity
                                   select s;

            return goodsOrderedAscq.ToList();
        }
        /*
        public List<Goods> GetAllWithInclude()
        {

            return _table.Include(c => c.CargoTrailer).ToList();

        }
        */
        public List<dynamic> GetAllWithJoin()
        {
            var result = _tableContext.Goodss.Join(_tableContext.CargoTrailers, goods => goods.Id, cargotrailer => cargotrailer.GoodsId,
                (goods, cargotrailer) => new { goods, cargotrailer }).Select(ob => ob.goods);

            return null;
        }

        public Goods Where(int price)
        {
            var result = _table.Where(x => x.Price == price).FirstOrDefault();
            return result!;
        }

        public void GroupBy()
        {
            var groupedGoods = _table.GroupBy(x => x.Id);

            var groupedGoods2 = from s in _table
                                group s by s.Price;

            foreach (var goodsGroupedByPrice in groupedGoods2)
            {
                Console.WriteLine("Goods group price: " + goodsGroupedByPrice.Key);

                foreach (var s in goodsGroupedByPrice)
                {
                    Console.WriteLine("Goods name: " + s.Name);
                }
            }
        }

    }
}
