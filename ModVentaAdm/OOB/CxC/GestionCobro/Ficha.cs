using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.CxC.GestionCobro
{
    public class Ficha
    {
        public string SucPrefijo { get; set; }
        public FichaCobro Cobro { get; set; }
        public FichaRecibo Recibo { get; set; }
        public List<FichaDocumento> Documentos { get; set; }
        public List<FichaMetodoPago> MetodosPago { get; set; }
        public FichaNotaAdm notaAdm { get; set; }
        public Retencion retencion { get; set; }
        public List<Caja> cajas { get; set; }
        public string autoCliente { get; set; }
        public decimal montoAnticipoDescargar { get; set; }
        public decimal factorCambio { get; set; }
        public decimal montoRecibido { get; set; }
        public DateTime fechaProceso { get; set; }
        public decimal montoAnticipoCargar { get; set; }
        public decimal montoNtCredito { get; set; }
        public Ficha() 
        {
            SucPrefijo = "";
            Cobro = new FichaCobro();
            Recibo = new FichaRecibo();
            Documentos = new List<FichaDocumento>();
            MetodosPago = new List<FichaMetodoPago>();
            notaAdm = null;
            retencion = null;
            cajas = null;
            autoCliente = "";
            montoAnticipoDescargar = 0m;
            factorCambio = 0m;
            montoRecibido = 0m;
            fechaProceso = DateTime.Now.Date;
            montoAnticipoCargar = 0m;
            montoNtCredito = 0m;
        }
    }
}