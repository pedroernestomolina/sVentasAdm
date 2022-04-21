using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Reportes.Modo.ListaDocumento
{

    public class Filtro : Reportes.Filtro.IFiltro
    {

        public bool ActivarSucursal { get { return true; } }
        public bool ActivarDesdeHasta { get { return true; } }
        public bool ActivarEstatus { get { return false; } }
        public bool ActivarMesAnoRelacion { get { return false; } }
        public bool ActivarCliente { get { return false; } }
        public bool ActivarTipoDocumento { get { return true; } }
        public bool ValidarTipoDocumento { get { return true; } }
        public bool ActivarProducto { get { return true; } }
        public bool ActivarPalabreClave { get { return false; } }

    }

}