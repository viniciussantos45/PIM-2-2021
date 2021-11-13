using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using System;

namespace backend.Models
{
    public class Bedroom
    {
        [Column("id")]
        [Key]
        public int Id {get; set;}
        

        [Column("apartment_number")]
        [JsonProperty(PropertyName = "apartment_number")]
        [Range(1, int.MaxValue, ErrorMessage = "Este campo é obrigatório")]
        public int ApartmentNumber {get;set; }
        
        
        [JsonIgnore]
        public ICollection<Reserve> Reserves {get; set;}
    }



    public class AvailableBedroom {
        [Required]
        public string Date;
    }
}