using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Business.Model
{
    public class CarModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Plate_Number { get; set; }
        public string? Company { get; set; }
        public string? Model { get; set; }
        public int Price { get; set; }
        public string? Color { get; set; }
        public string? Available { get; set; }

    }
}
