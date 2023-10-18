using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Documento.Agregar.Factura
{
    public class FichaAliadoResumen
    {
        public int idAliado { get; set; }
        public decimal montoDivisa { get; set; }
        public List<Servicio> servicios { get; set; }
        public FichaAliadoResumen()
        {
            idAliado = -1;
            montoDivisa = 0m;
        }
    }
}