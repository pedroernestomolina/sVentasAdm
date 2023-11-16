using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Documento.Remision.Lista
{
    public class Filtro
    {
        public string idCliente { get; set; }
        public string codTipoDoc { get; set; }
        public bool esPorRemision { get; set; }
        public Filtro()
        {
            idCliente = "";
            codTipoDoc = "";
            esPorRemision = true;
        }
    }
}