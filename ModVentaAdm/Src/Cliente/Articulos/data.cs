using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Cliente.Articulos
{

    public class data
    {


        private OOB.Maestro.Cliente.Articulos.Ficha it;


        public DateTime Fecha { get { return it.fecha; } }
        public string Tipo { get { return it.nombreTipoDoc; } }
        public string Serie { get { return it.serie; } }
        public string Documento { get { return it.documento; } }
        public string CodPrd { get { return it.codigoPrd; } }
        public string DescripcionPrd { get { return it.nombrePrd; } }
        public decimal Cantidad { get { return it.cantidad; } }
        public string EmpaqueCompra { get { return it.EmpaqueCont ; } }
        public decimal PrecioDivisa { get { return it.PrecioDivisa; } }
        public string Estatus { get { return it.IsAnulado ? "ANULADO" : ""; } }


        public data(OOB.Maestro.Cliente.Articulos.Ficha it)
        {
            this.it = it;
        }

    }

}