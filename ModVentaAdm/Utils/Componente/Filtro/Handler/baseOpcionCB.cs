using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.Componente.Filtro.Handler
{
    public class baseOpcionCB: LibUtilitis.CtrlCB.ImpCB,  Vista.IOpcionCB
    {
        private bool _habilitarOpcion;
        //
        public bool GetHabilitarOpcion { get { return _habilitarOpcion; } }
        //
        public baseOpcionCB()
            :base()
        {
            _habilitarOpcion = true;
        }
        public void setHabilitarOpcion(bool modo)
        {
            _habilitarOpcion = modo;
        }
        public void ObtenerData()
        {
        }
    }
}