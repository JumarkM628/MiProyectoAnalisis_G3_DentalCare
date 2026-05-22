using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.AccesoADatos.Entidades.Usuarios
{
    [Table("AspNetUsers")]
    public class AspNetUserEntidad
    {
        [Key]
        [Column("Id")]
        public string Id { get; set; }
        [Column("UserName")]
        public string UserName { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("EmailConfirmed")]
        public bool EmailConfirmed { get; set; }
        [Column("PasswordHash")]
        public string PasswordHash { get; set; }
        [Column("SecurityStamp")]
        public string SecurityStamp { get; set; }
        [Column("PhoneNumber")]
        public string PhoneNumber { get; set; }
        [Column("PhoneNumberConfirmed")]
        public bool PhoneNumberConfirmed { get; set; }
        [Column("TwoFactorEnabled")]
        public bool TwoFactorEnabled { get; set; }
        [Column("LockoutEndDateUtc")]
        public DateTime? LockoutEndDateUtc { get; set; }
        [Column("LockoutEnabled")]
        public bool LockoutEnabled { get; set; }
        [Column("AccessFailedCount")]
        public int AccessFailedCount { get; set; }


    }
}
