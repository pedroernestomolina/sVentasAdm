using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.HelperCxc
{
    public class Util
    {
        static public void EstadoCuenta(ModVentaAdm.Utils.DialogoFecha.IDialogoFecha _dialogoFecha,
            SrcTransporte.Reportes.Filtro.Vista.IFiltro _filtro) 
        {
            _dialogoFecha.Inicializa();
            _dialogoFecha.Inicia();
            if (_dialogoFecha.IsOk)
            {
                SrcTransporte.Reportes.IReporteConFiltroMasFecha _rep = new SrcTransporte.Reportes.Cxc.EdoCta.Imp();
                _rep.setDesde(_dialogoFecha.GetFecha);
                _rep.setFiltros(_filtro);
                _rep.Generar();
            }
        }
    }
}