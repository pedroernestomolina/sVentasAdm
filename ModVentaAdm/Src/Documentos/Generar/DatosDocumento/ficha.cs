using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Documentos.Generar.DatosDocumento
{
    
    public class ficha
    {

        public string id { get; set; }
        public string desc { get; set; }
        public string cod { get; set; }


        public ficha() 
        {
            Limpiar();
        }

        public ficha(string _id,  string _desc, string _cod="")
        {
            this.id = _id;
            this.desc = _desc;
            this.cod = _cod;
        }

        public void Limpiar()
        {
            id = "";
            desc = "";
            cod = "";
        }

    }

}