using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.Reporte.Cxc.EdoCta
{
    public class Movimiento
    {
        public enum tipoDocumentoEnum {SinDefinir=-1, FAC=1, NDB=2, NCR=3, PAGO};
        public string idDoc { get; set; }
        public DateTime fechaDoc { get; set; }
        public string nroDoc { get; set; }
        public string tipoDoc { get; set; }
        public DateTime fechaVencDoc { get; set; }
        public decimal importeDiv { get; set; }
        public int signoDoc { get; set; }
        public string notasDoc { get; set; }
        public string estatusDocAdm { get; set; }
        //
        public decimal anticipoCargado { get; set; }
        //
        public bool esDocAdministrativo { get { return estatusDocAdm.Trim().ToUpper() == "1"; } }
        public tipoDocumentoEnum tipoDocumento {get {return tipoDocumentoFunc();}}
        //
        private tipoDocumentoEnum tipoDocumentoFunc()
        {
            var tip = tipoDocumentoEnum.SinDefinir;
            switch (tipoDoc.Trim().ToUpper()) 
            {
                case "FAC":
                    tip=tipoDocumentoEnum.FAC;
                    break;
                case "NCR":
                    tip= tipoDocumentoEnum.NCR;
                    break;
                case "NDB":
                    tip = tipoDocumentoEnum.NDB;
                    break;
                case "PAG":
                    tip =tipoDocumentoEnum.PAGO;
                    break;
                default:
                    return tip ;
            }
            return tip;
        }
    }
}