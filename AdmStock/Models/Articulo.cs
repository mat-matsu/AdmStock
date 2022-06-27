using System.ComponentModel.DataAnnotations;

namespace AdmStock.Models
{
    public class Articulo
    {
        [Key]
        public int art_id { get; set; }
        [Required]
        [Display(Name = "Tipo de Articulo")]
        public string tipo_prod { get; set; }
    }
}
