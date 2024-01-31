using proiect_op_2_v3_final.Models;
using proiect_op_2_v3_final.Repositories.GenericRepository;
using System;
using System.Collections.Generic;

namespace proiect_op_2_v3_final.Repositories.TruckRepository
{
    public interface ITruckRepository : IGenericRepository<Truck>
    {
        List<Truck> OrderByYear(int year);
        List<Truck> GetAllWithInclude();
        List<dynamic> GetAllWithJoin();
        Truck Where(string brand);
        void GroupBy();
    }
}
