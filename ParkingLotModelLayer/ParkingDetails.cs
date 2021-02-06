using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingLotModelLayer
{
    public class ParkingDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ParkingID { get; set; }
        [Required]
        public string VehicleNumber { get; set; }
        [Required]
        public DateTime EntryTime { get; set; }
        [Required]
        [ForeignKey("ParkingTypeDetails")]
        public int ParkingType { get; set; }
        public ParkingTypeDetails ParkingTypeDetails { get; set; } 
        [Required]
        [ForeignKey("VehicleTypeDetails")]
        public int VehicleType { get; set; }
        public VehicleTypeDetails VehicleTypeDetails { get; set; }
        [Required]
        [ForeignKey("DriverTypeDetails")]
        public int DriverType { get; set; }
        public DriverTypeDetails DriverTypeDetails { get; set; }
        public DateTime ExitTime { get; set; }
        [Required]
        public int ParkingSlotNumber { get; set; }
        public double Charges { get; set; }
        [Required]
        public bool IsEmpty { get; set; }
    }
}
