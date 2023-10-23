using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.FiltrosCB.ConBusqueda.Caja
{
    public class Imp: LibUtilitis.CtrlCB.ImpCB, ICtrlConBusqueda
    {
        public Imp()
            :base()
        {
        }
        public void ObtenerData()
        {
            try
            {
                var _lst = new List<Idata>();
                var r01 = Sistema.MyData.Transporte_Caja_GetLista();
                foreach (var rg in r01.ListaD.OrderBy(o => o.descripcion).ToList()) 
                {
                    _lst.Add(new data(rg));
                }
                this.CargarData(_lst);
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
            }
        }
        public void setTextoBuscar(string desc)
        {
            try
            {
                var _lst = new List<Idata>();
                var r01 = Sistema.MyData.Transporte_Caja_GetLista();
                foreach (var rg in r01.ListaD.Where(w => w.descripcion.Trim().ToUpper().Contains(desc.Trim().ToUpper())).OrderBy(o => o.descripcion).ToList())
                {
                    _lst.Add(new data(rg));
                }
                this.CargarData(_lst);
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
            }
        }
        public string Get_TextoBuscar { get { return ""; } }
    }
}