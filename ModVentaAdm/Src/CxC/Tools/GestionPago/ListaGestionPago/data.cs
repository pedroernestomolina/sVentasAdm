using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.GestionPago.ListaGestionPago
{
    
    public class data
    {

        private bool _isPagarOk;
        private decimal _montoAbonar;
        private string _detalleAbono;


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
        //
        public bool isPagarOk { get { return _isPagarOk; } }
        public decimal montoAbonar { get { return _montoAbonar; } }
        public string DetalleAbono { get { return _detalleAbono; } }
        //
        public bool EstatusIsCancelado { get { return _montoAbonar >= montoResta; } }


        public data()
        {
            _montoAbonar = 0m;
            _isPagarOk = false;
            _detalleAbono = "";
            //
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


        public void setActivarAbono(decimal monto, string detalle)
        {
            _montoAbonar = monto;
            _detalleAbono = detalle;
            _isPagarOk = true;
        }
        public void setEliminarAbono()
        {
            _montoAbonar = 0m;
            _detalleAbono = "";
            _isPagarOk = false;
        }

    }

}