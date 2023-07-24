using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Sistema.Empresa.Entidad
{
    public class Ficha
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string CiRif  { get; set; }
        public string Telefono { get; set; }
        public byte[] logo { get; set; }
        public Ficha()
        {
            Nombre = "";
            Direccion = "";
            CiRif = "";
            Telefono = "";
            logo = new byte[] { };
        }
    }
}