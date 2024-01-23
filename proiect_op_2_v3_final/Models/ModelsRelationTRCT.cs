namespace proiect_op_2_v3_final.Models
{
    public class ModelsRelationTRCT
    {
        public Guid TruckId { get; set; }
        public Truck Truck { get; set; }

        public Guid CargoTrailerId { get; set; }
        public CargoTrailer CargoTrailer { get; set; }
    }
}
