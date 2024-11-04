using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace gar3._0.Models
{
    public class RecieptViewModel
    { 

        [Key]
        public string Regnr { get; set; } = string.Empty;
        public string RegnrFormatted { get; set; } = string.Empty;

        //[DataType(DataType.Time)]
        public DateTime Arrival { get; set; }
        public string ArrivalFormatted { get; set; } = string.Empty;

        //[DataType(DataType.Time)]
        public DateTime Departure { get; set; }
        public string DepartureFormatted { get; set; } = string.Empty;

        //[DataType(DataType.Time)]
        //public TimeSpan Parkingtime { get; set; }

        public int Parkedhours { get; set; }
        public string ParkedhoursFormatted { get; set; } = string.Empty;

        public int RatePerHour { get; set; } = 10;
        public string RatePerHourFormatted { get; set; } = string.Empty;

        public int Fee { get; set; }
        public string FeeFormatted { get; set; } = string.Empty;

    }
}
