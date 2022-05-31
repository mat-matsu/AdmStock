using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AdmStock.Models
{
    public class AdmStockContext : DbContext
    {
        public virtual DbSet<Proveedor> Proveedores { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Venta> Ventas { get; set; }
        public virtual DbSet<Articulo> Articulos { get; set; }
        public virtual DbSet<Lote> Lotes { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-L7TE2QO\\SQLEXPRESS; Initial Catalog=AdmStock; Integrated Security=true;");
            }
        }
    }
}
