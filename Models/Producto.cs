using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aw_s4t1.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio")]
        [Length(3, 100, ErrorMessage = "El nombre del producto debe tener entre 3 y 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El precio del producto es obligatorio")]
        [Range(0.01, 999999, ErrorMessage = "El precio debe ser mayor a 0")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El stock del producto es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "La unidad de medida es obligatoria")]
        [StringLength(20, ErrorMessage = "La unidad de medida no puede superar los 20 caracteres")]
        [Display(Name = "Unidad de Medida")]
        public string UnidadMedida { get; set; }

        //relacion entre la tabla Categoria y Producto
        [Required(ErrorMessage = "La categoría del producto es obligatoria")]
        [Display(Name = "Categoría")]
        public int CategoriaId { get; set; }
        [Display(Name = "Categoría")]
        public Categoria Categoria { get; set; }
    }
}
