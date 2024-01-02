using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Filtro.Vistas
{
    public class Enumerados
    {
        public enum EstatusDoc { SinDefinir = -1, Activo = 1, Anulado };
    }
    public interface IdataFiltrar
    {
        DateTime? Desde { get; set; }
        DateTime? Hasta { get; set; }
        Enumerados.EstatusDoc EstatusDoc { get; set; }
        int IdCaja { get; set; }
        string IdCliente { get; set; }
        int IdAliado { get; set; }
        //
        void Inicializa();
    }
}