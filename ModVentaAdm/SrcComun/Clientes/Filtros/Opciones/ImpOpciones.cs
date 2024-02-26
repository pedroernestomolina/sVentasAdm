using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcComun.Clientes.Filtros.Opciones
{
    public class ImpOpciones: IOpciones
    {
        public bool ActivarGrupo { get; set; }
        public bool ActivarEstado { get; set; }
        public bool ActivarZona { get; set; }
        public bool ActivarVendedor { get; set; }
        public bool ActivarCobrador { get; set; }
        public bool ActivarCategoria { get; set; }
        public bool ActivarNivel { get; set; }
        public bool ActivarEstatus { get; set; }
        public bool ActivarCredito { get; set; }
        public ImpOpciones()
        {
        }
    }
}