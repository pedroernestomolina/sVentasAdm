using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Producto.Lista
{
    
    public class data
    {

        public string auto { get; set; }
        public string descripcion { get; set; }
        public bool isActivo { get ; set;}


        public data()
        {
            auto = "";
            descripcion = "";
            isActivo = true;
        }

        public data(string id, string nombre, string est)
            :this()
        {
            this.auto = id;
            this.descripcion = nombre;
            this.isActivo = est.Trim().ToUpper() == "ACTIVO" ? true : false;
        }

    }

}