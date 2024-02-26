using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcComun.Clientes.Filtros.Opcion.Vista
{
    public interface IPorCombo
    {
        Utils.Componente.Filtro.Vista.IOpcionCB Opcion { get; }
        void Inicializa();
    }
}