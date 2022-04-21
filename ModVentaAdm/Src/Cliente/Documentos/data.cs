using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Cliente.Documentos
{
    

    public class data
    {

        private OOB.Maestro.Cliente.Documento.Ficha it;


        public string id { get { return it.id; } }
        public DateTime Fecha { get { return it.fecha; } }
        public string Tipo { get { return it.nombreTipoDoc; } }
        public string Serie { get { return it.serie; } }
        public string Documento { get { return it.documento; } }
        public decimal ImporteDivisa { get { return it.montoDivisa; } }
        public string Estatus { get { return it.IsAnulado ? "ANULADO" : ""; } }


        public data(OOB.Maestro.Cliente.Documento.Ficha it)
        {
            this.it = it;
        }

    }

}