using proiect_op_2_v3_final.Models.Base;

namespace proiect_op_2_v3_final.Models
{
    public class Goods : BaseE
    {
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }

        //relation one-to-one with CargoTrailer

        public CargoTrailer CargoTrailer { get; set; }
    }
}
