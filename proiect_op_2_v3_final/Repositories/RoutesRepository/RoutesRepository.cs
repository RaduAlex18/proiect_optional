using Microsoft.EntityFrameworkCore;
using proiect_op_2_v3_final.Data;
using proiect_op_2_v3_final.Models;
using proiect_op_2_v3_final.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace proiect_op_2_v3_final.Repositories.RoutesRepository
{
    public class RoutesRepository : GenericRepository<Routes>, IRoutesRepository
    {
        public RoutesRepository(tableContext tableContext) : base(tableContext) { }

        public List<Routes> OrderByKm(int km)
        {
            var routesOrderedByKm = from r in _table
                                    orderby r.km
                                    select r;

            return routesOrderedByKm.ToList();
        }
        /*
        public List<Routes> GetAllWithInclude()
        {
            var result = _table.Include(tr => tr.ModelsRelationsTRRT).ThenInclude(tr => tr.Truck).ToList();
            return result;
        }
        */
        public List<dynamic> GetAllWithJoin()
        {
            var result = _tableContext.Routess.Join(_tableContext.ModelsRelationsTRRT, routes => routes.Id, truck => truck.RoutesId,
                (routes, truck) => new { routes, truck }).Select(ob => ob.routes);

            return null;
        }

        public Routes Where(string cityStart)
        {
            var result = _table.Where(x => x.city_start == cityStart).FirstOrDefault();
            return result!;
        }

        public void GroupBy()
        {
            var groupedRoutes = _table.GroupBy(x => x.Id);

            var groupedRoutes2 = from r in _table
                                 group r by r.km;

            foreach (var routeGroupedByKm in groupedRoutes2)
            {
                Console.WriteLine("Route group km: " + routeGroupedByKm.Key);

                foreach (var r in routeGroupedByKm)
                {
                    Console.WriteLine("Route city start: " + r.city_start);
                }
            }
        }
    }
}
