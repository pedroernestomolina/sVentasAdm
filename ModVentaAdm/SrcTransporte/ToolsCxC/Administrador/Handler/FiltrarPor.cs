using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.ToolsCxC.Administrador.Handler
{
    public class FiltrarPor: SrcTransporte.Filtro.Vistas.IActivarFiltroPor 
    {
        private bool _porAliado;
        private bool _porCliente;
        private SrcTransporte.Filtro.Vistas.EntreFechas _porEntreFechas;
        private bool _porEstatusDoc;


        public bool PorAliado { get { return _porAliado; } }
        public bool PorCliente { get { return _porCliente; } }
        public bool PorEstatusDoc { get { return _porEstatusDoc; } }
        public SrcTransporte.Filtro.Vistas.EntreFechas PorEntreFechas { get { return _porEntreFechas; } }
        public FiltrarPor()
        {
            _porAliado = false;
            _porCliente = true;
            _porEntreFechas = new SrcTransporte.Filtro.Vistas.EntreFechas() { Activar = true, MostrarCheck = true };
            _porEstatusDoc = true;
        }
    }
}