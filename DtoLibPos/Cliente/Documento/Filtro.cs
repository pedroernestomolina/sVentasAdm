using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Cliente.Documento
{
    
    public class Filtro
    {

        public string autoCliente { get; set; }
        public DateTime? desde { get; set; }
        public DateTime? hasta { get; set; }
        public string tipoDoc { get; set; }


        public Filtro()
        {
            autoCliente = "";
            desde = DateTime.Now.Date;
            hasta = DateTime.Now.Date;
            tipoDoc = "";
        }

    }

}