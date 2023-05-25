using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RefaccionariaFrontend.Models
{
    public class DetalleVentaModel
    {
        [Display(Name = "Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "Id de Ventas")]
        [Required]
        public int IdVenta { get; set; }

        [Display(Name = "Codigo del Producto")]
        [Required]
        public int IdProducto { get; set; }

        [Display(Name = "Cantidad")]
        [Required]
        public int Cantidad { get; set; }

        [Display(Name = "Precio de venta")]
        [Required]
        [Column(TypeName = "SMALLMONEY")]
        public double Precioventa { get; set; }

         
    }
}