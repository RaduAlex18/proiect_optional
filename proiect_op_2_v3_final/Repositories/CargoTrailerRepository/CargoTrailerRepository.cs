using Microsoft.EntityFrameworkCore;
using proiect_op_2_v3_final.Data;
using proiect_op_2_v3_final.Models;
using proiect_op_2_v3_final.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace proiect_op_2_v3_final.Repositories.CargoTrailerRepository
{
    public class CargoTrailerRepository : GenericRepository<CargoTrailer>, ICargoTrailerRepository
    {
        public CargoTrailerRepository(tableContext tableContext) : base(tableContext) { }

        public List<CargoTrailer> OrderByYear(int year)
        {
            var cargoTrailersOrderedByYear = from ct in _table
                                             orderby ct.Year
                                             select ct;

            return cargoTrailersOrderedByYear.ToList();
        }

        public List<CargoTrailer> OrderByType(string type)
        {
            var cargoTrailersOrderedByType = from ct in _table
                                             orderby ct.Type
                                             select ct;

            return cargoTrailersOrderedByType.ToList();
        }

        public List<CargoTrailer> GetAllWithInclude()
        {
            var result = _table.Include(ct => ct.Goods).ToList();
            return result;
        }

        public List<dynamic> GetAllWithJoin()
        {
            var result = _tableContext.CargoTrailers.Join(_tableContext.ModelsRelationsTRCT, cargoTrailer => cargoTrailer.Id, truck => truck.CargoTrailerId,
                (cargoTrailer, truck) => new { cargoTrailer, truck }).Select(ob => ob.cargoTrailer);

            return null;
        }

        public List<CargoTrailer> GetAllWithModelRelationTRCT()
        {
            var result = _table.Include(ct => ct.ModelsRelationsTRCT).ThenInclude(truck => truck.Truck).ToList();
            return result;
        }

        public CargoTrailer Where(string type)
        {
            var result = _table.Where(x => x.Type == type).FirstOrDefault();
            return result!;
        }

        public void GroupBy()
        {
            var groupedCargoTrailers = _table.GroupBy(x => x.Id);

            var groupedCargoTrailers2 = from ct in _table
                                        group ct by ct.Year;

            foreach (var cargoTrailerGroupedByYear in groupedCargoTrailers2)
            {
                Console.WriteLine("Cargo Trailer group Year: " + cargoTrailerGroupedByYear.Key);

                foreach (var ct in cargoTrailerGroupedByYear)
                {
                    Console.WriteLine("Cargo Trailer Type: " + ct.Type);
                }
            }
        }
    }
}
