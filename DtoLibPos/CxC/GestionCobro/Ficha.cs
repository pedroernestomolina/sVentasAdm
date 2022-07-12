using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.CxC.GestionCobro
{
    
    public class Ficha
    {

        public string SucPrefijo { get; set; }
        public FichaCobro Cobro { get; set; }
        public FichaRecibo Recibo { get; set; }
        public List<FichaDocumento> Documentos { get; set; }
        public List<FichaMetodoPago> MetodosPago { get; set; }
        public FichaCliente saldoCliente { get; set; }
        public FichaNotaAdm notaAdm { get; set; }


        public Ficha() 
        {
            SucPrefijo = "";
            Cobro = new FichaCobro();
            Recibo = new FichaRecibo();
            Documentos = new List<FichaDocumento>();
            MetodosPago = new List<FichaMetodoPago>();
            saldoCliente = new FichaCliente();
            notaAdm = null;
        }

    }

}