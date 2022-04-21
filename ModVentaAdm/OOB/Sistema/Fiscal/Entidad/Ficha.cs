using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Sistema.Fiscal.Entidad
{
    
    public class Ficha
    {

        public string id { get; set; }
        public string descripcion { get; set; }
        public decimal tasa { get; set; }
        public string codigo 
        { 
            get 
            { 
                var rt="";
                switch (id)
                { 
                    case "0000000001":
                        rt = "01";
                        break;
                    case "0000000002":
                        rt = "02";
                        break;
                    case "0000000003":
                        rt = "03";
                        break;
                    case "0000000004":
                        rt = "04";
                        break;
                }
                return rt;
            } 
        }


        public Ficha()
        {
            id = "";
            descripcion = "";
            tasa = 0.0m;
        }

    }

}