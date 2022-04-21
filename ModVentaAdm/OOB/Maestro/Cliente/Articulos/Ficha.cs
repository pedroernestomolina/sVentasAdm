using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Maestro.Cliente.Articulos
{
    
    public class Ficha
    {

        public string codigoPrd { get; set; }
        public string nombrePrd { get; set; }
        public DateTime fecha { get; set; }
        public string documento { get; set; }
        public decimal cantidad { get; set; }
        public decimal cantUnd { get; set; }
        public string empaque { get; set; }
        public string estatus { get; set; }
        public int contenidoEmp { get; set; }
        public string codTipoDoc { get; set; }
        public string nombreTipoDoc { get; set; }
        public string serie { get; set; }
        public decimal tasaCambio { get; set; }
        public decimal precioUnd { get; set; }
        public int signo { get; set; }
        public string EmpaqueCont { get { return empaque.Trim() + "( " + contenidoEmp.ToString() + " )"; } }
        public decimal PrecioDivisa { get { return Math.Round( precioUnd / tasaCambio, 2, MidpointRounding.AwayFromZero); } }
        public bool IsAnulado { get { return estatus.Trim().ToUpper() == "0" ? false : true; } }


        public Ficha()
        {
            codigoPrd = "";
            nombrePrd = "";
            fecha = new DateTime().Date;
            documento = "";
            cantidad = 0.0m;
            cantUnd = 0.0m;
            empaque = "";
            estatus = "";
            contenidoEmp = 0;
            codTipoDoc = "";
            nombreTipoDoc = "";
            serie = "";
            tasaCambio = 0.0m;
            precioUnd = 0.0m;
            signo = 1;
        }

    }

}