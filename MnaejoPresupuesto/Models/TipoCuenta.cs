﻿using Microsoft.AspNetCore.Mvc;
using MnaejoPresupuesto.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace MnaejoPresupuesto.Models
{
    public class TipoCuenta
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [PrimeraLetraMayuscula]
        [Remote(action: "VerificarExisteTipoCuenta", controller: "TiposCuentas")]
        //[StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "La longitud del campo {0} debe estar entre {2} y {1}")]
        [Display(Name = "Nombre del tipo cuenta")]
        public string? Nombre { get; set; }

        public int UsuarioId { get; set; }

        public int Orden { get; set; }

        /*Pruebas de otras validaciones por defecto*/
        //[Required(ErrorMessage = "El campo {0} es requerido")]
        //[EmailAddress(ErrorMessage ="El campo debe ser un correo válido")]
        //public string? Email { get; set; }

        //[Range(minimum:18, maximum:130, ErrorMessage ="El valor debe estar comprendido entre {1} y {2}")]
        //public int Edad {  get; set; }

        //[Url(ErrorMessage ="El campo debe ser una URL válida")]
        //public string? Url { get; set; }

        //[CreditCard(ErrorMessage ="La tarjeta de crédito no es válida")]
        //[Display(Name ="Tarjeta de Crédito")]
        //public string? TarjetaDeCredito { get; set; }
    }
}
