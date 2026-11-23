using System.ComponentModel.DataAnnotations;

namespace aw_s4t1.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La cédula del cliente es obligatoria")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "La cédula debe tener 10 dígitos")]
        [Display(Name = "Cédula")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "Los nombres del cliente son obligatorios")]
        [Length(3, 100, ErrorMessage = "Los nombres deben tener entre 3 y 100 caracteres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El teléfono del cliente es obligatorio")]
        [RegularExpression(@"^\d{7,10}$", ErrorMessage = "El teléfono debe tener entre 7 y 10 dígitos")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El correo del cliente es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido")]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La dirección del cliente es obligatoria")]
        [Length(5, 200, ErrorMessage = "La dirección debe tener entre 5 y 200 caracteres")]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }
    }
}
