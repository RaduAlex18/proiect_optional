using proiect_op_2_v3_final.Models;
using System.Linq;

namespace proiect_op_2_v3_final.Helpers.Extensions
{
    public static class OnlyActive
    {
        public static IQueryable<User> GetActiveUsers(this IQueryable<User> query)
        {
            return query.Where(x => !string.IsNullOrEmpty(x.FirstName) && !string.IsNullOrEmpty(x.LastName));
        }
    }
}
