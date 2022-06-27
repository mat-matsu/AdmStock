using System.ComponentModel.DataAnnotations;

namespace AdmStock.Models
{
    public class Cliente
    {
        [Key]
        public int cliente_id { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        public string cliente_nom { get; set; }
        [Required]
        [Display(Name = "DNI")]
        public int cliente_dni { get; set; }
        [Required]
        [Display(Name = "Direccion")]
        public string cliente_dir { get; set; }
        [Required]
        [Display(Name = "Telefono")]
        public string cliente_tel { get; set; }

    }
}
