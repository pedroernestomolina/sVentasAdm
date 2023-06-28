using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils
{
    public class dataFiltro : LibUtilitis.Opcion.IData
    {
        public string id {get;set;}
        public string codigo {get;set;}
        public string desc {get;set;}


        public override string ToString()
        {
            return id + ", " + codigo + ", " + desc;
        }
    }
}