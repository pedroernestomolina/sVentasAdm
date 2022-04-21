using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Cliente.Administrador
{
    
    public class dataFiltro
    {

        private Enumerados.enumMetodoBusqueda _metodoBusqueda;
        private string _cadena;


        public string cadena { get { return _cadena; } }
        public Enumerados.enumMetodoBusqueda MetodoBusqueda { get { return _metodoBusqueda; } }



        public dataFiltro()
        {
            Limpiar();
        }


        public void Limpiar()
        {
            _metodoBusqueda = Enumerados.enumMetodoBusqueda.SinDefinir;
            _cadena = "";
        }

        public void setMetodoPorCodigo()
        {
            _metodoBusqueda = Enumerados.enumMetodoBusqueda.PorCodigo;
        }

        public void setMetodoPorNombre()
        {
            _metodoBusqueda = Enumerados.enumMetodoBusqueda.PorNombre;
        }

        public void setMetodoPorCiRif()
        {
            _metodoBusqueda = Enumerados.enumMetodoBusqueda.PorRif;
        }

        public void setCadena(string p)
        {
            _cadena = p;
        }

    }

}