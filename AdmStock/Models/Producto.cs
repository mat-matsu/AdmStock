using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdmStock.Models
{
    public class Producto
    {
        [Key]
        public int prod_id { get; set; }
        [Required]
        [ForeignKey("art_id")]
        [Display(Name = "Tipo de Articulo")]
        public int art_id { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        public string prod_nom { get; set; }
        [Required]
        [Display(Name = "Descripcion")]
        public string prod_desc { get; set; }
        
        public virtual Articulo? Articulos { get; set; }

    }
}
