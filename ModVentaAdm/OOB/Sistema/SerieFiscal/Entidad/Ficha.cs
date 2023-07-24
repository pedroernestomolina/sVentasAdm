using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Sistema.SerieFiscal.Entidad
{
    public class Ficha
    {
        public string id { get; set; }
        public string serie { get; set; }
        public string control { get; set; }
        public Ficha()
        {
            id= "";
            serie = "";
            control = "";
        }
    }
}