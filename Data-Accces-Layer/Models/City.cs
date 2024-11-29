using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Accces_Layer.Models
{
    public class City
    {

        public City()
        {
            
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(55)]
        public string Name { get; set; }

        [Required]
        public double PositionLat { get; set; }

        [Required]
        public double PositionLng { get; set; }

        [MaxLength(60)]
        public string? Country { get; set; }

        [MaxLength(10)]
        public string? PostalCode { get; set; }

        [MaxLength(70)]
        public string? State { get; set; }

        
    }
}
