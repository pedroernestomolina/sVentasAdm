using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcComun.Clientes.Filtros.Opcion.Handler
{
    abstract public class basePorCombo: Vista.IPorCombo
    {
        private Utils.Componente.Filtro.Vista.IOpcionCB _opcion;
        //
        public Utils.Componente.Filtro.Vista.IOpcionCB Opcion { get { return _opcion; } }
        //
        public basePorCombo()
        {
            _opcion = new Utils.Componente.Filtro.Handler.baseOpcionCB();
        }
        public void Inicializa()
        {
            _opcion.Inicializa();
        }
    }
}