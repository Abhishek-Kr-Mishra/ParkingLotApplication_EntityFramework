using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ParkingLotModelLayer
{
    public class DriverTypeDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DriverTypeID { get; set; }
        [Required]
        public string DriverType { get; set; }
        [Required]
        public double DriverCharges { get; set; }

    }
}
