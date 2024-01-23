namespace proiect_op_2_v3_final.Models
{
    public class ModelsRelationTRRT
    {
        public Guid TruckId { get; set; }
        public Truck Truck { get; set; }

        public Guid RoutesId { get; set; }
        public Routes Routes { get; set; }
    }
}
