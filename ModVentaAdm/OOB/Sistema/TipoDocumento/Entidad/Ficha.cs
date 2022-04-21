using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Sistema.TipoDocumento.Entidad
{
    
    public class Ficha
    {

        public string id { get; set; }
        public string descripcion { get; set; }
        public string codigo { get; set; }
        public string tipo { get; set; }
        public int signo { get; set; }
        public string siglas { get; set; }


        public Ficha()
        {
            Limpiar();
        }

        public Ficha(string id, string desc,string cod)
            : this()
        {
            this.id = id;
            this.descripcion = desc;
            this.codigo = cod;
        }

        public void Limpiar()
        {

            id = "";
            descripcion = "";
            codigo = "";
            tipo = "";
            siglas = "";
            signo = 1;
        }

        public void setFicha(Ficha ficha)
        {
            this.id = ficha.id ;
            this.descripcion = ficha.descripcion;
            this.codigo = ficha.codigo;
            this.siglas = ficha.siglas;
            this.signo = ficha.signo;
            this.tipo = ficha.tipo;
        }

    }

}