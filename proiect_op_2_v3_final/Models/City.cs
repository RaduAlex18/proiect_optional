using proiect_op_2_v3_final.Models.Base;

namespace proiect_op_2_v3_final.Models
{
    public class City : BaseE
    {
        public string? Country { get; set; }
        public string? Name { get; set; }
        public int Zip_code { get; set; }

        //relation one-to-many with "Driver"
        public ICollection<Driver> Drivers { get; set; }
    }
}
