using System.ComponentModel.DataAnnotations;

namespace gar3._0.Models
{
    public class ParkedVehicle
    { 
        public Vehicle Vehicletype { get; set; }

        [Key]
        public string Regnr { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Numofwheels { get; set; } = 0;

        //public List<string> Mylist { get; set; } = new List<string>();

        //[DataType(DataType.Time)]
        public DateTime Arrival { get; set; } = DateTime.Now;
    }

    public enum Vehicle
    {
        Car,
        Motorcycle
    }
}
