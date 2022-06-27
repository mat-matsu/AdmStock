using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdmStock.Models
{
    public class Lote
    {
        [Key]
        [Display(Name = "Nro Lote")]
        public int lote_id { get; set; }
        [Required]
        [ForeignKey("prod_id")]
        [Display(Name = "Producto")]
        public int prod_id { get; set; }
        [Required]
        [ForeignKey("prov_id")]
        [Display(Name = "Proveedor")]
        public int prov_id { get; set; }
        [Required]
        [Display(Name = "Cantidad")]
        public int lote_cant { get; set; }
        [Required]
        [Display(Name = "Precio")]
        public double lote_precio { get; set; }
        
        public virtual Producto? Productos { get; set; }
        public virtual Proveedor? Proveedores { get; set; }
    }
}
