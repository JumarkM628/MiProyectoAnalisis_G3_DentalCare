using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.AccesoADatos.Entidades
{
    [Table("FIDE_USUARIO_TB")]
    public class UsuariosEntidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID_USUARIO")]
        public int IdUsuario { get; set; }
        [Column("ASPNET_USER_ID")]
        public string ASPNET_USER_ID { get; set; }
        [Column("NOMBRE")]
        public string Nombre { get; set; }
        [Column("PRIMER_APELLIDO")]
        public string PrimerApellido { get; set; }
        [Column("SEGUNDO_APELLIDO")]
        public string SegundoApellido { get; set; }
        [Column("ID_AREA_USUARIO")]
        public int IdAreaUsuario { get; set; }
        [Column("ID_ESPECIALIDAD")]
        public int IdEspecialidad { get; set; }
        [Column("ID_ESTADO")]
        public int IdEstado { get; set; }
        [Column("FECHA_CONTRATACION")]
        public DateTime? FechaDeContratacion { get; set; }
        [Column("FECHA_DE_CREACION")]
        public DateTime? FechaDeCreacion { get; set; }
    }
}
