using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Cliente.Articulos
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