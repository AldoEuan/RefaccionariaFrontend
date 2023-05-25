using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RefaccionariaFrontend.Models
{
    public class ProductoModel
    {

        [Display(Name = "Código de producto")]
        [Required]
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        [Required]
        public string Descripcion { get; set; }

        [Display(Name = "Precio de Venta")]
        [Required]
        public double Preciocosto { get; set; }

        [Display(Name = "Precio de Compra")]
        [Required]
        public double Precioventa { get; set; }

        [Display(Name = "Existencias")]
        [Required]
        public int Existencia { get; set; }
    }
}