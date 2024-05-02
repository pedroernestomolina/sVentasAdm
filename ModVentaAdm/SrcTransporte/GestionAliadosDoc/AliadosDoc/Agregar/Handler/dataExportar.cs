using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.GestionAliadosDoc.AliadosDoc.Agregar.Handler
{
    public class dataExportar
    {
        public decimal montoDiv { get; set; }
        public string servDesc { get; set; }
        public LibUtilitis.Opcion.IData aliado { get; set; }
        public LibUtilitis.Opcion.IData servicio { get; set; }
    }
}