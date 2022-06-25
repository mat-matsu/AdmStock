using System.ComponentModel.DataAnnotations;

namespace AdmStock.Models
{
    public class Proveedor
    {
        [Key]
        public int prov_id { get; set; }
        [Required]
        public string prov_cuil { get; set; }
        [Required]
        public string prov_nom { get; set; }
        [Required]
        public string prov_dir { get; set; }
        [Required]
        public string prov_tel { get; set; }

        
    }
}
