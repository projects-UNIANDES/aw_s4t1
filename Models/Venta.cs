using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aw_s4t1.Models
{
    public class Venta
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El número de la venta es obligatorio")]
        [StringLength(20, ErrorMessage = "El número no puede superar los 20 caracteres")]
        [Display(Name = "Número")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "La fecha de la venta es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "El total de la venta es obligatorio")]
        [Range(0, 999999, ErrorMessage = "El total no puede ser negativo")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "La forma de pago es obligatoria")]
        [StringLength(20, ErrorMessage = "La forma de pago no puede superar los 20 caracteres")]
        [Display(Name = "Forma de Pago")]
        public string FormaPago { get; set; }

        //relacion entre la tabla Cliente y Venta
        [Required(ErrorMessage = "El cliente de la venta es obligatorio")]
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
    }
}
