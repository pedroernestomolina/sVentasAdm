using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Cliente.Buscar.Busqueda
{
    
    public class Gestion
    {

        private Enumerados.enumMetodoBusqueda _metodoBusqPred;
        private dataFiltro _filtrar;


        public Enumerados.enumMetodoBusqueda MetodoBusqueda { get { return _metodoBusqPred; } }


        public Gestion() 
        {
            _metodoBusqPred = Enumerados.enumMetodoBusqueda.SinDefinir;
            _filtrar = new dataFiltro();
        }


        public void Inicializa()
        {
            _filtrar.Limpiar();
        }

        public void Inicia()
        {
            if (CargarData())
            {
            }
        }

        public bool CargarData()
        {
            var rt = true;

            var r01 = Sistema.MyData.Configuracion_BusquedaCliente();
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return false;
            }
            asignaMetodoBusqueda(r01.Entidad);

            return rt;
        }

        private void asignaMetodoBusqueda(OOB.Configuracion.BusquedaCliente.Entidad.Ficha ficha)
        {
            switch (ficha.ModoBusqueda)
            {
                case OOB.Configuracion.BusquedaCliente.Entidad.Enumerado.EnumPreferenciaBusqueda.Codigo:
                    _metodoBusqPred =  Enumerados.enumMetodoBusqueda.PorCodigo;
                    _filtrar.setMetodoPorCodigo();
                    break;
                case OOB.Configuracion.BusquedaCliente.Entidad.Enumerado.EnumPreferenciaBusqueda.Nombre:
                    _metodoBusqPred =  Enumerados.enumMetodoBusqueda.PorNombre;
                    _filtrar.setMetodoPorNombre();
                    break;
                case OOB.Configuracion.BusquedaCliente.Entidad.Enumerado.EnumPreferenciaBusqueda.CiRif:
                    _metodoBusqPred =  Enumerados.enumMetodoBusqueda.PorRif;
                    _filtrar.setMetodoPorCiRif();
                    break;
            }
        }

        public void setCadena(string p)
        {
            _filtrar.setCadena(p);
        }

        public OOB.Maestro.Cliente.Lista.Filtro GenerarFiltro()
        {
            if (_filtrar.cadena.Trim() == "") { return null; }
            return new OOB.Maestro.Cliente.Lista.Filtro()
            {
                cadena = _filtrar.cadena,
                metodoBusqueda = (OOB.Maestro.Cliente.Lista.Enumerados.enumMetodoBusqueda)_filtrar.MetodoBusqueda,
            };
        }
        public void setMetodoPorCodigo()
        {
            _filtrar.setMetodoPorCodigo();
        }

        public void setMetodoPorNombre()
        {
            _filtrar.setMetodoPorNombre();
        }

        public void setMetodoPorCiRif()
        {
            _filtrar.setMetodoPorCiRif();
        }

        public void LimpiarBusqueda()
        {
            _filtrar.Limpiar();
            switch (_metodoBusqPred)
            {
                case Enumerados.enumMetodoBusqueda.PorCodigo :
                    _filtrar.setMetodoPorCodigo();
                    break;
                case Enumerados.enumMetodoBusqueda.PorNombre :
                    _filtrar.setMetodoPorNombre();
                    break;
                case Enumerados.enumMetodoBusqueda.PorRif :
                    _filtrar.setMetodoPorCiRif();
                    break;
            }
        }

    }

}