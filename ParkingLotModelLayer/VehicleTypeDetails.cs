using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ParkingLotModelLayer
{
    public class VehicleTypeDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VehicleTypeID { get; set; }
        [Required]
        public string VehicleType { get; set; }
        [Required]
        public double VehicleCharges { get; set; }
    }
}
