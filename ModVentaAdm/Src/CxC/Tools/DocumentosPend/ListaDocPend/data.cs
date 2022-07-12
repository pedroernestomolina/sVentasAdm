using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.DocumentosPend.ListaDocPend
{

    public class data
    {
        
        public string autoDoc { get; set; }
        public DateTime fechaEmisionDoc { get; set; }
        public string tipoDoc { get; set; }
        public string numeroDoc { get; set; }
        public DateTime fechaVencDoc { get; set; }
        public string notasDoc { get; set; }
        public decimal importeDoc { get; set; }
        public decimal acumuladoDoc { get; set; }
        public int signoDoc { get; set; }
        public string serieDoc { get; set; }
        public int diasCreditoDoc { get; set; }
        public decimal tasaCambioDoc { get; set; }
        public int diasVencida { get { return DateTime.Now.Date.Subtract(fechaVencDoc).Days; } }
        public decimal montoImporte { get { return importeDoc * signoDoc; } }
        public decimal montoAcumulado { get { return acumuladoDoc * signoDoc; } }
        public decimal montoResta { get { return montoImporte - montoAcumulado; } }


        public data()
        {
            autoDoc = "";
            fechaEmisionDoc = DateTime.Now.Date;
            tipoDoc = "";
            numeroDoc = "";
            fechaVencDoc = DateTime.Now.Date;
            notasDoc = "";
            importeDoc = 0m;
            acumuladoDoc = 0m;
            signoDoc = 1;
            serieDoc = "";
            diasCreditoDoc = 0;
            tasaCambioDoc = 0m;
        }

    }

}