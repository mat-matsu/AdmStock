using System.ComponentModel.DataAnnotations;

namespace AdmStock.Models
{
    public class Cliente
    {
        [Key]
        public int cliente_id { get; set; }
        [Required]
        public string cliente_nom { get; set; }
        [Required]
        public int cliente_dni { get; set; }
        [Required]
        public string cliente_dir { get; set; }
        [Required]
        public string cliente_tel { get; set; }

    }
}
