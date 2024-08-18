using Microsoft.AspNetCore.Mvc.Rendering;

namespace MnaejoPresupuesto.Models
{
    public class CuentaCreacionViewModel : Cuenta
    {
        public IEnumerable<SelectListItem> TiposCuentas { get; set; }
    }
}
