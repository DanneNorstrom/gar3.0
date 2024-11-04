using System.ComponentModel.DataAnnotations;

namespace gar3._0.Models
{
    public class OverviewViewModel
    {
        public Vehicle Vehicletype { get; set; } 

        [Key]
        public string Regnr { get; set; } = string.Empty;

        //[DataType(DataType.Time)]
        public DateTime Arrival { get; set; }
    }
}
