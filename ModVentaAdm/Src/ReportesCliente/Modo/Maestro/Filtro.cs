using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.ReportesCliente.Modo.Maestro
{

    public class Filtro: ReportesCliente.Filtro.IFiltro 
    {

        public bool ActivarGrupo
        {
            get { return true; }
        }

        public bool ActivarEstado
        {
            get { return true; }
        }

        public bool ActivarZona
        {
            get { return true; }
        }

        public bool ActivarTarifa
        {
            get { return true; }
        }

        public bool ActivarVendedor
        {
            get { return true; }
        }

        public bool ActivarCobrador
        {
            get { return true; }
        }

        public bool ActivarCategoria
        {
            get { return true; }
        }

        public bool ActivarCredito
        {
            get { return true; }
        }

        public bool ActivarNivel
        {
            get { return true; }
        }

        public bool ActivarEstatus
        {
            get { return true; }
        }

    }

}