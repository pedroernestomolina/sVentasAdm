using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.Componente.Busqueda.Vistas
{
    public interface IBusqueda
    {
        void Inicializa();
        IEnumerable<object> Buscar();
        void setFiltros(object filtros);
    }
}