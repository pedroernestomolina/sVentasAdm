using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.ReportesCliente 
{
    
    public class Gestion
    {

        private IGestion _gestion;
        private Filtro.Gestion _gestionFiltro;


        public Gestion()
        {
            _gestionFiltro = new Filtro.Gestion();
        }


        public void setGestion(IGestion gestion)
        {
            _gestion = gestion;
            _gestionFiltro.setFiltros(_gestion.Filtros);
        }

        public void Inicializa()
        {
            _gestionFiltro.Inicializa();
        }

        public void Inicia()
        {
            if (CargarData()) 
            {
                _gestionFiltro.Inicia();
                if (_gestionFiltro.ProcesarIsOk)
                {
                    _gestion.Generar(_gestionFiltro.Data);
                }
            }
        }

        private bool CargarData()
        {
            return _gestionFiltro.CargarData();
        }

    }

}