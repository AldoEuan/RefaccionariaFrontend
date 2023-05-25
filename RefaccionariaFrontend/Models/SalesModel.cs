using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RefaccionariaFrontend.Models
{
    public class SalesModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="Número de Venta")]
        public int Id { get; set; }

        [Required]
        [Column("SMALLMONEY")]
        public double Total { get; set; }

    }
}