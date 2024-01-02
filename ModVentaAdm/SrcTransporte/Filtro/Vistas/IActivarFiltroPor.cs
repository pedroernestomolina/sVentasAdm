using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Filtro.Vistas
{
    public class EntreFechas
    {
        public bool Activar { get; set; }
        public bool MostrarCheck { get; set; }
    }
    public interface IActivarFiltroPor
    {
        bool PorAliado { get; }
        bool PorCliente { get; }
        bool PorEstatusDoc { get;  }
        EntreFechas PorEntreFechas { get;  }
    }
}
