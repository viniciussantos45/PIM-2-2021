using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Newtonsoft.Json;
using System;

namespace backend.Models
{
    public class Reserve
    {
        [Column("id")]
        [Key]
        public int Id {get; set;}


        // Foreign key for bedroom
        [Column("bedroom_id")]
        [JsonProperty(PropertyName = "bedroom_id")]
        public int BedroomId {get;set; }
        public Bedroom  Bedroom {get;set; }

        // Foreign key for guest
        [Column("hotel_guest_id")]
        [JsonProperty(PropertyName = "hotel_guest_id")]
        public int HotelGuestId {get;set; }
        public User User {get;set; }

        [Column("check_in")]
        [JsonProperty(PropertyName = "check_in")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public DateTime CheckIn {get;set; }

        [Column("check_out")]
        [JsonProperty(PropertyName = "check_out")]
        public DateTime? CheckOut {get;set; }
        
    }
}