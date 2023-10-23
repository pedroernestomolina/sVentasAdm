using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.FiltrosCB.SinBusqueda.EstatusDoc
{
    public class Imp: LibUtilitis.CtrlCB.ImpCB, ICtrlSinBusqueda
    {
        public Imp()
            :base()
        {
        }
        public void ObtenerData()
        {
            var _lst = new List<Idata>();
            _lst.Add(new data() { id = "1", codigo = "", desc = "ACTIVO" });
            _lst.Add(new data() { id = "2", codigo = "", desc = "ANULADO" });
            this.CargarData(_lst);
        }
    }
}