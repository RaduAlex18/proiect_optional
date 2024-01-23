using proiect_op_2_v3_final.Models.Base;

namespace proiect_op_2_v3_final.Models
{
    public class CargoTrailer : BaseE
    {
        public string? Brand { get; set; }
        public string? Type { get; set; }
        public int Year { get; set; }
        public string? Color { get; set; }

        //relation one-to-one with "Goods"
        public Goods Goods { get; set; }
        public Guid GoodsId { get; set; }

        //relation many-to-many with "Truck"
        public ICollection<ModelsRelationTRCT> ModelsRelationsTRCT { get; set; }
    }
}
