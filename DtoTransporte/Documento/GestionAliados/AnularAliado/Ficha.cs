using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Documento.GestionAliados.AnularAliado
{
    public class Ficha
    {
        public int idAliado { get; set; }
        public decimal montoAnular { get; set; }
        public int idRefAliadoDoc { get; set; }
        public string idUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public string motivo { get; set; }
    }
}