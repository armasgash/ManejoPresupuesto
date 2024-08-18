﻿using MnaejoPresupuesto.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace MnaejoPresupuesto.Models
{
    public class Cuenta
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50)]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
        [Display(Name = "Tipos de Cuenta")]
        public int TipoCuentaId { get; set; }
        public decimal Balance { get; set; }
        [StringLength(maximumLength: 1000)]
        public string? Descripcion { get; set; }
        public string TipoCuenta { get; set; }
    }
}
