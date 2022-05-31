using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdmStock.Models
{
    public class Producto
    {
        [Key]
        public int prod_id { get; set; }
        [Required]
        public int art_id { get; set; }
        [Required]
        public string prod_nom { get; set; }
        [Required]
        public string prod_desc { get; set; }
        [ForeignKey("art_id")]
        public virtual Articulo Articulos { get; set; }
    }
}
