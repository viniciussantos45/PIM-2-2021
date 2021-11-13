using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace backend.Models
{
    public class User
    {
        [Column("id")]
        [Key]
        public int Id {get; set;}


        [Column("name")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(32, ErrorMessage = "Este campo deve conter entre 3 e 32 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 32 caracteres")]
        public string Name {get;set; }


        [Column("surname")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        public string Surname {get;set; }

        
        [Column("username")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(16, ErrorMessage = "Este campo deve conter entre 4 e 16 caracteres")]
        [MinLength(4, ErrorMessage = "Este campo deve conter entre 4 e 16 caracteres")]
        public string Username {get;set; }

        [Column("email")]
        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Digite um email válido")]
        public string Email {get;set; }

        [Column("password")]
        [Required(ErrorMessage = "Senha é obrigatória")]
        public string Password {get;set; }

        [Column("manager")]
        [DefaultValue(false)]
        public bool Manager {get; set;}

        [JsonIgnore]
        public ICollection<Reserve> Reserves {get; set;}
        
    }

    public class UserAuthenticate {
        [Column("email")]
        [EmailAddress(ErrorMessage = "Digite um email válido")]
        public string Email {get;set; }

        [Column("password")]
        public string Password {get;set; }
    } 
}