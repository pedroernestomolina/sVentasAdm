using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Documento.GetServicios.Presupuesto
{
    public class Ficha
    {
        public int idAliado { get; set; }
        public int idServ { get; set; }
        public string codServ { get; set; }
        public string descServ { get; set; }
        public string detServ { get; set; }
        public decimal importeServ { get; set; }
    }
}