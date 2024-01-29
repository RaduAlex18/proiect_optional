using Microsoft.EntityFrameworkCore;
using proiect_op_2_v3_final.Data;
using proiect_op_2_v3_final.Models;
using proiect_op_2_v3_final.Repositories.GenericRepository;

namespace proiect_op_2_v3_final.Repositories.CityRepository
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        public CityRepository(tableContext tableContext) : base(tableContext) { }

        public List<City> OrderByCountry(string name)
        {
            var citiesOrderedByCountry = from c in _table
                                      orderby c.Country
                                      select c;

            return citiesOrderedByCountry.ToList();
        }

        public List<City> OrderByZipCode(int zipCode)
        {
            var citiesOrderedByZipCode = from c in _table
                                         orderby c.Zip_code
                                         select c;

            return citiesOrderedByZipCode.ToList();
        }
        /*
        public List<City> GetAllWithInclude()
        {
            return _table.Include(d => d.Drivers).ToList();
        }
        */
        public List<dynamic> GetAllWithJoin()
        {
            var result = _tableContext.Cities.Join(_tableContext.Drivers, city => city.Id, driver => driver.CityId,
                (city, driver) => new { city, driver }).Select(ob => ob.city);

            return null;
        }

        public City Where(string name)
        {
            var result = _table.Where(x => x.Name == name).FirstOrDefault();
            return result!;
        }

        public void GroupBy()
        {
            var groupedCities = _table.GroupBy(x => x.Id);

            var groupedCities2 = from c in _table
                                 group c by c.Zip_code;

            foreach (var cityGroupedByZipCode in groupedCities2)
            {
                Console.WriteLine("City group Zip Code: " + cityGroupedByZipCode.Key);

                foreach (var c in cityGroupedByZipCode)
                {
                    Console.WriteLine("City name: " + c.Name);
                }
            }
        }
    }
}
