using System.ComponentModel.DataAnnotations;

namespace AdmStock.Models
{
    public class Proveedor
    {
        [Key]
        public int prov_id { get; set; }
        [Required]
        [Display(Name = "CUIL")]
        public string prov_cuil { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        public string prov_nom { get; set; }
        [Required]
        [Display(Name = "Direccion")]
        public string prov_dir { get; set; }
        [Required]
        [Display(Name = "Telefono")]
        public string prov_tel { get; set; }

        
    }
}
