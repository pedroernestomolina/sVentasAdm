using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.Reporte
{
    public class AliadoDetalleDoc
    {
        public string codigoAliado { get; set; }
        public string rifAliado { get; set; }
        public string nombreAliado { get; set; }
        public string rifCliente { get; set; }
        public string nombreCliente { get; set; }
        public string numDoc { get; set; }
        public DateTime fechaDoc { get; set; }
        public string nombreDoc { get; set; }
        public decimal importe { get; set; }
        public decimal acumulado { get; set; }
        public AliadoDetalleDoc()
        {
            codigoAliado = "";
            rifAliado = "";
            nombreAliado = "";
            rifCliente = "";
            nombreCliente = "";
            numDoc = "";
            fechaDoc = DateTime.Now.Date;
            nombreDoc = "";
            importe = 0m;
            acumulado = 0m;
        }
    }
}