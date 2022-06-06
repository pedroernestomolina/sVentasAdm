using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Cliente.Documento
{
    
    public class Ficha
    {

        public string id { get; set; }
        public DateTime fecha { get; set; }
        public string documento { get; set; }
        public decimal monto { get; set; }
        public decimal montoDivisa { get; set; }
        public decimal tasaDivisa { get; set; }
        public string estatus { get; set; }
        public string codTipoDoc { get; set; }
        public string nombreTipoDoc { get; set; }
        public string serie { get; set; }


        public Ficha()
        {
            id = "";
            fecha = new DateTime().Date;
            documento = "";
            monto = 0.0m;
            montoDivisa = 0.0m;
            tasaDivisa = 0.0m;
            estatus = "";
            codTipoDoc = "";
            nombreTipoDoc = "";
            serie = "";
        }

    }

}