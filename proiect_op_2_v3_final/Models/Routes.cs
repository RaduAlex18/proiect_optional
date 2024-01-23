using Org.BouncyCastle.Crypto.Macs;
using proiect_op_2_v3_final.Models.Base;

namespace proiect_op_2_v3_final.Models
{
    public class Routes : BaseE
    {
        public int km { get; set; }
        public  string? city_start { get; set; }
        public string? city_end { get; set; }

        //relation many-to-many with "Routes"
        public ICollection<ModelsRelationTRRT> ModelsRelationsTRRT { get; set; }
    }
}
