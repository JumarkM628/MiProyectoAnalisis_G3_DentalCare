using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DentalCare.Abstraccion.Modelo.Usuarios
{
    public class UsuarioDto
    {
        [Display(Name = "ID")]
        public int IdUsuario { get; set; }

        [Display(Name = "Nombre Completo")]
        public string NombreCompleto => $"{Nombre} {PrimerApellido} {SegundoApellido}".Trim();

        [Display(Name = "Área")]
        public string NombreArea { get; set; }

        [Display(Name = "Especialidad")]
        public string NombreEspecialidad { get; set; }

        [Display(Name = "Estado")]
        public string NombreEstado { get; set; }

        [Display(Name = "Fecha Creación")]
        public DateTime? FechaCreacion { get; set; }



        // Datos personales

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$",
            ErrorMessage = "El nombre solo puede contener letras.")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El primer apellido es obligatorio.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$",
            ErrorMessage = "El primer apellido solo puede contener letras.")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        [Display(Name = "Primer Apellido")]
        public string PrimerApellido { get; set; }

        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]*$",
            ErrorMessage = "El segundo apellido solo puede contener letras.")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        [Display(Name = "Segundo Apellido")]
        public string SegundoApellido { get; set; }

        [Required(ErrorMessage = "El tipo de cédula es obligatorio.")]
        [Display(Name = "Tipo de Cédula")]
        public string TipoCedula { get; set; }

        [Required(ErrorMessage = "El número de cédula es obligatorio.")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres.")]
        [Display(Name = "Número de Cédula")]
        public string NumeroCedula { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El teléfono solo puede contener números.")]
        [StringLength(15, ErrorMessage = "Máximo 15 caracteres.")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
        [StringLength(150, ErrorMessage = "Máximo 150 caracteres.")]
        [Display(Name = "Correo Electrónico")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El área es obligatoria.")]
        [Display(Name = "Área")]
        public int IdAreaUsuario { get; set; }

        [Required(ErrorMessage = "La especialidad es obligatoria.")]
        [Display(Name = "Especialidad")]
        public int IdEspecialidad { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        [Display(Name = "Estado")]
        public int IdEstado { get; set; }

        [Display(Name = "Fecha de Contratación")]
        public DateTime? FechaContratacion { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un usuario del sistema.")]
        [Display(Name = "Usuario del Sistema")]
        public string AspNetUserId { get; set; }

        public string RoleId { get; set; }              
        public string RoleName { get; set; }               
        public IEnumerable<SelectListItem> ListaRoles { get; set; } 
        public IEnumerable<SelectListItem> ListaAreas { get; set; }
        public IEnumerable<SelectListItem> ListaEspecialidades { get; set; }
        public IEnumerable<SelectListItem> ListaEstados { get; set; }
        public IEnumerable<SelectListItem> ListaTiposCedula { get; set; }
        public IEnumerable<SelectListItem> ListaAspNetUsuarios { get; set; }
    }
}
