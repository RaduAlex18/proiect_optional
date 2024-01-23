using proiect_op_2_v3_final.Models.Base;

namespace proiect_op_2_v3_final.Models
{
    public class Driver : BaseE
    {
        public string? First_Name { get; set; }
        public string? Last_Name { get; set; }
        public string? CNP { get; set; }
        public int Age { get; set; }

        //relation one-to-many with "City"
        public City City { get; set; }
        public Guid CityId { get; set; }

        //relation one-to-one with "Truck"
        public Truck Truck { get; set; }
    }
}
