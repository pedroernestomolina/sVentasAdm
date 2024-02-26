using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcComun.Clientes.Filtros.Opciones
{
    public interface IOpciones
    {
        bool ActivarGrupo { get; set; }
        bool ActivarEstado { get; set; }
        bool ActivarZona { get; set; }
        bool ActivarVendedor { get; set; }
        bool ActivarCobrador { get; set; }
        bool ActivarCategoria { get; set; }
        bool ActivarNivel { get; set; }
        bool ActivarEstatus { get; set; }
        bool ActivarCredito { get; set; }
    }
}