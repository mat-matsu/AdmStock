using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdmStock.Models
{
    public class Venta
    {
        [Key]
        public int venta_id { get; set; }
        [Required]
        [ForeignKey("lote_id")]
        [Display(Name = "Nro Lote")]
        public int lote_id { get; set; }
        [Required]
        [ForeignKey("cliente_id")]
        [Display(Name = "DNI Cliente")]
        public int cliente_id { get; set; }
        [Required]
        [Display(Name = "Cant Vendida")]
        public int venta_cant { get; set; }
        [Display(Name = "Fecha Venta")]
        public DateTime venta_fecha { get; set; }
        
        public virtual Cliente? Clientes { get; set; }
        public virtual Lote? Lote { get; set; }
    }
}
