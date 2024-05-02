using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.Documento.GestionAliados.ObtenerLista
{
    public class Documento
    {
        public string idDoc { get; set; }
        public string numeroDoc { get; set; }
        public DateTime fechaDoc { get; set; }
        public string entidadDoc{ get; set; }
        public string ciRifEntidadDoc { get; set; }
        public string idEntidadDoc { get; set; }
        public decimal montoDivDoc { get; set; }
        public string codigoTipoDoc { get; set; }
        public string nombreDoc { get; set; }
        public string estatusDoc { get; set; }
        public string serieDoc { get; set; }
    }
}