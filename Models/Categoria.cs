using System.ComponentModel.DataAnnotations;

namespace aw_s4t1.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El código de la categoría es obligatorio")]
        [Length(2, 10, ErrorMessage = "El código debe tener entre 2 y 10 caracteres")]
        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "El nombre de la categoría es obligatorio")]
        [Length(3, 50, ErrorMessage = "El nombre de la categoría debe tener entre 3 y 50 caracteres")]
        public string Nombre { get; set; }

        [StringLength(200, ErrorMessage = "La descripción no puede superar los 200 caracteres")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Activa")]
        public bool Activa { get; set; } = true;

        [Required(ErrorMessage = "La fecha de creación es obligatoria")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Creación")]
        public DateTime FechaCreacion { get; set; } = DateTime.Today;
    }
}
