using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.FiltrosCB.SinBusqueda.MetodosPago
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
            var r01 = Sistema.MyData.Sistema_MedioCobro_GetLista(); 
            foreach (var rg in r01.ListaD.OrderBy(o => o.nombre).ToList())
            {
                _lst.Add(new data(rg));
            }
            this.CargarData(_lst);
        }
    }
}
