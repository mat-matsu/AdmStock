using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdmStock.Models
{
    public class Lote
    {
        [Key]
        public int lote_id { get; set; }
        [Required]
        public int prod_id { get; set; }
        [Required]
        public int prov_id { get; set; }
        [Required]
        public int lote_cant { get; set; }
        [Required]
        public double lote_precio { get; set; }
        [ForeignKey("prod_id")]
        public virtual Producto Productos { get; set; }
        [ForeignKey("prov_id")]
        public virtual Proveedor Proveedores { get; set; }
    }
}
