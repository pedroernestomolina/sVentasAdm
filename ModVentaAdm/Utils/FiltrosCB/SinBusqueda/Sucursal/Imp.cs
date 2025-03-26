using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.FiltrosCB.SinBusqueda.Sucursal
{
    public class Imp : LibUtilitis.CtrlCB.ImpCB, ICtrlSinBusqueda
    {
        public Imp()
            : base()
        {
        }
        public void ObtenerData()
        {
            var _lst = new List<Idata>();
            var rt1 = Sistema.MyData.Sucursal_GetLista();
            if (rt1.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                throw new Exception(rt1.Mensaje);
            }
            foreach (var rg in rt1.ListaD.Where(w=>w.isActivo).OrderBy(o => o.nombre).ToList())
            {
                _lst.Add(new data(rg));
            }
            this.CargarData(_lst);
        }
    }
}
