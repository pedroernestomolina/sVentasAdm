using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Reportes.GeneralDocumentoDetalle
{
    
    public class Ficha
    {

        public string auto { get; set; }
        public string documento { get; set; }
        public DateTime fecha { get; set; }
        public string usuarioNombre { get; set; }
        public string usuarioCodigo { get; set; }
        public decimal total { get; set; }
        public int renglones { get; set; }
        public string nombreProducto { get; set; }
        public decimal cantidadUnd { get; set; }
        public decimal precioUnd { get; set; }
        public decimal totalRenglon { get; set; }
        public int signo { get; set; }
        public string documentoNombre { get; set; }
        public string hora { get; set; }
        public string sucCodigo { get; set; }
        public string sucNombre { get; set; }
        public string estacion { get; set; }
        public string ciRif { get; set; }
        public string razonSocial { get; set; }


        public Ficha()
        {
            auto = "";
            documento = "";
            fecha = new DateTime().Date;
            usuarioCodigo = "";
            usuarioNombre = "";
            total = 0.0m;
            renglones = 0;
            nombreProducto = "";
            cantidadUnd = 0.0m;
            precioUnd = 0.0m;
            totalRenglon = 0;
            signo = 1;
            documentoNombre = "";
            hora = "";
            sucCodigo = "";
            sucNombre = "";
            estacion = "";
            ciRif = "";
            razonSocial = "";
        }

    }

}