using proiect_op_2_v3_final.Models;
using proiect_op_2_v3_final.Repositories.GenericRepository;
using System;
using System.Collections.Generic;

namespace proiect_op_2_v3_final.Repositories.DriverRepository
{
    public interface IDriverRepository : IGenericRepository<Driver>
    {
        List<Driver> OrderByAge(int age);
        List<Driver> GetAllWithInclude();
        List<dynamic> GetAllWithJoin();
        Driver Where(string cnp);
        void GroupBy();
    }
}
