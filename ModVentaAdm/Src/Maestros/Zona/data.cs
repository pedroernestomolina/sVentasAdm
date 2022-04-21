using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Maestros.Zona
{
    
    public class data
    {

        private string _id;
        private string _codigo;
        private string _descripcion;


        public string Id { get { return _id; } }
        public string Codigo { get { return _codigo; } }
        public string Descripcion { get { return _descripcion; } }


        public data()
        {
            limpiar();
        }


        private void limpiar()
        {
            _id = "";
            _codigo = "";
            _descripcion = "";
        }

        public void setId(string p)
        {
            _id = p;
        }

        public void setNombre(string p)
        {
            _descripcion = p;
        }

        public void setCodigo(string p)
        {
            _codigo = p;
        }

        public void Limpiar()
        {
            limpiar();
        }

        public bool VerificarIsOk()
        {
            var rt = true;

            if (Codigo.Trim() == "")
            {
                Helpers.Msg.Error("Campo [ Codigo Zona ] No Puede Estar Vacio");
                return false;
            }
            if (Descripcion.Trim() == "")
            {
                Helpers.Msg.Error("Campo [ Descripción Zona ] No Puede Estar Vacio");
                return false;
            }

            return rt;
        }

    }

}