using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.GestionAliadosDoc.AliadosDoc.Handler
{
    public class Impdata: Vistas.Idata
    {
        private OOB.Transporte.Documento.GestionAliados.ObtenerLista.Item _it;
        //
        public int idRef { get; set; }
        public int aliadoId { get; set; }
        public string aliadoCiRif { get; set; }
        public string aliadoNombre { get; set; }
        public decimal importeDiv { get; set; }
        public decimal acumuladoDiv { get; set; }
        //
        public Impdata()
        {
        }
        public Impdata(OOB.Transporte.Documento.GestionAliados.ObtenerLista.Item it)
        {
            _it = it;
            idRef = it.idRefAliadoDoc;
            aliadoId = it.aliadoId;
            aliadoCiRif = it.aliadoCiRif;
            aliadoNombre = it.aliadoNombre;
            importeDiv = it.importeDivAliadoDoc;
            acumuladoDiv = it.acumuladoDivAliadoDoc;
        }
    }
}