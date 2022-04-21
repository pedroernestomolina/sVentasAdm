using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Cliente.Lista
{
    
    public class data
    {

        public string auto { get; set; }
        public string ciRif { get ;set ; }
        public string razonSocial { get; set; }
        public bool isActivo { get ; set;}


        public data()
        {
            auto = "";
            ciRif = "";
            razonSocial = "";
            isActivo = true;
        }

        public data(string id, string rif, string nombre, string est)
            :this()
        {
            this.auto = id;
            this.ciRif = rif;
            this.razonSocial = nombre;
            this.isActivo = est.Trim().ToUpper() == "ACTIVO" ? true : false;
        }

    }

}