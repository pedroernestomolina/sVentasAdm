using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Filtro.Handler
{
    public class dataFiltrar: Vistas.IdataFiltrar
    {
        public DateTime? Desde { get; set; }
        public DateTime? Hasta { get; set; }
        public Vistas.Enumerados.EstatusDoc EstatusDoc { get; set; }
        public int IdCaja { get; set; }
        public string IdCliente { get; set; }


        public dataFiltrar()
        {
            Desde = null;
            Hasta = null;
            EstatusDoc = Vistas.Enumerados.EstatusDoc.SinDefinir;
            IdCaja = -1;
            IdCliente= "";
        }
        public void Inicializa()
        {
            Desde = null;
            Hasta = null;
            EstatusDoc = Vistas.Enumerados.EstatusDoc.SinDefinir;
            IdCaja = -1;
            IdCliente = "";
        }
    }
}