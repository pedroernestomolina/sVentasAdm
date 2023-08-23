using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Documento.Agregar.Factura
{
    public class FichaAliadoDocRef
    {
        public int idAliado { get; set; }
        public string idDocRef { get; set; }
        public FichaAliadoDocRef()
        {
            idAliado = -1;
            idDocRef = "";
        }
    }
}