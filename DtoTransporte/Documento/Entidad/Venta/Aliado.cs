using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Documento.Entidad.Venta
{
    public class Aliado
    {
        public  int idItem { get; set; }
        public int idAliado { get; set; }
        public string ciRifAliado { get; set; }
        public string codigoAliado { get; set; }
        public string nombreAliado { get; set; }
        public decimal pNeto { get; set; }
        public decimal cantDias { get; set; }
        public decimal importe { get; set; }
    }
}