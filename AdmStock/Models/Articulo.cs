using System.ComponentModel.DataAnnotations;

namespace AdmStock.Models
{
    public class Articulo
    {
        [Key]
        public int art_id { get; set; }
        [Required]
        public string tipo_prod { get; set; }
    }
}
