using Microsoft.EntityFrameworkCore;
using proiect_op_2_v3_final.Data;
using proiect_op_2_v3_final.Models;
using proiect_op_2_v3_final.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace proiect_op_2_v3_final.Repositories.TruckRepository
{
    public class TruckRepository : GenericRepository<Truck>, ITruckRepository
    {
        public TruckRepository(tableContext tableContext) : base(tableContext) { }

        public List<Truck> OrderByYear(int year)
        {
            var trucksOrderedAsc = from t in _table
                                   orderby t.Year
                                   select t;

            return trucksOrderedAsc.ToList();
        }

        public List<Truck> GetAllWithInclude()
        {
            return _table.Include(t1 => t1.Driver).Include(t2 => t2.ModelsRelationsTRCT).Include(t3 => t3.ModelsRelationsTRRT).ToList();
        }

        public List<dynamic> GetAllWithJoin()
        {
            var result = _tableContext.Trucks
                .Join(_tableContext.ModelsRelationsTRCT, truck => truck.Id, cargoTrailer => cargoTrailer.TruckId,
                    (truck, cargoTrailer) => new { truck, cargoTrailer }).Select(ob => ob.truck)
                .Join(_tableContext.ModelsRelationsTRRT, truck => truck.Id, route => route.TruckId,
                    (truck, route) => new { truck, route }).Select(ob => ob.truck);
                
            return null;
        }

        public Truck Where(string brand)
        {
            var result = _table.Where(x => x.Brand_T == brand).FirstOrDefault();
            return result;
        }

        public void GroupBy()
        {
            var groupedTrucks = _table.GroupBy(x => x.Id);

            var groupedTrucks2 = from t in _table
                                 group t by t.Year;

            foreach (var truckGroupedByYear in groupedTrucks2)
            {
                Console.WriteLine("Truck group year: " + truckGroupedByYear.Key);

                foreach (var t in truckGroupedByYear)
                {
                    Console.WriteLine("Truck brand: " + t.Brand_T);
                }
            }
        }
    }
}
