using Microsoft.EntityFrameworkCore;
using proiect_op_2_v3_final.Data;
using proiect_op_2_v3_final.Models;
using proiect_op_2_v3_final.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace proiect_op_2_v3_final.Repositories.DriverRepository
{
    public class DriverRepository : GenericRepository<Driver>, IDriverRepository
    {
        public DriverRepository(tableContext tableContext) : base(tableContext) { }

        public List<Driver> OrderByAge(int age)
        {
            var driversOrderedAsc = from d in _table
                                    orderby d.Age
                                    select d;

            return driversOrderedAsc.ToList();
        }

        public List<Driver> GetAllWithInclude()
        {
            return _table.Include(d => d.City).ToList();
        }

        public List<dynamic> GetAllWithJoin()
        {
            var result = _tableContext.Drivers.Join(_tableContext.Trucks, driver => driver.Id, truck => truck.DriverId,
                    (driver, truck) => new { driver, truck }).Select(ob => ob.driver);

            return null;
        }

        public Driver Where(string cnp)
        {
            var result = _table.Where(x => x.CNP == cnp).FirstOrDefault();
            return result;
        }

        public void GroupBy()
        {
            var groupedDrivers = _table.GroupBy(x => x.Id);

            var groupedDrivers2 = from d in _table
                                  group d by d.Age;

            foreach (var driverGroupedByAge in groupedDrivers2)
            {
                Console.WriteLine("Driver group age: " + driverGroupedByAge.Key);

                foreach (var d in driverGroupedByAge)
                {
                    Console.WriteLine("Driver name: " + d.First_Name + " " + d.Last_Name);
                }
            }
        }
    }
}
