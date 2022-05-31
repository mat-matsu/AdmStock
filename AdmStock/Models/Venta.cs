using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdmStock.Models
{
    public class Venta
    {
        [Key]
        public int venta_id { get; set; }
        [Required]
        public int lote_id { get; set; }
        [Required]
        public int cliente_id { get; set; }
        [Required]
        public int venta_cant { get; set; }
        public DateTime venta_fecha { get; set; }
        [ForeignKey("cliente_id")]
        public virtual Cliente Clientes { get; set; }
        [ForeignKey("lote_id")]
        public virtual Lote Lote { get; set; }
    }
}
