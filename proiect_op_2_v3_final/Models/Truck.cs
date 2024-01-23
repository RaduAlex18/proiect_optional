using proiect_op_2_v3_final.Models.Base;

namespace proiect_op_2_v3_final.Models
{
    public class Truck : BaseE
    {
        public string? Brand_T { get; set; }
        public int Year { get; set; }
        public string? Color { get; set; }

        //relation one-to-one with "Driver"
        public Driver Driver { get; set; }
        public Guid DriverId { get; set; }

        //relation many-to-many with "CargoTrailer"
        public ICollection<ModelsRelationTRCT> ModelsRelationsTRCT { get; set; }

        //relation many-to-many with "Routes"
        public ICollection<ModelsRelationTRRT> ModelsRelationsTRRT { get; set; }
    }
}
