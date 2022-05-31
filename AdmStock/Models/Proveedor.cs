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

        public Proveedor(int prov_id, string prov_cuil, string prov_nom, string prov_dir, string prov_tel)
        {
            this.prov_id = prov_id;
            this.prov_cuil = prov_cuil;
            this.prov_nom = prov_nom;
            this.prov_dir = prov_dir;
            this.prov_tel = prov_tel;
        }

    }
}
