using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Documento.Anular
{
    public class baseAliado
    {
        public int idAliado { get; set; }
        public decimal montoDivisa { get; set; }
        public baseAliado()
        {
            idAliado = -1;
            montoDivisa=0m;
        }
    }
}
