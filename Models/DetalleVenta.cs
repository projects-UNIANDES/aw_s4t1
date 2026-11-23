using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aw_s4t1.Models
{
    public class DetalleVenta
    {
        public int Id { get; set; }

        //relacion entre la tabla Venta y DetalleVenta
        [Required(ErrorMessage = "La venta es obligatoria")]
        [Display(Name = "Venta")]
        public int VentaId { get; set; }
        public Venta Venta { get; set; }

        //relacion entre la tabla Producto y DetalleVenta
        [Required(ErrorMessage = "El producto es obligatorio")]
        [Display(Name = "Producto")]
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El precio unitario es obligatorio")]
        [Range(0.01, 999999, ErrorMessage = "El precio unitario debe ser mayor a 0")]
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Precio Unitario")]
        public decimal PrecioUnitario { get; set; }

        [Required(ErrorMessage = "El descuento es obligatorio")]
        [Range(0, 100, ErrorMessage = "El descuento debe estar entre 0 y 100")]
        [Column(TypeName = "decimal(5,2)")]
        [Display(Name = "Descuento (%)")]
        public decimal Descuento { get; set; }
    }
}
